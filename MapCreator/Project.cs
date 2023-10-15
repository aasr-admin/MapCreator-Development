using Assets;

using Cartography;

using Photoshop;

using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Xml;

namespace MapCreator
{
	public sealed class Project : IXmlEntry
	{
		public static Project? CurrentProject { get; set; }

		public static HashSet<Project> Projects { get; } = new HashSet<Project>();

		public static string ProjectsDirectory => Path.Combine(Environment.CurrentDirectory, "Projects");

		static Project()
		{
			foreach (var filePath in Directory.EnumerateFiles(ProjectsDirectory, "*.mcproj", SearchOption.AllDirectories))
			{
				_ = Projects.Add(new Project(filePath));
			}
		}

		public static Project OpenOrCreate(string name)
		{
			var rootDirectoryPath = Path.Combine(ProjectsDirectory, name);

			_ = Directory.CreateDirectory(rootDirectoryPath);

			var projectFilePath = Path.Combine(rootDirectoryPath, $"{name}.mcproj");

			var project = new Project(projectFilePath);

			if (!File.Exists(project.ProjectFile))
			{
				project.Save();
			}
			else
			{
				project.Load();
			}

			return project;
		}

		public string ProjectFile { get; private set; }

		public string RootDirectory => Path.GetDirectoryName(ProjectFile) ?? Path.Combine(ProjectsDirectory, Path.GetFileNameWithoutExtension(ProjectFile));
		
		public string DataDirectory => Path.Combine(RootDirectory, "Data");
		public string OutputDirectory => Path.Combine(RootDirectory, "Output");

		public string Name
		{
			get => Path.GetFileNameWithoutExtension(ProjectFile);
			set
			{
				if (String.IsNullOrWhiteSpace(value))
				{
					value = "Project";
				}
				else
				{
					foreach (var c in Path.GetInvalidFileNameChars())
					{
						value = value.Replace(c, '_');
					}

					value = value.Trim();
					value = value.Trim('_');
					value = value.Trim();
				}

				var newPath = Path.Combine(RootDirectory, $"{value}.mcproj");

				if (ProjectFile != newPath)
				{
					if (File.Exists(newPath))
					{
						File.Move(newPath, $"{newPath}.bak");
					}

					if (File.Exists(ProjectFile))
					{
						File.Move(ProjectFile, newPath, true);
					}

					ProjectFile = newPath;
				}
			}
		}

		private IProgress<ProgressUpdateEventArgs> _Progress;

		public Progress<ProgressUpdateEventArgs> Progress { get; }

		public Logging Logger { get; } = new();

		public bool Loaded { get; private set; }
		public bool Compiling { get; private set; }

		public ProjectSettings Settings { get; } = new();

		public Facet Facet { get; } = new();
		public Terrains Terrains { get; } = new();
		public Altitudes Altitudes { get; } = new();
		public Transitions Transitions { get; } = new();
		public EdgeMutator Mutator { get; } = new();
		public Structures Structures { get; } = new();

		public Bitmap? TerrainImage { get; private set; }
		public Bitmap? AltitudeImage { get; private set; }

		public Graphics? TerrainGraphics { get; private set; }
		public Graphics? AltitudeGraphics { get; private set; }

		#region Ultima Data

		private ArtData? _UltimaArt;

		public ArtData UltimaArt
		{
			get
			{
				_UltimaArt ??= new ArtData();

				UltimaLoader("Art Data", _UltimaArt.Directory, _UltimaArt.Load);

				return _UltimaArt;
			}
		}

		private GumpData? _UltimaGumps;

		public GumpData UltimaGumps
		{
			get
			{
				_UltimaGumps ??= new GumpData();

				UltimaLoader("Gump Data", _UltimaGumps.Directory, _UltimaGumps.Load);

				return _UltimaGumps;
			}
		}

		private HueData? _UltimaHues;

		public HueData UltimaHues
		{
			get
			{
				_UltimaHues ??= new HueData();

				UltimaLoader("Hue Data", _UltimaHues.Directory, _UltimaHues.Load);

				return _UltimaHues;
			}
		}

		private ClilocData? _UltimaClilocs;

		public ClilocData UltimaClilocs
		{
			get
			{
				_UltimaClilocs ??= new ClilocData();

				UltimaLoader("Cliloc Data", _UltimaClilocs.Directory, _UltimaClilocs.Load);

				return _UltimaClilocs;
			}
		}

		private TileData? _UltimaTiles;

		public TileData UltimaTiles
		{
			get
			{
				_UltimaTiles ??= new TileData();

				UltimaLoader("Tile Data", _UltimaTiles.Directory, _UltimaTiles.Load);

				return _UltimaTiles;
			}
		}

		private RadarData? _UltimaRadar;

		public RadarData UltimaRadar
		{
			get
			{
				_UltimaRadar ??= new RadarData();

				UltimaLoader("Radar Data", _UltimaRadar.Directory, _UltimaRadar.Load);

				return _UltimaRadar;
			}
		}

		#endregion

		public Project(string filePath)
		{
			ProjectFile = filePath;

			_Progress = Progress = new Progress<ProgressUpdateEventArgs>();
		}

		public override string ToString()
		{
			return Name;
		}

		public void SaveXml(string filePath)
		{
			XmlHelper.Save(filePath, "Project", this);
		}

		public void Save(XmlDocument doc)
		{
			XmlHelper.Save(doc, "Project", this);
		}

		public void Save(XmlElement node)
		{
			Settings.Save(node);
		}

		public bool LoadXml(string filePath)
		{
			return XmlHelper.Load(filePath, "Project", this);
		}

		public bool Load(XmlDocument doc)
		{
			return XmlHelper.Load(doc, "Project", this);
		}

		public void Load(XmlElement node)
		{
			Settings.Load(node);
		}

		public void Save()
		{
			CreateDirectories();

			XmlHelper.Save(ProjectFile, "Project", this);

			var dataDirectoryPath = DataDirectory;

			Facet.SaveXml(Path.Combine(dataDirectoryPath, "Facet.xml"));
			Terrains.SaveXml(Path.Combine(dataDirectoryPath, "Terrain.xml"));
			Altitudes.SaveXml(Path.Combine(dataDirectoryPath, "Altitude.xml"));
			Transitions.SaveXml(Path.Combine(dataDirectoryPath, "Transitions.xml"));
			Mutator.SaveXml(Path.Combine(dataDirectoryPath, "Mutator.xml"));
			Structures.SaveXml(Path.Combine(dataDirectoryPath, "Structures.xml"));

			TerrainImage?.Save(Path.Combine(dataDirectoryPath, "Terrain.bmp"));
			AltitudeImage?.Save(Path.Combine(dataDirectoryPath, "Altitude.bmp"));

			Loaded = true;
		}

		public void Load()
		{
			Unload();

			CreateDirectories();

			_ = XmlHelper.Load(ProjectFile, "Project", this);

			var dataDirectoryPath = DataDirectory;

			_ = Facet.LoadXml(Path.Combine(dataDirectoryPath, "Facet.xml"));
			_ = Terrains.LoadXml(Path.Combine(dataDirectoryPath, "Terrain.xml"));
			_ = Altitudes.LoadXml(Path.Combine(dataDirectoryPath, "Altitude.xml"));
			_ = Transitions.LoadXml(Path.Combine(dataDirectoryPath, "Transitions.xml"));
			_ = Mutator.LoadXml(Path.Combine(dataDirectoryPath, "Mutator.xml"));
			_ = Structures.LoadXml(Path.Combine(dataDirectoryPath, "Structures.xml"));

			var terrainImageFilePath = Path.Combine(dataDirectoryPath, "Terrain.bmp");

			if (File.Exists(terrainImageFilePath))
			{
				TerrainImage = new Bitmap(terrainImageFilePath);
				TerrainGraphics = Graphics.FromImage(TerrainImage);
			}
			else
			{
				TerrainImage = CreateImage(Terrains);
				TerrainGraphics = Graphics.FromImage(TerrainImage);

				using var brush = new SolidBrush(Terrains[9].Color);

				TerrainGraphics.FillRectangle(brush, Facet.Bounds);

				TerrainImage.Save(terrainImageFilePath, ImageFormat.Bmp);
			}

			var altitudeImageFilePath = Path.Combine(dataDirectoryPath, "Altitude.bmp");

			if (File.Exists(altitudeImageFilePath))
			{
				AltitudeImage = new Bitmap(altitudeImageFilePath);
				AltitudeGraphics = Graphics.FromImage(AltitudeImage);
			}
			else
			{
				AltitudeImage = CreateImage(Altitudes);
				AltitudeGraphics = Graphics.FromImage(AltitudeImage);

				using var brush = new SolidBrush(Altitudes[66].Color);

				AltitudeGraphics.FillRectangle(brush, Facet.Bounds);

				AltitudeImage.Save(altitudeImageFilePath, ImageFormat.Bmp);
			}

			Loaded = true;
		}

		public void Unload()
		{
			UnloadUltima();

			Facet.Clear();
			Terrains.Reset();
			Altitudes.Reset();
			Transitions.Clear();
			Mutator.Clear();

			TerrainGraphics?.Dispose();
			TerrainGraphics = null;

			AltitudeGraphics?.Dispose();
			AltitudeGraphics = null;

			TerrainImage?.Dispose();
			TerrainImage = null;

			AltitudeImage?.Dispose();
			AltitudeImage = null;

			Loaded = false;
		}

		public void UnloadUltima()
		{
			_UltimaArt?.Clear();
			_UltimaArt = null;

			_UltimaGumps?.Clear();
			_UltimaGumps = null;

			_UltimaHues?.Clear();
			_UltimaHues = null;

			_UltimaClilocs?.Clear();
			_UltimaClilocs = null;

			_UltimaTiles?.Clear();
			_UltimaTiles = null;

			_UltimaRadar?.Clear();
			_UltimaRadar = null;
		}

		public void CreateDirectories()
		{
			_ = Directory.CreateDirectory(RootDirectory);
			_ = Directory.CreateDirectory(DataDirectory);
			_ = Directory.CreateDirectory(OutputDirectory);
		}

		public void Compile()
		{
			if (Compiling)
			{
				return;
			}

			CreateDirectories();

			ReportCompileProgress("Started", 0, 1, LogType.Info);

			if (!Loaded)
			{
				Load();
			}

			string processName;
			long processIndex, processCount;

			var outputDirectoryPath = OutputDirectory;

			try
			{
				#region Preparing Terrain

				processName = "Preparing Terrain";
				processIndex = 0;
				processCount = 3;

				ReportCompileProgress(processName, processIndex, processCount, LogType.Info);

				byte[] terrainData;

				TerrainImage ??= CreateImage(Terrains);

				var terrainImageData = TerrainImage.LockBits(Facet.Bounds, ImageLockMode.ReadWrite, TerrainImage.PixelFormat);

				try
				{
					terrainData = new byte[Facet.Area];

					Marshal.Copy(terrainImageData.Scan0, terrainData, 0, terrainData.Length);
				}
				finally
				{
					TerrainImage.UnlockBits(terrainImageData);
				}

				ReportCompileProgress(processName, ++processIndex, processCount, null);

				byte[] altitudeData;

				AltitudeImage ??= CreateImage(Altitudes);

				var altitudeImageData = AltitudeImage.LockBits(Facet.Bounds, ImageLockMode.ReadWrite, AltitudeImage.PixelFormat);

				try
				{
					altitudeData = new byte[Facet.Area];

					Marshal.Copy(altitudeImageData.Scan0, altitudeData, 0, altitudeData.Length);
				}
				finally
				{
					AltitudeImage.UnlockBits(altitudeImageData);
				}

				ReportCompileProgress(processName, ++processIndex, processCount, null);

				for (var x = 0; x < Facet.Width; x++)
				{
					for (var y = 0; y < Facet.Height; y++)
					{
						var alt = altitudeData[(y * Facet.Width) + x];

						Facet.SetLand(x, y, Altitudes[alt].Z);
					}
				}

				ReportCompileProgress(processName, ++processIndex, processCount, LogType.Info);

				#endregion

				#region Applying Terrain Transitions

				processName = "Applying Terrain Transitions";
				processIndex = 0;
				processCount = Facet.Area;

				ReportCompileProgress(processName, processIndex, processCount, LogType.Info);

				for (var x = 0; x < Facet.Width; x++)
				{
					for (var y = 0; y < Facet.Height; y++)
					{
						sbyte z = 0;

						ref var tile = ref Facet.GetLand(x, y);

						var sequence = Facet.GetLandSequence(x, y);

						var transition = Transitions.Find(sequence);

						transition?.Apply(Facet, x, y, z, Settings.RandomStatics);

						if (++processIndex < processCount)
						{
							ReportCompileProgress(processName, processIndex, processCount, null);
						}
					}
				}

				ReportCompileProgress(processName, processIndex, processCount, LogType.Info);

				#endregion

				#region Applying Terrain Mutations

				processName = "Applying Terrain Mutations";
				processIndex = 0;
				processCount = Facet.Area;

				ReportCompileProgress(processName, processIndex, processCount, LogType.Info);

				for (var x = 0; x < Facet.Width; x++)
				{
					for (var y = 0; y < Facet.Height; y++)
					{
						ref var tile = ref Mutator.Mutate(Facet, x, y);

						ref var terrianGroup = ref Terrains[tile.Group];

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

						if (++processIndex < processCount)
						{
							ReportCompileProgress(processName, processIndex, processCount, null);
						}
					}
				}

				ReportCompileProgress(processName, processIndex, processCount, LogType.Info);

				#endregion

				#region Building Static Structures

				processName = "Building Static Structures";
				processIndex = 0;
				processCount = Structures.Count;

				ReportCompileProgress(processName, processIndex, processCount, LogType.Info);

				foreach (var structure in Structures)
				{
					foreach (var cell in structure)
					{
						_ = ref Facet.AddStatic(cell.X, cell.Y, cell.Z, cell.ID, cell.Hue);
					}

					if (++processIndex < processCount)
					{
						ReportCompileProgress(processName, processIndex, processCount, null);
					}
				}

				ReportCompileProgress(processName, processIndex, processCount, LogType.Info);

				#endregion

				#region Writing Land Blocks

				processName = "Writing Land Blocks";
				processIndex = 0;
				processCount = Facet.Area / 8;

				ReportCompileProgress(processName, processIndex, processCount, LogType.Info);

				var mulPath = Path.Combine(outputDirectoryPath, $"map{Facet.Index}.mul");

				using var mulStream = new FileStream(mulPath, FileMode.Create);
				using var mulWriter = new BinaryWriter(mulStream);

				try
				{
					for (var x = 0; x < Facet.Width; x += 8)
					{
						for (var y = 0; y < Facet.Height; y += 8)
						{
							mulWriter.Write(1);

							for (var by = 0; by < 8; by++)
							{
								for (var bx = 0; bx < 8; bx++)
								{
									ref var tile = ref Facet.GetLand(x + bx, y + by);

									mulWriter.Write(tile.ID);
									mulWriter.Write(tile.Z);
								}
							}

							if (++processIndex < processCount)
							{
								ReportCompileProgress(processName, processIndex, processCount, null);
							}
						}
					}
				}
				finally
				{
					mulWriter.Flush();
				}

				ReportCompileProgress(processName, processIndex, processCount, LogType.Info);

				#endregion

				#region Writing Static Blocks

				processName = "Writing Static Blocks";
				processIndex = 0;
				processCount = Facet.Area;

				ReportCompileProgress(processName, processIndex, processCount, LogType.Info);

				var staIdxPath = Path.Combine(outputDirectoryPath, $"StaIdx{Facet.Index}.mul");

				using var staIdxFile = new FileStream(staIdxPath, FileMode.Create);
				using var staIdxWriter = new BinaryWriter(staIdxFile);

				var staticsPath = Path.Combine(outputDirectoryPath, $"Statics{Facet.Index}.mul");

				using var staticsStream = new MemoryStream();
				using var staticsWriter = new BinaryWriter(staticsStream);

				try
				{
					var staticsPosition = 0;

					for (var x = 0; x < Facet.Width; x++)
					{
						for (var y = 0; y < Facet.Height; y++)
						{
							var length = 0;

							ref var tiles = ref Facet.GetStatics(x, y);

							for (var i = 0; i < tiles.Length; i++)
							{
								ref var tile = ref tiles[i];

								staticsWriter.Write(tile.ID);
								staticsWriter.Write((byte)tile.X);
								staticsWriter.Write((byte)tile.Y);
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

							if (++processIndex < processCount)
							{
								ReportCompileProgress(processName, processIndex, processCount, null);
							}
						}
					}
				}
				finally
				{
					staIdxWriter.Flush();
					staticsWriter.Flush();
				}

				ReportCompileProgress(processName, processIndex, processCount, LogType.Info);

				#endregion
			}
			catch (Exception e)
			{
				ReportCompileProgress(e.Message, 1, 1, LogType.Error);
			}
			finally
			{
				Compiling = false;
			}

			ReportCompileProgress("Finished", 1, 1, LogType.Info);
		}

		private Bitmap CreateImage<T>(T table) where T : IColorCollection
		{
			var bitmap = new Bitmap(Facet.Width, Facet.Height, PixelFormat.Format8bppIndexed);

			table.FillPallette(bitmap.Palette);

			return bitmap;
		}

		private void ReportProgress(string title, string summary, long value, long limit, LogType? log)
		{
			if (log != null)
			{
				if (value > 0)
				{
					if (limit > 0)
					{
						Logger.Log(log.Value, $"[{title}] {summary} [{value / (double)limit:P1}]");
					}
					else
					{
						Logger.Log(log.Value, $"[{title}] {summary} [{value:N0}]");
					}
				}
				else
				{
					if (limit > 0)
					{
						Logger.Log(log.Value, $"[{title}] {summary} [{limit:N0}]");
					}
					else
					{
						Logger.Log(log.Value, $"[{title}] {summary}");
					}
				}
			}

			_Progress ??= Progress;
			_Progress.Report(new ProgressUpdateEventArgs(this, title, summary, value, limit));
		}

		private void ReportCompileProgress(string summary, long value, long limit, LogType? log)
		{
			ReportProgress("Compile", summary, value, limit, log);
		}

		private void UltimaLoader(string title, string root, Action<string> loader)
		{
			var ultimaPath = Settings.UltimaDirectory;

			if (!Directory.Exists(ultimaPath))
			{
				ReportProgress(title, "Ultima Directory Not Found", 1, 1, LogType.Warn);

				return;
			}

			if (root == ultimaPath)
			{
				return;
			}

			try
			{
				ReportProgress(title, "Loading", 0, 1, LogType.Info);

				loader(ultimaPath);

				ReportProgress(title, "Loaded", 1, 1, LogType.Info);
			}
			catch (Exception e)
			{
				ReportProgress(title, e.Message, 1, 1, LogType.Error);
			}
		}
	}
}