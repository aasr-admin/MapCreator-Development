using System.Drawing;
using System.Text;

namespace Photoshop
{
	public static class ColorSwatchHelper
    {
        private const ushort NULL16 = 0;

        public static void Export<T>(string filePath, T[] entries, ColorFormat format) where T : IColorEntry
        {
            using var fileStream = new FileStream(filePath, FileMode.Create);
            using var binaryWriter = new BinaryWriter(fileStream);

            binaryWriter.Write((ushort)1);
            binaryWriter.Write((ushort)entries.Length);

            foreach (var entry in entries)
            {
                Write(binaryWriter, entry, false, format);
            }

            binaryWriter.Write((ushort)2);
            binaryWriter.Write((ushort)entries.Length);

            foreach (var entry in entries)
            {
                Write(binaryWriter, entry, true, format);
            }

            binaryWriter.Flush();
            fileStream.Flush();
        }

        public static void Write<T>(BinaryWriter writer, T entry, bool name, ColorFormat format) where T : IColorEntry
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
                var value = entry.Name ?? String.Empty;

                writer.Write(value.Length + 1);

                var bytes = Encoding.BigEndianUnicode.GetBytes(value);

                writer.Write(bytes, 0, bytes.Length);
                writer.Write(NULL16);
            }
        }

        public static void Import<T>(string filePath, T[] entries) where T : IColorEntry
        {
            using var file = new FileStream(filePath, FileMode.Open);
            using var reader = new BinaryReader(file);

            _ = reader.ReadUInt16(); // v1

            var count1 = reader.ReadUInt16();
            var index1 = -1;

            while (--count1 >= 0 && ++index1 < entries.Length)
            {
                Read(reader, entries[index1], false);
            }

            _ = reader.ReadUInt16(); // v2

            var count2 = reader.ReadUInt16();
            var index2 = -1;

            while (--count2 >= 0 && ++index2 < entries.Length)
            {
                Read(reader, entries[index2], true);
            }
        }

        public static void Read<T>(BinaryReader reader, T entry, bool name) where T : IColorEntry
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
                var count = reader.ReadInt32();
                var data = reader.ReadBytes((count * 2) - 1);

                entry.Name = Encoding.BigEndianUnicode.GetString(data);

                _ = reader.ReadUInt16();
            }
        }
    }
}
