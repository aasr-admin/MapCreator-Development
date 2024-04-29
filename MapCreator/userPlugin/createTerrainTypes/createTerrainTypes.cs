using System.Collections;
using System.ComponentModel;
using System.IO;

using Cartography.compiler;

using Microsoft.VisualBasic.CompilerServices;

using UltimaSDK;

namespace MapCreator.userPlugin
{
    public partial class createTerrainTypes : Form
    {
        private readonly Point[,] StaticGrid;
        private readonly ClsTerrainTable iTerrain;
        private RandomStatics iRandomStatic;

        private canvasControlBox _canvasControlBox;
        private staticSelector _staticSelector;

        private OpenFileDialog _openFileDialog;
        private SaveFileDialog _saveFileDialog;

        public createTerrainTypes()
        {
            StaticGrid = new Point[13, 13];
            iTerrain = new ClsTerrainTable();
            iRandomStatic = new RandomStatics();

            InitializeComponent();

            var col = 13 * 22;
            var row = 22;

            var gx = 0;
            var gy = 0;

            do
            {
                do
                {
                    ref var p = ref StaticGrid[gy, gx];

                    p.X = col - (gy * 22);
                    p.Y = row + (gy * 22);
                }
                while (++gy < 13);

                gy = 0;

                col += 22;
                row += 22;
            }
            while (++gx < 13);

            createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay_ScrollGrid(0, 0);
        }

        private void createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay_ScrollGrid(sbyte x, sbyte y)
        {
            createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay_scrollMarker.Location = StaticGrid[6 + y, 6 + x];

            createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay.ScrollControlIntoView(createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay_scrollMarker);
        }

        /// Form Load Operations
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            iTerrain.Load();
            iTerrain.Display(createTerrainTypes_tabControl_tabPage_ConfigureTerrain_label_baseTerrain_comboBox);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            _canvasControlBox?.Close();
            _staticSelector?.Close();
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            if (_canvasControlBox != null)
            {
                _canvasControlBox.Visible = Visible;
            }

            if (_staticSelector != null)
            {
                _staticSelector.Visible = Visible;
            }
        }

        private void createTerrainTypes_tabControl_tabPage_ConfigureTerrain_Enter(object sender, EventArgs e)
        {
            if (_canvasControlBox?.IsDisposed != false)
            {
                _canvasControlBox = new canvasControlBox();

                _canvasControlBox.ActionNorth += createTerrainTypes_tabControl_tabPage_ConfigureTerrain_ActionNorth;
                _canvasControlBox.ActionNorthEast += createTerrainTypes_tabControl_tabPage_ConfigureTerrain_ActionNorthEast;
                _canvasControlBox.ActionEast += createTerrainTypes_tabControl_tabPage_ConfigureTerrain_ActionEast;
                _canvasControlBox.ActionSouthEast += createTerrainTypes_tabControl_tabPage_ConfigureTerrain_ActionSouthEast;
                _canvasControlBox.ActionSouth += createTerrainTypes_tabControl_tabPage_ConfigureTerrain_ActionSouth;
                _canvasControlBox.ActionSouthWest += createTerrainTypes_tabControl_tabPage_ConfigureTerrain_ActionSouthWest;
                _canvasControlBox.ActionWest += createTerrainTypes_tabControl_tabPage_ConfigureTerrain_ActionWest;
                _canvasControlBox.ActionNorthWest += createTerrainTypes_tabControl_tabPage_ConfigureTerrain_ActionNorthWest;

                _canvasControlBox.ActionChangeX += createTerrainTypes_tabControl_tabPage_ConfigureTerrain_ActionChangeX;
                _canvasControlBox.ActionChangeY += createTerrainTypes_tabControl_tabPage_ConfigureTerrain_ActionChangeY;
                _canvasControlBox.ActionChangeZ += createTerrainTypes_tabControl_tabPage_ConfigureTerrain_ActionChangeZ;
            }

            if (_canvasControlBox?.IsDisposed == false)
            {
                if (_canvasControlBox.Visible)
                {
                    _canvasControlBox.BringToFront();
                }
                else
                {
                    _canvasControlBox.Show(this);
                }
            }
        }

        private void createTerrainTypes_tabControl_tabPage_ConfigureTerrain_Leave(object sender, EventArgs e)
        {
            if (_canvasControlBox?.IsDisposed == false)
            {
                _canvasControlBox.SendToBack();

                _canvasControlBox.Visible = false;
            }
        }

        private void createTerrainTypes_tabControl_tabPage_ConfigureTerrain_ActionChangeX(object sender, EventArgs e)
        {
            var x = (sbyte)Math.Clamp(_canvasControlBox.xAxis_label_numUpDown.Value, -6, 6);
            var y = (sbyte)Math.Clamp(_canvasControlBox.yAxis_label_numUpDown.Value, -6, 6);

            RandomStatic selectedItem = (RandomStatic)staticPlacement_tabControl_tabPage_entryCompnentList_listBox_individualStaticList.SelectedItem;

            if (selectedItem != null)
            {
                selectedItem.X = x;
            }

            createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay_ScrollGrid(x, y);
        }

        private void createTerrainTypes_tabControl_tabPage_ConfigureTerrain_ActionChangeY(object sender, EventArgs e)
        {
            var x = (sbyte)Math.Clamp(_canvasControlBox.xAxis_label_numUpDown.Value, -6, 6);
            var y = (sbyte)Math.Clamp(_canvasControlBox.yAxis_label_numUpDown.Value, -6, 6);

            RandomStatic selectedItem = (RandomStatic)staticPlacement_tabControl_tabPage_entryCompnentList_listBox_individualStaticList.SelectedItem;

            if (selectedItem != null)
            {
                selectedItem.Y = y;
            }

            createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay_ScrollGrid(x, y);
        }

        private void createTerrainTypes_tabControl_tabPage_ConfigureTerrain_ActionChangeZ(object sender, EventArgs e)
        {
            var z = Math.Clamp(_canvasControlBox.zAxis_label_numUpDown.Value, -128, 127);
            
            RandomStatic selectedItem = (RandomStatic)staticPlacement_tabControl_tabPage_entryCompnentList_listBox_individualStaticList.SelectedItem;

            if (selectedItem != null)
            {
                selectedItem.Z = (sbyte)z;
            }
            
            createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay.Refresh();
        }

        private void createTerrainTypes_tabControl_tabPage_ConfigureTerrain_ActionNorth(object sender, EventArgs e)
        {
            createTerrainTypes_tabControl_tabPage_ConfigureTerrain_MoveTerrainTile(0, -1, 0);
        }

        private void createTerrainTypes_tabControl_tabPage_ConfigureTerrain_ActionNorthEast(object sender, EventArgs e)
        {
            createTerrainTypes_tabControl_tabPage_ConfigureTerrain_MoveTerrainTile(1, -1, 0);
        }

        private void createTerrainTypes_tabControl_tabPage_ConfigureTerrain_ActionEast(object sender, EventArgs e)
        {
            createTerrainTypes_tabControl_tabPage_ConfigureTerrain_MoveTerrainTile(1, 0, 0);
        }

        private void createTerrainTypes_tabControl_tabPage_ConfigureTerrain_ActionSouthEast(object sender, EventArgs e)
        {
            createTerrainTypes_tabControl_tabPage_ConfigureTerrain_MoveTerrainTile(1, 1, 0);
        }

        private void createTerrainTypes_tabControl_tabPage_ConfigureTerrain_ActionSouth(object sender, EventArgs e)
        {
            createTerrainTypes_tabControl_tabPage_ConfigureTerrain_MoveTerrainTile(0, 1, 0);
        }

        private void createTerrainTypes_tabControl_tabPage_ConfigureTerrain_ActionSouthWest(object sender, EventArgs e)
        {
            createTerrainTypes_tabControl_tabPage_ConfigureTerrain_MoveTerrainTile(-1, 1, 0);
        }

        private void createTerrainTypes_tabControl_tabPage_ConfigureTerrain_ActionWest(object sender, EventArgs e)
        {
            createTerrainTypes_tabControl_tabPage_ConfigureTerrain_MoveTerrainTile(-1, 0, 0);
        }

        private void createTerrainTypes_tabControl_tabPage_ConfigureTerrain_ActionNorthWest(object sender, EventArgs e)
        {
            createTerrainTypes_tabControl_tabPage_ConfigureTerrain_MoveTerrainTile(-1, -1, 0);
        }

        private void createTerrainTypes_tabControl_tabPage_ConfigureTerrain_MoveTerrainTile(sbyte deltaX, sbyte deltaY, sbyte deltaZ)
        {
            createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay.SuspendLayout();

            RandomStatic selectedItem = (RandomStatic)staticPlacement_tabControl_tabPage_entryCompnentList_listBox_individualStaticList.SelectedItem;

            if (selectedItem != null)
            {
                var x = selectedItem.X;
                var y = selectedItem.Y;
                var z = selectedItem.Y;

                x = (sbyte)Math.Clamp(x + deltaX, -6, 6);
                y = (sbyte)Math.Clamp(y + deltaY, -6, 6);
                z = (sbyte)Math.Clamp(z + deltaZ, -128, 127);

                _canvasControlBox.xAxis_label_numUpDown.Value = selectedItem.X = x;
                _canvasControlBox.yAxis_label_numUpDown.Value = selectedItem.Y = y;
                _canvasControlBox.zAxis_label_numUpDown.Value = selectedItem.Z = z;
            }
            else
            {
                var x = _canvasControlBox.xAxis_label_numUpDown.Value;
                var y = _canvasControlBox.yAxis_label_numUpDown.Value;
                var z = _canvasControlBox.zAxis_label_numUpDown.Value;

                x = Math.Clamp(x + deltaX, -6, 6);
                y = Math.Clamp(y + deltaY, -6, 6);
                z = Math.Clamp(z + deltaZ, -128, 127);

                _canvasControlBox.xAxis_label_numUpDown.Value = x;
                _canvasControlBox.yAxis_label_numUpDown.Value = y;
                _canvasControlBox.zAxis_label_numUpDown.Value = z;
            }

            createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay.ResumeLayout(true);
        }

        /// Form Top Menu Buttons
        private void createTerrainTypes_mainMenu_button_newTerrainType_Click(object sender, EventArgs e)
        {
            var owner = Owner;

            Dispose();

            StaticForm<createTerrainTypes>.Open(owner);
        }

        private void createTerrainTypes_mainMenu_button_loadTerrainType_Click(object sender, EventArgs e)
        {
            _openFileDialog ??= new OpenFileDialog();

            _openFileDialog.InitialDirectory = string.Format("{0}MapCompiler/Engine/TerrainTypes", AppDomain.CurrentDomain.BaseDirectory);
            /// {0}Data/Statics
            _openFileDialog.Filter = "xml files (*.xml)|*.xml";
            _openFileDialog.FilterIndex = 2;
            _openFileDialog.RestoreDirectory = true;

            if (_openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                var fileInfo = new FileInfo(_openFileDialog.FileName);
                createTerrainTypes_tabControl_tabPage_ConfigureTerrain_label_terrainType_textBox.Text = fileInfo.Name;
                iRandomStatic = new RandomStatics(fileInfo.Name);
                iRandomStatic.Display(staticPlacement_tabControl_tabPage_staticEntries_listBox_staticGroupEntryList);
                createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay.Refresh();
            }
        }

        private void createTerrainTypes_mainMenu_button_saveTerrainType_Click(object sender, EventArgs e)
        {
            _saveFileDialog ??= new SaveFileDialog();

            _saveFileDialog.InitialDirectory = string.Format("{0}MapCompiler/Engine/TerrainTypes", AppDomain.CurrentDomain.BaseDirectory);
            /// {0}Data/Statics
            _saveFileDialog.Filter = "xml files (*.xml)|*.xml";
            _saveFileDialog.FileName = createTerrainTypes_tabControl_tabPage_ConfigureTerrain_label_terrainType_textBox.Text;
            _saveFileDialog.FilterIndex = 2;
            _saveFileDialog.RestoreDirectory = true;

            if (_saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                iRandomStatic.Save(_saveFileDialog.FileName);
            }
        }

        private void createTerrainTypes_mainMenu_button_facetBuilder_Click(object sender, EventArgs e)
        {
            Hide();

            StaticForm<facetBuilder>.Open();
        }

        private void createTerrainTypes_mainMenu_button_communityCredits_Click(object sender, EventArgs e)
        {
            StaticForm<communityCredits>.Open();
        }

        private void createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay_Paint(object sender, PaintEventArgs e)
        {
            var pen = Pens.Gray;

            var terrain = (ClsTerrain)createTerrainTypes_tabControl_tabPage_ConfigureTerrain_label_baseTerrain_comboBox.SelectedItem;

            var terrainImage = terrain != null ? Art.GetLand(terrain.TileID) : null;

            var x = 0;
            var y = 0;

            do
            {
                do
                {
                    ref var p = ref StaticGrid[y, x];

                    if (terrainImage != null)
                    {
                        e.Graphics.DrawImage(terrainImage, p.X - 22, p.Y - 22);
                    }

                    e.Graphics.DrawLine(pen, p.X - 22, p.Y, p.X, p.Y + 22);
                    e.Graphics.DrawLine(pen, p.X, p.Y + 22, p.X + 22, p.Y);
                    e.Graphics.DrawLine(pen, p.X + 22, p.Y, p.X, p.Y - 22);
                    e.Graphics.DrawLine(pen, p.X, p.Y - 22, p.X - 22, p.Y);
                }
                while (++y < 13);

                y = 0;
            }
            while (++x < 13);

            pen = Pens.Blue;

            ref var pCenter = ref StaticGrid[6, 6];

            e.Graphics.DrawLine(pen, pCenter.X - 22, pCenter.Y, pCenter.X, pCenter.Y + 22);
            e.Graphics.DrawLine(pen, pCenter.X, pCenter.Y + 22, pCenter.X + 22, pCenter.Y);
            e.Graphics.DrawLine(pen, pCenter.X + 22, pCenter.Y, pCenter.X, pCenter.Y - 22);
            e.Graphics.DrawLine(pen, pCenter.X, pCenter.Y - 22, pCenter.X - 22, pCenter.Y);

            if (_canvasControlBox != null)
            {
                pen = Pens.Red;

                var sy = (int)(6 + _canvasControlBox.yAxis_label_numUpDown.Value); // Yaxis
                var sx = (int)(6 + _canvasControlBox.xAxis_label_numUpDown.Value); // Xaxis

                ref var pSelected = ref StaticGrid[sy, sx];

                e.Graphics.DrawLine(pen, pSelected.X - 22, pSelected.Y, pSelected.X, pSelected.Y + 22);
                e.Graphics.DrawLine(pen, pSelected.X, pSelected.Y + 22, pSelected.X + 22, pSelected.Y);
                e.Graphics.DrawLine(pen, pSelected.X + 22, pSelected.Y, pSelected.X, pSelected.Y - 22);
                e.Graphics.DrawLine(pen, pSelected.X, pSelected.Y - 22, pSelected.X - 22, pSelected.Y);
            }

            foreach (RandomStatic current in staticPlacement_tabControl_tabPage_entryCompnentList_listBox_individualStaticList.Items)
            {
                var sy = 6 + current.Y;
                var sx = 6 + current.X;

                if (sx < 0 || sx >= 13)
                {
                    continue;
                }

                var image = Art.GetStatic(current.TileID);
                var dispose = false;

                if (current.Hue > 0)
                {
                    var hue = Hues.GetHue(current.Hue & 0x3FFF);

                    if (hue != null)
                    {
                        image = new Bitmap(image);

                        dispose = true;

                        ref var data = ref TileData.ItemTable[current.TileID];

                        var partial = data.Flags.HasFlag(TileFlag.PartialHue) || (current.Hue & 0x8000) != 0;

                        hue.ApplyTo(image, partial);
                    }
                }

                ref var sp = ref StaticGrid[sy, sx];

                e.Graphics.DrawImage(image, sp.X - (image.Width / 2f), sp.Y - image.Height + 22f);

                if (dispose)
                {
                    image.Dispose();
                }
            }
        }

        private void createTerrainTypes_tabControl_tabPage_ConfigureTerrain_label_baseTerrain_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay.Refresh();
        }

        /// Static Frequencies
        private void staticPlacement_tabControl_tabPage_staticEntries_label_randomStaticFrequency_numUpDown_ValueChanged(object sender, EventArgs e)
        {
            iRandomStatic.Freq = Convert.ToInt32(staticPlacement_tabControl_tabPage_staticEntries_label_randomStaticFrequency_numUpDown.Value);
        }

        private void staticPlacement_tabControl_tabPage_staticEntries_label_selectedEntryFrequency_numUpDown_ValueChanged(object sender, EventArgs e)
        {
            var selectedItem = (RandomStaticCollection)staticPlacement_tabControl_tabPage_staticEntries_listBox_staticGroupEntryList.SelectedItem;

            if (selectedItem != null)
            {
                selectedItem.Freq = Convert.ToInt32(staticPlacement_tabControl_tabPage_staticEntries_label_selectedEntryFrequency_numUpDown.Value);
            }
        }

        /// Static Group Entries
        private void staticPlacement_tabControl_tabPage_staticEntries_listBox_staticGroupEntryList_SelectedIndexChanged(object sender, EventArgs e)
        {
            staticPlacement_tabControl_tabPage_entryCompnentList_panel_staticPictureBox.Image = null;
            var selectedItem = (RandomStaticCollection)staticPlacement_tabControl_tabPage_staticEntries_listBox_staticGroupEntryList.SelectedItem;

            if (selectedItem != null)
            {
                staticPlacement_tabControl_tabPage_staticEntries_label_staticEntryDescription_textBox.Text = selectedItem.Description;
                staticPlacement_tabControl_tabPage_staticEntries_label_selectedEntryFrequency_numUpDown.Value = new decimal(selectedItem.Freq);
                selectedItem.Display(staticPlacement_tabControl_tabPage_entryCompnentList_listBox_individualStaticList);
                createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay.Refresh();
            }
        }

        private void staticPlacement_tabControl_tabPage_staticEntries_toolStrip_button_addStatics_Click(object sender, EventArgs e)
        {
            if (StringType.StrCmp(staticPlacement_tabControl_tabPage_staticEntries_label_staticEntryDescription_textBox.Text, string.Empty, false) == 0)
            {
                return;
            }

            iRandomStatic.Add(new RandomStaticCollection(staticPlacement_tabControl_tabPage_staticEntries_label_staticEntryDescription_textBox.Text, Convert.ToInt32(staticPlacement_tabControl_tabPage_staticEntries_label_selectedEntryFrequency_numUpDown.Value)));
            iRandomStatic.Display(staticPlacement_tabControl_tabPage_staticEntries_listBox_staticGroupEntryList);

            createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay.Refresh();
        }

        private void staticPlacement_tabControl_tabPage_staticEntries_toolStrip_button_deleteStatics_Click(object sender, EventArgs e)
        {
            iRandomStatic.Remove((RandomStaticCollection)staticPlacement_tabControl_tabPage_staticEntries_listBox_staticGroupEntryList.SelectedItem);
            iRandomStatic.Display(staticPlacement_tabControl_tabPage_staticEntries_listBox_staticGroupEntryList);
            createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay.Refresh();
        }

        private void staticPlacement_tabControl_tabPage_staticEntries_toolStrip_button_refreshStatics_Click(object sender, EventArgs e)
        {
            staticPlacement_tabControl_tabPage_staticEntries_listBox_staticGroupEntryList.Items.Clear();
            createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay.Refresh();
        }

        private void staticPlacement_tabControl_tabPage_entryCompnentList_panel_button_staticSelector_Click(object sender, EventArgs e)
        {
            if (_staticSelector?.IsDisposed != false)
            {
                _staticSelector = new staticSelector();

                _staticSelector.ValueChanged += staticPlacement_tabControl_tabPage_entryCompnentList_panel_button_staticSelector_SelectionChanged;
            }

            if (_staticSelector?.IsDisposed == false)
            {
                _staticSelector.Value = staticPlacement_tabControl_tabPage_entryCompnentList_panel_staticPictureBox_vScroll.Value;

                if (_staticSelector.Visible)
                {
                    _staticSelector.BringToFront();
                }
                else
                {
                    _staticSelector.Show(this);
                }
            }
        }

        private void staticPlacement_tabControl_tabPage_entryCompnentList_panel_button_staticSelector_SelectionChanged(object sender, EventArgs e)
        {
            staticPlacement_tabControl_tabPage_entryCompnentList_panel_staticPictureBox_vScroll.Value = _staticSelector.Value;
        }

        private void staticPlacement_tabControl_tabPage_entryCompnentList_panel_button_staticSelector_MouseEnter(object sender, EventArgs e)
        {
            staticPlacement_tabControl_tabPage_entryCompnentList_panel_button_staticSelector.ForeColor = Color.LimeGreen;
        }

        private void staticPlacement_tabControl_tabPage_entryCompnentList_panel_button_staticSelector_MouseLeave(object sender, EventArgs e)
        {
            staticPlacement_tabControl_tabPage_entryCompnentList_panel_button_staticSelector.ForeColor = Color.SlateGray;
        }

        private void UpdatePanel2()
        {
            if (Art.IsValidStatic(staticPlacement_tabControl_tabPage_entryCompnentList_panel_staticPictureBox_vScroll.Value))
            {
                createTerrainTypes_tabControl_tabPage_ConfigureTerrain_label_tileID_textBox.Text = staticPlacement_tabControl_tabPage_entryCompnentList_panel_staticPictureBox_vScroll.Value.ToString();
                staticPlacement_tabControl_tabPage_entryCompnentList_panel_staticPictureBox.Image = Art.GetStatic(staticPlacement_tabControl_tabPage_entryCompnentList_panel_staticPictureBox_vScroll.Value);
                staticPlacement_tabControl_tabPage_staticProperties_propertyGrid.SelectedObject = TileData.ItemTable[staticPlacement_tabControl_tabPage_entryCompnentList_panel_staticPictureBox_vScroll.Value];
                staticPlacement_tabControl_tabPage_entryCompnentList_panel_textBox_staticDescription.Text = string.Format("{0} ({1})", TileData.ItemTable[staticPlacement_tabControl_tabPage_entryCompnentList_panel_staticPictureBox_vScroll.Value].Name, staticPlacement_tabControl_tabPage_entryCompnentList_panel_staticPictureBox_vScroll.Value);
            }
        }

        private void staticPlacement_tabControl_tabPage_entryCompnentList_panel_staticPictureBox_vScroll_Scroll(object sender, ScrollEventArgs e)
        {
            UpdatePanel2();
        }

        private void staticPlacement_tabControl_tabPage_entryCompnentList_panel_staticPictureBox_vScroll_ValueChanged(object sender, EventArgs e)
        {
            UpdatePanel2();
        }

        private void staticPlacement_tabControl_tabPage_entryCompnentList_listBox_individualStaticList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedItem = (RandomStatic)staticPlacement_tabControl_tabPage_entryCompnentList_listBox_individualStaticList.SelectedItem;

            if (selectedItem != null)
            {
                var randomStatic = selectedItem;
                staticPlacement_tabControl_tabPage_entryCompnentList_panel_staticPictureBox_vScroll.Value = randomStatic.TileID;

                if (Art.GetStatic(randomStatic.TileID) != null)
                {
                    staticPlacement_tabControl_tabPage_entryCompnentList_panel_staticPictureBox.Image = Art.GetStatic(randomStatic.TileID);
                    staticPlacement_tabControl_tabPage_staticProperties_propertyGrid.SelectedObject = TileData.ItemTable[randomStatic.TileID];
                }

                createTerrainTypes_tabControl_tabPage_ConfigureTerrain_label_tileID_textBox.Text = Convert.ToString(randomStatic.TileID);

                if (_canvasControlBox != null)
                {
                    _canvasControlBox.xAxis_label_numUpDown.Value = new decimal(randomStatic.X);
                    _canvasControlBox.yAxis_label_numUpDown.Value = new decimal(randomStatic.Y);
                    _canvasControlBox.zAxis_label_numUpDown.Value = new decimal(randomStatic.Z);   // Z-Axis Is Not Implemented Yet
                }

                createTerrainTypes_tabControl_tabPage_ConfigureTerrain_label_hueID_textBox.Text = Convert.ToString(randomStatic.Hue); // Hues Are Not Implemented Yet

            }
        }

        private void staticPlacement_tabControl_tabPage_entryCompnentList_toolStrip_button_addStatics_Click(object sender, EventArgs e)
        {
            var selectedItem = (RandomStaticCollection)staticPlacement_tabControl_tabPage_staticEntries_listBox_staticGroupEntryList.SelectedItem;

            if (selectedItem != null)
            {
                var tileIDString = createTerrainTypes_tabControl_tabPage_ConfigureTerrain_label_tileID_textBox.Text;

                if (string.IsNullOrWhiteSpace(tileIDString))
                {
                    tileIDString = "0";
                }

                var hueIDString = createTerrainTypes_tabControl_tabPage_ConfigureTerrain_label_hueID_textBox.Text;

                if (string.IsNullOrWhiteSpace(hueIDString))
                {
                    hueIDString = "0";
                }

                var tileID = ushort.Parse(tileIDString);
                var x = Convert.ToSByte(_canvasControlBox?.xAxis_label_numUpDown.Value ?? 0);
                var y = Convert.ToSByte(_canvasControlBox?.yAxis_label_numUpDown.Value ?? 0);
                var z = Convert.ToSByte(_canvasControlBox?.zAxis_label_numUpDown.Value ?? 0);
                var hue = ushort.Parse(hueIDString);

                selectedItem.Add(new RandomStatic(tileID, x, y, z, hue));
                selectedItem.Display(staticPlacement_tabControl_tabPage_entryCompnentList_listBox_individualStaticList);
                createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay_ScrollGrid(x, y);
            }
        }

        private void staticPlacement_tabControl_tabPage_entryCompnentList_toolStrip_button_deleteStatics_Click(object sender, EventArgs e)
        {
            var selectedItem = (RandomStaticCollection)staticPlacement_tabControl_tabPage_staticEntries_listBox_staticGroupEntryList.SelectedItem;

            if (selectedItem != null)
            {
                selectedItem.Remove((RandomStatic)staticPlacement_tabControl_tabPage_entryCompnentList_listBox_individualStaticList.SelectedItem);
                selectedItem.Display(staticPlacement_tabControl_tabPage_entryCompnentList_listBox_individualStaticList);
                createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay.Refresh();
            }
        }

        private void staticPlacement_tabControl_tabPage_entryCompnentList_toolStrip_button_refreshStatics_Click(object sender, EventArgs e)
        {
            staticPlacement_tabControl_tabPage_entryCompnentList_listBox_individualStaticList.Items.Clear();
            createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay.Refresh();
        }
    }
}
