using System.Collections;
using System.Drawing.Imaging;

namespace Photoshop
{
	public abstract class ColorCollection<T> : IColorCollection<T> where T : IColorEntry, new()
	{
		private readonly Type _BaseType = typeof(T);

		private readonly T[] _Entries;

		public ref T this[int index]
		{
			get
			{
				ref var entry = ref _Entries[index];

				if (entry is null && PopulateEntries)
				{
					entry = new T();
				}

				return ref _Entries[index];
			}
		}

		public int Length => _Entries.Length;

		public virtual bool PopulateEntries => _BaseType.IsClass;

		public ColorCollection(int fixedSize)
		{
			if (fixedSize > 0)
			{
				_Entries = new T[fixedSize];
			}
			else
			{
				_Entries = [];
			}

			Populate();
		}

		private void Populate()
		{
			if (PopulateEntries)
			{
				for (var i = 0; i < _Entries.Length; i++)
				{
					_Entries[i] ??= new T();
				}
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public IEnumerator<T> GetEnumerator()
		{
			for (var i = 0; i < _Entries.Length; i++)
			{
				T entry;

				if (PopulateEntries)
				{
					entry = _Entries[i] ??= new T();
				}
				else
				{
					entry = _Entries[i];
				}

				yield return entry;
			}
		}

		public void Clear()
		{
			Array.Clear(_Entries);

			OnClear();
		}

		protected virtual void OnClear()
		{
			Populate();
		}

		public void Reset()
		{
			Clear();

			OnReset();
		}

		protected virtual void OnReset()
		{
			Populate();
		}

		public Color GetColor(int index)
		{
			ref var entry = ref _Entries[index];

			if (PopulateEntries)
			{
				entry ??= new T();
			}

			return entry?.Color ?? Color.Empty;
		}

		public void SetColor(int index, Color color)
		{
			ref var entry = ref _Entries[index];

			entry ??= new T();

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
				ref var entry = ref _Entries[i];

				if (PopulateEntries)
				{
					entry ??= new T();
				}

				if (entry?.Color == color)
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

		public ColorPalette CreatePallette()
		{
			using var temp = new Bitmap(2, 2, PixelFormat.Format8bppIndexed);

			var pallette = temp.Palette;

			FillPallette(pallette);

			return pallette;
		}

		public virtual void FillPallette(ColorPalette palette)
		{
			for (var i = 0; i < palette.Entries.Length; i++)
			{
				if (i < _Entries.Length)
				{
					ref var entry = ref this[i];

					palette.Entries[i] = entry?.Color ?? Color.Empty;
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
