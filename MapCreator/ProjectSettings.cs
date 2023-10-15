using System.Xml;

namespace MapCreator
{
	public sealed class ProjectSettings : IXmlEntry
	{
		public bool RandomStatics { get; set; } = true;

		public void Save(XmlElement node)
		{
			XmlHelper.WriteNode(node, nameof(RandomStatics), RandomStatics);
		}

		public void Load(XmlElement node)
		{
			RandomStatics = XmlHelper.ReadNode(node, nameof(RandomStatics), Boolean.Parse);
		}
	}
}