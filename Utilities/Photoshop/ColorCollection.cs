using System.Collections;

namespace Photoshop
{
	public abstract class ColorCollection<T> : IEnumerable<T> where T : IColorEntry, new()
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
			foreach (var entry in _Entries)
			{
				yield return entry;
			}
		}

        public IEnumerator<T> GetEnumerator()
        {
			foreach (var entry in _Entries)
			{
				yield return entry;
			}
        }

        public void SaveSwatch(string filePath, ColorFormat format)
        {
            ColorSwatchHelper.Export(filePath, _Entries, format);
        }

        public void LoadSwatch(string filePath)
        {
            ColorSwatchHelper.Import(filePath, _Entries);
        }

        public void SaveTable(string filePath)
        {
            ColorTableHelper.Export(filePath, _Entries);
        }

        public void LoadTable(string filePath)
        {
            ColorTableHelper.Import(filePath, _Entries);
        }
    }
}
