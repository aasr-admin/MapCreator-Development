namespace Assets
{
	public static class AssetData
	{
		public static ClilocData Clilocs { get; } = new();
		public static ArtData Art { get; } = new();
		public static GumpData Gumps { get; } = new();
		public static HueData Hues { get; } = new();
		public static TileData Tiles { get; } = new();
		public static RadarData Radar { get; } = new();

		public static void Clear()
		{
			Clilocs.Clear();
			Art.Clear();
			Gumps.Clear();
			Hues.Clear();
			Tiles.Clear();
			Radar.Clear();
		}

		public static void Load(string directoryPath, string language, bool uop)
		{
			Clilocs.Load(directoryPath, language);
			Art.Load(directoryPath, uop);
			Gumps.Load(directoryPath, uop);
			Hues.Load(directoryPath);
			Tiles.Load(directoryPath);
			Radar.Load(directoryPath);
		}
	}
}
