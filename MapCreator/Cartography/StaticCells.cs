using System.Xml;

namespace Cartography
{
	public class StaticCells : List<StaticCell>, IXmlEntry
	{
		public StaticCell RandomTile => Utility.RandomList(this);

		public override string ToString()
		{
			return $"Statics ({Count:N0})";
		}

		public virtual void Save(XmlElement node)
		{
			var count = XmlHelper.SaveChildren(node, "Static", this);

			node.SetAttribute("Count", $"{count:N0}");
		}

		public virtual void Load(XmlElement node)
		{
			foreach (var entry in XmlHelper.LoadChildren<StaticCell>(node, "Static"))
			{
				Add(entry);
			}
		}
	}
}