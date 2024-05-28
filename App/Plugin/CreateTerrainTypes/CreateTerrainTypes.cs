using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Numerics;

using UltimaSDK;

namespace MapCreator
{
	public partial class CreateTerrainTypes : Form
	{
		public const byte LAND_SIZE = 44;
		public const sbyte LAND_OFFSET = LAND_SIZE / 2;

		public const byte GRID_MIN_SIZE = 13;
		public const byte GRID_MAX_SIZE = 255;

		private record class GridEntry : IDisposable
		{
			public GraphicsPath Path { get; }

			public Region Region { get; }

			public Vector3 GridCell { get; }

			public byte GridX => (byte)GridCell.X;
			public byte GridY => (byte)GridCell.Y;
			public sbyte GridZ => (sbyte)GridCell.Z;

			public Point GridOffset { get; }

			public sbyte GridOffsetX => (sbyte)GridOffset.X;
			public sbyte GridOffsetY => (sbyte)GridOffset.Y;
			public sbyte GridOffsetZ => (sbyte)GridCell.Z;

			public Point ClientCenter { get; }

			public int ClientCenterX => ClientCenter.X;
			public int ClientCenterY => ClientCenter.Y;

			public Point ClientLocation { get; }

			public int ClientLocationX => ClientLocation.X;
			public int ClientLocationY => ClientLocation.Y;

			public GridEntry(sbyte gridCenter, byte gridX, byte gridY, sbyte gridZ, int centerX, int centerY)
			{
				GridCell = new Vector3(gridX, gridY, gridZ);

				GridOffset = new Point(gridX - gridCenter, gridY - gridCenter);

				ClientCenter = new Point(centerX, centerY);
				ClientLocation = new Point(centerX - LAND_OFFSET, centerY - LAND_OFFSET);

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

		private byte _gridSize = GRID_MIN_SIZE;

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

				foreach (var tile in staticComponentsView.Items.Cast<RandomStatic>())
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

				GridCenter = (sbyte)((value - 1) / 2);

				PopulateGrid();
			}
		}

		public sbyte GridCenter { get; private set; } = (GRID_MIN_SIZE - 1) / 2;

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

			staticEntriesBindingSource.DataSource = _randomStatics;
		}

		private void PopulateGrid()
		{
			var nSize = GridSize;

			if (_staticGrid != null)
			{
				var oHeight = _staticGrid?.GetLength(0) ?? 0;
				var oWidth = _staticGrid?.GetLength(1) ?? 0;

				if (oWidth == nSize && oHeight == nSize)
				{
					return;
				}

				for (var y = 0; y < oHeight; y++)
				{
					for (var x = 0; x < oWidth; x++)
					{
						_staticGrid[y, x]?.Dispose();
					}
				}
			}

			_staticGrid = new GridEntry[nSize, nSize];

			var px = nSize * LAND_OFFSET;
			int py = LAND_OFFSET;

			byte gx = 0;
			byte gy = 0;
			sbyte gz = 0;

			do
			{
				do
				{
					_staticGrid[gy, gx]?.Dispose();

					_staticGrid[gy, gx] = new GridEntry(GridCenter, gx, gy, gz, px - (gy * LAND_OFFSET), py + (gy * LAND_OFFSET));
				}
				while (++gy < nSize);

				gy = 0;

				px += LAND_OFFSET;
				py += LAND_OFFSET;
			}
			while (++gx < nSize);
		}

		/// Form Load Operations
		private void Initialize()
		{
			_randomStatics.Clear();

			_terrainTable.Clear();
			_terrainTable.Load();

			_terrainTable.Display(terrainBaseValue);

			if (terrainBaseValue.Items.Count > 1)
			{
				var randTerrain = Utility.Random(1, terrainBaseValue.Items.Count);

				terrainBaseValue.SelectedIndex = randTerrain;
			}
			else if (terrainBaseValue.Items.Count > 0)
			{
				terrainBaseValue.SelectedIndex = 0;
			}

			terrainTypeValue.Text = null;

			staticComponentsView.Enabled = false;
			staticComponentsView.SelectedItem = null;
			staticEntriesView.SelectedItem = null;

			staticSelectorValue.Value = 0;

			var landSize = new Size(LAND_SIZE, LAND_SIZE);

			terrainPreviewMarker.SuspendLayout();
			terrainPreviewMarker.MaximumSize = landSize;
			terrainPreviewMarker.MinimumSize = landSize;
			terrainPreviewMarker.Size = landSize;
			terrainPreviewMarker.ResumeLayout(true);

			var gridSize = new Size(LAND_SIZE * GridSize, LAND_SIZE * GridSize);

			terrainPreviewGrid.SuspendLayout();
			terrainPreviewGrid.MaximumSize = gridSize;
			terrainPreviewGrid.MinimumSize = gridSize;
			terrainPreviewGrid.Size = gridSize;
			terrainPreviewGrid.ResumeLayout(true);

			if (_canvasControls?.IsDisposed == false)
			{
				_canvasControls.XAxisMinimum = _canvasControls.YAxisMinimum = (sbyte)-GridCenter;
				_canvasControls.XAxisMaximum = _canvasControls.YAxisMaximum = GridCenter;

				_ = _canvasControls.UpdateAxis(0, 0, 0);
			}
			else
			{
				ScrollGrid();
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

		private void OnTerrainPageEnter(object sender, EventArgs e)
		{
			if (_canvasControls?.IsDisposed != false)
			{
				_canvasControls = new CanvasControlBox();

				_canvasControls.XAxisMinimum = _canvasControls.YAxisMinimum = (sbyte)-GridCenter;
				_canvasControls.XAxisMaximum = _canvasControls.YAxisMaximum = GridCenter;

				_canvasControls.AxisValueChanged += OnAxisValueChanged;
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

		private void OnTerrainPageLeave(object sender, EventArgs e)
		{
			if (_canvasControls?.IsDisposed == false)
			{
				_canvasControls.SendToBack();

				_canvasControls.Visible = false;
			}
		}

		private void OnAxisValueChanged(object sender, EventArgs e)
		{
			if (_canvasControls?.IsDisposed == false)
			{
				if (staticComponentsView.SelectedItem is RandomStatic selectedStatic)
				{
					selectedStatic.X = _canvasControls.XAxisValue;
					selectedStatic.Y = _canvasControls.YAxisValue;
					selectedStatic.Z = _canvasControls.ZAxisValue;
				}
			}

			ScrollGrid();
		}

		private void ScrollGrid()
		{
			terrainPreviewGrid.Invalidate();

			var loc = terrainPreviewGrid.Location;

			if (_canvasControls?.IsDisposed == false)
			{
				loc.Offset(_staticGrid[GridCenter + _canvasControls.YAxisValue, GridCenter + _canvasControls.XAxisValue].ClientLocation);
			}
			else
			{
				loc.Offset(_staticGrid[GridCenter, GridCenter].ClientLocation);
			}

			terrainPreviewMarker.Location = loc;

			terrainPreviewMarker.Refresh();

			terrainPreviewView.ScrollControlIntoView(terrainPreviewMarker);
		}

		/// Form Top Menu Buttons
		private void OnTerrainNewButtonClick(object sender, EventArgs e)
		{
			Initialize();
		}

		private void OnTerrainLoadButtonClick(object sender, EventArgs e)
		{
			_openFileDialog ??= new OpenFileDialog();

			_openFileDialog.Filter = "xml files (*.xml)|*.xml";
			_openFileDialog.InitialDirectory = Path.Combine(Environment.CurrentDirectory, "MapCompiler", "Engine", "TerrainTypes");

			if (_openFileDialog.ShowDialog(this) == DialogResult.OK)
			{
				var fileInfo = new FileInfo(_openFileDialog.FileName);

				terrainTypeValue.Text = fileInfo.Name;

				_randomStatics.Load(fileInfo.Name);

				staticEntriesView.Invalidate();
				//_randomStatics.Display(staticEntriesList);

				terrainPreviewGrid.Invalidate();
			}
		}

		private void OnTerrainSaveButtonClick(object sender, EventArgs e)
		{
			_saveFileDialog ??= new SaveFileDialog();

			_saveFileDialog.Filter = "xml files (*.xml)|*.xml";
			_saveFileDialog.InitialDirectory = Path.Combine(Environment.CurrentDirectory, "MapCompiler", "Engine", "TerrainTypes");
			_saveFileDialog.FileName = terrainTypeValue.Text;

			if (_saveFileDialog.ShowDialog(this) == DialogResult.OK)
			{
				_randomStatics.Save(_saveFileDialog.FileName);
			}
		}

		private void OnFacetBuilderButtonClick(object sender, EventArgs e)
		{
			Hide();

			_ = StaticForm<facetBuilder>.Open();
		}

		private void OnCommunityCreditsButtonClick(object sender, EventArgs e)
		{
			_ = StaticForm<communityCredits>.Open();
		}

		private readonly record struct TileInfo(object Source, byte GridX, byte GridY, sbyte GridZ, byte GridH, TileFlag Flags);

		private IOrderedEnumerable<TileInfo> EnumerateTiles(bool terrain, bool statics)
		{
			var tiles = EnumerateTileInfo(terrain, statics);

			return OrderTileInfo(tiles);
		}

		private IOrderedEnumerable<TileInfo> OrderTileInfo(IEnumerable<TileInfo> tiles)
		{
			var sorted = tiles.OrderBy(o => ((o.GridX * GridSize) + o.GridY) * 2);

			sorted = sorted.ThenBy(o => o.GridZ);
			sorted = sorted.ThenByDescending(o => o.Source is GridEntry);
			sorted = sorted.ThenByDescending(o => o.Flags.HasFlag(TileFlag.Background));
			sorted = sorted.ThenByDescending(o => o.Flags.HasFlag(TileFlag.Surface));
			sorted = sorted.ThenByDescending(o => o.Flags.HasFlag(TileFlag.Wall));
			sorted = sorted.ThenBy(o => o.Flags.HasFlag(TileFlag.Roof));
			sorted = sorted.ThenBy(o => o.Flags.HasFlag(TileFlag.Foliage));
			sorted = sorted.ThenBy(o => o.GridH);

			return sorted;
		}

		private IEnumerable<TileInfo> EnumerateTileInfo(bool terrain, bool statics)
		{
			if (terrain)
			{
				var terrainFlags = TileFlag.None;

				if (terrainBaseValue.SelectedItem is ClsTerrain t)
				{
					terrainFlags = t.Data.Flags;
				}

				foreach (var o in _staticGrid)
				{
					yield return new TileInfo(o, o.GridX, o.GridY, 0, 0, terrainFlags);
				}
			}

			if (statics)
			{
				foreach (var o in staticComponentsView.Items.Cast<RandomStatic>())
				{
					yield return new TileInfo(o, (byte)(GridCenter + o.X), (byte)(GridCenter + o.Y), o.Z, o.Data.Height, o.Data.Flags);
				}
			}
		}

		private void OnTerrainPreviewGridMouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				var clickLoc = e.Location;

				GridEntry clicked = null;
				RandomStatic selected = null;

				foreach (var tile in EnumerateTiles(true, true))
				{
					if (tile.Source is RandomStatic staticTile)
					{
						var image = Art.GetStatic(staticTile.TileID);

						if (image != null)
						{
							var entry = _staticGrid[tile.GridY, tile.GridX];

							var location = entry.ClientCenter;

							location.Offset(image.Width / -2, -(image.Height + (tile.GridZ * 2)) + LAND_OFFSET);

							var px = clickLoc.X - location.X;
							var py = clickLoc.Y - location.Y;

							if (px >= 0 && px < image.Width && py >= 0 && py < image.Height && image.GetPixel(px, py).A > 0)
							{
								clicked = entry;
								selected = staticTile;
							}
						}
					}
					else if (tile.Source is GridEntry entry)
					{
						if (entry.Contains(clickLoc))
						{
							clicked = entry;
							selected = null;
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
				else if (selected == null)
				{
					foreach (var tile in EnumerateTiles(false, true))
					{
						if (tile.GridX == clicked.GridX && tile.GridY == clicked.GridY)
						{
							selected = (RandomStatic)tile.Source;

							break;
						}
					}
				}

				staticComponentsView.SelectedItem = selected;

				if (selected == null)
				{
					staticSelectorValue.Value = 0;

					if (clicked != null)
					{
						_ = (_canvasControls?.UpdateAxis(clicked.GridOffsetX, clicked.GridOffsetY, clicked.GridOffsetZ));
					}
				}
			}
			else if (e.Button == MouseButtons.Right)
			{
				staticComponentsView.SelectedItem = null;
			}
		}

		private void OnTerrainPreviewGridPaint(object sender, PaintEventArgs e)
		{
			Image terrainImage = null;

			if (terrainBaseValue.SelectedItem is ClsTerrain terrain)
			{
				terrainImage = Art.GetLand(terrain.TileID);
			}

			var selectedStatic = staticComponentsView.SelectedItem as RandomStatic;

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

					if (selectedStatic == staticTile)
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

					e.Graphics.DrawImage(image, sp);

					if (image.Tag is Hue)
					{
						image.Dispose();
					}
				}
			}
		}

		private void OnBaseTerrainSelectionChanged(object sender, EventArgs e)
		{
			terrainPreviewGrid.Invalidate();
		}

		private void OnStaticEntriesSelectionChanged(object sender, EventArgs e)
		{
			staticTabs.SuspendLayout();

			if (staticEntriesView.SelectedItem is RandomStaticCollection scol)
			{
				staticComponentsBindingSource.DataSource = null;
				staticComponentsBindingSource.DataSource = scol;

				staticComponentsView.Enabled = true;
			}
			else
			{
				staticComponentsBindingSource.DataSource = null;

				staticComponentsView.Enabled = false;
			}

			staticTabs.ResumeLayout(true);

			terrainPreviewGrid.Invalidate();
		}

		private void OnStaticEntriesRowsChanged(object sender, EventArgs e)
		{
			terrainPreviewGrid.Invalidate();
		}

		private void OnStaticComponentsSelectionChanged(object sender, EventArgs e)
		{
			staticTabs.SuspendLayout();

			if (staticComponentsView.SelectedItem is RandomStatic selectedStatic)
			{
				staticSelectorValue.Value = selectedStatic.TileID;

				_ = (_canvasControls?.UpdateAxis(selectedStatic.X, selectedStatic.Y, selectedStatic.Z));
			}
			else
			{
				staticSelectorValue.Value = 0;

				_ = (_canvasControls?.UpdateAxis(0, 0, 0));
			}

			staticTabs.ResumeLayout(true);

			terrainPreviewGrid.Invalidate();
		}

		private void OnStaticComponentsRowsChanged(object sender, EventArgs e)
		{
			terrainPreviewGrid.Invalidate();
		}

		private void OnStaticSelectorButtonClick(object sender, EventArgs e)
		{
			if (_staticSelector?.IsDisposed != false)
			{
				_staticSelector = new StaticSelector();

				_staticSelector.ValueChanged += OnStaticSelectorSelectionChanged;
			}

			if (_staticSelector?.IsDisposed == false)
			{
				_staticSelector.Value = staticSelectorValue.Value;

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

		private void OnStaticSelectorButtonMouseEnter(object sender, EventArgs e)
		{
			staticSelectorButton.ForeColor = Color.LimeGreen;
		}

		private void OnStaticSelectorButtonMouseLeave(object sender, EventArgs e)
		{
			staticSelectorButton.ForeColor = Color.SlateGray;
		}

		private void OnStaticSelectorSelectionChanged(object sender, EventArgs e)
		{
			if (_staticSelector?.IsDisposed == false)
			{
				staticSelectorValue.Value = _staticSelector.Value;
			}
		}

		private void OnStaticSelectorValueChanged(object sender, EventArgs e)
		{
			if (_staticSelector?.IsDisposed == false)
			{
				_staticSelector.Value = staticSelectorValue.Value;
			}

			var tileID = (ushort)staticSelectorValue.Value;

			var image = Art.GetStatic(tileID);

			ref var data = ref TileData.ItemTable[tileID];

			staticPropertiesView.SelectedObject = data;

			var name = data.Name;

			if (String.IsNullOrWhiteSpace(name))
			{
				name = "Static";
			}

			staticSelectorDescription.Text = $"{name} [0x{tileID:X4} | {tileID:D5}]";

			if (staticComponentsView.SelectedItem is RandomStatic selectedStatic)
			{
				selectedStatic.TileID = tileID;

				terrainPreviewGrid.Invalidate();

				if (image != null && selectedStatic.Hue > 0)
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
			}

			var oldImage = staticSelectorPreview.Image;

			staticSelectorPreview.Image = image;

			if (oldImage?.Tag is Hue)
			{
				oldImage.Dispose();
			}
		}
	}
}
