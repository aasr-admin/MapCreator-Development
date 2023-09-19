using System.Xml;

namespace Cartography
{
	public class TransitionTable : HashSet<Transition>, IXmlEntry
	{
		public TransitionTable()
		{
		}

		public Transition Find(IEnumerable<LandCell> tiles)
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

		public void Save(XmlElement node)
		{
			var count = XmlHelper.SaveChildren(node, "Transition", this);

			node.SetAttribute("Count", $"{count:N0}");
		}

		public void Load(XmlElement node)
		{
			foreach (var entry in XmlHelper.LoadChildren<Transition>(node, "Transition"))
			{
				_ = Add(entry);
			}
		}
	}
}