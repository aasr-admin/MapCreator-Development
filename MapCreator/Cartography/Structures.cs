using System.Xml;

namespace Cartography
{
	public class Structures : HashSet<Structure>, IXmlEntry
	{
		public override string ToString()
		{
			return $"Structures ({Count:N0})";
		}

		public virtual void SaveXml(string filePath)
		{
			XmlHelper.Save(filePath, "Structures", this);
		}

		public virtual void Save(XmlDocument doc)
		{
			XmlHelper.Save(doc, "Structures", this);
		}

		public virtual void Save(XmlElement node)
		{
			var count = XmlHelper.SaveChildren(node, "Structure", this);

			node.SetAttribute("Count", $"{count:N0}");
		}

		public virtual bool LoadXml(string filePath)
		{
			return XmlHelper.Load(filePath, "Structures", this);
		}

		public virtual bool Load(XmlDocument doc)
		{
			return XmlHelper.Load(doc, "Structures", this);
		}

		public virtual void Load(XmlElement node)
		{
			foreach (var entry in XmlHelper.LoadChildren<Structure>(node, "Structure"))
			{
				_ = Add(entry);
			}
		}
	}
}