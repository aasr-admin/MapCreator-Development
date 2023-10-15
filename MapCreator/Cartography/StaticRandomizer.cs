using System.Xml;

namespace Cartography
{
	public class StaticRandomizer : List<RandomStatics>, IXmlEntry
	{
		public string Name { get; set; }
		public byte Weight { get; set; }

		public override string ToString()
		{
			return $"[{Weight}%] ({Count:N0}) {Name}";
		}

		public bool Randomize(FacetMatrix matrix, int x, int y, sbyte z)
		{
			if (Utility.RandomDouble() <= Weight / 100.0)
			{
				var random = Utility.RandomList(this);

				if (random?.FillBlock(matrix, x, y, z) > 0)
				{
					return true;
				}
			}

			return false;
		}

		public virtual void Save(XmlElement node)
		{
			node.SetAttribute("Weight", $"{Weight}");
			node.SetAttribute("Name", $"{Name}");

			var count = XmlHelper.SaveChildren(node, "Statics", this);

			node.SetAttribute("Count", $"{count:N0}");
		}

		public virtual void Load(XmlElement node)
		{
			Weight = Byte.Parse(node.GetAttribute("Weight"));
			Name = node.GetAttribute("Name");

			foreach (var entry in XmlHelper.LoadChildren<RandomStatics>(node, "Statics"))
			{
				if (entry.Count > 0)
				{
					Add(entry);
				}
			}
		}
	}
}