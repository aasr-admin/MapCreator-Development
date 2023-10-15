using System.Collections;

namespace Cartography
{
	public abstract class Matrix<T> : IEnumerable<T>
	{
		private T[,] _Matrix;

		public int Width { get; private set; }
		public int Height { get; private set; }

		public int Length { get; private set; }

		public ref T this[int x, int y] => ref _Matrix[x, y];

		public ref T this[int index] => ref _Matrix[index / Width, index % Width];

		public Matrix(int width, int height)
		{
			Width = width;
			Height = height;

			Length = width * height;

			_Matrix = new T[width, height];
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			foreach (var entry in _Matrix)
			{
				yield return entry;
			}
		}

		public IEnumerator<T> GetEnumerator()
		{
			foreach (var entry in _Matrix)
			{
				yield return entry;
			}
		}

		public void Clear()
		{
			for (var x = 0; x < Width; x++)
			{
				for (var y = 0; y < Height; y++)
				{
					Clear(x, y);
				}
			}
		}

		public void Clear(int x, int y)
		{
			this[x, y] = default;
		}

		public void Resize(int width, int height)
		{
			if (Width == width && Height == height)
			{
				return;
			}

			width = Math.Max(0, width);
			height = Math.Max(0, height);

			var matrix = new T[width, height];

			for (var x = 0; x < width && x < Width; x++)
			{
				for (var y = 0; y < height && y < Height; y++)
				{
					matrix[x, y] = _Matrix[x, y];
				}
			}

			Width = width;
			Height = height;

			Length = width * height;

			_Matrix = matrix;
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

			var width = x2 - x1;
			var height = y2 - y1;

			var matrix = new T[width, height];

			for (var x = x1; x < x2; x++)
			{
				for (var y = y1; y < y2; y++)
				{
					matrix[x - x1, y - y1] = _Matrix[x, y];
				}
			}

			Width = width;
			Height = height;

			Length = width * height;

			_Matrix = matrix;
		}
	}
}