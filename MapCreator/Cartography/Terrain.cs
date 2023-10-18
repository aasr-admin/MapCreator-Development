using Photoshop;

using System.Globalization;
using System.Xml;

namespace Cartography
{
	public record struct Terrain : IColorEntry, IXmlEntry
	{
		public ushort TileID { get; set; }

		public Color Color { get; set; }

		public bool RandomZ { get; set; }

		public string Name { get; set; } = "Terrain";

		public Terrain()
		{
		}

		public void Set(ushort tileID, Color color, bool random, string name)
		{
			TileID = tileID;
			Color = color;
			RandomZ = random;
			Name = name;
		}

		public override readonly string ToString()
		{
			return $"({TileID}) {Name}";
		}

		public readonly void Save(XmlElement node)
		{
			node.SetAttribute("Tile", $"{TileID}");
			node.SetAttribute("Color", $"{Color.R:X2}{Color.G:X2}{Color.B:X2}");
			node.SetAttribute("RandomZ", $"{RandomZ}");
			node.SetAttribute("Name", Name);
		}

		public void Load(XmlElement node)
		{
			TileID = UInt16.Parse(node.GetAttribute("Tile"));
			Color = Color.FromArgb(Int32.Parse($"FF{node.GetAttribute("Color")}", NumberStyles.HexNumber));
			RandomZ = Boolean.Parse(node.GetAttribute("RandomZ"));
			Name = node.GetAttribute("Name");
		}
	}
}