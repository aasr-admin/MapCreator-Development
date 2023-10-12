using Cartography;

using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Xml;

namespace MapCreator
{
	public class Project
	{
		public static Project CurrentProject { get; set; }

		private string _Name;

		public string Name
		{
			get => _Name;
			set
			{
				if (String.IsNullOrWhiteSpace(value))
				{
					value = "Project";
				}

				if (_Name == value)
				{
					return;
				}

				var oldRoot = RootDirectory;

				if (Directory.Exists(oldRoot))
				{
					_Name = value;

					var index = 1;

					while (Directory.Exists(RootDirectory))
					{
						_Name = $"{value}{index++}";
					}

					Directory.Move(oldRoot, RootDirectory);
				}
			}
		}

		public TerrainTable TerrainTable { get; } = new();
		public AltitudeTable AltitudeTable { get; } = new();
		public TransitionTable TransitionTable { get; } = new();

		public Bitmap AltitudeImage { get; private set; }
		public Bitmap TerrainImage { get; private set; }

		public bool RandomStatics { get; set; } = true;

		public string RootDirectory => Path.Combine("Projects", Name);
		public string OutputDirectory => Path.Combine(RootDirectory, "Output");
		public string DataDirectory => Path.Combine(RootDirectory, "Data");

		public string TransitionsDirectory => Path.Combine(DataDirectory, "Transitions");
		public string StructuresDirectory => Path.Combine(DataDirectory, "Structures");

		public Project(string name)
		{
			Name = name;
		}

		public void Load()
		{
			Unload();

			var dataDirectoryPath = DataDirectory;

			_ = Directory.CreateDirectory(dataDirectoryPath);

			var terrainTableFilePath = Path.Combine(dataDirectoryPath, "Terrain.xml");

			TerrainTable.LoadXml(terrainTableFilePath);

			var altitudeTableFilePath = Path.Combine(dataDirectoryPath, "Altitude.xml");

			AltitudeTable.LoadXml(altitudeTableFilePath);

			var transitionsDirectoryPath = TransitionsDirectory;

			_ = Directory.CreateDirectory(transitionsDirectoryPath);

			foreach (var transition in XmlHelper.LoadDirectory<Transition>(transitionsDirectoryPath, "Transition"))
			{
				TransitionTable.Add(transition);
			}
		}

		public void Unload()
		{
			TerrainTable.Reset();
			AltitudeTable.Reset();
			TransitionTable.Clear();
		}

		public void Compile(Facet facet)
		{
			Load();

			var dataDirectoryPath = DataDirectory;

			var terrainMapFilePath = Path.Combine(dataDirectoryPath, $"Facet{facet.Index}", "Terrain.bmp");

			using var terrainImage = new Bitmap(terrainMapFilePath);

			var altitudeMapFilePath = Path.Combine(dataDirectoryPath, $"Facet{facet.Index}", "Altitude.bmp");

			using var altitudeImage = new Bitmap(altitudeMapFilePath);

			var facetMatrix = new FacetMatrix(facet.Width, facet.Height);

			byte[] terrainData;

			var terrainImageData = terrainImage.LockBits(facetMatrix.Bounds, ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);

			try
			{
				terrainData = new byte[facetMatrix.Width * facetMatrix.Height];

				Marshal.Copy(terrainImageData.Scan0, terrainData, 0, terrainData.Length);
			}
			finally
			{
				terrainImage.UnlockBits(terrainImageData);
			}

			byte[] altitudeData;

			var altitudeImageData = altitudeImage.LockBits(facetMatrix.Bounds, ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);

			try
			{
				altitudeData = new byte[facetMatrix.Width * facetMatrix.Height];

				Marshal.Copy(altitudeImageData.Scan0, altitudeData, 0, altitudeData.Length);
			}
			finally
			{
				altitudeImage.UnlockBits(altitudeImageData);
			}

			for (var x = 0; x < facetMatrix.LandMatrix.Width; x++)
			{
				for (var y = 0; y < facetMatrix.LandMatrix.Height; y++)
				{
					var alt = altitudeData[(y * facetMatrix.LandMatrix.Width) + x];

					facetMatrix.LandMatrix.Set(x, y, AltitudeTable[alt].Z);
				}
			}

			var landIndex = 0;
			var landCount = facetMatrix.LandMatrix.Length;

			for (var x = 0; x < facetMatrix.LandMatrix.Width; x++)
			{
				for (var y = 0; y < facetMatrix.LandMatrix.Height; y++)
				{
					sbyte z = 0;

					ref var tile = ref facetMatrix.LandMatrix[x, y];

					var terrianGroup = TerrainTable[tile.Group];

					var landSequence = GetLandSequence(facetMatrix.LandMatrix, x, y);

					var transition = TransitionTable.Find(landSequence);

					var randTile = transition?.RandomLandTile();

					tile.ID = randTile?.ID ?? terrianGroup.TileID;
					tile.Z = randTile?.Z ?? terrianGroup.Z;

					transition?.AddRandomStaticTiles(facetMatrix.StaticMatrix, x, y, z, RandomStatics);

					++landIndex;
				}
			}

			landIndex = 0;
			landCount = facetMatrix.LandMatrix.Length;

			var roughEdge = new RoughEdge();

			for (var x = 0; x < facetMatrix.LandMatrix.Width; x++)
			{
				for (var y = 0; y < facetMatrix.LandMatrix.Height; y++)
				{
					ref var tile = ref facetMatrix.LandMatrix[x, y];

					if (x > 0 && y > 0 && x < facetMatrix.LandMatrix.Width && y < facetMatrix.LandMatrix.Height)
					{
						ref var corner = ref facetMatrix.LandMatrix[x - 1, y - 1];
						ref var left = ref facetMatrix.LandMatrix[x - 1, y];
						ref var top = ref facetMatrix.LandMatrix[x, y - 1];

						tile.Z = roughEdge.Check(corner.ID, left.ID, top.ID);
					}

					ref var terrianGroup = ref TerrainTable[tile.Group];

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
				}
			}

			var structuresDirectoryPath = StructuresDirectory;

			foreach (var cells in XmlHelper.LoadDirectory<Structure>(structuresDirectoryPath, "Structure"))
			{
				foreach (var cell in cells)
				{
					_ = ref facetMatrix.AddStatic(cell.X, cell.Y, cell.Z, cell.ID, cell.Hue);
				}

				cells.Clear();
			}

			var outputDirectoryPath = OutputDirectory;

			try
			{
				var mulPath = Path.Combine(outputDirectoryPath, $"map{facet.Index}.mul");

				//_Logs.LogMessage(mulPath);

				using var mulStream = new FileStream(mulPath, FileMode.Create);
				using var mulWriter = new BinaryWriter(mulStream);

				try
				{
					for (var x = 0; x < facetMatrix.LandMatrix.Width; x += 8)
					{
						for (var y = 0; y < facetMatrix.LandMatrix.Height; y += 8)
						{
							mulWriter.Write(1);

							for (var by = 0; by < 8; by++)
							{
								for (var bx = 0; bx < 8; bx++)
								{
									ref var tile = ref facetMatrix.LandMatrix[x + bx, y + by];

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

				var staIdxPath = Path.Combine(outputDirectoryPath, $"StaIdx{facet.Index}.mul");

				//_Logs.LogMessage(staIdxPath);

				using var staIdxFile = new FileStream(staIdxPath, FileMode.Create);
				using var staIdxWriter = new BinaryWriter(staIdxFile);

				var staticsPath = Path.Combine(outputDirectoryPath, $"Statics{facet.Index}.mul");

				//_Logs.LogMessage(staticsPath);

				using var staticsStream = new MemoryStream();
				using var staticsWriter = new BinaryWriter(staticsStream);

				try
				{
					var staticsPosition = 0;

					for (var x = 0; x < facetMatrix.StaticMatrix.Width; x++)
					{
						for (var y = 0; y < facetMatrix.StaticMatrix.Height; y++)
						{
							var length = 0;

							ref var tiles = ref facetMatrix.StaticMatrix[x, y];

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
				//_Logs.EndTask();
				//_Logs.LogTimeStamp();
				//_Logs.LogMessage("Done.");
			}
		}

		public Bitmap CreateTerrainBitmap(Facet facet)
		{
			var bitmap = new Bitmap(facet.Width, facet.Height, PixelFormat.Format8bppIndexed)
			{
				Palette = TerrainTable.GetPalette()
			};

			var bd = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);

			try
			{
				var matrix = new byte[bd.Width * bd.Height];

				Array.Fill(matrix, (byte)9); // default terrain index

				Marshal.Copy(matrix, 0, bd.Scan0, matrix.Length);
			}
			finally
			{
				bitmap.UnlockBits(bd);
			}

			return bitmap;
		}

		public Bitmap CreateAltitudeBitmap(Facet facet)
		{
			var bitmap = new Bitmap(facet.Width, facet.Height, PixelFormat.Format8bppIndexed)
			{
				Palette = AltitudeTable.GetPalette()
			};

			var bd = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);

			try
			{
				var matrix = new byte[bd.Width * bd.Height];

				Array.Fill(matrix, (byte)66); // default altitude index

				Marshal.Copy(matrix, 0, bd.Scan0, matrix.Length);
			}
			finally
			{
				bitmap.UnlockBits(bd);
			}

			return bitmap;
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
	}
}