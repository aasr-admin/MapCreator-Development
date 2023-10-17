using Photoshop;

using System.Globalization;
using System.Xml;

namespace Cartography
{
	public record struct Altitude : IColorEntry, IXmlEntry
	{
		public sbyte Z { get; set; }

		public Color Color { get; set; }

		public string Name { get; set; } = "Altitude";

		public Altitude()
		{
		}

		public void Set(sbyte z, Color color, string name)
		{
			Z = z;
			Color = color;
			Name = name;
		}

		public override readonly string ToString()
		{
			return $"[{Z}] {Name}";
		}

		public readonly void Save(XmlElement node)
		{
			node.SetAttribute("Z", $"{Z}");
			node.SetAttribute("Color", $"{Color.R:X2}{Color.G:X2}{Color.B:X2}");
			node.SetAttribute("Name", $"{Name}");
		}

		public void Load(XmlElement node)
		{
			Z = SByte.Parse(node.GetAttribute("Z"));
			Color = Color.FromArgb(Int32.Parse($"FF{node.GetAttribute("Color")}", NumberStyles.HexNumber));
			Name = node.GetAttribute("Name");
		}
	}
}