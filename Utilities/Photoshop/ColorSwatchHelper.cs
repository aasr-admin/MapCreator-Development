using System.Buffers.Binary;
using System.Text;

namespace Photoshop
{
	/// <summary>
	/// Exposes helper functions for handling 
	/// <see href="https://www.adobe.com/devnet-apps/photoshop/fileformatashtml/PhotoshopFileFormats.htm#50577411_pgfId-1055819">ACO Color Swatches</see>
	/// </summary>
	public static class ColorSwatchHelper
	{
		private static readonly UnicodeEncoding _Encoding = new(true, true);

		private static void WriteUInt16(Stream stream, ushort value)
		{
			Span<byte> buffer = stackalloc byte[2];

			BinaryPrimitives.WriteUInt16BigEndian(buffer, value);

			stream.Write(buffer);
		}

		private static ushort ReadUInt16(Stream stream)
		{
			Span<byte> buffer = stackalloc byte[2];

			_ = stream.Read(buffer);

			return BinaryPrimitives.ReadUInt16BigEndian(buffer);
		}

		private static void WriteInt32(Stream stream, int value)
		{
			Span<byte> buffer = stackalloc byte[4];

			BinaryPrimitives.WriteInt32BigEndian(buffer, value);

			stream.Write(buffer);
		}

		private static int ReadInt32(Stream stream)
		{
			Span<byte> buffer = stackalloc byte[4];

			_ = stream.Read(buffer);

			return BinaryPrimitives.ReadInt32BigEndian(buffer);
		}

		private static void WriteString(Stream stream, string value)
		{
			value ??= String.Empty;

			var length = value.Length + 1;

			WriteInt32(stream, length);

			Span<byte> buffer = stackalloc byte[length * 2];

			_ = _Encoding.GetBytes(value, buffer);

			stream.Write(buffer);
		}

		private static string ReadString(Stream stream)
		{
			var length = ReadInt32(stream) * 2;

			Span<byte> buffer = stackalloc byte[length];

			_ = stream.Read(buffer);

			var value = Encoding.BigEndianUnicode.GetString(buffer);

			return value.Trim();
		}

		public static void Write<T>(Stream stream, T? entry, bool name, ColorFormat format) where T : IColorEntry
		{
			WriteUInt16(stream, (ushort)format);

			var color = entry?.Color ?? Color.Empty;

			ColorHelper.Convert(color, format, out var v0, out var v1, out var v2, out var v3);

			WriteUInt16(stream, v0);
			WriteUInt16(stream, v1);
			WriteUInt16(stream, v2);
			WriteUInt16(stream, v3);

			if (name)
			{
				var entryName = entry?.Name ?? color.Name;

				WriteString(stream, entryName);
			}

			stream.Flush();
		}

		public static T? Read<T>(Stream stream, T? entry, bool name) where T : IColorEntry
		{
			var format = (ColorFormat)ReadUInt16(stream);

			var v0 = ReadUInt16(stream);
			var v1 = ReadUInt16(stream);
			var v2 = ReadUInt16(stream);
			var v3 = ReadUInt16(stream);

			ColorHelper.Convert(v0, v1, v2, v3, format, out var color);

			if (entry != null)
			{
				entry.Color = color;
			}

			if (name)
			{
				var entryName = ReadString(stream);

				if (entry != null)
				{
					entry.Name = entryName;
				}
			}

			return entry;
		}

		public static void Export<T>(string filePath, T?[] entries, ColorFormat format) where T : IColorEntry
		{
			using var file = new FileStream(filePath, FileMode.Create);

			WriteUInt16(file, 1);
			WriteUInt16(file, (ushort)entries.Length);

			foreach (var entry in entries)
			{
				Write(file, entry, false, format);
			}

			WriteUInt16(file, 2);
			WriteUInt16(file, (ushort)entries.Length);

			foreach (var entry in entries)
			{
				Write(file, entry, true, format);
			}

			file.Flush();
		}

		public static bool Import<T>(string filePath, T?[] entries) where T : IColorEntry
		{
			if (!File.Exists(filePath))
			{
				return false;
			}

			using var file = new FileStream(filePath, FileMode.Open);

			var ver = ReadUInt16(file); // v1

			if (ver is < 0 or > 2)
			{
				return false;
			}

			if (ver == 1)
			{
				var count = ReadUInt16(file);
				var index = -1;

				while (--count >= 0 && ++index < entries.Length)
				{
					entries[index] = Read(file, entries[index], false);
				}

				ver = ReadUInt16(file); // v2
			}

			if (ver == 2)
			{
				var count = ReadUInt16(file);
				var index = -1;

				while (--count >= 0 && ++index < entries.Length)
				{
					entries[index] = Read(file, entries[index], true);
				}
			}

			return true;
		}
	}
}
