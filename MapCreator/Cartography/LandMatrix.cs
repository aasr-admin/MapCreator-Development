namespace Cartography
{
	public class LandMatrix : Matrix<LandCell>
	{
		public const int BLOCK_SIZE = 8;

		public LandMatrix(int width, int height)
			: base(Utility.RoundUp(width, BLOCK_SIZE), Utility.RoundUp(height, BLOCK_SIZE))
		{
		}

		public override void Resize(int width, int height)
		{
			base.Resize(Utility.RoundUp(width, BLOCK_SIZE), Utility.RoundUp(height, BLOCK_SIZE));
		}

		public ref LandCell Set(int x, int y, sbyte z)
		{
			ref var tile = ref this[x, y];

			tile.Z = z;

			return ref tile;
		}

		public ref LandCell Set(int x, int y, sbyte z, ushort tileID)
		{
			ref var tile = ref this[x, y];

			tile.Set(tileID, z);

			return ref tile;
		}
	}
}