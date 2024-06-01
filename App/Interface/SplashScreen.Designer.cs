namespace MapCreator
{
    partial class SplashScreen
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Disables The Close [X] Button In The Titlebar
        /// </summary>
        private const int CP_DISABLE_CLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle = cp.ClassStyle | CP_DISABLE_CLOSE_BUTTON;
                return cp;
            }
        }

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashScreen));
            splashScreen_pictureBox_titleImage = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)splashScreen_pictureBox_titleImage).BeginInit();
            SuspendLayout();
            // 
            // splashScreen_pictureBox_titleImage
            // 
            splashScreen_pictureBox_titleImage.BackColor = SystemColors.ActiveCaptionText;
            splashScreen_pictureBox_titleImage.Dock = DockStyle.Fill;
            splashScreen_pictureBox_titleImage.Image = (Image)resources.GetObject("splashScreen_pictureBox_titleImage.Image");
            splashScreen_pictureBox_titleImage.Location = new Point(0, 0);
            splashScreen_pictureBox_titleImage.Name = "splashScreen_pictureBox_titleImage";
            splashScreen_pictureBox_titleImage.Size = new Size(546, 224);
            splashScreen_pictureBox_titleImage.SizeMode = PictureBoxSizeMode.StretchImage;
            splashScreen_pictureBox_titleImage.TabIndex = 0;
            splashScreen_pictureBox_titleImage.TabStop = false;
            // 
            // splashScreen
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(546, 224);
            ControlBox = false;
            Controls.Add(splashScreen_pictureBox_titleImage);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "splashScreen";
            ShowIcon = false;
            ShowInTaskbar = false;
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Map Creator";
            TopMost = true;
            ((System.ComponentModel.ISupportInitialize)splashScreen_pictureBox_titleImage).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox splashScreen_pictureBox_titleImage;
    }
}