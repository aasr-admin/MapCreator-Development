using Assets;

using Cartography;

using Microsoft.VisualBasic.CompilerServices;

using System.Collections;

namespace MapCreator.userPlugin
{
	public partial class CreateTerrainTypes : Form
	{
		private readonly ArtData _ArtData = new();
		private readonly TileData _TileData = new();
		private readonly HueData _HueData = new();

		private readonly Point[,] StaticGrid;
		private readonly TerrainTable iTerrain;
		private StaticCellRandomizer iRandomStatic;

		private CommunityCredits _communityCredits;
		private CanvasControlBox _canvasControlBox;
		private CreateTerrainTypes _createTerrainTypes;
		private FacetBuilder _facetBuilder;
		private StaticSelector _staticSelector;

		private OpenFileDialog _openFileDialog;
		private SaveFileDialog _saveFileDialog;

		public CreateTerrainTypes()
		{
			MaximizeBox = false;
			MinimizeBox = false;

			StaticGrid = new Point[13, 13];
			iTerrain = new TerrainTable();
			iRandomStatic = new StaticCellRandomizer();

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

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			//iTerrain.LoadXml();

			createTerrainTypes_tabControl_tabPage_ConfigureTerrain_label_baseTerrain_comboBox.Fill(iTerrain);
		}

		private void createTerrainTypes_tabControl_tabPage_ConfigureTerrain_Enter(object sender, EventArgs e)
		{
			if (_canvasControlBox?.IsDisposed != false)
			{
				_canvasControlBox = new CanvasControlBox();

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

			_canvasControlBox.Show();
		}

		private void createTerrainTypes_tabControl_tabPage_ConfigureTerrain_Leave(object sender, EventArgs e)
		{
			_canvasControlBox?.Hide();
		}

		private void createTerrainTypes_tabControl_tabPage_ConfigureTerrain_ActionChangeX(object sender, EventArgs e)
		{
			createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay.Refresh();
		}

		private void createTerrainTypes_tabControl_tabPage_ConfigureTerrain_ActionChangeY(object sender, EventArgs e)
		{
			createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay.Refresh();
		}

		private void createTerrainTypes_tabControl_tabPage_ConfigureTerrain_ActionChangeZ(object sender, EventArgs e)
		{
			createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay.Refresh();
		}

		private void createTerrainTypes_tabControl_tabPage_ConfigureTerrain_ActionNorth(object sender, EventArgs e)
		{
			var x = Convert.ToByte(_canvasControlBox.xAxis_label_numUpDown.Value);
			var y = Convert.ToByte(_canvasControlBox.yAxis_label_numUpDown.Value);

			if (staticPlacement_tabControl_tabPage_entryCompnentList_listBox_individualStaticList.SelectedItem is StaticCell selectedItem)
			{
				x = selectedItem.X;
				y = selectedItem.Y;

				var tag = _canvasControlBox.NorthButton.Tag;

				if (ObjectType.ObjTst(tag, 2, false) == 0)
				{
					--y;
				}

				selectedItem.X = x;
				selectedItem.Y = y;
			}

			_canvasControlBox.xAxis_label_numUpDown.Value = x;
			_canvasControlBox.yAxis_label_numUpDown.Value = y;

			createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay.Refresh();
		}

		private void createTerrainTypes_tabControl_tabPage_ConfigureTerrain_ActionNorthEast(object sender, EventArgs e)
		{
			var x = Convert.ToByte(_canvasControlBox.xAxis_label_numUpDown.Value);
			var y = Convert.ToByte(_canvasControlBox.yAxis_label_numUpDown.Value);

			if (staticPlacement_tabControl_tabPage_entryCompnentList_listBox_individualStaticList.SelectedItem is StaticCell selectedItem)
			{
				x = selectedItem.X;
				y = selectedItem.Y;

				var tag = _canvasControlBox.NorthEastButton.Tag;

				if (ObjectType.ObjTst(tag, 3, false) == 0)
				{
					++x;
					--y;
				}

				selectedItem.X = x;
				selectedItem.Y = y;
			}

			_canvasControlBox.xAxis_label_numUpDown.Value = x;
			_canvasControlBox.yAxis_label_numUpDown.Value = y;

			createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay.Refresh();
		}

		private void createTerrainTypes_tabControl_tabPage_ConfigureTerrain_ActionEast(object sender, EventArgs e)
		{
			var x = Convert.ToByte(_canvasControlBox.xAxis_label_numUpDown.Value);
			var y = Convert.ToByte(_canvasControlBox.yAxis_label_numUpDown.Value);

			if (staticPlacement_tabControl_tabPage_entryCompnentList_listBox_individualStaticList.SelectedItem is StaticCell selectedItem)
			{
				x = selectedItem.X;
				y = selectedItem.Y;

				var tag = _canvasControlBox.EastButton.Tag;

				if (ObjectType.ObjTst(tag, 6, false) == 0)
				{
					++x;
				}

				selectedItem.X = x;
				selectedItem.Y = y;
			}

			_canvasControlBox.xAxis_label_numUpDown.Value = x;
			_canvasControlBox.yAxis_label_numUpDown.Value = y;

			createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay.Refresh();
		}

		private void createTerrainTypes_tabControl_tabPage_ConfigureTerrain_ActionSouthEast(object sender, EventArgs e)
		{
			var x = Convert.ToByte(_canvasControlBox.xAxis_label_numUpDown.Value);
			var y = Convert.ToByte(_canvasControlBox.yAxis_label_numUpDown.Value);

			if (staticPlacement_tabControl_tabPage_entryCompnentList_listBox_individualStaticList.SelectedItem is StaticCell selectedItem)
			{
				x = selectedItem.X;
				y = selectedItem.Y;

				var tag = _canvasControlBox.SouthEastButton.Tag;

				if (ObjectType.ObjTst(tag, 9, false) == 0)
				{
					++x;
					++y;
				}

				selectedItem.X = x;
				selectedItem.Y = y;
			}

			_canvasControlBox.xAxis_label_numUpDown.Value = x;
			_canvasControlBox.yAxis_label_numUpDown.Value = y;

			createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay.Refresh();
		}

		private void createTerrainTypes_tabControl_tabPage_ConfigureTerrain_ActionSouth(object sender, EventArgs e)
		{
			var x = Convert.ToByte(_canvasControlBox.xAxis_label_numUpDown.Value);
			var y = Convert.ToByte(_canvasControlBox.yAxis_label_numUpDown.Value);

			if (staticPlacement_tabControl_tabPage_entryCompnentList_listBox_individualStaticList.SelectedItem is StaticCell selectedItem)
			{
				x = selectedItem.X;
				y = selectedItem.Y;

				var tag = _canvasControlBox.SouthButton.Tag;

				if (ObjectType.ObjTst(tag, 8, false) == 0)
				{
					++y;
				}

				selectedItem.X = x;
				selectedItem.Y = y;
			}

			_canvasControlBox.xAxis_label_numUpDown.Value = x;
			_canvasControlBox.yAxis_label_numUpDown.Value = y;

			createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay.Refresh();
		}

		private void createTerrainTypes_tabControl_tabPage_ConfigureTerrain_ActionSouthWest(object sender, EventArgs e)
		{
			var x = Convert.ToByte(_canvasControlBox.xAxis_label_numUpDown.Value);
			var y = Convert.ToByte(_canvasControlBox.yAxis_label_numUpDown.Value);

			if (staticPlacement_tabControl_tabPage_entryCompnentList_listBox_individualStaticList.SelectedItem is StaticCell selectedItem)
			{
				x = selectedItem.X;
				y = selectedItem.Y;

				var tag = _canvasControlBox.SouthWestButton.Tag;

				if (ObjectType.ObjTst(tag, 7, false) == 0)
				{
					--x;
					++y;
				}

				selectedItem.X = x;
				selectedItem.Y = y;
			}

			_canvasControlBox.xAxis_label_numUpDown.Value = x;
			_canvasControlBox.yAxis_label_numUpDown.Value = y;

			createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay.Refresh();
		}

		private void createTerrainTypes_tabControl_tabPage_ConfigureTerrain_ActionWest(object sender, EventArgs e)
		{
			var x = Convert.ToByte(_canvasControlBox.xAxis_label_numUpDown.Value);
			var y = Convert.ToByte(_canvasControlBox.yAxis_label_numUpDown.Value);

			if (staticPlacement_tabControl_tabPage_entryCompnentList_listBox_individualStaticList.SelectedItem is StaticCell selectedItem)
			{
				x = selectedItem.X;
				y = selectedItem.Y;


				var tag = _canvasControlBox.WestButton.Tag;

				if (ObjectType.ObjTst(tag, 4, false) == 0)
				{
					--x;
				}

				selectedItem.X = x;
				selectedItem.Y = y;
			}

			_canvasControlBox.xAxis_label_numUpDown.Value = x;
			_canvasControlBox.yAxis_label_numUpDown.Value = y;

			createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay.Refresh();
		}

		private void createTerrainTypes_tabControl_tabPage_ConfigureTerrain_ActionNorthWest(object sender, EventArgs e)
		{
			var x = Convert.ToByte(_canvasControlBox.xAxis_label_numUpDown.Value);
			var y = Convert.ToByte(_canvasControlBox.yAxis_label_numUpDown.Value);

			if (staticPlacement_tabControl_tabPage_entryCompnentList_listBox_individualStaticList.SelectedItem is StaticCell selectedItem)
			{
				x = selectedItem.X;
				y = selectedItem.Y;

				var tag = _canvasControlBox.NorthWestButton.Tag;

				if (ObjectType.ObjTst(tag, 1, false) == 0)
				{
					--x;
					--y;
				}

				selectedItem.X = x;
				selectedItem.Y = y;
			}

			_canvasControlBox.xAxis_label_numUpDown.Value = x;
			_canvasControlBox.yAxis_label_numUpDown.Value = y;

			createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay.Refresh();
		}

		/// Form Top Menu Buttons
		private void createTerrainTypes_mainMenu_button_newTerrainType_Click(object sender, EventArgs e)
		{
			_createTerrainTypes ??= new CreateTerrainTypes();
			_createTerrainTypes.Show();

			Hide();
		}

		private void createTerrainTypes_mainMenu_button_loadTerrainType_Click(object sender, EventArgs e)
		{
			_openFileDialog ??= new OpenFileDialog();

			_openFileDialog.InitialDirectory = String.Format("{0}MapCompiler/Engine/TerrainTypes", AppDomain.CurrentDomain.BaseDirectory);
			/// {0}Data/Statics
			_openFileDialog.Filter = "xml files (*.xml)|*.xml";
			_openFileDialog.FilterIndex = 2;
			_openFileDialog.RestoreDirectory = true;

			if (_openFileDialog.ShowDialog() == DialogResult.OK)
			{
				createTerrainTypes_tabControl_tabPage_ConfigureTerrain_label_terrainType_textBox.Text = _openFileDialog.FileName;

				iRandomStatic = new StaticCellRandomizer();
				iRandomStatic.LoadXml(_openFileDialog.FileName);

				staticPlacement_tabControl_tabPage_staticEntries_listBox_staticGroupEntryList.Fill(iRandomStatic);

				createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay.Refresh();
			}
		}

		private void createTerrainTypes_mainMenu_button_saveTerrainType_Click(object sender, EventArgs e)
		{
			_saveFileDialog ??= new SaveFileDialog();

			_saveFileDialog.InitialDirectory = String.Format("{0}MapCompiler/Engine/TerrainTypes", AppDomain.CurrentDomain.BaseDirectory);
			/// {0}Data/Statics
			_saveFileDialog.Filter = "xml files (*.xml)|*.xml";
			_saveFileDialog.FileName = createTerrainTypes_tabControl_tabPage_ConfigureTerrain_label_terrainType_textBox.Text;
			_saveFileDialog.FilterIndex = 2;
			_saveFileDialog.RestoreDirectory = true;

			if (_saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				iRandomStatic.SaveXml(_saveFileDialog.FileName);
			}
		}

		private void createTerrainTypes_mainMenu_button_facetBuilder_Click(object sender, EventArgs e)
		{
			_facetBuilder ??= new FacetBuilder();
			_facetBuilder.Show();

			Hide();
		}

		private void createTerrainTypes_mainMenu_button_communityCredits_Click(object sender, EventArgs e)
		{
			_communityCredits ??= new CommunityCredits();
			_communityCredits.Show();
		}

		/// Terrain Grid Display (Connects To canvasControlBox Lines 195 and 196)
		private void createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay_Paint(object sender, PaintEventArgs e)
		{
			IEnumerator enumerator = null;
			_ = e.Graphics;
			var pen = new Pen(Color.Gray);

			if (createTerrainTypes_tabControl_tabPage_ConfigureTerrain_label_baseTerrain_comboBox.SelectedItem is Terrain selectedItem)
			{
				var num = 0;

				do
				{
					var num1 = 0;

					do
					{
						var num2 = num1;
						var num3 = num;

						e.Graphics.DrawImage(_ArtData.GetLand(selectedItem.TileID), checked(StaticGrid[num2, num3].X - 22), checked(StaticGrid[num2, num3].Y - 22));

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

				var num4 = Convert.ToInt32(Decimal.Add(new decimal(6L), /*this.Yaxis.Value*/0)); // Yaxis
				var num5 = Convert.ToInt32(Decimal.Add(new decimal(6L), /*this.Xaxis.Value*/0)); // Xaxis

				e.Graphics.DrawLine(pen, checked(StaticGrid[num4, num5].X - 22), StaticGrid[num4, num5].Y, StaticGrid[num4, num5].X, checked(StaticGrid[num4, num5].Y + 22));
				e.Graphics.DrawLine(pen, StaticGrid[num4, num5].X, checked(StaticGrid[num4, num5].Y + 22), checked(StaticGrid[num4, num5].X + 22), StaticGrid[num4, num5].Y);
				e.Graphics.DrawLine(pen, checked(StaticGrid[num4, num5].X + 22), StaticGrid[num4, num5].Y, StaticGrid[num4, num5].X, checked(StaticGrid[num4, num5].Y - 22));
				e.Graphics.DrawLine(pen, StaticGrid[num4, num5].X, checked(StaticGrid[num4, num5].Y - 22), checked(StaticGrid[num4, num5].X - 22), StaticGrid[num4, num5].Y);

				try
				{
					enumerator = staticPlacement_tabControl_tabPage_entryCompnentList_listBox_individualStaticList.Items.GetEnumerator();

					while (enumerator.MoveNext())
					{
						var current = (StaticCell)enumerator.Current;
						var y = checked(6 + current.Y);
						var x = checked(6 + current.X);
						var @static = _ArtData.GetStatic(current.ID);
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
			}
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
			iRandomStatic.Weight = Convert.ToByte(staticPlacement_tabControl_tabPage_staticEntries_label_randomStaticFrequency_numUpDown.Value);
		}

		private void staticPlacement_tabControl_tabPage_staticEntries_label_selectedEntryFrequency_numUpDown_ValueChanged(object sender, EventArgs e)
		{
			if (staticPlacement_tabControl_tabPage_staticEntries_listBox_staticGroupEntryList.SelectedItem is RandomStaticCells selectedItem)
			{
				selectedItem.Weight = Convert.ToByte(staticPlacement_tabControl_tabPage_staticEntries_label_selectedEntryFrequency_numUpDown.Value);
			}
		}

		/// Static Group Entries
		private void staticPlacement_tabControl_tabPage_staticEntries_listBox_staticGroupEntryList_SelectedIndexChanged(object sender, EventArgs e)
		{
			staticPlacement_tabControl_tabPage_entryCompnentList_panel_staticPictureBox.Image = null;

			if (staticPlacement_tabControl_tabPage_staticEntries_listBox_staticGroupEntryList.SelectedItem is RandomStaticCells selectedItem)
			{
				staticPlacement_tabControl_tabPage_staticEntries_label_staticEntryDescription_textBox.Text = selectedItem.Name;
				staticPlacement_tabControl_tabPage_staticEntries_label_selectedEntryFrequency_numUpDown.Value = new decimal(selectedItem.Weight);

				staticPlacement_tabControl_tabPage_entryCompnentList_listBox_individualStaticList.Fill(selectedItem);

				createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay.Refresh();
			}
		}

		private void staticPlacement_tabControl_tabPage_staticEntries_toolStrip_button_addStatics_Click(object sender, EventArgs e)
		{
			if (StringType.StrCmp(staticPlacement_tabControl_tabPage_staticEntries_label_staticEntryDescription_textBox.Text, String.Empty, false) == 0)
			{
				return;
			}

			iRandomStatic.Add(new RandomStaticCells(staticPlacement_tabControl_tabPage_staticEntries_label_staticEntryDescription_textBox.Text, Convert.ToByte(staticPlacement_tabControl_tabPage_staticEntries_label_selectedEntryFrequency_numUpDown.Value)));
			
			staticPlacement_tabControl_tabPage_staticEntries_listBox_staticGroupEntryList.Fill(iRandomStatic);

			createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay.Refresh();
		}

		private void staticPlacement_tabControl_tabPage_staticEntries_toolStrip_button_deleteStatics_Click(object sender, EventArgs e)
		{
			if (staticPlacement_tabControl_tabPage_staticEntries_listBox_staticGroupEntryList.SelectedItem is RandomStaticCells selectedItem)
			{
				iRandomStatic.Remove(selectedItem);
			}

			staticPlacement_tabControl_tabPage_staticEntries_listBox_staticGroupEntryList.Fill(iRandomStatic);

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
			_staticSelector ??= new StaticSelector();
			_staticSelector.Tag = staticPlacement_tabControl_tabPage_entryCompnentList_panel_staticPictureBox_vScroll;
			_staticSelector.Show();
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
			var image = _ArtData.GetStatic(staticPlacement_tabControl_tabPage_entryCompnentList_panel_staticPictureBox_vScroll.Value);

			if (image != null)
			{
				createTerrainTypes_tabControl_tabPage_ConfigureTerrain_label_tileID_textBox.Text = staticPlacement_tabControl_tabPage_entryCompnentList_panel_staticPictureBox_vScroll.Value.ToString();
				staticPlacement_tabControl_tabPage_entryCompnentList_panel_staticPictureBox.Image = image;
				staticPlacement_tabControl_tabPage_staticProperties_propertyGrid.SelectedObject = _TileData.ItemTable[staticPlacement_tabControl_tabPage_entryCompnentList_panel_staticPictureBox_vScroll.Value];
				staticPlacement_tabControl_tabPage_entryCompnentList_panel_textBox_staticDescription.Text = String.Format("{0} ({1})", _TileData.ItemTable[staticPlacement_tabControl_tabPage_entryCompnentList_panel_staticPictureBox_vScroll.Value].Name, staticPlacement_tabControl_tabPage_entryCompnentList_panel_staticPictureBox_vScroll.Value);
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
			if (staticPlacement_tabControl_tabPage_entryCompnentList_listBox_individualStaticList.SelectedItem is StaticCell randomStatic)
			{
				staticPlacement_tabControl_tabPage_entryCompnentList_panel_staticPictureBox_vScroll.Value = randomStatic.ID;

				if (_ArtData.GetStatic(randomStatic.ID) != null)
				{
					staticPlacement_tabControl_tabPage_entryCompnentList_panel_staticPictureBox.Image = _ArtData.GetStatic(randomStatic.ID);
					staticPlacement_tabControl_tabPage_staticProperties_propertyGrid.SelectedObject = _TileData.ItemTable[randomStatic.ID];
				}

				createTerrainTypes_tabControl_tabPage_ConfigureTerrain_label_tileID_textBox.Text = Convert.ToString(randomStatic.ID);

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
			if (staticPlacement_tabControl_tabPage_staticEntries_listBox_staticGroupEntryList.SelectedItem is RandomStaticCells selectedItem)
			{
				var tileIDString = createTerrainTypes_tabControl_tabPage_ConfigureTerrain_label_tileID_textBox.Text;

				if (String.IsNullOrWhiteSpace(tileIDString))
				{
					tileIDString = "0";
				}

				var hueIDString = createTerrainTypes_tabControl_tabPage_ConfigureTerrain_label_hueID_textBox.Text;

				if (String.IsNullOrWhiteSpace(hueIDString))
				{
					hueIDString = "0";
				}

				var tile = new StaticCell()
				{
					ID = UInt16.Parse(tileIDString),
					X = Convert.ToByte(_canvasControlBox?.xAxis_label_numUpDown.Value ?? 0),
					Y = Convert.ToByte(_canvasControlBox?.yAxis_label_numUpDown.Value ?? 0),
					Z = Convert.ToSByte(_canvasControlBox?.zAxis_label_numUpDown.Value ?? 0),
					Hue = UInt16.Parse(hueIDString)
				};

				selectedItem.Add(tile);

				staticPlacement_tabControl_tabPage_entryCompnentList_listBox_individualStaticList.Fill(selectedItem);

				createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay.Refresh();
			}
		}

		private void staticPlacement_tabControl_tabPage_entryCompnentList_toolStrip_button_deleteStatics_Click(object sender, EventArgs e)
		{
			if (staticPlacement_tabControl_tabPage_staticEntries_listBox_staticGroupEntryList.SelectedItem is RandomStaticCells selectedItem)
			{
				if (staticPlacement_tabControl_tabPage_entryCompnentList_listBox_individualStaticList.SelectedItem is StaticCell tile)
				{
					selectedItem.Remove(tile);

					staticPlacement_tabControl_tabPage_entryCompnentList_listBox_individualStaticList.Fill(selectedItem);

					createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay.Refresh();
				}
			}
		}

		private void staticPlacement_tabControl_tabPage_entryCompnentList_toolStrip_button_refreshStatics_Click(object sender, EventArgs e)
		{
			staticPlacement_tabControl_tabPage_entryCompnentList_listBox_individualStaticList.Items.Clear();
			createTerrainTypes_groupBox_terrainPreview_panel_terrainGridDisplay.Refresh();
		}
	}
}
