using Photoshop;

using System.Drawing.Imaging;
using System.Text;
using System.Xml;

namespace MapCreator
{
	public struct ClsAltitude : IColorEntry
	{
		Color IColorEntry.Color { readonly get => AltitudeColor; set => AltitudeColor = value; }
		string IColorEntry.Name { readonly get => Type; set => Type = value; }

		public sbyte GetAltitude { get; set; }

		public Color AltitudeColor { get; set; }

		public string Type { get; set; }

		public ClsAltitude()
		{
		}

		public ClsAltitude(string iType, sbyte iAlt, Color iAltColor)
		{
			Type = iType;
			GetAltitude = iAlt;
			AltitudeColor = iAltColor;
		}

		public ClsAltitude(XmlElement xmlInfo)
		{
			Load(xmlInfo);
		}

		public override readonly string ToString()
		{
			return $"{Type} [{GetAltitude}]";
		}

		public readonly void Save(XmlTextWriter xmlInfo)
		{
			xmlInfo.WriteStartElement("Altitude");

			xmlInfo.WriteAttributeString("Type", Type);

			xmlInfo.WriteAttributeString("Altitude", Convert.ToString(GetAltitude));

			xmlInfo.WriteAttributeString("R", Convert.ToString(AltitudeColor.R));
			xmlInfo.WriteAttributeString("G", Convert.ToString(AltitudeColor.G));
			xmlInfo.WriteAttributeString("B", Convert.ToString(AltitudeColor.B));

			xmlInfo.WriteEndElement();
		}

		public void Load(XmlElement xmlInfo)
		{
			Type = xmlInfo.GetAttribute("Type");

			var alt = Utility.ParseNumber<int>(xmlInfo.GetAttribute("Altitude"));

			GetAltitude = (sbyte)Math.Clamp(alt, SByte.MinValue, SByte.MaxValue);

			var r = Utility.ParseNumber<byte>(xmlInfo.GetAttribute("R"));
			var g = Utility.ParseNumber<byte>(xmlInfo.GetAttribute("G"));
			var b = Utility.ParseNumber<byte>(xmlInfo.GetAttribute("B"));

			AltitudeColor = Color.FromArgb(r, g, b);
		}
	}

	public class ClsAltitudeTable : ColorCollection<ClsAltitude>
	{
		public void SetAltitude(int index, ClsAltitude Value)
		{
			this[index] = Value;
		}

		public ClsAltitude GetAltitude(int index)
		{
			return this[index];
		}

		public ClsAltitudeTable()
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

				iList.Items.Add(entry);
			}

			iList.EndUpdate();
			iList.Invalidate();
		}

		public ColorPalette GetAltPalette()
		{
			return CreatePallette();
		}

		#region Altitude Swatch And Color Table

		public void Load()
		{
			var xmlPath = Utility.FindDataFile("MapCompiler/Engine", "Altitude.xml");

			var xmlDocument = new XmlDocument();

			xmlDocument.Load(xmlPath);

			Clear();

			var index = -1;

			foreach (XmlElement node in xmlDocument.SelectNodes("Altitudes/Altitude"))
			{
				var entry = new ClsAltitude(node);

				var keyAttr = node.GetAttribute("Key");

				if (keyAttr != null)
				{
					var key = Utility.ParseNumber<int>(keyAttr);

					this[key] = entry;
				}
				else
				{
					this[++index % Length] = entry;
				}
			}
		}

		public void Save()
		{
			var xmlPath = Utility.FindDataFile("MapCompiler/Engine", "Altitude.xml");

			var xmlTextWriter = new XmlTextWriter(xmlPath, Encoding.UTF8)
			{
				Indentation = 2,
				Formatting = Formatting.Indented
			};

			xmlTextWriter.WriteStartDocument();
			xmlTextWriter.WriteStartElement("Altitudes");

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
			path = Path.Combine("Development", "DrawingTools", "AdobePhotoshop", "ColorSwatches", "Altitude.aco");

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
			path = Path.Combine("Development", "DrawingTools", "AdobePhotoshop", "OptimizedColors", "Altitude.act");

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