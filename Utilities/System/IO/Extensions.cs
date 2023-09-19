namespace System.IO
{
	public static class Extensions
	{
		private const ulong NULL64 = 0;
		private const uint NULL32 = 0;
		private const ushort NULL16 = 0;
		private const byte NULL8 = 0;

		public static void Fill(this BinaryWriter writer, int count)
		{
			while (count > 0)
			{
				if (count >= 8)
				{
					writer.Write(NULL64);

					count -= 8;
				}
				else if (count >= 4)
				{
					writer.Write(NULL32);

					count -= 4;
				}
				else if (count >= 2)
				{
					writer.Write(NULL16);

					count -= 2;
				}
				else
				{
					writer.Write(NULL8);

					count -= 1;
				}
			}
		}

		public static void Skip(this BinaryReader reader, int count)
		{
			while (--count >= 0 && reader.PeekChar() >= 0)
			{
				_ = reader.ReadByte();
			}
		}
	}
}