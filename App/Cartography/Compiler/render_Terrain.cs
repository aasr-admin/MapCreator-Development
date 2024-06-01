using Assets;

using Photoshop;

using System.ComponentModel;
using System.Text;
using System.Xml;

namespace MapCreator
{
	public struct ClsTerrain : IColorEntry
	{
		[Category("Tile Altitude")]
		public byte AltID { get; set; }

		[Category("Colour")]
		public Color Color { get; set; } = Color.Black;

		[Category("Key")]
		public byte GroupID { get; set; }

		[Category("Group ID")]
		public readonly string GroupIDHex => $"{GroupID:X}";

		[Category("Description")]
		public string Name { get; set; }

		[Category("Random Altitude")]
		public bool RandAlt { get; set; }

		[Category("Tile ID")]
		public ushort TileID { get; set; }

		public readonly ref LandData Data => ref AssetData.Tiles.LandTable[TileID];

		public ClsTerrain()
		{
		}

		public ClsTerrain(string iName, byte iGroupID, ushort iTileID, Color iColor, byte iBase, bool iRandAlt)
		{
			Name = iName;
			GroupID = iGroupID;
			TileID = iTileID;
			Color = iColor;
			AltID = iBase;
			RandAlt = iRandAlt;
		}

		public ClsTerrain(XmlElement xmlInfo)
		{
			Load(xmlInfo);
		}

		public override readonly string ToString()
		{
			return !RandAlt ? $"[{GroupID:X2}] {Name}" : $"[{GroupID:X2}] *{Name}";
		}

		public readonly void Save(XmlTextWriter xmlInfo)
		{
			xmlInfo.WriteStartElement("Terrain");

			xmlInfo.WriteAttributeString("Name", Name);
			xmlInfo.WriteAttributeString("ID", Convert.ToString(GroupID));
			xmlInfo.WriteAttributeString("TileID", Convert.ToString(TileID));
			xmlInfo.WriteAttributeString("R", Convert.ToString(Color.R));
			xmlInfo.WriteAttributeString("G", Convert.ToString(Color.G));
			xmlInfo.WriteAttributeString("B", Convert.ToString(Color.B));
			xmlInfo.WriteAttributeString("Base", Convert.ToString(AltID));
			xmlInfo.WriteAttributeString("Random", Convert.ToString(RandAlt));

			xmlInfo.WriteEndElement();
		}

		public void Load(XmlElement xmlInfo)
		{
			Name = xmlInfo.GetAttribute("Name");
			GroupID = Utility.ParseNumber<byte>(xmlInfo.GetAttribute("ID"));
			TileID = Utility.ParseNumber<ushort>(xmlInfo.GetAttribute("TileID"));
			Color = Color.FromArgb(Utility.ParseNumber<byte>(xmlInfo.GetAttribute("R")), Utility.ParseNumber<byte>(xmlInfo.GetAttribute("G")), Utility.ParseNumber<byte>(xmlInfo.GetAttribute("B")));
			AltID = Utility.ParseNumber<byte>(xmlInfo.GetAttribute("Base"));

			if (Boolean.TryParse(xmlInfo.GetAttribute("Random"), out var rand))
			{
				RandAlt = rand;
			}
		}
	}

	public class ClsTerrainTable : ColorCollection<ClsTerrain>
	{
		public ClsTerrainTable()
			: base(256)
		{
		}

		public void Display(ListBox iList)
		{
			iList.BeginUpdate();

			iList.Items.Clear();

			for (var i = 0; i < Length; i++)
			{
				ref var entry = ref this[i];

				_ = iList.Items.Add(entry);
			}

			iList.EndUpdate();
			iList.Invalidate();
		}

		public void Display(ComboBox iCombo)
		{
			iCombo.BeginUpdate();

			iCombo.Items.Clear();

			for (var i = 0; i < Length; i++)
			{
				ref var entry = ref this[i];

				_ = iCombo.Items.Add(entry);
			}

			iCombo.EndUpdate();
			iCombo.Invalidate();
		}

		#region Terrain Swatch And Color Table

		public void Load()
		{
			var xmlPath = Utility.FindDataFile("MapCompiler/Engine", "Terrain.xml");

			var xmlDocument = new XmlDocument();

			xmlDocument.Load(xmlPath);

			Clear();

			foreach (XmlElement node in xmlDocument.SelectNodes("Terrains/Terrain"))
			{
				var entry = new ClsTerrain(node);

				this[entry.GroupID] = entry;
			}
		}

		public void Save()
		{
			var xmlPath = Utility.FindDataFile("MapCompiler/Engine", "Terrain.xml");

			var xmlTextWriter = new XmlTextWriter(xmlPath, Encoding.UTF8)
			{
				Indentation = 2,
				Formatting = Formatting.Indented
			};

			xmlTextWriter.WriteStartDocument();
			xmlTextWriter.WriteStartElement("Terrains");

			for (var i = 0; i < Length; i++)
			{
				ref var entry = ref this[i];

				entry.Save(xmlTextWriter);
			}

			xmlTextWriter.WriteEndElement();
			xmlTextWriter.WriteEndDocument();

			xmlTextWriter.Close();
		}

		public bool SaveACO(out string path)
		{
			path = Path.Combine("Development", "DrawingTools", "AdobePhotoshop", "ColorSwatches", "Terrain.aco");

			try
			{
				SaveSwatch(path, ColorFormat.RGB);

				return true;
			}
			catch
			{
				return false;
			}
		}

		public bool SaveACT(out string path)
		{
			path = Path.Combine("Development", "DrawingTools", "AdobePhotoshop", "OptimizedColors", "Terrain.act");

			try
			{
				SaveTable(path);

				return true;
			}
			catch
			{
				return false;
			}
		}

		#endregion
	}
}