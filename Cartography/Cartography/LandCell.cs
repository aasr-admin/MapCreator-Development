using System.Xml;

namespace Cartography
{
	public record struct LandCell : IXmlEntry
	{
		public ushort ID;
		public sbyte Z;

		public int Group = -1;

		public LandCell()
		{
		}

		public void Set(ushort tileID, sbyte z)
		{
			ID = tileID;
			Z = z;
		}

		public override readonly string ToString()
		{
			if (Group >= 0)
			{
				return $"{ID:D5} [#{Group}]";
			}

			return $"{ID:D5}";
		}

		public readonly void Save(XmlElement node)
		{
			node.SetAttribute("ID", $"{ID}");
			node.SetAttribute("Z", $"{Z}");

			if (Group >= 0)
			{
				node.SetAttribute("Group", $"{Group}");
			}
		}

		public void Load(XmlElement node)
		{
			ID = UInt16.Parse(node.GetAttribute("ID"));
			Z = SByte.Parse(node.GetAttribute("Z"));

			if (!Int32.TryParse(node.GetAttribute("Group"), out Group))
			{
				Group = -1;
			}
		}
	}
}