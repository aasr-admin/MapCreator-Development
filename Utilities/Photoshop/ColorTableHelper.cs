namespace Photoshop
{
	/// <summary>
	/// Exposes helper functions for handling 
	/// <see href="https://www.adobe.com/devnet-apps/photoshop/fileformatashtml/#50577411_pgfId-1070626">ACT Color Tables</see>
	/// </summary>
	public static class ColorTableHelper
	{
		public static void Write<T>(BinaryWriter writer, T entry) where T : IColorEntry
		{
			var color = entry?.Color ?? Color.Empty;

			writer.Write(color.R);
			writer.Write(color.G);
			writer.Write(color.B);
		}

		public static T Read<T>(BinaryReader reader, T entry) where T : IColorEntry
		{
			var r = reader.ReadByte();
			var g = reader.ReadByte();
			var b = reader.ReadByte();

			entry.Color = Color.FromArgb(Byte.MaxValue, r, g, b);

			return entry;
		}

		public static void Export<T>(string filePath, T[] entries) where T : IColorEntry
		{
			using var file = new FileStream(filePath, FileMode.Create);
			using var writer = new BinaryWriter(file);

			foreach (var entry in entries)
			{
				Write(writer, entry);
			}

			writer.Flush();
			file.Flush();
		}

		public static bool Import<T>(string filePath, T[] entries) where T : IColorEntry
		{
			if (!File.Exists(filePath))
			{
				return false;
			}

			using var file = new FileStream(filePath, FileMode.Open);
			using var reader = new BinaryReader(file);

			var index = -1;

			while (++index < entries.Length)
			{
				entries[index] = Read(reader, entries[index]);
			}

			return true;
		}
	}
}
