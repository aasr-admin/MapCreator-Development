using System.Drawing.Imaging;

namespace Photoshop
{
	public interface IColorCollection
	{
		int Length { get; }

		void Reset();

		int IndexOf(Color color);
		int IndexOf(Color color, int startIndex);

		bool Contains(Color color);

		void FillPallette(ColorPalette palette);
	}

	public interface IColorCollection<T> : IColorCollection, IEnumerable<T> where T : IColorEntry
	{
		ref T this[int index] { get; }

		int IndexOf(T o);
		int IndexOf(T o, int startIndex);

		bool Contains(T o);
	}
}
