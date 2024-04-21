using System.Collections;
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

        private communityCredits _communityCredits;
        private canvasControlBox _canvasControlBox;
        private createTerrainTypes _createTerrainTypes;
        private facetBuilder _facetBuilder;
        private staticSelector _staticSelector;

        private OpenFileDialog _openFileDialog;
        private SaveFileDialog _saveFileDialog;

        public createTerrainTypes()
        {
            MaximizeBox = false;
            MinimizeBox = false;

            var cTT = this;
            base.Load += new EventHandler(cTT.createTerrainTypes_Load);

            StaticGrid = new Point[13, 13];
            iTerrain = new ClsTerrainTable();
            iRandomStatic = new RandomStatics();

            InitializeComponent();

            var num = 302;
            var num1 = 246;
            var num2 = 0;

            do
            {
                var num3 = 0;

                do
                {
                    var staticGrid = StaticGrid;
                    var point = new Point(checked(num - checked(num3 * 22)), checked(num1 + checked(num3 * 22)));
                    staticGrid[num3, num2] = point;
                    num3++;
                }
                while (num3 <= 12);
                num = checked(num + 22);
                num1 = checked(num1 + 22);
                num2++;
            }
            while (num2 <= 12);
        }

        /// Form Load Operations
        private void createTerrainTypes_Load(object sender, EventArgs e)
        {
            iTerrain.Load();
            iTerrain.Display(createTerrainTypes_tabControl_tabPage_ConfigureTerrain_label_baseTerrain_comboBox);
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

            _canvasControlBox?.Show(this);
        }

        private void createTerrainTypes_tabControl_tabPage_ConfigureTerrain_Leave(object sender, EventArgs e)
        {
            _canvasControlBox?.Hide();
        }

        private void createTerrainTypes_tabControl_tabPage_ConfigureTerrain_ActionChangeX(object sender, EventArgs e)
        {
            this.createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay.Refresh();
        }

        private void createTerrainTypes_tabControl_tabPage_ConfigureTerrain_ActionChangeY(object sender, EventArgs e)
        {
            this.createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay.Refresh();
        }

        private void createTerrainTypes_tabControl_tabPage_ConfigureTerrain_ActionChangeZ(object sender, EventArgs e)
        {
            this.createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay.Refresh();
        }

        private void createTerrainTypes_tabControl_tabPage_ConfigureTerrain_ActionNorth(object sender, EventArgs e)
        {
            createTerrainTypes_tabControl_tabPage_ConfigureTerrain_MoveTerrainTile(0, -1);
        }

        private void createTerrainTypes_tabControl_tabPage_ConfigureTerrain_ActionNorthEast(object sender, EventArgs e)
        {
            createTerrainTypes_tabControl_tabPage_ConfigureTerrain_MoveTerrainTile(1, -1);
        }

        private void createTerrainTypes_tabControl_tabPage_ConfigureTerrain_ActionEast(object sender, EventArgs e)
        {
            createTerrainTypes_tabControl_tabPage_ConfigureTerrain_MoveTerrainTile(1, 0);
        }

        private void createTerrainTypes_tabControl_tabPage_ConfigureTerrain_ActionSouthEast(object sender, EventArgs e)
        {
            createTerrainTypes_tabControl_tabPage_ConfigureTerrain_MoveTerrainTile(1, 1);
        }

        private void createTerrainTypes_tabControl_tabPage_ConfigureTerrain_ActionSouth(object sender, EventArgs e)
        {
            createTerrainTypes_tabControl_tabPage_ConfigureTerrain_MoveTerrainTile(0, 1);
        }

        private void createTerrainTypes_tabControl_tabPage_ConfigureTerrain_ActionSouthWest(object sender, EventArgs e)
        {
            createTerrainTypes_tabControl_tabPage_ConfigureTerrain_MoveTerrainTile(-1, 1);
        }

        private void createTerrainTypes_tabControl_tabPage_ConfigureTerrain_ActionWest(object sender, EventArgs e)
        {
            createTerrainTypes_tabControl_tabPage_ConfigureTerrain_MoveTerrainTile(-1, 0);
        }

        private void createTerrainTypes_tabControl_tabPage_ConfigureTerrain_ActionNorthWest(object sender, EventArgs e)
        {
            createTerrainTypes_tabControl_tabPage_ConfigureTerrain_MoveTerrainTile(-1, -1);
        }

        private void createTerrainTypes_tabControl_tabPage_ConfigureTerrain_MoveTerrainTile(sbyte deltaX, sbyte deltaY)
        {
            RandomStatic selectedItem = (RandomStatic)staticPlacement_tabControl_tabPage_entryCompnentList_listBox_individualStaticList.SelectedItem;

            if (selectedItem != null)
            {
                var x = selectedItem.X;
                var y = selectedItem.Y;

                x = (byte)Math.Clamp(x + deltaX, 0, 12);
                y = (byte)Math.Clamp(y + deltaY, 0, 12);

                _canvasControlBox.xAxis_label_numUpDown.Value = selectedItem.X = x;
                _canvasControlBox.yAxis_label_numUpDown.Value = selectedItem.Y = y;
            }
            else
            {
                _canvasControlBox.xAxis_label_numUpDown.Value = 0;
                _canvasControlBox.yAxis_label_numUpDown.Value = 0;
                _canvasControlBox.zAxis_label_numUpDown.Value = 0;
            }

            createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay.Refresh();
        }

        /// Form Top Menu Buttons
        private void createTerrainTypes_mainMenu_button_newTerrainType_Click(object sender, EventArgs e)
        {
            _createTerrainTypes ??= new createTerrainTypes();
            _createTerrainTypes.Show();

            Hide();
        }

        private void createTerrainTypes_mainMenu_button_loadTerrainType_Click(object sender, EventArgs e)
        {
            _openFileDialog ??= new OpenFileDialog();

            _openFileDialog.InitialDirectory = string.Format("{0}MapCompiler/Engine/TerrainTypes", AppDomain.CurrentDomain.BaseDirectory);
            /// {0}Data/Statics
            _openFileDialog.Filter = "xml files (*.xml)|*.xml";
            _openFileDialog.FilterIndex = 2;
            _openFileDialog.RestoreDirectory = true;

            if (_openFileDialog.ShowDialog() == DialogResult.OK)
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

            if (_saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                iRandomStatic.Save(_saveFileDialog.FileName);
            }
        }

        private void createTerrainTypes_mainMenu_button_facetBuilder_Click(object sender, EventArgs e)
        {
            _facetBuilder ??= new facetBuilder();
            _facetBuilder.Show();

            Hide();
        }

        private void createTerrainTypes_mainMenu_button_communityCredits_Click(object sender, EventArgs e)
        {
            _communityCredits ??= new communityCredits();
            _communityCredits.Show();
        }

        /// Terrain Grid Display (Connects To canvasControlBox Lines 195 and 196)
        private void createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay_Paint(object sender, PaintEventArgs e)
        {
            IEnumerator enumerator = null;
            var graphics = e.Graphics;
            var pen = new Pen(Color.Gray);
            var selectedItem = (ClsTerrain)createTerrainTypes_tabControl_tabPage_ConfigureTerrain_label_baseTerrain_comboBox.SelectedItem;

            var num = 0;

            do
            {
                var num1 = 0;

                do
                {
                    var num2 = num1;
                    var num3 = num;

                    if (selectedItem != null)
                    {
                        e.Graphics.DrawImage(Art.GetLand(selectedItem.TileID), checked(StaticGrid[num2, num3].X - 22), checked(StaticGrid[num2, num3].Y - 22));
                    }

                    e.Graphics.DrawLine(pen, checked(StaticGrid[num2, num3].X - 22), StaticGrid[num2, num3].Y, StaticGrid[num2, num3].X, checked(StaticGrid[num2, num3].Y + 22));
                    e.Graphics.DrawLine(pen, StaticGrid[num2, num3].X, checked(StaticGrid[num2, num3].Y + 22), checked(StaticGrid[num2, num3].X + 22), StaticGrid[num2, num3].Y);
                    e.Graphics.DrawLine(pen, checked(StaticGrid[num2, num3].X + 22), StaticGrid[num2, num3].Y, StaticGrid[num2, num3].X, checked(StaticGrid[num2, num3].Y - 22));
                    e.Graphics.DrawLine(pen, StaticGrid[num2, num3].X, checked(StaticGrid[num2, num3].Y - 22), checked(StaticGrid[num2, num3].X - 22), StaticGrid[num2, num3].Y);
                    num1++;
                }
                while (num1 <= 12);
                num++;
            }
            while (num <= 12);
            pen = new Pen(Color.Red);

            var num4 = Convert.ToInt32(decimal.Add(new decimal(6L), /*this.Yaxis.Value*/0)); // Yaxis
            var num5 = Convert.ToInt32(decimal.Add(new decimal(6L), /*this.Xaxis.Value*/0)); // Xaxis

            e.Graphics.DrawLine(pen, checked(StaticGrid[num4, num5].X - 22), StaticGrid[num4, num5].Y, StaticGrid[num4, num5].X, checked(StaticGrid[num4, num5].Y + 22));
            e.Graphics.DrawLine(pen, StaticGrid[num4, num5].X, checked(StaticGrid[num4, num5].Y + 22), checked(StaticGrid[num4, num5].X + 22), StaticGrid[num4, num5].Y);
            e.Graphics.DrawLine(pen, checked(StaticGrid[num4, num5].X + 22), StaticGrid[num4, num5].Y, StaticGrid[num4, num5].X, checked(StaticGrid[num4, num5].Y - 22));
            e.Graphics.DrawLine(pen, StaticGrid[num4, num5].X, checked(StaticGrid[num4, num5].Y - 22), checked(StaticGrid[num4, num5].X - 22), StaticGrid[num4, num5].Y);

            try
            {
                enumerator = staticPlacement_tabControl_tabPage_entryCompnentList_listBox_individualStaticList.Items.GetEnumerator();

                while (enumerator.MoveNext())
                {
                    var current = (RandomStatic)enumerator.Current;
                    var y = current.Y;
                    var x = current.X;
                    var @static = Art.GetStatic(current.TileID);
                    var point = new Point(checked((int)Math.Round(StaticGrid[y, x].X - ((double)@static.Width / 2))), checked(checked(StaticGrid[y, x].Y - @static.Height) + 22));
                    e.Graphics.DrawImage(@static, point);
                }
            }
            finally
            {
                if (enumerator is IDisposable)
                {
                    ((IDisposable)enumerator).Dispose();
                }
            }

            graphics = null;
        }

        private void createTerrainTypes_tabControl_tabPage_ConfigureTerrain_label_baseTerrain_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay.Refresh();
        }

        private void UpdatePanel1()
        {
            var tGD = createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay;
            var point = new Point(checked(createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay_hScrollBar.Value * -1), checked(createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay_vScrollBar.Value * -1));
            tGD.Location = point;
        }

        private void createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay_vScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            UpdatePanel1();
        }

        private void createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay_hScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            UpdatePanel1();
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

        /// Entry Component List  (Connects To canvasControlBox Lines 365, 366, 367, 390)
        private void staticPlacement_tabControl_tabPage_entryCompnentList_panel_button_staticSelector_Click(object sender, EventArgs e)
        {
            _staticSelector ??= new staticSelector();
            _staticSelector.Tag = staticPlacement_tabControl_tabPage_entryCompnentList_panel_staticPictureBox_vScroll;
            _staticSelector.Show();
        }

        private void staticPlacement_tabControl_tabPage_entryCompnentList_panel_button_staticSelector_MouseEnter(object sender, EventArgs e)
        {
            staticPlacement_tabControl_tabPage_entryCompnentList_panel_button_staticSelector.ForeColor = System.Drawing.Color.LimeGreen;
        }

        private void staticPlacement_tabControl_tabPage_entryCompnentList_panel_button_staticSelector_MouseLeave(object sender, EventArgs e)
        {
            staticPlacement_tabControl_tabPage_entryCompnentList_panel_button_staticSelector.ForeColor = System.Drawing.Color.SlateGray;
        }

        private void UpdatePanel2()
        {
            if (Art.GetStatic(staticPlacement_tabControl_tabPage_entryCompnentList_panel_staticPictureBox_vScroll.Value) != null && staticPlacement_tabControl_tabPage_entryCompnentList_panel_staticPictureBox_vScroll.Value < TileData.ItemTable.Length)
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
                var x = Convert.ToByte(_canvasControlBox?.xAxis_label_numUpDown.Value ?? 0);
                var y = Convert.ToByte(_canvasControlBox?.yAxis_label_numUpDown.Value ?? 0);
                var z = Convert.ToSByte(_canvasControlBox?.zAxis_label_numUpDown.Value ?? 0);
                var hue = ushort.Parse(hueIDString);

                selectedItem.Add(new RandomStatic(tileID, x, y, z, hue));
                selectedItem.Display(staticPlacement_tabControl_tabPage_entryCompnentList_listBox_individualStaticList);
                createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay.Refresh();
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
