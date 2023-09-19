using System.Xml;

namespace Cartography
{
	public class FacetList : List<Facet>, IEnumerable<Facet>, IXmlEntry
	{
		public override string ToString()
		{
			return $"({Count:N0})";
		}

		public void SaveXml(string filePath)
		{
			XmlHelper.Save(filePath, "Facets", this);
		}

		public void Save(XmlDocument doc)
		{
			XmlHelper.Save(doc, "Facets", this);
		}

		public virtual void Save(XmlElement node)
		{
			var count = XmlHelper.SaveChildren(node, "Facet", this);

			node.SetAttribute("Count", $"{count:N0}");
		}

		public bool LoadXml(string filePath)
		{
			return XmlHelper.Load(filePath, "Facets", this);
		}

		public bool Load(XmlDocument doc)
		{
			return XmlHelper.Load(doc, "Facets", this);
		}

		public virtual void Load(XmlElement node)
		{
			foreach (var entry in XmlHelper.LoadChildren<Facet>(node, "Facet"))
			{
				Add(entry);
			}
		}
	}
}