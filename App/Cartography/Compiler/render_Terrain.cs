using Assets;

using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

using Photoshop;

using System.Collections;
using System.ComponentModel;
using System.Drawing.Imaging;
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
			GroupID = Utility.Parse<byte>(xmlInfo.GetAttribute("ID"));
			TileID = Utility.Parse<ushort>(xmlInfo.GetAttribute("TileID"));
			Color = Color.FromArgb(Utility.Parse<byte>(xmlInfo.GetAttribute("R")), Utility.Parse<byte>(xmlInfo.GetAttribute("G")), Utility.Parse<byte>(xmlInfo.GetAttribute("B")));
			AltID = Utility.Parse<byte>(xmlInfo.GetAttribute("Base"));

			var attribute = xmlInfo.GetAttribute("Random");

			if (StringType.StrCmp(attribute, "False", false) == 0)
			{
				RandAlt = false;
			}
			else if (StringType.StrCmp(attribute, "True", false) == 0)
			{
				RandAlt = true;
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

			try
			{
				var xmlDocument = new XmlDocument();

				xmlDocument.Load(xmlPath);

				Clear();

				foreach (XmlElement node in xmlDocument.SelectNodes("Terrains/Terrain"))
				{
					var entry = new ClsTerrain(node);

					this[entry.GroupID] = entry;
				}
			}
			catch (Exception exception)
			{
				ProjectData.SetProjectError(exception);
				_ = Interaction.MsgBox(exception.Message, MsgBoxStyle.OkOnly, null);
				_ = Interaction.MsgBox(String.Format("XMLFile:{0}", xmlPath), MsgBoxStyle.OkOnly, null);
				ProjectData.ClearProjectError();
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

		public void SaveACO()
		{
			var acoPath = Path.Combine("Development", "DrawingTools", "AdobePhotoshop", "ColorSwatches", "Terrain.aco");

			SaveSwatch(acoPath, ColorFormat.RGB);

			_ = Interaction.MsgBox("Terrain.aco Saved", MsgBoxStyle.OkOnly, null);
		}

		public void SaveACT()
		{
			var actPath = Path.Combine("Development", "DrawingTools", "AdobePhotoshop", "OptimizedColors", "Terrain.act");

			SaveTable(actPath);

			_ = Interaction.MsgBox("Terrain.act Saved", MsgBoxStyle.OkOnly, null);
		}

		#endregion
	}
}