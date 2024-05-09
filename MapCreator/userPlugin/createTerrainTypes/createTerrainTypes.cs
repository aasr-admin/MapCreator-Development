using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Linq;

using UltimaSDK;

namespace MapCreator
{
    public partial class CreateTerrainTypes : Form
    {
        public const byte LAND_SIZE = 44;
        public const sbyte LAND_OFFSET = LAND_SIZE / 2;

        public const byte GRID_MIN_SIZE = 13;
        public const byte GRID_MAX_SIZE = 255;

        private byte _gridSize = GRID_MIN_SIZE;
        private sbyte _gridCenter = (GRID_MIN_SIZE - 1) / 2;

        public byte GridSize
        {
            get => _gridSize;
            set
            {
                value = Math.Clamp(value, GRID_MIN_SIZE, GRID_MAX_SIZE);

                if ((value - 1) % 2 != 0)
                {
                    ++value;
                }

                int size = value;

                foreach (RandomStatic tile in staticPlacement_tabControl_tabPage_entryCompnentList_listBox_individualStaticList.Items)
                {
                    var tx = ((size - 1) / 2) + tile.X;

                    if (tx < 0)
                    {
                        size += Math.Abs(tx);
                    }
                    else if (tx >= size)
                    {
                        size = tx + 1;
                    }

                    var ty = ((size - 1) / 2) + tile.Y;

                    if (ty < 0)
                    {
                        size += Math.Abs(ty);
                    }
                    else if (ty >= size)
                    {
                        size = ty + 1;
                    }
                }

                value = (byte)size;

                if ((value - 1) % 2 != 0)
                {
                    ++value;
                }

                _gridSize = value;
                _gridCenter = (sbyte)((value - 1) / 2);
            }
        }

        public sbyte GridCenter => _gridCenter;

        private record class GridEntry : IDisposable
        {
            public GraphicsPath Path { get; }

            public Region Region { get; }

            private readonly Point _gridCell;

            public Point GridCell => _gridCell;

            public byte GridX => (byte)_gridCell.X;
            public byte GridY => (byte)_gridCell.Y;

            private readonly Point _gridOffset;

            public Point GridOffset => _gridOffset;

            public sbyte GridOffsetX => (sbyte)_gridOffset.X;
            public sbyte GridOffsetY => (sbyte)_gridOffset.Y;

            private readonly Point _clientCenter;

            public Point ClientCenter => _clientCenter;

            public int ClientCenterX => _clientCenter.X;
            public int ClientCenterY => _clientCenter.Y;

            private readonly Point _clientLocation;

            public Point ClientLocation => _clientLocation;

            public int ClientLocationX => _clientLocation.X;
            public int ClientLocationY => _clientLocation.Y;

            public GridEntry(sbyte gridCenter, byte gridX, byte gridY, int centerX, int centerY)
            {
                _gridCell.Offset(gridX, gridY);
                _gridOffset.Offset(gridX - gridCenter, gridY - gridCenter);

                _clientCenter.Offset(centerX, centerY);
                _clientLocation.Offset(centerX - LAND_OFFSET, centerY - LAND_OFFSET);

                Path = new GraphicsPath();

                Path.AddPolygon(new Point[]
                {
                    new(centerX - LAND_OFFSET, centerY),
                    new(centerX, centerY - LAND_OFFSET),
                    new(centerX + LAND_OFFSET, centerY),
                    new(centerX, centerY + LAND_OFFSET),
                });

                Region = new Region(Path);
            }

            public bool Contains(Point p)
            {
                return Region.IsVisible(p);
            }

            public bool Contains(PointF p)
            {
                return Region.IsVisible(p);
            }

            public bool Contains(float x, float y)
            {
                return Region.IsVisible(x, y);
            }

            public void Dispose()
            {
                Region.Dispose();
                Path.Dispose();
            }
        }

        private GridEntry[,] _staticGrid;

        private readonly ClsTerrainTable _terrainTable = new();

        private readonly RandomStatics _randomStatics = [];

        private readonly Pen _basePen = new(Color.FromArgb(96, Color.Gray));
        private readonly SolidBrush _baseBrush = new(Color.FromArgb(96, Color.LightGray));

        private readonly Pen _highlightPen = new(Color.FromArgb(96, Color.SkyBlue));
        private readonly SolidBrush _highlightBrush = new(Color.FromArgb(96, Color.LightSkyBlue));

        private CanvasControlBox _canvasControls;
        private StaticSelector _staticSelector;

        private OpenFileDialog _openFileDialog;
        private SaveFileDialog _saveFileDialog;

        public CreateTerrainTypes()
        {
            PopulateGrid();

            InitializeComponent();
        }

        private void PopulateGrid()
        {
            _staticGrid = new GridEntry[GridSize, GridSize];

            int px = GridSize * LAND_OFFSET;
            int py = LAND_OFFSET;

            byte gx = 0;
            byte gy = 0;

            do
            {
                do
                {
                    _staticGrid[gy, gx] = new GridEntry(GridCenter, gx, gy, px - (gy * LAND_OFFSET), py + (gy * LAND_OFFSET));
                }
                while (++gy < GridSize);

                gy = 0;

                px += LAND_OFFSET;
                py += LAND_OFFSET;
            }
            while (++gx < GridSize);
        }

        /// Form Load Operations
        private void Initialize()
        {
            _randomStatics.Clear();

            _terrainTable.Clear();
            _terrainTable.Load();

            _terrainTable.Display(createTerrainTypes_tabControl_tabPage_ConfigureTerrain_label_baseTerrain_comboBox);

            if (createTerrainTypes_tabControl_tabPage_ConfigureTerrain_label_baseTerrain_comboBox.Items.Count > 1)
            {
                var randTerrain = Utility.Random(1, createTerrainTypes_tabControl_tabPage_ConfigureTerrain_label_baseTerrain_comboBox.Items.Count);

                createTerrainTypes_tabControl_tabPage_ConfigureTerrain_label_baseTerrain_comboBox.SelectedIndex = randTerrain;
            }
            else if (createTerrainTypes_tabControl_tabPage_ConfigureTerrain_label_baseTerrain_comboBox.Items.Count > 0)
            {
                createTerrainTypes_tabControl_tabPage_ConfigureTerrain_label_baseTerrain_comboBox.SelectedIndex = 0;
            }

            staticPlacement_tabControl_tabPage_entryCompnentList_listBox_individualStaticList.SelectedIndex = -1;
            staticPlacement_tabControl_tabPage_entryCompnentList_listBox_individualStaticList.BeginUpdate();
            staticPlacement_tabControl_tabPage_entryCompnentList_listBox_individualStaticList.Items.Clear();
            staticPlacement_tabControl_tabPage_entryCompnentList_listBox_individualStaticList.EndUpdate();
            staticPlacement_tabControl_tabPage_entryCompnentList_listBox_individualStaticList.Invalidate();

            staticPlacement_tabControl_tabPage_staticEntries_listBox_staticGroupEntryList.SelectedIndex = -1;
            staticPlacement_tabControl_tabPage_staticEntries_listBox_staticGroupEntryList.BeginUpdate();
            staticPlacement_tabControl_tabPage_staticEntries_listBox_staticGroupEntryList.Items.Clear();
            staticPlacement_tabControl_tabPage_staticEntries_listBox_staticGroupEntryList.EndUpdate();
            staticPlacement_tabControl_tabPage_staticEntries_listBox_staticGroupEntryList.Invalidate();

            var landSize = new Size(LAND_SIZE, LAND_SIZE);

            createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay_scrollMarker.SuspendLayout();
            createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay_scrollMarker.MaximumSize = landSize;
            createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay_scrollMarker.MinimumSize = landSize;
            createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay_scrollMarker.Size = landSize;
            createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay_scrollMarker.ResumeLayout(true);

            var gridSize = new Size(LAND_SIZE * GridSize, LAND_SIZE * GridSize);

            createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay.SuspendLayout();
            createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay.MaximumSize = gridSize;
            createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay.MinimumSize = gridSize;
            createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay.Size = gridSize;
            createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay.ResumeLayout(true);

            if (_canvasControls?.IsDisposed == false)
            {
                _canvasControls.XAxisMinimum = _canvasControls.YAxisMinimum = (sbyte)-GridCenter;
                _canvasControls.XAxisMaximum = _canvasControls.YAxisMaximum = GridCenter;

                _canvasControls.UpdateAxis(0, 0, 0);
            }
            else
            {
                createTerrainTypes_tabControl_tabPage_ConfigureTerrain_ScrollGrid();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Initialize();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            _canvasControls?.Close();
            _staticSelector?.Close();
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            if (_canvasControls?.IsDisposed == false)
            {
                _canvasControls.Visible = Visible;
            }

            if (_staticSelector != null)
            {
                _staticSelector.Visible = Visible;
            }
        }

        private void createTerrainTypes_tabControl_tabPage_ConfigureTerrain_Enter(object sender, EventArgs e)
        {
            if (_canvasControls?.IsDisposed != false)
            {
                _canvasControls = new CanvasControlBox();

                _canvasControls.XAxisMinimum = _canvasControls.YAxisMinimum = (sbyte)-GridCenter;
                _canvasControls.XAxisMaximum = _canvasControls.YAxisMaximum = GridCenter;

                _canvasControls.AxisValueChanged += createTerrainTypes_tabControl_tabPage_ConfigureTerrain_AxisValueChanged;
            }

            if (_canvasControls?.IsDisposed == false)
            {
                if (_canvasControls.Visible)
                {
                    _canvasControls.BringToFront();
                }
                else
                {
                    _canvasControls.Show(this);
                }
            }
        }

        private void createTerrainTypes_tabControl_tabPage_ConfigureTerrain_Leave(object sender, EventArgs e)
        {
            if (_canvasControls?.IsDisposed == false)
            {
                _canvasControls.SendToBack();

                _canvasControls.Visible = false;
            }
        }

        private void createTerrainTypes_tabControl_tabPage_ConfigureTerrain_AxisValueChanged(object sender, EventArgs e)
        {
            if (_canvasControls?.IsDisposed == false)
            {
                if (staticPlacement_tabControl_tabPage_entryCompnentList_listBox_individualStaticList.SelectedItem is RandomStatic selectedStatic)
                {
                    selectedStatic.X = _canvasControls.XAxisValue;
                    selectedStatic.Y = _canvasControls.YAxisValue;
                    selectedStatic.Z = _canvasControls.ZAxisValue;
                }
            }

            createTerrainTypes_tabControl_tabPage_ConfigureTerrain_ScrollGrid();
        }

        private void createTerrainTypes_tabControl_tabPage_ConfigureTerrain_ScrollGrid()
        {
            createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay.Invalidate();

            var loc = createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay.Location;

            if (_canvasControls?.IsDisposed == false)
            {
                loc.Offset(_staticGrid[GridCenter + _canvasControls.YAxisValue, GridCenter + _canvasControls.XAxisValue].ClientLocation);
            }
            else
            {
                loc.Offset(_staticGrid[GridCenter, GridCenter].ClientLocation);
            }

            createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay_scrollMarker.Location = loc;

            createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay_scrollMarker.Refresh();

            createTerrainTypes_groupBox_terrainPreview_panel.ScrollControlIntoView(createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay_scrollMarker);
        }

        /// Form Top Menu Buttons
        private void createTerrainTypes_mainMenu_button_newTerrainType_Click(object sender, EventArgs e)
        {
            Initialize();
        }

        private void createTerrainTypes_mainMenu_button_loadTerrainType_Click(object sender, EventArgs e)
        {
            _openFileDialog ??= new OpenFileDialog();

            _openFileDialog.Filter = "xml files (*.xml)|*.xml";
            _openFileDialog.InitialDirectory = Path.Combine(Environment.CurrentDirectory, "MapCompiler", "Engine", "TerrainTypes");

            if (_openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                var fileInfo = new FileInfo(_openFileDialog.FileName);

                createTerrainTypes_tabControl_tabPage_ConfigureTerrain_label_terrainType_textBox.Text = fileInfo.Name;

                _randomStatics.Load(fileInfo.Name);

                _randomStatics.Display(staticPlacement_tabControl_tabPage_staticEntries_listBox_staticGroupEntryList);

                createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay.Invalidate();
            }
        }

        private void createTerrainTypes_mainMenu_button_saveTerrainType_Click(object sender, EventArgs e)
        {
            _saveFileDialog ??= new SaveFileDialog();

            _saveFileDialog.Filter = "xml files (*.xml)|*.xml";
            _openFileDialog.InitialDirectory = Path.Combine(Environment.CurrentDirectory, "MapCompiler", "Engine", "TerrainTypes");
            _saveFileDialog.FileName = createTerrainTypes_tabControl_tabPage_ConfigureTerrain_label_terrainType_textBox.Text;

            if (_saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                _randomStatics.Save(_saveFileDialog.FileName);
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

        private readonly record struct TileInfo(object Source, byte GridX, byte GridY, sbyte GridZ, byte GridH, TileFlag Flags);

        private IEnumerable<TileInfo> EnumerateTiles(bool terrain, bool statics)
        {
            var terrainFlags = TileFlag.None;

            if (createTerrainTypes_tabControl_tabPage_ConfigureTerrain_label_baseTerrain_comboBox.SelectedItem is ClsTerrain t)
            {
                terrainFlags = TileData.LandTable[t.TileID].Flags;
            }

            var gTiles = terrain ? _staticGrid.Cast<GridEntry>().Select(o => new TileInfo(o, o.GridX, o.GridY, 0, 0, terrainFlags)) : [];

            var list = staticPlacement_tabControl_tabPage_entryCompnentList_listBox_individualStaticList.Items;

            var sTiles = statics ? list.Cast<RandomStatic>().Select(o => new TileInfo(o, (byte)(GridCenter + o.X), (byte)(GridCenter + o.Y), o.Z, o.Data.Height, o.Data.Flags)) : [];

            var union = Enumerable.Union(gTiles, sTiles);

            var sorted = union.OrderBy(o => ((o.GridX * GridSize) + o.GridY) * 2);

            sorted = sorted.ThenBy(o => o.GridZ);
            sorted = sorted.ThenByDescending(o => o.Flags.HasFlag(TileFlag.Background) || o.Flags.HasFlag(TileFlag.Surface) || o.Flags.HasFlag(TileFlag.Wall));
            sorted = sorted.ThenBy(o => o.Flags.HasFlag(TileFlag.Roof) || o.Flags.HasFlag(TileFlag.Foliage));
            sorted = sorted.ThenBy(o => o.GridH);

            return sorted;
        }

        private void CreateTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var clickLoc = createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay.PointToClient(Cursor.Position);

                GridEntry clicked = null;
                RandomStatic selected = null;

                foreach (var tile in EnumerateTiles(false, true))
                {
                    if (tile.Source is RandomStatic staticTile)
                    {
                        var image = Art.GetStatic(staticTile.TileID);

                        if (image != null)
                        {
                            var gridEntry = _staticGrid[tile.GridY, tile.GridX];

                            var location = gridEntry.ClientCenter;

                            location.Offset(image.Width / -2, -(image.Height + (tile.GridZ * 2)) + LAND_OFFSET);

                            var px = clickLoc.X - location.X;
                            var py = clickLoc.Y - location.Y;

                            if (px >= 0 && px < image.Width && py >= 0 && py < image.Height && image.GetPixel(px, py).A > 0)
                            {
                                clicked = gridEntry;
                                selected = staticTile;
                            }
                        }
                    }
                }

                if (clicked == null)
                {
                    foreach (var entry in _staticGrid)
                    {
                        if (!entry.Contains(clickLoc))
                        {
                            continue;
                        }

                        clicked = entry;

                        break;
                    }
                }

                if (selected != null)
                {
                    staticPlacement_tabControl_tabPage_entryCompnentList_listBox_individualStaticList.SelectedItem = selected;

                    staticPlacement_tabControl.SelectTab(staticPlacement_tabControl_tabPage_entryCompnentList);
                }
                else
                {
                    staticPlacement_tabControl_tabPage_entryCompnentList_listBox_individualStaticList.SelectedIndex = -1;
                }

                if (_canvasControls?.UpdateAxis(clicked?.GridOffsetX, clicked?.GridOffsetY, selected?.Z ?? 0) == true)
                {
                    createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay.Invalidate();
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                staticPlacement_tabControl_tabPage_entryCompnentList_listBox_individualStaticList.SelectedIndex = -1;

                createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay.Invalidate();
            }
        }

        private void createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay_Paint(object sender, PaintEventArgs e)
        {
            Image terrainImage = null;

            if (createTerrainTypes_tabControl_tabPage_ConfigureTerrain_label_baseTerrain_comboBox.SelectedItem is ClsTerrain terrain)
            {
                terrainImage = Art.GetLand(terrain.TileID);
            }

            foreach (var tile in EnumerateTiles(true, true))
            {
                if (tile.Source is GridEntry gridEntry)
                {
                    if (terrainImage != null)
                    {
                        e.Graphics.DrawImage(terrainImage, gridEntry.ClientLocation);
                    }
                    else
                    {
                        e.Graphics.FillPath(_baseBrush, gridEntry.Path);
                    }

                    e.Graphics.DrawPath(_basePen, gridEntry.Path);

                    if (_canvasControls?.IsDisposed == false)
                    {
                        if (gridEntry.GridOffsetX == _canvasControls.XAxisValue && gridEntry.GridOffsetY == _canvasControls.YAxisValue)
                        {
                            e.Graphics.FillPath(_highlightBrush, gridEntry.Path);
                            e.Graphics.DrawPath(_highlightPen, gridEntry.Path);
                        }
                    }
                }
                else if (tile.Source is RandomStatic staticTile)
                {
                    var image = Art.GetStatic(staticTile.TileID);

                    if (image == null)
                    {
                        continue;
                    }

                    if (staticPlacement_tabControl_tabPage_entryCompnentList_listBox_individualStaticList.SelectedItem == staticTile)
                    {
                        var hue = Hues.GetHue(0x33);

                        if (hue != null)
                        {
                            image = new Bitmap(image)
                            {
                                Tag = hue
                            };

                            hue.ApplyTo(image, false);
                        }
                    }
                    else if (staticTile.Hue > 0)
                    {
                        var hue = Hues.GetHue(staticTile.Hue & 0x3FFF);

                        if (hue != null)
                        {
                            image = new Bitmap(image)
                            {
                                Tag = hue
                            };

                            ref var data = ref TileData.ItemTable[staticTile.TileID];

                            var partial = data.Flags.HasFlag(TileFlag.PartialHue) || (staticTile.Hue & 0x8000) != 0;

                            hue.ApplyTo(image, partial);
                        }
                    }

                    var sp = _staticGrid[tile.GridY, tile.GridX].ClientCenter;

                    sp.Offset(image.Width / -2, -(image.Height + (tile.GridZ * 2)) + LAND_OFFSET);
                    /*
                    if (staticPlacement_tabControl_tabPage_entryCompnentList_listBox_individualStaticList.SelectedItem == staticTile)
                    {
                        using var highlightImage = new Bitmap(image, image.Width + 4, image.Height + 4);

                        var hue = Hues.GetHue(0x33);

                        hue.ApplyTo(highlightImage, false);

                        e.Graphics.DrawImage(highlightImage, sp.X - 4, sp.Y - 4);
                    }
                    */
                    e.Graphics.DrawImage(image, sp);

                    if (image.Tag is Hue)
                    {
                        image.Dispose();
                    }
                }
            }
        }

        private void createTerrainTypes_tabControl_tabPage_ConfigureTerrain_label_baseTerrain_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay.Invalidate();
        }

        /// Static Frequencies
        private void staticPlacement_tabControl_tabPage_staticEntries_label_randomStaticFrequency_numUpDown_ValueChanged(object sender, EventArgs e)
        {
            _randomStatics.Freq = (int)staticPlacement_tabControl_tabPage_staticEntries_label_randomStaticFrequency_numUpDown.Value;
        }

        private void staticPlacement_tabControl_tabPage_staticEntries_label_selectedEntryFrequency_numUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (staticPlacement_tabControl_tabPage_staticEntries_listBox_staticGroupEntryList.SelectedItem is RandomStaticCollection col)
            {
                col.Freq = (int)staticPlacement_tabControl_tabPage_staticEntries_label_selectedEntryFrequency_numUpDown.Value;
            }
        }

        /// Static Group Entries
        private void staticPlacement_tabControl_tabPage_staticEntries_listBox_staticGroupEntryList_SelectedIndexChanged(object sender, EventArgs e)
        {
            staticPlacement_tabControl_tabPage_entryCompnentList_panel_staticPictureBox.Image = null;

            if (staticPlacement_tabControl_tabPage_staticEntries_listBox_staticGroupEntryList.SelectedItem is RandomStaticCollection col)
            {
                staticPlacement_tabControl_tabPage_staticEntries_label_staticEntryDescription_textBox.Text = col.Description;

                staticPlacement_tabControl_tabPage_staticEntries_label_selectedEntryFrequency_numUpDown.Value = col.Freq;

                col.Display(staticPlacement_tabControl_tabPage_entryCompnentList_listBox_individualStaticList);

                createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay.Invalidate();
            }
        }

        private void staticPlacement_tabControl_tabPage_staticEntries_toolStrip_button_addStatics_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(staticPlacement_tabControl_tabPage_staticEntries_label_staticEntryDescription_textBox.Text))
            {
                var freq = (int)staticPlacement_tabControl_tabPage_staticEntries_label_selectedEntryFrequency_numUpDown.Value;

                var col = new RandomStaticCollection(staticPlacement_tabControl_tabPage_staticEntries_label_staticEntryDescription_textBox.Text, freq);

                _randomStatics.Add(col);

                _randomStatics.Display(staticPlacement_tabControl_tabPage_staticEntries_listBox_staticGroupEntryList);

                staticPlacement_tabControl_tabPage_staticEntries_listBox_staticGroupEntryList.SelectedItem = col;

                createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay.Invalidate();
            }
        }

        private void staticPlacement_tabControl_tabPage_staticEntries_toolStrip_button_deleteStatics_Click(object sender, EventArgs e)
        {
            if (staticPlacement_tabControl_tabPage_staticEntries_listBox_staticGroupEntryList.SelectedIndex >= 0)
            {
                var index = staticPlacement_tabControl_tabPage_staticEntries_listBox_staticGroupEntryList.SelectedIndex;

                staticPlacement_tabControl_tabPage_staticEntries_listBox_staticGroupEntryList.SelectedIndex = -1;

                _randomStatics.RemoveAt(index);

                _randomStatics.Display(staticPlacement_tabControl_tabPage_staticEntries_listBox_staticGroupEntryList);

                if (index < staticPlacement_tabControl_tabPage_staticEntries_listBox_staticGroupEntryList.Items.Count)
                {
                    staticPlacement_tabControl_tabPage_staticEntries_listBox_staticGroupEntryList.SelectedIndex = index;
                }
                else
                {
                    staticPlacement_tabControl_tabPage_staticEntries_listBox_staticGroupEntryList.SelectedIndex = staticPlacement_tabControl_tabPage_staticEntries_listBox_staticGroupEntryList.Items.Count - 1;
                }

                createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay.Invalidate();
            }
        }

        private void staticPlacement_tabControl_tabPage_staticEntries_toolStrip_button_refreshStatics_Click(object sender, EventArgs e)
        {
            staticPlacement_tabControl_tabPage_staticEntries_listBox_staticGroupEntryList.SelectedIndex = -1;

            staticPlacement_tabControl_tabPage_staticEntries_listBox_staticGroupEntryList.BeginUpdate();

            staticPlacement_tabControl_tabPage_staticEntries_listBox_staticGroupEntryList.Items.Clear();

            staticPlacement_tabControl_tabPage_staticEntries_listBox_staticGroupEntryList.EndUpdate();
            staticPlacement_tabControl_tabPage_staticEntries_listBox_staticGroupEntryList.Invalidate();

            createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay.Invalidate();
        }

        private void staticPlacement_tabControl_tabPage_entryCompnentList_panel_button_staticSelector_Click(object sender, EventArgs e)
        {
            if (_staticSelector?.IsDisposed != false)
            {
                _staticSelector = new StaticSelector();

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
            var index = staticPlacement_tabControl_tabPage_entryCompnentList_panel_staticPictureBox_vScroll.Value;

            createTerrainTypes_tabControl_tabPage_ConfigureTerrain_label_tileID_textBox.Text = $"{index}";

            staticPlacement_tabControl_tabPage_entryCompnentList_panel_staticPictureBox.Image = Art.GetStatic(index);

            staticPlacement_tabControl_tabPage_staticProperties_propertyGrid.SelectedObject = TileData.ItemTable[index];

            staticPlacement_tabControl_tabPage_entryCompnentList_panel_textBox_staticDescription.Text = $"[{index:D5}] {TileData.ItemTable[index].Name}";
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
            if (staticPlacement_tabControl_tabPage_entryCompnentList_listBox_individualStaticList.SelectedItem is RandomStatic selectedStatic)
            {
                staticPlacement_tabControl_tabPage_entryCompnentList_panel_staticPictureBox_vScroll.Value = selectedStatic.TileID;

                var image = Art.GetStatic(selectedStatic.TileID);

                if (image != null)
                {
                    ref var data = ref TileData.ItemTable[selectedStatic.TileID];

                    staticPlacement_tabControl_tabPage_staticProperties_propertyGrid.SelectedObject = data;

                    if (selectedStatic.Hue > 0)
                    {
                        var hue = Hues.GetHue(selectedStatic.Hue);

                        if (hue != null)
                        {
                            image = new Bitmap(image)
                            {
                                Tag = hue
                            };

                            var partial = data.Flags.HasFlag(TileFlag.PartialHue) || (selectedStatic.Hue & 0x8000) != 0;

                            hue.ApplyTo(image, partial);
                        }
                    }

                    var oldImage = staticPlacement_tabControl_tabPage_entryCompnentList_panel_staticPictureBox.Image;

                    staticPlacement_tabControl_tabPage_entryCompnentList_panel_staticPictureBox.Image = image;

                    if (oldImage?.Tag is Hue)
                    {
                        oldImage.Dispose();
                    }
                }

                createTerrainTypes_tabControl_tabPage_ConfigureTerrain_label_tileID_textBox.Text = $"{selectedStatic.TileID}";
                createTerrainTypes_tabControl_tabPage_ConfigureTerrain_label_hueID_textBox.Text = $"{selectedStatic.Hue}";

                _canvasControls?.UpdateAxis(selectedStatic.X, selectedStatic.Y, selectedStatic.Z);
            }
        }

        private void staticPlacement_tabControl_tabPage_entryCompnentList_toolStrip_button_addStatics_Click(object sender, EventArgs e)
        {
            if (staticPlacement_tabControl_tabPage_staticEntries_listBox_staticGroupEntryList.SelectedItem is RandomStaticCollection col)
            {
                var tileID = Utility.Parse<ushort>(createTerrainTypes_tabControl_tabPage_ConfigureTerrain_label_tileID_textBox.Text);
                var hue = Utility.Parse<ushort>(createTerrainTypes_tabControl_tabPage_ConfigureTerrain_label_hueID_textBox.Text);

                var x = _canvasControls?.XAxisValue ?? 0;
                var y = _canvasControls?.YAxisValue ?? 0;
                var z = _canvasControls?.ZAxisValue ?? 0;

                col.Add(new RandomStatic(tileID, x, y, z, hue));

                col.Display(staticPlacement_tabControl_tabPage_entryCompnentList_listBox_individualStaticList);

                staticPlacement_tabControl_tabPage_entryCompnentList_listBox_individualStaticList.SelectedItem = col;

                createTerrainTypes_tabControl_tabPage_ConfigureTerrain_ScrollGrid();
            }
        }

        private void staticPlacement_tabControl_tabPage_entryCompnentList_toolStrip_button_deleteStatics_Click(object sender, EventArgs e)
        {
            if (staticPlacement_tabControl_tabPage_staticEntries_listBox_staticGroupEntryList.SelectedItem is RandomStaticCollection col)
            {
                if (staticPlacement_tabControl_tabPage_entryCompnentList_listBox_individualStaticList.SelectedIndex >= 0)
                {
                    var index = staticPlacement_tabControl_tabPage_entryCompnentList_listBox_individualStaticList.SelectedIndex;

                    staticPlacement_tabControl_tabPage_entryCompnentList_listBox_individualStaticList.SelectedIndex = -1;

                    col.RemoveAt(index);

                    col.Display(staticPlacement_tabControl_tabPage_entryCompnentList_listBox_individualStaticList);

                    if (index < staticPlacement_tabControl_tabPage_entryCompnentList_listBox_individualStaticList.Items.Count)
                    {
                        staticPlacement_tabControl_tabPage_entryCompnentList_listBox_individualStaticList.SelectedIndex = index;
                    }
                    else
                    {
                        staticPlacement_tabControl_tabPage_entryCompnentList_listBox_individualStaticList.SelectedIndex = staticPlacement_tabControl_tabPage_entryCompnentList_listBox_individualStaticList.Items.Count - 1;
                    }

                    createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay.Invalidate();
                }
            }
        }

        private void staticPlacement_tabControl_tabPage_entryCompnentList_toolStrip_button_refreshStatics_Click(object sender, EventArgs e)
        {
            staticPlacement_tabControl_tabPage_entryCompnentList_listBox_individualStaticList.SelectedIndex = -1;

            staticPlacement_tabControl_tabPage_entryCompnentList_listBox_individualStaticList.BeginUpdate();

            staticPlacement_tabControl_tabPage_entryCompnentList_listBox_individualStaticList.Items.Clear();

            staticPlacement_tabControl_tabPage_entryCompnentList_listBox_individualStaticList.EndUpdate();
            staticPlacement_tabControl_tabPage_entryCompnentList_listBox_individualStaticList.Invalidate();

            createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay.Invalidate();
        }
    }
}
