using System.Xml;

namespace Cartography
{
	public class StaticCellRandomizer : List<RandomStaticCells>, IEnumerableDisplay<RandomStaticCells>, IXmlEntry
	{
		public string Name { get; set; }
		public byte Weight { get; set; }

		public override string ToString()
		{
			return $"[{Weight}%] ({Count:N0})";
		}

		public bool Randomize(StaticMatrix statics, int x, int y, sbyte z)
		{
			if (Utility.RandomDouble() <= Weight / 100.0)
			{
				var random = Utility.RandomList(this);

				if (random?.FillBlock(x / 8, y / 8, z, statics) > 0)
				{
					return true;
				}
			}

			return false;
		}

		public void SaveXml(string filePath)
		{
			XmlHelper.Save(filePath, "StaticRandomizer", this);
		}

		public void Save(XmlDocument doc)
		{
			XmlHelper.Save(doc, "StaticRandomizer", this);
		}

		public void Save(XmlElement node)
		{
			node.SetAttribute("Weight", $"{Weight}");
			node.SetAttribute("Name", $"{Name}");

			var count = XmlHelper.SaveChildren(node, "Statics", this);

			node.SetAttribute("Count", $"{count:N0}");
		}

		public bool LoadXml(string filePath)
		{
			return XmlHelper.Load(filePath, "StaticRandomizer", this);
		}

		public bool Load(XmlDocument doc)
		{
			return XmlHelper.Load(doc, "StaticRandomizer", this);
		}

		public void Load(XmlElement node)
		{
			Weight = Byte.Parse(node.GetAttribute("Weight"));
			Name = node.GetAttribute("Name");

			foreach (var entry in XmlHelper.LoadChildren<RandomStaticCells>(node, "Statics"))
			{
				if (entry.Count > 0)
				{
					Add(entry);
				}
			}
		}
	}
}