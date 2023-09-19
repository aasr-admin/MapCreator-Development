using System.Xml;

namespace Cartography
{
	public class LandCells : List<LandCell>, IXmlEntry
	{
		public LandCell RandomTile => Utility.RandomList(this);

		public LandCells()
		{
		}

		public override string ToString()
		{
			return $"Land ({Count:N0})";
		}

		public void Save(XmlElement node)
		{
			var count = XmlHelper.SaveChildren(node, "Land", this);

			node.SetAttribute("Count", $"{count:N0}");
		}

		public void Load(XmlElement node)
		{
			foreach (var entry in XmlHelper.LoadChildren<LandCell>(node, "Land"))
			{
				Add(entry);
			}
		}
	}
}