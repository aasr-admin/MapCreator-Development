using System.Xml;

namespace Cartography
{
	public record struct StaticCell : IXmlEntry
	{
		public ushort ID;
		public byte X;
		public byte Y;
		public sbyte Z;
		public ushort Hue;

		public int Group = -1;

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

		public void Set(ushort tileID, byte x, byte y, sbyte z)
		{
			ID = tileID;
			X = x;
			Y = y;
			Z = z;
		}

		public void Set(ushort tileID, byte x, byte y, sbyte z, ushort hue)
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
			X = Byte.Parse(node.GetAttribute("X"));
			Y = Byte.Parse(node.GetAttribute("Y"));
			Z = SByte.Parse(node.GetAttribute("Z"));
			Hue = UInt16.Parse(node.GetAttribute("Hue"));

			if (!Int32.TryParse(node.GetAttribute("Group"), out Group))
			{
				Group = -1;
			}
		}
	}
}