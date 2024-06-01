using System.Drawing.Imaging;

namespace Photoshop
{
	public interface IColorCollection
	{
		int Length { get; }

		void Reset();

		Color GetColor(int index);
		void SetColor(int index, Color color);

		int IndexOf(Color color);
		int IndexOf(Color color, int startIndex);

		bool Contains(Color color);

		void FillPallette(ColorPalette palette);

		void SaveSwatch(string filePath, ColorFormat format);
		bool LoadSwatch(string filePath);

		void SaveTable(string filePath);
		bool LoadTable(string filePath);
	}

	public interface IColorCollection<T> : IColorCollection, IEnumerable<T> where T : IColorEntry
	{
		ref T this[int index] { get; }

		int IndexOf(T o);
		int IndexOf(T o, int startIndex);

		bool Contains(T o);
	}
}
