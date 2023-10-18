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
		public static HashSet<Project> Projects { get; } = new HashSet<Project>();

		public static string ProjectsDirectory => Path.Combine(Environment.CurrentDirectory, "Projects");

		static Project()
		{
			RefreshProjects();
		}

		public static void RefreshProjects()
		{
			HashSet<string>? existing = null;

			Projects.RemoveWhere(project =>
			{
				if (!File.Exists(project.ProjectFile))
				{
					project.Unload();

					return true;
				}

				existing ??= new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);
				existing.Add(project.ProjectFile);

				return false;
			});

			if (Directory.Exists(ProjectsDirectory))
			{
				foreach (var filePath in Directory.EnumerateFiles(ProjectsDirectory, "*.mcproj", SearchOption.AllDirectories))
				{
					if (existing?.Contains(filePath) != true)
					{
						_ = Projects.Add(new Project(filePath));
					}
				}
			}
		}

		public static bool Exists(string projectFilePath)
		{
			return Find(projectFilePath) != null;
		}

		public static Project? Find(string projectFilePath)
		{
			return Projects.FirstOrDefault(project => String.Equals(project.ProjectFile, projectFilePath, StringComparison.InvariantCultureIgnoreCase));
		}

		public static bool Delete(string projectFilePath)
		{
			var rootDirectoryPath = Path.GetDirectoryName(projectFilePath);

			if (!Directory.Exists(rootDirectoryPath))
			{
				return false;
			}

			if (File.Exists(projectFilePath))
			{
				File.Delete(projectFilePath);
			}

			var dataDirectoryPath = Path.Combine(rootDirectoryPath, "Data");

			if (Directory.Exists(dataDirectoryPath))
			{
				Directory.Delete(dataDirectoryPath, true);
			}

			var outputDirectoryPath = Path.Combine(rootDirectoryPath, "Output");

			if (Directory.Exists(outputDirectoryPath))
			{
				Directory.Delete(outputDirectoryPath, true);
			}

			if (!Directory.EnumerateFiles(rootDirectoryPath, "*", SearchOption.AllDirectories).Any())
			{
				Directory.Delete(rootDirectoryPath, true);
			}

			return true;
		}

		public static Project OpenOrCreate(string projectFilePath)
		{
			return OpenOrCreate(projectFilePath, out _);
		}

		public static Project OpenOrCreate(string projectFilePath, out bool created)
		{
			created = false;

			var project = Find(projectFilePath);

			if (project == null)
			{
				project = new Project(projectFilePath);

				created = true;
			}

			if (!File.Exists(project.ProjectFile))
			{
				project.Save();
			}
			else if (!project.Loaded)
			{
				project.Load();
			}

			return project;
		}

		public static string GetFilePath(string name)
		{
			return Path.Combine(ProjectsDirectory, name, $"{name}.mcproj");
		}

		public string ProjectFile { get; private set; }

		public string RootDirectory => Path.GetDirectoryName(ProjectFile) ?? Path.Combine(ProjectsDirectory, Name);

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

		private IProgress<ProjectProgressEventArgs> _Progress;

		public Progress<ProjectProgressEventArgs> Progress { get; }

		public Logging Logger { get; } = new();

		public bool Saving { get; private set; }
		public bool Saved { get; private set; }

		public bool Loading { get; private set; }
		public bool Loaded { get; private set; }

		public bool Compiling { get; private set; }
		public bool Exporting { get; private set; }

		public ProjectSettings Settings { get; } = new();

		public Facet Facet { get; } = new();

		public Terrains Terrains { get; } = new();
		public Altitudes Altitudes { get; } = new();
		public Transitions Transitions { get; } = new();
		public Mutations Mutations { get; } = new();
		public Structures Structures { get; } = new();

		public Bitmap? TerrainImage { get; private set; }
		public Bitmap? AltitudeImage { get; private set; }

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

		private Project(string filePath)
		{
			ProjectFile = filePath;

			_Progress = Progress = new Progress<ProjectProgressEventArgs>();

			LoadImages();
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
			XmlHelper.Save(node, nameof(Settings), Settings);
		}

		public bool Save()
		{
			if (Saving)
			{
				return false;
			}

			Saving = true;

			try
			{
				CreateDirectories();

				XmlHelper.Save(ProjectFile, "Project", this);

				var dataDirectoryPath = DataDirectory;

				XmlHelper.Save(Path.Combine(dataDirectoryPath, "Facet.xml"), Facet);
				XmlHelper.Save(Path.Combine(dataDirectoryPath, "Transitions.xml"), Transitions);
				XmlHelper.Save(Path.Combine(dataDirectoryPath, "Mutations.xml"), Mutations);
				XmlHelper.Save(Path.Combine(dataDirectoryPath, "Structures.xml"), Facet);

				SaveImages();

				Loaded = Saved = true;
			}
			finally
			{
				Saving = false;
			}

			return Saved;
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
			_ = XmlHelper.Load(node, nameof(Settings), Settings);
		}

		public bool Load()
		{
			if (Loading)
			{
				return false;
			}

			Loading = true;

			try
			{
				Unload();

				CreateDirectories();

				_ = XmlHelper.Load(ProjectFile, "Project", this);

				var dataDirectoryPath = DataDirectory;

				XmlHelper.Load(Path.Combine(dataDirectoryPath, "Facet.xml"), Facet);
				//XmlHelper.Load(Path.Combine(dataDirectoryPath, "Terrains.xml"), Terrains);
				//XmlHelper.Load(Path.Combine(dataDirectoryPath, "Altitudes.xml"), Altitudes);
				XmlHelper.Load(Path.Combine(dataDirectoryPath, "Transitions.xml"), Transitions);
				XmlHelper.Load(Path.Combine(dataDirectoryPath, "Mutations.xml"), Mutations);
				XmlHelper.Load(Path.Combine(dataDirectoryPath, "Structures.xml"), Facet);

				LoadImages();

				Loaded = true;
			}
			finally
			{
				Loading = false;
			}

			return Loaded;
		}

		public void SaveImages()
		{
			CreateDirectories();

			CreateImages();

			var dataDirectoryPath = DataDirectory;

			if (AltitudeImage != null)
			{
				Altitudes.SaveSwatch(Path.Combine(dataDirectoryPath, "Altitude.aco"), ColorFormat.RGB);

				AltitudeImage.Save(Path.Combine(dataDirectoryPath, "Altitude.bmp"), ImageFormat.Bmp);
			}

			if (TerrainImage != null)
			{
				Terrains.SaveSwatch(Path.Combine(dataDirectoryPath, "Terrain.aco"), ColorFormat.RGB);

				TerrainImage.Save(Path.Combine(dataDirectoryPath, "Terrain.bmp"), ImageFormat.Bmp);
			}
		}

		public void LoadImages()
		{
			CreateDirectories();

			var dataDirectoryPath = DataDirectory;

			var terrainSwatchFilePath = Path.Combine(dataDirectoryPath, "Terrain.aco");
			var terrainImageFilePath = Path.Combine(dataDirectoryPath, "Terrain.bmp");

			TerrainImage?.Dispose();
			TerrainImage = LoadImage(Terrains, terrainSwatchFilePath, terrainImageFilePath);

			var altitudeSwatchFilePath = Path.Combine(dataDirectoryPath, "Altitude.aco");
			var altitudeImageFilePath = Path.Combine(dataDirectoryPath, "Altitude.bmp");

			AltitudeImage?.Dispose();
			AltitudeImage = LoadImage(Altitudes, altitudeSwatchFilePath, altitudeImageFilePath);

			if (Loaded && CreateImages())
			{
				SaveImages();
			}
		}

		public bool CreateImages()
		{
			var created = false;

			if (TerrainImage == null)
			{
				TerrainImage = CreateImage(Terrains, Facet.Width, Facet.Height);
				TerrainImage.Fill(9);

				created = true;
			}

			if (AltitudeImage == null)
			{
				AltitudeImage = CreateImage(Altitudes, Facet.Width, Facet.Height);
				AltitudeImage.Fill(66);

				created = true;
			}

			return created;
		}

		public void Unload()
		{
			UnloadImages();
			UnloadUltima();

			Facet.Clear();
			Terrains.Reset();
			Altitudes.Reset();
			Transitions.Clear();
			Mutations.Clear();

			Loaded = false;
		}

		public void UnloadImages()
		{
			TerrainImage?.Dispose();
			TerrainImage = null!;

			AltitudeImage?.Dispose();
			AltitudeImage = null!;
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

		public bool DeleteFiles()
		{
			return Delete(ProjectFile);
		}

		public bool Compile()
		{
			if (Compiling)
			{
				return false;
			}

			CreateDirectories();

			if (!Loaded)
			{
				Load();

				if (!Loaded)
				{
					return false;
				}
			}

			Compiling = true;

			ReportCompileProgress("Started", 0, 1, LogType.Info);

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

				TerrainImage ??= CreateImage(Terrains, Facet.Width, Facet.Height);

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

				AltitudeImage ??= CreateImage(Altitudes, Facet.Width, Facet.Height);

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
						var altIndex = altitudeData[(y * Facet.Width) + x];

						Facet.SetLand(x, y, (sbyte)(SByte.MinValue + altIndex));
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
						ref var tile = ref Mutations.Mutate(Facet, x, y);

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

				return true;
			}
			catch (Exception e)
			{
				ReportCompileProgress(e.Message, 1, 1, LogType.Error);

				return false;
			}
			finally
			{
				Compiling = false;

				ReportCompileProgress("Finished", 1, 1, LogType.Info);
			}
		}

		public bool ExportMapFiles(bool uop)
		{
			if (Exporting)
			{
				return false;
			}

			CreateDirectories();

			Exporting = true;

			ReportExportProgress("Started", 0, 1, LogType.Info);

			string processName;
			long processIndex, processCount;

			var outputDirectoryPath = OutputDirectory;

			try
			{
				#region Writing Land Blocks

				processName = "Writing Land Blocks";
				processIndex = 0;
				processCount = Facet.Area / 8;

				ReportExportProgress(processName, processIndex, processCount, LogType.Info);

				var mulPath = Path.Combine(outputDirectoryPath, $"map{Facet.Index}.mul");

				Facet.SaveLandMatrix(mulPath, (i, c) => ReportExportProgress(processName, processIndex = i, processCount = c, null));

				ReportExportProgress(processName, processIndex, processCount, LogType.Info);

				#endregion

				#region Writing Static Blocks

				processName = "Writing Static Blocks";
				processIndex = 0;
				processCount = Facet.Area;

				ReportExportProgress(processName, processIndex, processCount, LogType.Info);

				var staIdxPath = Path.Combine(outputDirectoryPath, $"StaIdx{Facet.Index}.mul");
				var staticsPath = Path.Combine(outputDirectoryPath, $"Statics{Facet.Index}.mul");

				Facet.SaveStaticMatrix(staIdxPath, staticsPath, (i, c) => ReportExportProgress(processName, processIndex = i, processCount = c, null));

				ReportExportProgress(processName, processIndex, processCount, LogType.Info);

				#endregion

				return true;
			}
			catch (Exception e)
			{
				ReportExportProgress(e.Message, 1, 1, LogType.Error);

				return false;
			}
			finally
			{
				Exporting = false;

				ReportExportProgress("Finished", 1, 1, LogType.Info);
			}
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
			_Progress.Report(new ProjectProgressEventArgs(this, title, summary, value, limit));
		}

		private void ReportCompileProgress(string summary, long value, long limit, LogType? log)
		{
			ReportProgress("Compile", summary, value, limit, log);
		}

		private void ReportExportProgress(string summary, long value, long limit, LogType? log)
		{
			ReportProgress("Export", summary, value, limit, log);
		}

		private void UltimaLoader(string title, string? root, Action<string> loader)
		{
			var ultimaPath = Settings.UltimaDirectory;

			if (!Directory.Exists(ultimaPath))
			{
				if (!Directory.Exists(root))
				{
					ReportProgress(title, "Ultima Directory Not Found", 1, 1, LogType.Warn);
					return;
				}

				(root, ultimaPath) = (ultimaPath, root);
			}

			if (ultimaPath == null)
			{
				ReportProgress(title, "Ultima Directory Not Found", 1, 1, LogType.Warn);
				return;
			}

			if (ultimaPath == root)
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

		private static Bitmap? LoadImage<T>(T table, string swatchPath, string imagePath) where T : IColorCollection
		{
			Bitmap? image = null;

			var swatchLoaded = false;

			if (File.Exists(swatchPath))
			{
				swatchLoaded = table.LoadSwatch(swatchPath);
			}

			if (File.Exists(imagePath))
			{
				image = new Bitmap(imagePath);

				if (swatchLoaded)
				{
					table.FillPallette(image.Palette);
				}
				else
				{
					for (var i = 0; i < table.Length; i++)
					{
						table.SetColor(i, image.Palette.Entries[i]);
					}
				}
			}

			return image;
		}

		private static Bitmap CreateImage<T>(T table, int width, int height) where T : IColorCollection
		{
			var bitmap = new Bitmap(width, height, PixelFormat.Format8bppIndexed);

			table.FillPallette(bitmap.Palette);

			return bitmap;
		}
	}

	public class ProjectEventArgs : EventArgs
	{
		public Project? Project { get; }

		public ProjectEventArgs(Project? project)
		{
			Project = project;
		}
	}

	public class ProjectProgressEventArgs : ProjectEventArgs
	{
		public string Title { get; }
		public string Summary { get; }

		public long Value { get; }
		public long Limit { get; }

		public bool IsComplete => Value >= Limit;

		public ProjectProgressEventArgs(Project project, string title, string summary, long value, long limit)
			: base(project)
		{
			Title = title;
			Summary = summary;
			Value = value;
			Limit = limit;
		}
	}
}