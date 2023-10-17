namespace Cartography
{
	public class StaticMatrix : Matrix<StaticCell[]>
	{
		public const int BLOCK_SIZE = 8;

		public StaticMatrix(int width, int height)
			: base(Utility.RoundUp(width, BLOCK_SIZE), Utility.RoundUp(height, BLOCK_SIZE))
		{
		}

		public override void Resize(int width, int height)
		{
			base.Resize(Utility.RoundUp(width, BLOCK_SIZE), Utility.RoundUp(height, BLOCK_SIZE));
		}

		public ref StaticCell Add(int x, int y, sbyte z, ushort tileID)
		{
			return ref Add(x, y, z, tileID, 0);
		}

		public ref StaticCell Add(int x, int y, sbyte z, ushort tileID, ushort hue)
		{
			ref var list = ref this[x, y];

			if (list != null)
			{
				for (var i = 0; i < list.Length; i++)
				{
					ref var s = ref list[i];

					if (s.X == x && s.Y == y && s.Z == z && s.ID == tileID)
					{
						s.Hue = hue;

						return ref s;
					}
				}

				Array.Resize(ref list, 1 + list.Length);
			}
			else
			{
				Array.Resize(ref list, 1);
			}

			ref var tile = ref list[^1];

			tile.Set(tileID, x, y, z, hue);

			Utility.InsertionSort(list);

			return ref tile;
		}
	}
}