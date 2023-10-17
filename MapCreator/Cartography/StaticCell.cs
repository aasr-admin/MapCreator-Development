using System.Xml;

namespace Cartography
{
	public record struct StaticCell : IComparable<StaticCell>, ICell, IXmlEntry
	{
		public ushort ID { get; set; }
		public int X { get; set; }
		public int Y { get; set; }
		public sbyte Z { get; set; }
		public ushort Hue { get; set; }

		public int Group { get; set; } = -1;

		public StaticCell()
		{
		}

		public override readonly string ToString()
		{
			if (Group >= 0)
			{
				return $"{ID:D5} [#{Group}]";
			}

			return $"{ID:D5}";
		}

		public readonly int CompareTo(StaticCell other)
		{
			return Z.CompareTo(other.Z);
		}

		public void Set(ushort tileID, int x, int y, sbyte z)
		{
			ID = tileID;
			X = x;
			Y = y;
			Z = z;
		}

		public void Set(ushort tileID, int x, int y, sbyte z, ushort hue)
		{
			ID = tileID;
			X = x;
			Y = y;
			Z = z;
			Hue = hue;
		}

		public readonly void Save(XmlElement node)
		{
			node.SetAttribute("ID", $"{ID}");
			node.SetAttribute("X", $"{X}");
			node.SetAttribute("Y", $"{Y}");
			node.SetAttribute("Z", $"{Z}");
			node.SetAttribute("Hue", $"{Hue}");

			if (Group >= 0)
			{
				node.SetAttribute("Group", $"{Group}");
			}
		}

		public void Load(XmlElement node)
		{
			ID = UInt16.Parse(node.GetAttribute("ID"));
			X = Int32.Parse(node.GetAttribute("X"));
			Y = Int32.Parse(node.GetAttribute("Y"));
			Z = SByte.Parse(node.GetAttribute("Z"));
			Hue = UInt16.Parse(node.GetAttribute("Hue"));

			if (Int32.TryParse(node.GetAttribute("Group"), out var group))
			{
				Group = group;
			}
		}
	}
}