using Assets;

using System.Drawing.Imaging;
using System.Xml;

namespace Cartography
{
	public class Transition : IXmlEntry
	{
		public static string GetHash(IEnumerable<LandCell> cells)
		{
			return String.Join("-", cells.Where(s => s.Group >= 0).Select(s => s.Group));
		}

		private readonly StaticCellRandomizer _Randomizer = new();

		public string Name { get; set; } = "Transition";

		public string File { get; set; }

		public LandCells LandTiles { get; } = new();
		public StaticCells StaticTiles { get; } = new();

		public string GroupHash => GetHash(LandTiles);

		public virtual LandCell RandomLandTile()
		{
			return LandTiles.RandomTile;
		}

		public virtual void AddRandomStaticTiles(StaticMatrix statics, int x, int y, sbyte z, bool randomizer)
		{
			if (StaticTiles.Count > 0)
			{
				var randomTile = StaticTiles.RandomTile;

				_ = statics.Add(x, y, (sbyte)(z + randomTile.Z), randomTile.ID);
			}

			if (randomizer)
			{
				_ = _Randomizer.Randomize(statics, x, y, z);
			}
		}

		public override string ToString()
		{
			return $"{Name}";
		}

		public Bitmap GetImage(TerrainTable terrain)
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

		public void Save(XmlElement node)
		{
			node.SetAttribute("Name", Name);
			node.SetAttribute("File", File);

			var landTilesRoot = node.OwnerDocument.CreateElement("LandTiles");

			LandTiles.Save(landTilesRoot);

			_ = node.AppendChild(landTilesRoot);

			var staticTilesRoot = node.OwnerDocument.CreateElement("StaticTiles");

			StaticTiles.Save(staticTilesRoot);

			_ = node.AppendChild(staticTilesRoot);
		}

		public void Load(XmlElement node)
		{
			Name = node.GetAttribute("Name");
			File = node.GetAttribute("File");

			if (node.SelectSingleNode("LandTiles") is XmlElement landTilesRoot)
			{
				LandTiles.Load(landTilesRoot);
			}

			if (node.SelectSingleNode("StaticTiles") is XmlElement staticTilesRoot)
			{
				StaticTiles.Load(staticTilesRoot);
			}

			if (!String.IsNullOrWhiteSpace(File))
			{
				_ = _Randomizer.LoadXml(File);
			}
		}
	}
}