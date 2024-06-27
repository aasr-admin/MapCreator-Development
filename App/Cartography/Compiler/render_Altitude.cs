using Photoshop;

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;

namespace MapCreator
{
	public record class ClsAltitude : IColorEntry, INotifyPropertyChanged
	{
		public static event PropertyChangedEventHandler GlobalPropertyChanged;

		private sbyte _Altitude;

		public sbyte Altitude
		{
			get => _Altitude;
			set
			{
				_Altitude = value;

				InvokePropertyChanged();
			}
		}

		private Color _Color;

		public Color Color
		{
			get => _Color;
			set
			{
				_Color = value;

				InvokePropertyChanged();
			}
		}

		private string _Name;

		public string Name
		{
			get => _Name;
			set
			{
				_Name = value;

				InvokePropertyChanged();
			}
		}

		public ClsAltitude()
		{
		}

		public ClsAltitude(string iType, sbyte iAlt, Color iAltColor)
		{
			_Name = iType;
			_Altitude = iAlt;
			_Color = iAltColor;
		}

		public ClsAltitude(XmlElement xmlInfo)
		{
			Load(xmlInfo);
		}

		public override string ToString()
		{
			return $"[{_Altitude}z] {_Name}";
		}

		public void Save(XmlTextWriter xmlInfo)
		{
			xmlInfo.WriteStartElement("Altitude");

			xmlInfo.WriteAttributeString("Type", _Name);

			xmlInfo.WriteAttributeString("Altitude", Convert.ToString(_Altitude));

			xmlInfo.WriteAttributeString("R", Convert.ToString(_Color.R));
			xmlInfo.WriteAttributeString("G", Convert.ToString(_Color.G));
			xmlInfo.WriteAttributeString("B", Convert.ToString(_Color.B));

			xmlInfo.WriteEndElement();
		}

		public void Load(XmlElement xmlInfo)
		{
			_Name = xmlInfo.GetAttribute("Type");

			var alt = Utility.ParseNumber<int>(xmlInfo.GetAttribute("Altitude"));

			_Altitude = (sbyte)Math.Clamp(alt, SByte.MinValue, SByte.MaxValue);

			var r = Utility.ParseNumber<byte>(xmlInfo.GetAttribute("R"));
			var g = Utility.ParseNumber<byte>(xmlInfo.GetAttribute("G"));
			var b = Utility.ParseNumber<byte>(xmlInfo.GetAttribute("B"));

			_Color = Color.FromArgb(r, g, b);
		}

		event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
		{
			add => GlobalPropertyChanged += value;
			remove => GlobalPropertyChanged -= value;
		}

		private void InvokePropertyChanged([CallerMemberName] string propertyName = null)
		{
			GlobalPropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}

	public class ClsAltitudeTable : ColorCollection<ClsAltitude>
	{
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

				_ = iList.Items.Add(entry);
			}

			iList.EndUpdate();
			iList.Invalidate();
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
				++index;

				var entry = new ClsAltitude(node);

				var keyAttr = node.GetAttribute("Key");

				if (keyAttr != null)
				{
					var key = Utility.ParseNumber<int>(keyAttr);

					this[key] = entry;
				}
				else
				{
					this[index % Length] = entry;
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

				entry?.Save(xmlTextWriter);
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