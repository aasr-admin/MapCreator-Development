using Cartography;

using Compiler;

using System.Diagnostics;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Xml;

namespace MapCreator
{
	public partial class FacetBuilder : Form
	{
		private readonly BuildLogger _Logs = new();

		private readonly UserPlugins _UserPlugins = new();
		private readonly colorTables _ColorTables = new();
		private readonly CommunityCredits _CommunityCredits = new();

		private readonly TerrainTable _TerrainTable = new();
		private readonly AltitudeTable _AltitudeTable = new();

		private bool _RandomStatics = true;

		public FacetBuilder()
		{
			InitializeComponent();

			MaximizeBox = false;
			MinimizeBox = false;
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			if (Visible)
			{
				facetBuilder_panel_workbench.Hide();
			}

			_Logs.Show();

			Location = new Point(_Logs.Location.X + 100, _Logs.Location.Y + 100);

			#region Workbench Loading

			facetBuilder_panel_workbench_groupBox_createFacetBitmapFiles_label_projectFolderLocation_textBox.Text = Directory.GetCurrentDirectory();
			facetBuilder_panel_workbench_groupBox_syncYourAltitudeBitmap_label_projectFolderLocation_textBox.Text = Directory.GetCurrentDirectory();
			facetBuilder_panel_workbench_groupBox_compileYourNewFacet_label_projectFolderLocation_textBox.Text = Directory.GetCurrentDirectory();

			var facetsPath = Path.Combine("MapCompiler", "Engine", "Facets.xml");

			try
			{
				var facets = new FacetList();

				facets.LoadXml(facetsPath);

				facetBuilder_panel_workbench_groupBox_createFacetBitmapFiles_label_facetSize_comboBox.Fill(facets);
			}
			catch 
			{
				_Logs.LogMessage($"Unable to find: {facetsPath}");
			}

			//_TerrainTable.LoadXml();
			//_AltitudeTable.LoadXml();

			facetBuilder_panel_workbench_groupBox_createFacetBitmapFiles_label_baseTerrain_comboBox.Fill(_TerrainTable);

			#endregion
		}

		private void facetBuilder_menuStrip_button_gettingStarted_createColorTables_Click(object sender, EventArgs e)
		{
			//Hide();

			//_Logs.Hide();

			_ = _ColorTables.ShowDialog(this);
		}

		private void facetBuilder_menuStrip_button_gettingStarted_mapCreatorManual_Click(object sender, EventArgs e)
		{

		}

		private void facetBuilder_menuStrip_button_drawingTools_adobePhotoshop_Click(object sender, EventArgs e)
		{
			var path = Path.Combine("Development", "DrawingTools", "AdobePhotoshop");

			_ = Directory.CreateDirectory(path);

			_ = Process.Start("explorer.exe", path);
		}

		private void facetBuilder_menuStrip_button_drawingTools_freePaintSoftware_Click(object sender, EventArgs e)
		{
			var path = Path.Combine("Development", "DrawingTools", "FreePaintSoftware");

			_ = Directory.CreateDirectory(path);

			_ = Process.Start("explorer.exe", path);
		}

		private void facetBuilder_menuStrip_button_facetTutorials_Click(object sender, EventArgs e)
		{
			var path = Path.Combine("Development", "FacetTutorials");

			_ = Directory.CreateDirectory(path);

			_ = Process.Start("explorer.exe", path);
		}

		private void facetBuilder_menuStrip_button_userPlugins_Click(object sender, EventArgs e)
		{
			//Hide();

			//_Logs.Hide();

			_ = _UserPlugins.ShowDialog(this);
		}

		private void facetBuilder_menuStrip_button_uploadPlugin_Click(object sender, EventArgs e)
		{

		}

		private void facetBuilder_menuStrip_button_information_Click(object sender, EventArgs e)
		{
			//Hide();

			//_Logs.Hide();

			_ = _CommunityCredits.ShowDialog(this);
		}

		private void facetBuilder_panel_workbench_selection_groupBox_createYourWorld_button_createFacetBitmapFiles_MouseEnter(object sender, EventArgs e)
		{
			facetBuilder_panel_workbench_selection_groupBox_createYourWorld_button_createFacetBitmapFiles.ForeColor = Color.LimeGreen;
		}

		private void facetBuilder_panel_workbench_selection_groupBox_createYourWorld_button_createFacetBitmapFiles_Click(object sender, EventArgs e)
		{
			facetBuilder_panel_workbench.Show();
			facetBuilder_panel_workbench_groupBox_createFacetBitmapFiles.Show();

			facetBuilder_panel_workbench_groupBox_syncYourAltitudeBitmap.Hide();
			facetBuilder_panel_workbench_groupBox_compileYourNewFacet.Hide();
		}

		private void facetBuilder_panel_workbench_selection_groupBox_createYourWorld_button_createFacetBitmapFiles_MouseLeave(object sender, EventArgs e)
		{
			facetBuilder_panel_workbench_selection_groupBox_createYourWorld_button_createFacetBitmapFiles.ForeColor = Color.SlateGray;
		}

		#region GroupBox Functionality

		private void facetBuilder_panel_workbench_groupBox_createFacetBitmapFiles_button_locateProject_Click(object sender, EventArgs e)
		{
			var path = facetBuilder_panel_workbench_groupBox_createFacetBitmapFiles_label_projectFolderLocation_textBox.Text;

			if (String.IsNullOrWhiteSpace(path))
			{
				path = Directory.GetCurrentDirectory();
			}

			var folderBrowserDialog = new FolderBrowserDialog()
			{
				SelectedPath = path
			};

			if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				facetBuilder_panel_workbench_groupBox_createFacetBitmapFiles_label_projectFolderLocation_textBox.Text = folderBrowserDialog.SelectedPath;
			}
		}

		private void facetBuilder_panel_workbench_groupBox_createFacetBitmapFiles_button_createFacetBitmapFiles_Click(object sender, EventArgs e)
		{
			byte terrainIndex, altitudeIndex;

			if (facetBuilder_panel_workbench_groupBox_createFacetBitmapFiles_label_facetSize_comboBox.SelectedItem is not Facet facet)
			{
				_Logs.LogMessage("Error: Select a Map Type.");
			}
			else if (String.IsNullOrWhiteSpace(facetBuilder_panel_workbench_groupBox_createFacetBitmapFiles_label_facetName_textBox.Text))
			{
				_Logs.LogMessage("Error: Enter a project Name.");
			}
			else
			{
				var str = Path.Combine(facetBuilder_panel_workbench_groupBox_createFacetBitmapFiles_label_projectFolderLocation_textBox.Text, facetBuilder_panel_workbench_groupBox_createFacetBitmapFiles_label_facetName_textBox.Text, $"Facet{facet.Index}");

				_ = Directory.CreateDirectory(str);

				if (facetBuilder_panel_workbench_groupBox_createFacetBitmapFiles_label_baseTerrain_comboBox.SelectedItem != null)
				{
					terrainIndex = 0;
					altitudeIndex = (byte)facetBuilder_panel_workbench_groupBox_createFacetBitmapFiles_label_baseTerrain_comboBox.SelectedIndex;
				}
				else
				{
					terrainIndex = 0;
					altitudeIndex = 66;
				}

				_Logs.LogMessage("Creating Terrain Image.");
				_Logs.StartTask();

				try
				{
					try
					{
						var str1 = String.Format("{0}/{1}", str, facetBuilder_panel_workbench_groupBox_createFacetBitmapFiles_label_terrainBitmap_textBox.Text);

						using var terrainImage = MakeTerrainBitmapFile(facet.Width, facet.Height, terrainIndex, facetBuilder_panel_workbench_groupBox_createFacetBitmapFiles_label_addDungeonArea_checkBox.Checked);

						terrainImage.Save(str1, ImageFormat.Bmp);
					}
					catch
					{
						_Logs.LogMessage("Error: Problem creating Terrain Image.");

						return;
					}

					//this.iLogger.EndTask();
					_Logs.LogTimeStamp();
					_Logs.LogMessage("Creating Altitude Image.");
					_Logs.StartTask();

					try
					{
						var str2 = String.Format("{0}/{1}", str, facetBuilder_panel_workbench_groupBox_createFacetBitmapFiles_label_altitudeBitmap_textBox.Text);

						using var altitudeImage = MakeAltitudeBitmapFile(facet.Width, facet.Height, altitudeIndex, facetBuilder_panel_workbench_groupBox_createFacetBitmapFiles_label_addDungeonArea_checkBox.Checked);

						altitudeImage.Save(str2, ImageFormat.Bmp);
					}
					catch 
					{
						_Logs.LogMessage("Error: Problem creating Altitude Image.");

						return;
					}
				}
				finally
				{
					_Logs.EndTask();
					_Logs.LogTimeStamp();
					_Logs.LogMessage("Done.");
				}
			}
		}

		public Bitmap MakeTerrainBitmapFile(int xSize, int ySize, byte DefaultTerrain, bool Dungeon)
		{
			var bitmap = new Bitmap(xSize, ySize, PixelFormat.Format8bppIndexed)
			{
				Palette = _TerrainTable.GetPalette()
			};

			var rectangle = new Rectangle(0, 0, xSize, ySize);
			var bitmapDatum = bitmap.LockBits(rectangle, ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);
			var scan0 = bitmapDatum.Scan0;
			var width = checked(bitmapDatum.Width * bitmapDatum.Height);
			var defaultTerrain = new byte[checked(checked(width - 1) + 1)];

			Marshal.Copy(scan0, defaultTerrain, 0, width);

			if (!Dungeon)
			{
				var num = checked(xSize - 1);

				for (var i = 0; i <= num; i++)
				{
					var num1 = checked(ySize - 1);

					for (var j = 0; j <= num1; j++)
					{
						defaultTerrain[checked(checked(j * xSize) + i)] = DefaultTerrain;
					}
				}
			}
			else
			{
				var num2 = checked(xSize - 1);

				for (var k = 0; k <= num2; k++)
				{
					var num3 = checked(ySize - 1);

					for (var l = 0; l <= num3; l++)
					{
						if (k <= 5119)
						{
							defaultTerrain[checked(checked(l * xSize) + k)] = DefaultTerrain;
						}
						else
						{
							defaultTerrain[checked(checked(l * xSize) + k)] = 19;
						}
					}
				}
			}

			Marshal.Copy(defaultTerrain, 0, scan0, width);
			bitmap.UnlockBits(bitmapDatum);

			return bitmap;
		}

		public Bitmap MakeAltitudeBitmapFile(int width, int height, byte altitude, bool Dungeon)
		{
			var bitmap = new Bitmap(width, height, PixelFormat.Format8bppIndexed)
			{
				Palette = _AltitudeTable.GetPalette()
			};

			var bd = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);

			try
			{
				var matrix = new byte[bd.Width * bd.Height];

				Marshal.Copy(bd.Scan0, matrix, 0, matrix.Length);

				for (var x = 0; x < width; x++)
				{
					for (var y = 0; y < height; y++)
					{
						if (!Dungeon || x <= 5119)
						{
							matrix[(y * width) + x] = altitude;
						}
						else
						{
							matrix[(y * width) + x] = 72;
						}
					}
				}

				Marshal.Copy(matrix, 0, bd.Scan0, matrix.Length);
			}
			finally
			{
				bitmap.UnlockBits(bd);
			}

			return bitmap;
		}

		#endregion

		private void facetBuilder_panel_workbench_selection_groupBox_createYourWorld_button_syncYourAltitudeBitmap_MouseEnter(object sender, EventArgs e)
		{
			facetBuilder_panel_workbench_selection_groupBox_createYourWorld_button_syncYourAltitudeBitmap.ForeColor = Color.LimeGreen;
		}

		private void facetBuilder_panel_workbench_selection_groupBox_createYourWorld_button_syncYourAltitudeBitmap_Click(object sender, EventArgs e)
		{
			/// Show These Controls
			Control fBPW = facetBuilder_panel_workbench;
			Thread.Sleep(25);
			fBPW.Show();

			Control sYAB = facetBuilder_panel_workbench_groupBox_syncYourAltitudeBitmap;
			Thread.Sleep(25);
			sYAB.Show();

			/// Hide These Controls
			Control cFBF = facetBuilder_panel_workbench_groupBox_createFacetBitmapFiles;
			Thread.Sleep(25);
			cFBF.Hide();

			Control cYNF = facetBuilder_panel_workbench_groupBox_compileYourNewFacet;
			Thread.Sleep(25);
			cYNF.Hide();
		}

		private void facetBuilder_panel_workbench_selection_groupBox_createYourWorld_button_syncYourAltitudeBitmap_MouseLeave(object sender, EventArgs e)
		{
			facetBuilder_panel_workbench_selection_groupBox_createYourWorld_button_syncYourAltitudeBitmap.ForeColor = Color.SlateGray;
		}

		#region GroupBox Functionality

		private void facetBuilder_panel_workbench_groupBox_syncYourAltitudeBitmap_button_locateProject_Click(object sender, EventArgs e)
		{
			var folderBrowserDialog = new FolderBrowserDialog()
			{
				SelectedPath = facetBuilder_panel_workbench_groupBox_syncYourAltitudeBitmap_label_projectFolderLocation_textBox.Text
			};
			if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				facetBuilder_panel_workbench_groupBox_syncYourAltitudeBitmap_label_projectFolderLocation_textBox.Text = folderBrowserDialog.SelectedPath;
			}
		}

		private void facetBuilder_panel_workbench_groupBox_syncYourAltitudeBitmap_button_syncAltitudeBitmapFile_Click(object sender, EventArgs e)
		{
			/*
			var progress = new Progress<int>(i => { facetBuilder_panel_workbench_progressBar.Value = Math.Abs(i); }); // TODO: temporary fix, i didn't get why it put -73
			var logger = new Progress<string>(_Logs.LogMessage);
			var resetProgress = new Task(() =>
			{
				Thread.Sleep(1000);
				((IProgress<int>)progress).Report(0);
			});
			await Task.Run(() => EncodeAltitudeBitmapHelper.MakeAltitudeImage(facetBuilder_panel_workbench_groupBox_syncYourAltitudeBitmap_label_projectFolderLocation_textBox.Text, facetBuilder_panel_workbench_groupBox_syncYourAltitudeBitmap_label_terrainBitmap_textBox.Text, facetBuilder_panel_workbench_groupBox_syncYourAltitudeBitmap_label_altitudeBitmap_textBox.Text, _AltitudeTable, _TerrainTable, progress, logger)).ContinueWith(c => resetProgress.Start());
			*/		
		}

		#endregion

		private void facetBuilder_panel_workbench_selection_groupBox_createYourWorld_button_compileYourNewFacet_MouseEnter(object sender, EventArgs e)
		{
			facetBuilder_panel_workbench_selection_groupBox_createYourWorld_button_compileYourNewFacet.ForeColor = Color.LimeGreen;
		}

		private void facetBuilder_panel_workbench_selection_groupBox_createYourWorld_button_compileYourNewFacet_Click(object sender, EventArgs e)
		{
			/// Show These Controls
			Control fBPW = facetBuilder_panel_workbench;
			Thread.Sleep(25);
			fBPW.Show();

			Control cYNF = facetBuilder_panel_workbench_groupBox_compileYourNewFacet;
			Thread.Sleep(25);
			cYNF.Show();

			/// Hide These Controls
			Control cFBF = facetBuilder_panel_workbench_groupBox_createFacetBitmapFiles;
			Thread.Sleep(25);
			cFBF.Hide();

			Control sYAB = facetBuilder_panel_workbench_groupBox_syncYourAltitudeBitmap;
			Thread.Sleep(25);
			sYAB.Hide();
		}

		private void facetBuilder_panel_workbench_selection_groupBox_createYourWorld_button_compileYourNewFacet_MouseLeave(object sender, EventArgs e)
		{
			facetBuilder_panel_workbench_selection_groupBox_createYourWorld_button_compileYourNewFacet.ForeColor = Color.SlateGray;
		}

		#region GroupBox Functionality

		private void facetBuilder_panel_workbench_groupBox_compileYourNewFacet_button_locateProject_Click(object sender, EventArgs e)
		{
			var folderBrowserDialog = new FolderBrowserDialog()
			{
				SelectedPath = facetBuilder_panel_workbench_groupBox_compileYourNewFacet_label_projectFolderLocation_textBox.Text
			};

			if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				facetBuilder_panel_workbench_groupBox_compileYourNewFacet_label_projectFolderLocation_textBox.Text = folderBrowserDialog.SelectedPath;
			}
		}

		private void facetBuilder_panel_workbench_groupBox_compileYourNewFacet_label_toggleFacetStatics_radioButton_on_CheckedChanged(object sender, EventArgs e)
		{
			_RandomStatics = true;
			/// Form NotificationAlertOn = new NotificationAlertOn();
			/// NotificationAlertOn.Show();
			System.Media.SystemSounds.Beep.Play();
		}

		private void facetBuilder_panel_workbench_groupBox_compileYourNewFacet_label_toggleFacetStatics_radioButton_off_CheckedChanged(object sender, EventArgs e)
		{
			_RandomStatics = false;
			/// Form NotificationAlertOff = new NotificationAlertOff();
			/// NotificationAlertOff.Show();
			System.Media.SystemSounds.Beep.Play();
		}

		private void facetBuilder_panel_workbench_groupBox_compileYourNewFacet_button_createFacetFiles_Click(object sender, EventArgs e)
		{
			/*
			if (Interaction.MsgBox("You are about to create the Mul Files\r\nAre you sure ?", MsgBoxStyle.YesNo, "Make UO Map") == MsgBoxResult.Yes)
			{
				var mc_MapMake = this;
				///CompileYourNewMap uOMapMake = this;
				///
				new Thread(new ThreadStart(CreateFacet_mul_Files)).Start();
			}
			*/
		}

		private void CreateFacet_mul_Files()
		{
			_Logs.StartTask();
			_Logs.LogMessage("Loading Terrain Image...");

			// Reading Terrain.bmp (Bitmap File)
			var terrainPath = Path.Combine(facetBuilder_panel_workbench_groupBox_compileYourNewFacet_label_projectFolderLocation_textBox.Text, facetBuilder_panel_workbench_groupBox_compileYourNewFacet_label_terrainBitmap_textBox.Text);

			_Logs.LogMessage(terrainPath);

			using var terrainImage = new Bitmap(terrainPath);

			_Logs.LogMessage("Loading Altitude Image...");

			// Reading Altitude.bmp (Bitmap File)
			var altitudePath = Path.Combine(facetBuilder_panel_workbench_groupBox_compileYourNewFacet_label_projectFolderLocation_textBox.Text, facetBuilder_panel_workbench_groupBox_compileYourNewFacet_label_altitudeBitmap_textBox.Text);

			_Logs.LogMessage(altitudePath);

			using var altitudeImage = new Bitmap(altitudePath);

			_Logs.EndTask();
			_Logs.LogTimeStamp();

			_Logs.LogMessage("Preparing Image Files...");
			_Logs.StartTask();

			var matrix = new FacetMatrix(terrainImage.Width, terrainImage.Height);

			byte[] terrainData;

			var terrainImageData = terrainImage.LockBits(matrix.Bounds, ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);

			try
			{
				terrainData = new byte[matrix.Width * matrix.Height];

				Marshal.Copy(terrainImageData.Scan0, terrainData, 0, terrainData.Length);
			}
			finally
			{
				terrainImage.UnlockBits(terrainImageData);
			}

			byte[] altitudeData;

			var altitudeImageData = altitudeImage.LockBits(matrix.Bounds, ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);

			try
			{
				altitudeData = new byte[matrix.Width * matrix.Height];

				Marshal.Copy(altitudeImageData.Scan0, altitudeData, 0, altitudeData.Length);
			}
			finally
			{
				altitudeImage.UnlockBits(altitudeImageData);
			}

			_Logs.LogTimeStamp();
			_Logs.LogMessage("Creating Master Terrian Table...");
			_Logs.StartTask();

			var altitudeTable = new AltitudeTable();

			//altitudeTable.LoadXml();

			for (var x = 0; x < matrix.LandMatrix.Width; x++)
			{
				for (var y = 0; y < matrix.LandMatrix.Height; y++)
				{
					var alt = altitudeData[(y * matrix.LandMatrix.Width) + x];

					matrix.LandMatrix.Set(x, y, altitudeTable[alt].Z);
				}
			}

			_Logs.LogTimeStamp();

			_Logs.LogMessage("Load Transition Tables.");
			_Logs.StartTask();

			var baseDirectory = Path.Combine("MapCompiler", "Engine", "Transitions");

			if (!Directory.Exists(baseDirectory))
			{
				_Logs.LogMessage("Unable to find Transition Data files in the following path: ");
				_Logs.LogMessage(baseDirectory);

				return;
			}

			var transitionTable = new TransitionTable();

			foreach (var trans in XmlHelper.LoadDirectory<Transition>(baseDirectory, "Transitions"))
			{
				transitionTable.Add(trans);
			}

			_Logs.LogTimeStamp();
			_Logs.LogMessage("Preparing Static Tables");

			_Logs.LogMessage("Applying Transition Tables.");
			_Logs.StartTask();

			var terrainTable = new TerrainTable();

			//terrainTable.LoadXml();

			var landIndex = 0;
			var landCount = matrix.LandMatrix.Length;

			Invoke(() => facetBuilder_panel_workbench_progressBar.Maximum = landCount);

			for (var x = 0; x < matrix.LandMatrix.Width; x++)
			{
				for (var y = 0; y < matrix.LandMatrix.Height; y++)
				{
					sbyte z = 0;

					ref var tile = ref matrix.LandMatrix[x, y];

					var terrianGroup = terrainTable[tile.Group];

					var landSequence = GetLandSequence(matrix.LandMatrix, x, y);

					var transition = transitionTable.Find(landSequence);

					var randTile = transition?.RandomLandTile();

					tile.ID = randTile?.ID ?? terrianGroup.TileID;
					tile.Z = randTile?.Z ?? terrianGroup.Z;

					transition?.AddRandomStaticTiles(matrix.StaticMatrix, x, y, z, _RandomStatics);

					++landIndex;

					Invoke(() => facetBuilder_panel_workbench_progressBar.Value = landIndex);
				}
			}

			_Logs.LogTimeStamp();
			_Logs.LogMessage("Second Pass.");
			_Logs.StartTask();

			landIndex = 0;
			landCount = matrix.LandMatrix.Length;

			Invoke(() => facetBuilder_panel_workbench_progressBar.Maximum = landCount);

			var roughEdge = new RoughEdge();

			for (var x = 0; x < matrix.LandMatrix.Width; x++)
			{
				for (var y = 0; y < matrix.LandMatrix.Height; y++)
				{
					ref var tile = ref matrix.LandMatrix[x, y];

					if (x > 0 && y > 0 && x < matrix.LandMatrix.Width && y < matrix.LandMatrix.Height)
					{
						ref var corner = ref matrix.LandMatrix[x - 1, y - 1];
						ref var left = ref matrix.LandMatrix[x - 1, y];
						ref var top = ref matrix.LandMatrix[x, y - 1];

						tile.Z = roughEdge.Check(corner.ID, left.ID, top.ID);
					}

					ref var terrianGroup = ref terrainTable[tile.Group];

					if (terrianGroup.RandomZ)
					{
						switch (Utility.Random(100))
						{
							case >= 0 and < 10: tile.Z -= 4; break;
							case >= 10 and < 20: tile.Z -= 2; break;
							case >= 20 and < 80: break;
							case >= 80 and < 90: tile.Z += 2; break;
							case >= 90 and < 100: tile.Z += 4; break;
						}
					}

					++landIndex;

					Invoke(() => facetBuilder_panel_workbench_progressBar.Value = landIndex);
				}
			}

			_Logs.LogTimeStamp();

			var facetIndex = 0;

			_Logs.LogMessage("\r\n");
			_Logs.LogMessage("Import Tiles...");
			_Logs.StartTask();

			var directoryPath = Path.Combine(facetBuilder_panel_workbench_groupBox_compileYourNewFacet_label_projectFolderLocation_textBox.Text);

			_ = Directory.CreateDirectory(directoryPath);

			try
			{
				foreach (var filePath in Directory.EnumerateFiles(directoryPath, "*.xml", SearchOption.AllDirectories))
				{
					matrix.StaticMatrix.LoadXml(filePath);
				}

				_Logs.LogTimeStamp();
			}
			finally
			{
				_Logs.EndTask();
				_Logs.LogTimeStamp();
				_Logs.LogMessage("Done.");
			}

			_Logs.LogMessage("\r\n");
			_Logs.LogMessage("Write Mul Files...");
			_Logs.StartTask();

			try
			{
				var mulPath = Path.Combine(directoryPath, $"map{facetIndex}.mul");

				_Logs.LogMessage(mulPath);

				using var mulStream = new FileStream(mulPath, FileMode.Create);
				using var mulWriter = new BinaryWriter(mulStream);

				try
				{
					for (var x = 0; x < matrix.LandMatrix.Width; x += 8)
					{
						for (var y = 0; y < matrix.LandMatrix.Height; y += 8)
						{
							mulWriter.Write(1);

							for (var by = 0; by < 8; by++)
							{
								for (var bx = 0; bx < 8; bx++)
								{
									ref var tile = ref matrix.LandMatrix[x + bx, y + by];

									mulWriter.Write(tile.ID);
									mulWriter.Write(tile.Z);
								}
							}
						}
					}
				}
				finally
				{
					mulWriter.Flush();
				}

				var staIdxPath = Path.Combine(directoryPath, $"StaIdx{facetIndex}.mul");

				_Logs.LogMessage(staIdxPath);

				using var staIdxFile = new FileStream(staIdxPath, FileMode.Create);
				using var staIdxWriter = new BinaryWriter(staIdxFile);

				var staticsPath = Path.Combine(directoryPath, $"Statics{facetIndex}.mul");

				_Logs.LogMessage(staticsPath);

				using var staticsStream = new MemoryStream();
				using var staticsWriter = new BinaryWriter(staticsStream);

				try
				{
					var staticsPosition = 0;

					for (var x = 0; x < matrix.StaticMatrix.Width; x++)
					{
						for (var y = 0; y < matrix.StaticMatrix.Height; y++)
						{
							var length = 0;

							ref var tiles = ref matrix.StaticMatrix[x, y];

							for (var i = 0; i < tiles.Length; i++)
							{
								ref var tile = ref tiles[i];

								staticsWriter.Write(tile.ID);
								staticsWriter.Write(tile.X);
								staticsWriter.Write(tile.Y);
								staticsWriter.Write(tile.Z);
								staticsWriter.Write(tile.Hue);

								length += 7;
							}

							if (length > 0)
							{
								staIdxWriter.Write(staticsPosition);

								staticsPosition += length;
							}
							else
							{
								staIdxWriter.Write(-1);
							}

							staIdxWriter.Write(length);
							staIdxWriter.Write(1);
						}
					}
				}
				finally
				{
					staIdxWriter.Flush();
					staticsWriter.Flush();
				}
			}
			finally
			{
				_Logs.EndTask();
				_Logs.LogTimeStamp();
				_Logs.LogMessage("Done.");
			}
		}

		private static IEnumerable<LandCell> GetLandSequence(LandMatrix m, int x, int y)
		{
			yield return m[x - 1, y - 1];
			yield return m[x, y - 1];
			yield return m[x + 1, y - 1];
			yield return m[x - 1, y];
			yield return m[x, y];
			yield return m[x + 1, y];
			yield return m[x - 1, y + 1];
			yield return m[x, y + 1];
			yield return m[x + 1, y + 1];
		}

		#endregion

		private void facetBuilder_FormClosed(object sender, FormClosedEventArgs e)
		{
			Application.Exit();
		}
	}
}