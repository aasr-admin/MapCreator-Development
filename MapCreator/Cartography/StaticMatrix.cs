using System.Xml;

namespace Cartography
{
	public class StaticMatrix : Matrix<StaticCell[]>//, IXmlEntry
	{
		//public int TotalCells => this.Sum(o => o?.Length ?? 0);

		public StaticMatrix()
			: this(0, 0)
		{
		}

		public StaticMatrix(int width, int height)
			: base(width / 8, height / 8)
		{
		}

		public ref StaticCell Add(int x, int y, sbyte oz, ushort tileID)
		{
			return ref Add(x, y, oz, tileID, 0);
		}

		public ref StaticCell Add(int x, int y, sbyte oz, ushort tileID, ushort hue)
		{
			return ref Add(x / 8, y / 8, (byte)(x % 8), (byte)(y % 8), oz, tileID, hue);
		}

		public ref StaticCell Add(int bx, int by, byte ox, byte oy, sbyte oz, ushort tileID)
		{
			return ref Add(bx, by, ox, oy, oz, tileID, 0);
		}

		public ref StaticCell Add(int bx, int by, byte ox, byte oy, sbyte oz, ushort tileID, ushort hue)
		{
			ref var list = ref this[bx, by];

			if (list != null)
			{
				for (var i = 0; i < list.Length; i++)
				{
					ref var s = ref list[i];

					if (s.X == ox && s.Y == oy && s.Z == oz && s.ID == tileID)
					{
						s.Hue = hue;

						return ref s;
					}
				}

				Array.Resize(ref list, 1 + list.Length);
			}
			else
			{
				Array.Resize(ref list, 1);
			}

			ref var tile = ref list[^1];

			tile.Set(tileID, ox, oy, oz, hue);

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

			node.SetAttribute("Count", $"{TotalCells:N0}");

			for (int x = 0, bx = 0; x < Width; x++, bx += 8)
			{
				for (int y = 0, by = 0; y < Height; y++, by += 8)
				{
					foreach (var entry in this[x, y])
					{
						var child = node.OwnerDocument.CreateElement("Static");

						child.SetAttribute("ID", $"{entry.ID}");
						child.SetAttribute("X", $"{bx + entry.X}");
						child.SetAttribute("Y", $"{by + entry.Y}");
						child.SetAttribute("Z", $"{entry.Z}");
						child.SetAttribute("Hue", $"{entry.Hue}");

						if (entry.Group >= 0)
						{
							child.SetAttribute("Group", $"{entry.Group}");
						}

						_ = node.AppendChild(child);
					}
				}
			}
		}

		public virtual bool LoadXml(string filePath)
		{
			return XmlHelper.Load(filePath, "StaticMatrix", this);
		}

		public virtual bool Load(XmlDocument doc)
		{
			return XmlHelper.Load(doc, "StaticMatrix", this);
		}

		public virtual void Load(XmlElement node)
		{
			var width = Int32.Parse(node.GetAttribute("Width"));
			var height = Int32.Parse(node.GetAttribute("Height"));

			Resize(Math.Max(width, Width), Math.Max(height, Height));

			var nodes = node.SelectNodes("Static");

			if (nodes?.Count > 0)
			{
				foreach (XmlElement child in nodes)
				{
					var x = UInt16.Parse(child.GetAttribute("X"));
					var y = UInt16.Parse(child.GetAttribute("Y"));

					if (x < Width && y < Height)
					{
						var tileID = UInt16.Parse(child.GetAttribute("ID"));
						var z = SByte.Parse(child.GetAttribute("Z"));
						var hue = UInt16.Parse(child.GetAttribute("Hue"));

						ref var entry = ref Add(x, y, z, tileID, hue);

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