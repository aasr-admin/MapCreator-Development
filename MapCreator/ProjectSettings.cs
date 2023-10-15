using System.Xml;

namespace MapCreator
{
	public sealed class ProjectSettings : IXmlEntry
	{
		public string UltimaDirectory { get; set; }

		public bool RandomStatics { get; set; } = true;

		public void Save(XmlElement node)
		{
			XmlHelper.WriteNode(node, nameof(UltimaDirectory), UltimaDirectory);
			XmlHelper.WriteNode(node, nameof(RandomStatics), RandomStatics);
		}

		public void Load(XmlElement node)
		{
			UltimaDirectory = XmlHelper.ReadNode(node, nameof(UltimaDirectory), s => s);
			RandomStatics = XmlHelper.ReadNode(node, nameof(RandomStatics), Boolean.Parse);
		}
	}
}