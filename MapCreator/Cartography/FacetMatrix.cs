namespace Cartography
{
	public class FacetMatrix
	{
		public Rectangle Bounds { get; private set; }

		public Size Size => Bounds.Size;

		public int Width => Bounds.Width;
		public int Height => Bounds.Height;

		public int Area => Bounds.Width * Bounds.Height;

		public LandMatrix LandMatrix { get; }
		public StaticMatrix StaticMatrix { get; }

		public FacetMatrix()
			: this(0, 0)
		{
		}

		public FacetMatrix(int width, int height)
		{
			Bounds = new Rectangle(0, 0, width, height);

			LandMatrix = new LandMatrix(width, height);
			StaticMatrix = new StaticMatrix(width, height);
		}

		public void Clear()
		{
			LandMatrix.Clear();
			StaticMatrix.Clear();
		}

		public void Clear(int x, int y)
		{
			if (x >= 0 && x < Width && y >= 0 && y < Height)
			{
				LandMatrix.Clear(x, y);
				StaticMatrix.Clear(x, y);
			}
		}

		public void Resize(int width, int height)
		{
			if (Width == width && Height == height)
			{
				return;
			}

			width = Math.Max(0, width);
			height = Math.Max(0, height);

			Bounds = new Rectangle(0, 0, width, height);

			LandMatrix.Resize(width, height);
			StaticMatrix.Resize(width, height);
		}

		public void Crop(int x1, int y1, int x2, int y2)
		{
			if (x1 > x2)
			{
				(x1, x2) = (x2, x1);
			}

			x1 = Math.Clamp(x1, 0, Width);
			x2 = Math.Clamp(x2, 0, Width);

			if (y1 > y2)
			{
				(y1, y2) = (y2, y1);
			}

			y1 = Math.Clamp(y1, 0, Height);
			y2 = Math.Clamp(y2, 0, Height);

			Bounds = new Rectangle(0, 0, x2 - x1, y2 - y1);

			LandMatrix.Crop(x1, y1, x2, y2);
			StaticMatrix.Crop(x1, y1, x2, y2);
		}

		public ref LandCell GetLand(int x, int y)
		{
			return ref LandMatrix[x, y];
		}

		public void SetLand(int x, int y, LandCell tile)
		{
			LandMatrix[x, y] = tile;
		}

		public void SetLand(int x, int y, sbyte z)
		{
			_ = LandMatrix.Set(x, y, z);
		}

		public void SetLand(int x, int y, sbyte z, ushort tileID)
		{
			_ = LandMatrix.Set(x, y, z, tileID);
		}

		public void ClearStatics(int x, int y)
		{
			StaticMatrix.Clear(x, y);
		}

		public ref StaticCell[] GetStatics(int x, int y)
		{
			return ref StaticMatrix[x, y];
		}

		public void SetStatics(int x, int y, StaticCell[] tiles)
		{
			StaticMatrix[x, y] = tiles;
		}

		public ref StaticCell AddStatic(int x, int y, sbyte oz, ushort tileID)
		{
			return ref StaticMatrix.Add(x, y, oz, tileID);
		}

		public ref StaticCell AddStatic(int x, int y, sbyte oz, ushort tileID, ushort hue)
		{
			return ref StaticMatrix.Add(x, y, oz, tileID, hue);
		}

		public ref StaticCell AddStatic(int x, int y, byte ox, byte oy, sbyte oz, ushort tileID)
		{
			return ref StaticMatrix.Add(x, y, ox, oy, oz, tileID);
		}

		public ref StaticCell AddStatic(int x, int y, byte ox, byte oy, sbyte oz, ushort tileID, ushort hue)
		{
			return ref StaticMatrix.Add(x, y, ox, oy, oz, tileID, hue);
		}
	}
}