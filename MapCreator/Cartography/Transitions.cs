using System.Xml;

namespace Cartography
{
	public class Transitions : HashSet<Transition>, IXmlEntry
	{
		public Transitions()
		{
		}

		public Transition? Find(IEnumerable<LandCell> tiles)
		{
			var hash = Transition.GetHash(tiles);

			foreach (var entry in this)
			{
				if (entry.GroupHash == hash)
				{
					return entry;
				}
			}

			return null;
		}

		public virtual void SaveXml(string filePath)
		{
			XmlHelper.Save(filePath, "Transitions", this);
		}

		public virtual void Save(XmlDocument doc)
		{
			XmlHelper.Save(doc, "Transitions", this);
		}

		public virtual void Save(XmlElement node)
		{
			var count = XmlHelper.SaveChildren(node, "Transition", this);

			node.SetAttribute("Count", $"{count:N0}");
		}

		public virtual bool LoadXml(string filePath)
		{
			return XmlHelper.Load(filePath, "Transitions", this);
		}

		public virtual bool Load(XmlDocument doc)
		{
			return XmlHelper.Load(doc, "Transitions", this);
		}

		public virtual void Load(XmlElement node)
		{
			foreach (var entry in XmlHelper.LoadChildren<Transition>(node, "Transition"))
			{
				_ = Add(entry);
			}
		}
	}
}