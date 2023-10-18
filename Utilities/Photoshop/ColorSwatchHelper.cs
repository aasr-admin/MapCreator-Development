using System.Text;

namespace Photoshop
{
	/// <summary>
	/// Exposes helper functions for handling 
	/// <see href="https://www.adobe.com/devnet-apps/photoshop/fileformatashtml/PhotoshopFileFormats.htm#50577411_pgfId-1055819">ACO Color Swatches</see>
	/// </summary>
	public static class ColorSwatchHelper
    {
        private const ushort NULL16 = 0;

        public static void Write<T>(BinaryWriter writer, T? entry, bool name, ColorFormat format) where T : IColorEntry
        {
            writer.Write((ushort)format);

            var color = entry?.Color ?? Color.Empty;

            ColorHelper.Convert(color, format, out var v0, out var v1, out var v2, out var v3);

            writer.Write(v0);
            writer.Write(v1);
            writer.Write(v2);
            writer.Write(v3);

            if (name)
            {
                var value = entry?.Name ?? String.Empty;

                writer.Write(value.Length);

                var data = Encoding.BigEndianUnicode.GetBytes(value);

                writer.Write(data, 0, data.Length);

                writer.Write(NULL16);
            }
		}

		public static T Read<T>(BinaryReader reader, T entry, bool name) where T : IColorEntry
		{
			var format = (ColorFormat)reader.ReadUInt16();

			var v0 = reader.ReadUInt16();
			var v1 = reader.ReadUInt16();
			var v2 = reader.ReadUInt16();
			var v3 = reader.ReadUInt16();

			ColorHelper.Convert(v0, v1, v2, v3, format, out var color);

			entry.Color = color;

			if (name)
			{
				var length = reader.ReadInt32();
				var data = reader.ReadBytes(length * 2);
				
				entry.Name = Encoding.BigEndianUnicode.GetString(data);

				_ = reader.ReadUInt16();
			}

			return entry;
		}

		public static void Export<T>(string filePath, T[] entries, ColorFormat format) where T : IColorEntry
		{
			using var file = new FileStream(filePath, FileMode.Create);
			using var writer = new BinaryWriter(file, Encoding.BigEndianUnicode);

			writer.Write((ushort)1);
			writer.Write((ushort)entries.Length);

			foreach (var entry in entries)
			{
				Write(writer, entry, false, format);
			}

			writer.Write((ushort)2);
			writer.Write((ushort)entries.Length);

			foreach (var entry in entries)
			{
				Write(writer, entry, true, format);
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
			using var reader = new BinaryReader(file, Encoding.BigEndianUnicode);

			_ = reader.ReadUInt16(); // v1

			var count1 = reader.ReadUInt16();
			var index1 = -1;

			while (--count1 >= 0 && ++index1 < entries.Length)
			{
				entries[index1] = Read(reader, entries[index1], false);
			}

			_ = reader.ReadUInt16(); // v2

			var count2 = reader.ReadUInt16();
			var index2 = -1;

			while (--count2 >= 0 && ++index2 < entries.Length)
			{
				entries[index2] = Read(reader, entries[index2], true);
			}

			return true;
		}
    }
}
