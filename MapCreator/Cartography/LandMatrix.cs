namespace Cartography
{
	public class LandMatrix : Matrix<LandCell>//, IXmlEntry
	{
		public LandMatrix(int width, int height)
			: base(width, height)
		{
		}

		public ref LandCell Set(int x, int y, sbyte z)
		{
			ref var tile = ref this[x, y];

			tile.Z = z;

			return ref tile;
		}

		public ref LandCell Set(int x, int y, sbyte z, ushort tileID)
		{
			ref var tile = ref this[x, y];

			tile.ID = tileID;
			tile.Z = z;

			return ref tile;
		}
		/*
		public virtual void SaveXml(string filePath)
		{
			XmlHelper.Save(filePath, "StaticMatrix", this);
		}

		public virtual void Save(XmlDocument doc)
		{
			XmlHelper.Save(doc, "StaticMatrix", this);
		}

		public virtual void Save(XmlElement node)
		{
			node.SetAttribute("Width", $"{Width}");
			node.SetAttribute("Height", $"{Height}");

			node.SetAttribute("Count", $"{Length:N0}");

			for (var x = 0; x < Width; x++)
			{
				for (var y = 0; y < Height; y++)
				{
					ref var entry = ref this[x, y];

					var child = node.OwnerDocument.CreateElement("Land");

					child.SetAttribute("ID", $"{entry.ID}");
					child.SetAttribute("X", $"{x}");
					child.SetAttribute("Y", $"{y}");
					child.SetAttribute("Z", $"{entry.Z}");

					if (entry.Group >= 0)
					{
						child.SetAttribute("Group", $"{entry.Group}");
					}

					_ = node.AppendChild(child);
				}
			}
		}

		public virtual bool LoadXml(string filePath)
		{
			return XmlHelper.Load(filePath, "LandMatrix", this);
		}

		public virtual bool Load(XmlDocument doc)
		{
			return XmlHelper.Load(doc, "LandMatrix", this);
		}

		public virtual void Load(XmlElement node)
		{
			var width = Int32.Parse(node.GetAttribute("Width"));
			var height = Int32.Parse(node.GetAttribute("Height"));

			Resize(Math.Max(width, Width), Math.Max(height, Height));

			var nodes = node.SelectNodes("Land");

			if (nodes?.Count > 0)
			{
				foreach (XmlElement child in nodes)
				{
					var x = Int32.Parse(child.GetAttribute("X"));
					var y = Int32.Parse(child.GetAttribute("Y"));

					if (x < Width && y < Width)
					{
						var tileID = UInt16.Parse(child.GetAttribute("ID"));
						var z = SByte.Parse(child.GetAttribute("Z"));

						ref var entry = ref Set(x, y, z, tileID);

						if (!Int32.TryParse(child.GetAttribute("Group"), out entry.Group))
						{
							entry.Group = -1;
						}
					}
				}
			}
		}
		*/
	}
}