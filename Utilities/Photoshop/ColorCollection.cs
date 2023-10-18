using System.Collections;
using System.Drawing.Imaging;

namespace Photoshop
{
	public abstract class ColorCollection<T> : IColorCollection<T> where T : IColorEntry, new()
    {
        private readonly Type _BaseType = typeof(T);

        private readonly T[] _Entries;

        public ref T this[int index] => ref _Entries[index];

        public int Length => _Entries.Length;

        public ColorCollection(int fixedSize)
        {
            if (fixedSize > 0)
            {
                _Entries = new T[fixedSize];

                if (_BaseType.IsClass)
                {
                    for (var i = 0; i < _Entries.Length; i++)
                    {
                        _Entries[i] = new T();
                    }
                }
            }
            else
            {
                _Entries = Array.Empty<T>();
            }
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _Entries.GetEnumerator();
		}

		public IEnumerator<T> GetEnumerator()
		{
			return _Entries.AsEnumerable().GetEnumerator();
		}

		public void Reset()
		{
			Array.Clear(_Entries);

			if (_BaseType.IsClass)
			{
				for (var i = 0; i < _Entries.Length; i++)
				{
					_Entries[i] = new T();
				}
			}
		}

		public Color GetColor(int index)
		{
			ref var entry = ref _Entries[index];

			return entry.Color;
		}

		public void SetColor(int index, Color color)
		{
			ref var entry = ref _Entries[index];

			entry.Color = color;
		}

		public int IndexOf(Color color)
		{
			return IndexOf(color, 0);
		}

		public int IndexOf(Color color, int startIndex)
		{
			for (var i = startIndex; i < _Entries.Length; i++)
			{
				if (_Entries[i].Color == color)
				{
					return i;
				}
			}

			return -1;
		}

		public int IndexOf(T o)
		{
			return IndexOf(o, 0);
		}

		public int IndexOf(T o, int startIndex)
		{
			return Array.IndexOf(_Entries, o, startIndex);
		}

		public bool Contains(T o)
		{
			return IndexOf(o) >= 0;
		}

		public bool Contains(Color color)
		{
			return IndexOf(color) >= 0;
		}

		public virtual void FillPallette(ColorPalette palette)
		{
			for (var i = 0; i < palette.Entries.Length; i++)
			{
				if (i < _Entries.Length)
				{
					palette.Entries[i] = _Entries[i].Color;
				}
				else
				{
					palette.Entries[i] = Color.Empty;
				}
			}
		}

		public virtual void SaveSwatch(string filePath, ColorFormat format)
        {
            ColorSwatchHelper.Export(filePath, _Entries, format);
        }

		public virtual bool LoadSwatch(string filePath)
        {
			return ColorSwatchHelper.Import(filePath, _Entries);
        }

		public virtual void SaveTable(string filePath)
        {
            ColorTableHelper.Export(filePath, _Entries);
        }

		public virtual bool LoadTable(string filePath)
        {
            return ColorTableHelper.Import(filePath, _Entries);
		}
	}
}
