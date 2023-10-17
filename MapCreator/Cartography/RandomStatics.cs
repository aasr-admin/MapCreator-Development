using System.Xml;

namespace Cartography
{
	public class RandomStatics : StaticCells
	{
		public string Name { get; set; } = "Statics";
		public byte Weight { get; set; }

		public RandomStatics()
		{
		}

		public RandomStatics(string name, byte weight)
		{
			Name = name;
			Weight = weight;
		}

		public override string ToString()
		{
			return $"[{Weight}%] ({Count:N0}) {Name}";
		}

		public int FillBlock(Facet facet, int x, int y, sbyte z)
		{
			x /= 8;
			y /= 8;

			var count = 0;

			foreach (var entry in this)
			{
				_ = facet.AddStatic(x + entry.X, y + entry.Y, (sbyte)(z + entry.Z), entry.ID, entry.Hue);

				++count;
			}

			return count;
		}

		public override void Save(XmlElement node)
		{
			base.Save(node);

			node.SetAttribute("Weight", $"{Weight}");
			node.SetAttribute("Name", Name);
		}

		public override void Load(XmlElement node)
		{
			base.Load(node);

			Weight = Byte.Parse(node.GetAttribute("Weight"));
			Name = node.GetAttribute("Name");
		}
	}
}