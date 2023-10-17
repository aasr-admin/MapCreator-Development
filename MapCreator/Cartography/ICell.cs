namespace Cartography
{
	public interface ICell : IComparable, IComparable<ICell>
	{
		ushort ID { get; set; }
		sbyte Z { get; set; }

		int IComparable.CompareTo(object? obj)
		{
			if (obj is ICell cell)
			{
				return CompareTo(cell);
			}

			return -1;
		}

		int IComparable<ICell>.CompareTo(ICell? other)
		{
			return Z.CompareTo(other?.Z);
		}
	}
}