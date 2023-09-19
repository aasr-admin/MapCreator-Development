using System.Xml;

namespace Cartography
{
	public class RandomStaticCells : List<StaticCell>, IXmlEntry
	{
		public string Name { get; set; }
		public byte Weight { get; set; }

		public RandomStaticCells()
		{
		}

		public RandomStaticCells(string name, byte weight)
		{
			Name = name;
			Weight = weight;
		}

		public override string ToString()
		{
			return $"[{Weight}%] ({Count:N0}) {Name}";
		}

		public int FillBlock(int bx, int by, sbyte z, StaticMatrix statics)
		{
			var count = 0;

			foreach (var entry in this)
			{
				_ = statics.Add(bx, by, entry.X, entry.Y, (sbyte)(z + entry.Z), entry.ID, entry.Hue);

				++count;
			}

			return count;
		}

		public void Save(XmlElement node)
		{
			node.SetAttribute("Weight", $"{Weight}");
			node.SetAttribute("Name", Name);

			var count = XmlHelper.SaveChildren(node, "Static", this);

			node.SetAttribute("Count", $"{count:N0}");
		}

		public void Load(XmlElement node)
		{
			Weight = Byte.Parse(node.GetAttribute("Weight"));
			Name = node.GetAttribute("Name");

			foreach (var entry in XmlHelper.LoadChildren<StaticCell>(node, "Static"))
			{
				Add(entry);
			}
		}
	}
}