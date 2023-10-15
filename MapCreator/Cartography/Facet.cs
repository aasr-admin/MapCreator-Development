using System.Xml;

namespace Cartography
{
	public sealed class Facet : FacetMatrix, IXmlEntry, IComparable<Facet>
	{
		public byte Index { get; set; }

		public string Name { get; set; }

		public bool IsValid => Area > 0 && !String.IsNullOrWhiteSpace(Name);

		public override string ToString()
		{
			return $"{Index} {Name} ({Width} x {Height})";
		}

		public int CompareTo(Facet other)
		{
			return Index.CompareTo(other?.Index);
		}

		public IEnumerable<LandCell> GetLandSequence(int x, int y)
		{
			LandCell land;

			if (GetLandSafe(x - 1, y - 1, out land))
			{
				yield return land;
			}

			if (GetLandSafe(x, y - 1, out land))
			{
				yield return land;
			}

			if (GetLandSafe(x + 1, y - 1, out land))
			{
				yield return land;
			}

			if (GetLandSafe(x - 1, y, out land))
			{
				yield return land;
			}

			if (GetLandSafe(x, y, out land))
			{
				yield return land;
			}

			if (GetLandSafe(x + 1, y, out land))
			{
				yield return land;
			}

			if (GetLandSafe(x - 1, y + 1, out land))
			{
				yield return land;
			}

			if (GetLandSafe(x, y + 1, out land))
			{
				yield return land;
			}

			if (GetLandSafe(x + 1, y + 1, out land))
			{
				yield return land;
			}
		}

		private bool GetLandSafe(int x, int y, out LandCell land)
		{
			if (x >= 0 && x < Width && y >= 0 && y < Height)
			{
				land = GetLand(x, y);

				return true;
			}

			land = default;

			return false;
		}

		public void SaveXml(string filePath)
		{
			XmlHelper.Save(filePath, "Facet", this);
		}

		public void Save(XmlDocument doc)
		{
			XmlHelper.Save(doc, "Facet", this);
		}

		public void Save(XmlElement node)
		{
			node.SetAttribute("Index", $"{Index}");

			node.SetAttribute("Name", $"{Name}");

			node.SetAttribute("Width", $"{Width}");
			node.SetAttribute("Height", $"{Height}");
		}

		public bool LoadXml(string filePath)
		{
			return XmlHelper.Load(filePath, "Facet", this);
		}

		public bool Load(XmlDocument doc)
		{
			return XmlHelper.Load(doc, "Facet", this);
		}

		public void Load(XmlElement node)
		{
			Index = XmlConvert.ToByte(node.GetAttribute("Index"));

			Name = node.GetAttribute("Name");

			var width = XmlConvert.ToInt32(node.GetAttribute("Width"));
			var height = XmlConvert.ToInt32(node.GetAttribute("Height"));

			Resize(width, height);
		}
	}
}