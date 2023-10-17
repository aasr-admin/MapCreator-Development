namespace Cartography
{
	public abstract class Matrix<T>
	{
		protected T[,] _Matrix;

		public int Width => _Matrix.GetLength(0);
		public int Height => _Matrix.GetLength(1);

		public int Length => _Matrix.Length;

		public virtual ref T this[int x, int y] => ref _Matrix[x, y];

		public virtual ref T this[int index] => ref _Matrix[index / Width, index % Width];

		public Matrix(int width, int height)
		{
			_Matrix = new T[width, height];
		}

		public virtual void Clear()
		{
			Array.Clear(_Matrix, 0, _Matrix.Length);
		}

		public virtual void Clear(int x, int y)
		{
			if (x >= 0 && x < Width && y >= 0 && y < Height)
			{
				this[x, y] = default!;
			}
		}

		public virtual void Resize(int width, int height)
		{
			if (Width != width || Height != height)
			{
				var matrix = new T[width, height];

				for (var x = 0; x < width && x < Width; x++)
				{
					for (var y = 0; y < height && y < Height; y++)
					{
						matrix[x, y] = _Matrix[x, y];
					}
				}

				_Matrix = matrix;
			}
		}
	}
}