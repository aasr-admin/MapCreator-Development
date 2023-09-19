using System.Xml;

namespace Cartography
{
	public record struct Facet : IXmlEntry
	{
		public byte Index { get; set; }

		public int Width { get; set; }
		public int Height { get; set; }

		public string Name { get; set; }

		public override readonly string ToString()
		{
			return $"{Name}";
		}

		public readonly void Save(XmlElement node)
		{
			node.SetAttribute("Index", $"{Index}");
			node.SetAttribute("Width", $"{Width}");
			node.SetAttribute("Height", $"{Height}");
			node.SetAttribute("Name", $"{Name}");
		}

		public void Load(XmlElement node)
		{
			Index = XmlConvert.ToByte(node.GetAttribute("Index"));
			Width = XmlConvert.ToInt32(node.GetAttribute("Width"));
			Height = XmlConvert.ToInt32(node.GetAttribute("Height"));
			Name = node.GetAttribute("Name");
		}
	}
}