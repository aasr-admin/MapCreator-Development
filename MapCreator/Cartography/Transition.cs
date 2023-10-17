using Assets;

using System.Drawing.Imaging;
using System.Xml;

namespace Cartography
{
	public class Transition : List<RandomStatics>, IXmlEntry
	{
		public static string GetHash(IEnumerable<LandCell> cells)
		{
			return String.Join("-", cells.Where(s => s.Group >= 0).Select(s => s.Group));
		}

		public string Name { get; set; } = "Transition";

		public byte Weight { get; set; }

		public LandCells LandTiles { get; } = new();
		public StaticCells StaticTiles { get; } = new();

		public string GroupHash => GetHash(LandTiles);

		public override string ToString()
		{
			return $"[{Weight}%] ({Count:N0}) {Name}";
		}

		public virtual void Apply(Facet facet, int x, int y, sbyte z, bool randomizer)
		{
			if (LandTiles.Count > 0)
			{
				var randTile = LandTiles.RandomTile;

				facet.SetLand(x, y, randTile);
			}

			if (StaticTiles.Count > 0)
			{
				var randomStatic = StaticTiles.RandomTile;

				_ = ref facet.AddStatic(x, y, (sbyte)(z + randomStatic.Z), randomStatic.ID);
			}

			if (randomizer && Count > 0)
			{
				if (Utility.RandomDouble() <= Weight / 100.0)
				{
					var random = Utility.RandomList(this);

					_ = random?.FillBlock(facet, x, y, z);
				}
			}
		}

		public virtual Bitmap GetImage(Terrains terrain)
		{
			var bitmap = new Bitmap(400, 168, PixelFormat.Format32bppArgb);

			using var graphics = Graphics.FromImage(bitmap);

			graphics.Clear(Color.White);

			graphics.DrawImage(AssetData.Art.GetLand(terrain[0].TileID), 61, 15);
			graphics.DrawImage(AssetData.Art.GetLand(terrain[1].TileID), 84, 38);
			graphics.DrawImage(AssetData.Art.GetLand(terrain[2].TileID), 107, 61);

			graphics.DrawImage(AssetData.Art.GetLand(terrain[3].TileID), 38, 38);
			graphics.DrawImage(AssetData.Art.GetLand(terrain[4].TileID), 61, 61);
			graphics.DrawImage(AssetData.Art.GetLand(terrain[5].TileID), 84, 84);

			graphics.DrawImage(AssetData.Art.GetLand(terrain[6].TileID), 15, 61);
			graphics.DrawImage(AssetData.Art.GetLand(terrain[7].TileID), 38, 84);
			graphics.DrawImage(AssetData.Art.GetLand(terrain[8].TileID), 61, 107);

			using var font = new Font("Arial", 10f);

			graphics.DrawString(ToString(), font, Brushes.Black, 151f, 2f);

			return bitmap;
		}

		public virtual void Save(XmlElement node)
		{
			node.SetAttribute("Name", Name);
			node.SetAttribute("Weight", $"{Weight}");

			var landTilesRoot = node.OwnerDocument.CreateElement("LandTiles");

			LandTiles.Save(landTilesRoot);

			_ = node.AppendChild(landTilesRoot);

			var staticTilesRoot = node.OwnerDocument.CreateElement("StaticTiles");

			StaticTiles.Save(staticTilesRoot);

			_ = node.AppendChild(staticTilesRoot);
		}

		public virtual void Load(XmlElement node)
		{
			Name = node.GetAttribute("Name");
			Weight = Byte.Parse(node.GetAttribute("Weight"));

			foreach (var entry in XmlHelper.LoadChildren<RandomStatics>(node, "Statics"))
			{
				if (entry.Count > 0)
				{
					Add(entry);
				}
			}

			if (node.SelectSingleNode("LandTiles") is XmlElement landTilesRoot)
			{
				LandTiles.Load(landTilesRoot);
			}

			if (node.SelectSingleNode("StaticTiles") is XmlElement staticTilesRoot)
			{
				StaticTiles.Load(staticTilesRoot);
			}
		}
	}
}