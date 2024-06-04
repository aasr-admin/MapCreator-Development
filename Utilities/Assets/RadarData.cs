namespace Assets
{
	public sealed class RadarData
	{
		public Color[] Colors { get; } = new Color[81920];

		public int Length => Colors.Length;

		public int MaxLandID { get; private set; }
		public int MaxItemID { get; private set; }

		public string? Directory { get; private set; }

		public bool IsLoaded => Directory != null;

		public void Clear()
		{
			Directory = null;

			MaxLandID = MaxItemID = 0;

			Array.Clear(Colors);
		}

		public void Load(string directoryPath)
		{
			Clear();

			var index = 0;
			var path = Utility.FindDataFile(directoryPath, "radarcol.mul");

			if (File.Exists(path))
			{
				Directory = directoryPath;

				using var file = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

				var buffer = new byte[file.Length];

				_ = file.Read(buffer, 0, buffer.Length);

				var count = buffer.Length / 2;

				do
				{
					Colors[index] = Color.FromArgb(HueData.Convert32(BitConverter.ToUInt16(buffer, index * 2)));
				}
				while (++index < count);

				MaxLandID = 16383;
				MaxItemID = count - (MaxLandID + 1);
			}

			while (index < Colors.Length)
			{
				Colors[index] = Color.Empty;

				++index;
			}
		}

		public Color GetLandColor(int index)
		{
			if (index < 0 || index > MaxLandID)
			{
				return Color.Empty;
			}

			return Colors[index];
		}

		public Color GetStaticColor(int index)
		{
			if (index < 0 || index > MaxItemID)
			{
				return Color.Empty;
			}

			index += MaxLandID + 1;

			return Colors[index];
		}
	}
}
