using System;
using System.Drawing;
using System.Windows.Forms;

using System.Numerics;
using PlanetViewer;
using SixLabors.ImageSharp.Processing;

namespace MapCreator
{
    public partial class PlanetViewer : Form
    {
        private readonly IGraphicsDevice? _graphicsDevice;
        private readonly IFilmRecorder? _filmRecorder;

        private bool _isSpinning = true;
        private float _speed = 1.0f;
        private float _zoom = 1.0f;
        private float _oldSpeed;

        private bool _shownFirstTime = true;

        private bool _isMouseDown = false;
        private Vector2 _mousePosition;

        private Camera? _camera;

        private string _lastTextureFileName;
        private FlipMode _textureFlipMode;
        private bool _flipUvs;

        public PlanetViewer()
        {
            _lastTextureFileName = string.Empty;
            _textureFlipMode = FlipMode.None;
            _flipUvs = false;
            _camera = null;

            //_axisControlsForm = null;

            InitializeComponent();
        }

        public PlanetViewer(IGraphicsDevice graphicsDevice, IFilmRecorder filmRecorder)
        {
            _graphicsDevice = graphicsDevice;
            _filmRecorder = filmRecorder;

            _lastTextureFileName = string.Empty;
            _textureFlipMode = FlipMode.None;
            _flipUvs = false;
            _camera = null;

            //_axisControlsForm = axisControlsForm;

            InitializeComponent();

            Shown += PlanetViewer_Shown;
            MainTimer.Tick += MainTimer_Tick;
        }

        #region Main Menu Selections

        // Open Map File
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                MainTimer.Enabled = false;
                _lastTextureFileName = openFileDialog.FileName;
                _graphicsDevice?.SetTextureFromFile(_lastTextureFileName, _textureFlipMode);
                MainTimer.Enabled = true;
            }
        }

        // Solid Frame

        private void FlipTextureHorizontally()
        {
            _textureFlipMode = FlipMode.Horizontal;

            MainTimer.Enabled = false;
            _graphicsDevice?.SetTextureFromFile(_lastTextureFileName, _textureFlipMode);
            MainTimer.Enabled = true;
        }

        private void FlipTextureVertically()
        {
            _textureFlipMode = FlipMode.Vertical;

            MainTimer.Enabled = false;
            _graphicsDevice?.SetTextureFromFile(_lastTextureFileName, _textureFlipMode);
            MainTimer.Enabled = true;
        }

        private void setSolidFrameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _graphicsDevice?.SetSolid(true);
        }

        private void flipTextureHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FlipTextureHorizontally();
        }

        private void flipTextureVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FlipTextureVertically();
        }

        // Wire Frame
        private void FlipMeshInsideOut()
        {
            _flipUvs = true;
        }

        private void FlipMeshOutsideIn()
        {
            _flipUvs = false;
        }

        private void setWireFrameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _graphicsDevice?.SetSolid(false);
        }

        private void flipMeshInsideOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FlipMeshInsideOut();
        }

        private void flipMeshOutsideInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FlipMeshOutsideIn();
        }

        // Background: Image
        private void setBackgroundImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                _graphicsDevice?.SetBackgroundTextureFromFile(openFileDialog.FileName, FlipMode.None);
            }
        }

        // Background: Color
        private void chooseASolidColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                var color = colorDialog.Color;
                _graphicsDevice?.SetBackgroundColor(new Vector4(color.R / 255.0f, color.G / 255.0f, color.B / 255.0f, 1.0f));
            }
        }

        // Background: Gradient
        private void selectAColorGradientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var colorDialog = new ColorDialog();

            var topColor = Color.Purple;
            var bottomColor = Color.Orange;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                topColor = colorDialog.Color;

            }

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                bottomColor = colorDialog.Color;
            }

            var topColorVector = new Vector4(topColor.R / 255.0f, topColor.G / 255.0f, topColor.B / 255.0f, 1.0f);
            var bottomColorVector = new Vector4(bottomColor.R / 255.0f, bottomColor.G / 255.0f, bottomColor.B / 255.0f, 1.0f);
            _graphicsDevice?.SetBackgroundGradientColor(
                topColorVector,
                bottomColorVector);
        }

        // Start And Stop Planet Spin
        private void startStopRotationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _isSpinning = !_isSpinning;
            if (!_isSpinning)
            {
                _oldSpeed = _speed;
                _speed = 0.0f;
            }
            else
            {
                _speed = _oldSpeed;
            }
        }

        // Rotation Speed: Faster
        private void speedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _speed += 0.1f;
        }

        // Rotation Speed: Slower
        private void speedToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _speed -= 0.1f;
        }

        // Frame-by-Frame Rotation
        private void singleFrameRotationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // To Be Implemented At A Later Date
        }

        // Zoom Into Planet
        private void zoomTowardPlanetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _zoom += 1.0f;
            _camera!.Position += _camera.Front * 0.1f;
            _graphicsDevice?.Zoom(_zoom);
        }

        // Zoom Away From Planet
        private void zoomOutOfPlanetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _zoom -= 1.0f;
            _camera!.Position -= _camera.Front * 0.1f;
            _graphicsDevice?.Zoom(_zoom);
        }

        // Revert To Initial Settings
        private void clearImageAndRevertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _isSpinning = true;
            _zoom = 1.0f;
            _speed = 1.0f;

            _graphicsDevice?.Zoom(_zoom);
        }

        #endregion

        private void MainTimer_Tick(object sender, EventArgs e)
        {
            // _graphicsDevice?.SetGlobeRotation(_axisControlsForm!.XAxisValue,_axisControlsForm!.ZAxisValue);

            _graphicsDevice?.SetGlobeRotation(XAxisValue, ZAxisValue);
            _graphicsDevice?.Draw(_speed, _flipUvs);
        }

        #region Facet Key Controller

        private void PlanetViewer_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            /// https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.keys?view=windowsdesktop-6.0

            // Spin Slower
            if (e.KeyCode == Keys.Add)
            {
                _speed -= 0.1f;
            }

            // Spin Faster
            if (e.KeyCode == Keys.Subtract)
            {
                _speed += 0.1f;
            }

            // Zoom Into Planet
            if (e.KeyCode == Keys.Up)
            {
                _camera!.Position += _camera.Front * 0.1f;
            }

            // Zoom Away From Planet
            if (e.KeyCode == Keys.Down)
            {
                _camera!.Position -= _camera.Front * 0.1f;
            }

            // Pan Left
            if (e.KeyCode == Keys.Left)
            {
                _camera!.Position -= _camera.Right * 0.1f;
            }

            // Pan Right
            if (e.KeyCode == Keys.Right)
            {
                _camera!.Position += _camera.Right * 0.1f;
            }
        }

        #endregion

        private void PlanetViewer_Resize(object sender, EventArgs e)
        {
            if (DesignMode)
            {
                return;
            }

            if (!_shownFirstTime)
            {
                _graphicsDevice?.Resize(
                    MainPanel.ClientSize.Width,
                    MainPanel.ClientSize.Height);
            }
        }

        #region Animation Recordings

        // Recording: Reset Frames
        private void tToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _filmRecorder?.Reset();
        }

        // Recording: Add Frames
        private void tToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var tempFilePath = _graphicsDevice?.CaptureWindow();

            var frameImage = SixLabors.ImageSharp.Image.Load(tempFilePath);
            _filmRecorder?.AddFrame(frameImage);
        }

        // Recording: Del Frames
        private void tToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            _filmRecorder?.RemoveFrame();
        }

        // Recording: Save Media
        private void tToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            using var saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                _filmRecorder?.SaveAs(saveFileDialog.FileName);
            }
        }

        #endregion

        private void MainPanel_MouseDown(object sender, MouseEventArgs e)
        {
            _isMouseDown = true;
            _mousePosition = new Vector2(e.X, e.Y);
        }

        private void MainPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isMouseDown)
            {
                var mouseDelta = new Vector2(e.X - _mousePosition.X, e.Y - _mousePosition.Y);
                _camera!.Yaw += mouseDelta.X * 0.1f;
                _camera.Pitch += -mouseDelta.Y * 0.1f;
                _mousePosition = new Vector2(e.X, e.Y);
            }
        }

        private void MainPanel_MouseUp(object sender, MouseEventArgs e)
        {
            _mousePosition = new Vector2(e.X, e.Y);
            _isMouseDown = false;
        }

        private void PlanetViewer_Shown(object sender, EventArgs e)
        {
            if (DesignMode)
            {
                return;
            }

            if (_shownFirstTime)
            {
                var x = MainPanel.Handle;

                _camera = new Camera(new Vector3(0, 0, 5), MainPanel.ClientSize.Width / (float)MainPanel.ClientSize.Height);
                _graphicsDevice?.Initialize(
                    MainPanel.Handle,
                    MainPanel.ClientSize.Width,
                    MainPanel.ClientSize.Height,
                    _camera);

                //_axisControlsForm?.Show();
                MainTimer.Enabled = true;
                Focus();

                _shownFirstTime = false;
            }
        }

        #region Facet Axis Rotation

        public int XAxisValue
        {
            get => XAxisRotation.Value;
            set => XAxisRotation.Value = value;
        }

        public int ZAxisValue
        {
            get => ZAxisRotation.Value;
            set => ZAxisRotation.Value = value;
        }

        private void XAxisRotation_ValueChanged(object sender, EventArgs e)
        {
            XAxisAngleTextBox.TextChanged -= XAxisAngleTextBox_TextChanged;
            XAxisAngleTextBox.Text = XAxisRotation.Value.ToString();
            XAxisAngleTextBox.TextChanged += XAxisAngleTextBox_TextChanged;
        }

        private void XAxisAngleTextBox_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(XAxisAngleTextBox.Text, out var axisAngle))
            {
                XAxisRotation.Value = axisAngle;
            }
        }

        private void ZAxisRotation_ValueChanged(object sender, EventArgs e)
        {
            ZAxisAngleTextBox.TextChanged -= ZAxisAngleTextBox_TextChanged;
            ZAxisAngleTextBox.Text = ZAxisRotation.Value.ToString();
            ZAxisAngleTextBox.TextChanged += ZAxisAngleTextBox_TextChanged;
        }

        private void ZAxisAngleTextBox_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(ZAxisAngleTextBox.Text, out var axisAngle))
            {
                ZAxisRotation.Value = axisAngle;
            }
        }

        private void mainMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainTimer.Enabled = false;
            Close();
        }

        private void ZAxisLabel_DoubleClick(object sender, EventArgs e)
        {
            ToggleControls();
        }

        private void XAxisLabel_DoubleClick(object sender, EventArgs e)
        {
            ToggleControls();
        }

        private void ToggleControls()
        {
            if (XAxisLabel.BackColor == Color.LimeGreen)
            {
                XAxisLabel.BackColor = Color.Red;
                ZAxisLabel.BackColor = Color.Red;

                XAxisAngleTextBox.Enabled = false;
                ZAxisAngleTextBox.Enabled = false;
                XAxisRotation.Enabled = false;
                ZAxisRotation.Enabled = false;
            }
            else
            {
                XAxisLabel.BackColor = Color.LimeGreen;
                ZAxisLabel.BackColor = Color.LimeGreen;

                XAxisAngleTextBox.Enabled = true;
                ZAxisAngleTextBox.Enabled = true;
                XAxisRotation.Enabled = true;
                ZAxisRotation.Enabled = true;

            }
        }

        #endregion
    }
}
