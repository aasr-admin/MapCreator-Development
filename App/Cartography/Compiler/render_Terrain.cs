using Assets;

using Photoshop;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;

namespace MapCreator
{
	public record class ClsTerrain : IColorEntry, INotifyPropertyChanged
	{
		public static event PropertyChangedEventHandler GlobalPropertyChanged;

		private byte _GroupID;

		[Browsable(false), Editable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public byte GroupID
		{
			get => _GroupID;
			set
			{
				_GroupID = value;

				InvokePropertyChanged();
			}
		}

		private byte _AltID;

		public byte AltID
		{
			get => _AltID;
			set
			{
				_AltID = value;

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

		private bool _RandAlt;

		public bool RandAlt
		{
			get => _RandAlt;
			set
			{
				_RandAlt = value;

				InvokePropertyChanged();
			}
		}

		private ushort _TileID;

		public ushort TileID
		{
			get => _TileID;
			set
			{
				_TileID = value;

				InvokePropertyChanged();
			}
		}

		[Browsable(false), Editable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public string GroupIDHex => $"{GroupID:X2}";

		[Browsable(false), Editable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public ref LandData Data => ref AssetData.Tiles.LandTable[TileID];

		public ClsTerrain()
		{
		}

		public ClsTerrain(string iName, byte iGroupID, ushort iTileID, Color iColor, byte iBase, bool iRandAlt)
		{
			_Name = iName;
			_GroupID = iGroupID;
			_TileID = iTileID;
			_Color = iColor;
			_AltID = iBase;
			_RandAlt = iRandAlt;
		}

		public ClsTerrain(XmlElement xmlInfo)
		{
			Load(xmlInfo);
		}

		public override string ToString()
		{
			return !_RandAlt ? $"[{_GroupID:X2}] {_Name}" : $"[{GroupID:X2}] *{_Name}";
		}

		public void Save(XmlTextWriter xmlInfo)
		{
			xmlInfo.WriteStartElement("Terrain");

			xmlInfo.WriteAttributeString("Name", _Name);
			xmlInfo.WriteAttributeString("ID", Convert.ToString(_GroupID));
			xmlInfo.WriteAttributeString("TileID", Convert.ToString(_TileID));
			xmlInfo.WriteAttributeString("R", Convert.ToString(_Color.R));
			xmlInfo.WriteAttributeString("G", Convert.ToString(_Color.G));
			xmlInfo.WriteAttributeString("B", Convert.ToString(_Color.B));
			xmlInfo.WriteAttributeString("Base", Convert.ToString(_AltID));
			xmlInfo.WriteAttributeString("Random", Convert.ToString(_RandAlt));

			xmlInfo.WriteEndElement();
		}

		public void Load(XmlElement xmlInfo)
		{
			_Name = xmlInfo.GetAttribute("Name");
			_GroupID = Utility.ParseNumber<byte>(xmlInfo.GetAttribute("ID"));
			_TileID = Utility.ParseNumber<ushort>(xmlInfo.GetAttribute("TileID"));
			_Color = Color.FromArgb(Utility.ParseNumber<byte>(xmlInfo.GetAttribute("R")), Utility.ParseNumber<byte>(xmlInfo.GetAttribute("G")), Utility.ParseNumber<byte>(xmlInfo.GetAttribute("B")));
			_AltID = Utility.ParseNumber<byte>(xmlInfo.GetAttribute("Base"));

			if (Boolean.TryParse(xmlInfo.GetAttribute("Random"), out var rand))
			{
				_RandAlt = rand;
			}
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

	public class ClsTerrainTable : ColorCollection<ClsTerrain>
	{
		public ClsTerrainTable()
			: base(256)
		{
			Init();
		}

		private void Init()
		{
			for (var i = 0; i < Length; i++)
			{
				ref var entry = ref this[i];

				entry.GroupID = (byte)i;
			}
		}

		protected override void OnClear()
		{
			base.OnClear();

			Init();
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