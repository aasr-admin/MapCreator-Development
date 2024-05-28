using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

using System.Collections;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;

using UltimaSDK;

namespace MapCreator
{
	/// Transition
	public class Transition
	{
		private readonly RandomStatics m_RandomTiles;

		#region Getters And Setters

		public string File { get; set; }

		public string Description { get; set; }

		public string HashKey
		{
			get => GetHaskKeyTable.ToString();
			set
			{
				byte b = 0;
				do
				{
					GetHaskKeyTable.Add(new HashKey(Strings.Mid(value, (b * 2) + 1, 2)));
				}
				while (++b <= 8);
			}
		}

		public MapTileCollection GetMapTiles { get; set; }

		public StaticTileCollection GetStaticTiles { get; set; }

		public HashKeyCollection GetHaskKeyTable { get; }

		#endregion

		public byte GetKey(int Index)
		{
			return GetHaskKeyTable[Index].Key;
		}

		public virtual MapTile GetRandomMapTile()
		{
			MapTile randomTile = null;
			if (GetMapTiles.Count > 0)
			{
				randomTile = GetMapTiles.RandomTile;
			}

			return randomTile;
		}

		public virtual void GetRandomStaticTiles(byte X, byte Y, sbyte Z, Collection[,] StaticMap, bool iRandom)
		{
			if (GetStaticTiles.Count > 0)
			{
				var randomTile = GetStaticTiles.RandomTile;
				StaticMap[X >> 3, Y >> 3].Add(new StaticCell(randomTile.TileID, (byte)(X % 8), (byte)(Y % 8), (sbyte)(Z + randomTile.AltIDMod)), null, null, null);
			}

			if (iRandom)
			{
				m_RandomTiles?.GetRandomStatic(X, Y, Z, StaticMap);
			}
		}

		public void Clone(ClsTerrain iGroupA, ClsTerrain iGroupB)
		{
			Description = Description.Replace(iGroupA.Name, iGroupB.Name);
			var num = 0;

			do
			{
				if (GetHaskKeyTable[num].Key == iGroupA.GroupID)
				{
					GetHaskKeyTable[num].Key = iGroupB.GroupID;
				}
			}
			while (++num <= 8);
		}

		public void SetHashKey(int iKey, byte iKeyHash)
		{
			GetHaskKeyTable[iKey].Key = iKeyHash;
		}

		public void AddMapTile(ushort TileID, sbyte AltIDMod)
		{
			GetMapTiles.Add(new MapTile(TileID, AltIDMod));
		}

		public void RemoveMapTile(MapTile iMapTile)
		{
			GetMapTiles.Remove(iMapTile);
		}

		public void AddStaticTile(ushort TileID, sbyte AltIDMod)
		{
			GetStaticTiles.Add(new StaticTile(TileID, AltIDMod));
		}

		public void RemoveStaticTile(StaticTile iStaticTile)
		{
			GetStaticTiles.Remove(iStaticTile);
		}

		public override string ToString()
		{
			return String.Format("[{0}] {1}", GetHaskKeyTable.ToString(), Description);
		}

		public Bitmap TransitionImage(ClsTerrainTable iTerrain)
		{
			var bitmap = new Bitmap(400, 168, PixelFormat.Format16bppRgb555);
			var graphics = Graphics.FromImage(bitmap);
			var font = new Font("Arial", 10f);
			var graphics2 = graphics;
			graphics2.Clear(Color.White);
			var arg_5E_0 = graphics2;
			Image arg_5E_1 = Art.GetLand(iTerrain.TerrianGroup(0).TileID);
			var point = new Point(61, 15);
			arg_5E_0.DrawImage(arg_5E_1, point);
			var arg_85_0 = graphics2;
			Image arg_85_1 = Art.GetLand(iTerrain.TerrianGroup(1).TileID);
			point = new Point(84, 38);
			arg_85_0.DrawImage(arg_85_1, point);
			var arg_AC_0 = graphics2;
			Image arg_AC_1 = Art.GetLand(iTerrain.TerrianGroup(2).TileID);
			point = new Point(107, 61);
			arg_AC_0.DrawImage(arg_AC_1, point);
			var arg_D3_0 = graphics2;
			Image arg_D3_1 = Art.GetLand(iTerrain.TerrianGroup(3).TileID);
			point = new Point(38, 38);
			arg_D3_0.DrawImage(arg_D3_1, point);
			var arg_FA_0 = graphics2;
			Image arg_FA_1 = Art.GetLand(iTerrain.TerrianGroup(4).TileID);
			point = new Point(61, 61);
			arg_FA_0.DrawImage(arg_FA_1, point);
			var arg_121_0 = graphics2;
			Image arg_121_1 = Art.GetLand(iTerrain.TerrianGroup(5).TileID);
			point = new Point(84, 84);
			arg_121_0.DrawImage(arg_121_1, point);
			var arg_148_0 = graphics2;
			Image arg_148_1 = Art.GetLand(iTerrain.TerrianGroup(6).TileID);
			point = new Point(15, 61);
			arg_148_0.DrawImage(arg_148_1, point);
			var arg_16F_0 = graphics2;
			Image arg_16F_1 = Art.GetLand(iTerrain.TerrianGroup(7).TileID);
			point = new Point(38, 84);
			arg_16F_0.DrawImage(arg_16F_1, point);
			var arg_196_0 = graphics2;
			Image arg_196_1 = Art.GetLand(iTerrain.TerrianGroup(8).TileID);
			point = new Point(61, 107);
			arg_196_0.DrawImage(arg_196_1, point);
			graphics2.DrawString(ToString(), font, Brushes.Black, 151f, 2f);
			graphics.Dispose();
			return bitmap;
		}

		public void Save(XmlTextWriter xmlInfo)
		{
			xmlInfo.WriteStartElement("TransInfo");
			xmlInfo.WriteAttributeString("Description", Description);
			xmlInfo.WriteAttributeString("HashKey", GetHaskKeyTable.ToString());
			if (File != null)
			{
				xmlInfo.WriteAttributeString("File", File);
			}

			GetMapTiles.Save(xmlInfo);
			GetStaticTiles.Save(xmlInfo);
			xmlInfo.WriteEndElement();
		}

		public Transition(XmlElement xmlInfo)
		{
			GetHaskKeyTable = [];
			GetStaticTiles = [];
			GetMapTiles = [];
			m_RandomTiles = null;
			File = null;
			Description = xmlInfo.GetAttribute("Description");
			GetHaskKeyTable.AddHashKey(xmlInfo.GetAttribute("HashKey"));
			if (StringType.StrCmp(xmlInfo.GetAttribute("File"), String.Empty, false) != 0)
			{
				m_RandomTiles = new RandomStatics(xmlInfo.GetAttribute("File"));
				File = xmlInfo.GetAttribute("File");
			}

			GetMapTiles.Load(xmlInfo);
			GetStaticTiles.Load(xmlInfo);
		}

		public Transition()
		{
			GetHaskKeyTable = [];
			GetStaticTiles = [];
			GetMapTiles = [];
			m_RandomTiles = null;
			File = null;
			Description = "<New Transition>";
			GetHaskKeyTable.Clear();
			var b = 0;
			do
			{
				GetHaskKeyTable.Add(new HashKey());
			}
			while (++b <= 8);
		}

		public Transition(string iDescription, string iHashKey, MapTileCollection iMapTiles, StaticTileCollection iStaticTiles)
		{
			GetHaskKeyTable = [];
			GetStaticTiles = [];
			GetMapTiles = [];
			m_RandomTiles = null;
			File = null;
			Description = iDescription;
			GetHaskKeyTable.AddHashKey(iHashKey);

			var enumerator = iMapTiles.GetEnumerator();

			try
			{
				while (enumerator.MoveNext())
				{
					var value = (MapTile)enumerator.Current;
					GetMapTiles.Add(value);
				}
			}
			finally
			{
				if (enumerator is IDisposable)
				{
					((IDisposable)enumerator).Dispose();
				}
			}

			var enumerator2 = iStaticTiles.GetEnumerator();

			try
			{
				while (enumerator2.MoveNext())
				{
					var value2 = (StaticTile)enumerator2.Current;
					GetStaticTiles.Add(value2);
				}
			}
			finally
			{
				if (enumerator2 is IDisposable)
				{
					((IDisposable)enumerator2).Dispose();
				}
			}
		}

		public Transition(string iDescription, ClsTerrain iGroupA, ClsTerrain iGroupB, string iHashKey)
		{
			GetHaskKeyTable = [];
			GetStaticTiles = [];
			GetMapTiles = [];
			m_RandomTiles = null;
			File = null;
			Description = iDescription;
			var b = 0;
			do
			{
				var sLeft = Strings.Mid(iHashKey, b + 1, 1);
				if (StringType.StrCmp(sLeft, "A", false) == 0)
				{
					GetHaskKeyTable.Add(new HashKey(iGroupA.GroupID));
				}
				else if (StringType.StrCmp(sLeft, "B", false) == 0)
				{
					GetHaskKeyTable.Add(new HashKey(iGroupB.GroupID));
				}
			}
			while (++b <= 8);
		}

		public Transition(string iDescription, string iHashKey, ClsTerrain iGroupA, ClsTerrain iGroupB, MapTileCollection iMapTiles, StaticTileCollection iStaticTiles)
		{
			GetHaskKeyTable = [];
			GetStaticTiles = [];
			GetMapTiles = [];
			m_RandomTiles = null;
			File = null;
			Description = iDescription;
			var b = 0;
			do
			{
				var sLeft = Strings.Mid(iHashKey, b + 1, 1);
				if (StringType.StrCmp(sLeft, "A", false) == 0)
				{
					GetHaskKeyTable.Add(new HashKey(iGroupA.GroupID));
				}
				else if (StringType.StrCmp(sLeft, "B", false) == 0)
				{
					GetHaskKeyTable.Add(new HashKey(iGroupB.GroupID));
				}
			}
			while (++b <= 8);
			if (iMapTiles != null)
			{
				var enumerator = iMapTiles.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						var value = (MapTile)enumerator.Current;
						GetMapTiles.Add(value);
					}
				}
				finally
				{
					if (enumerator is IDisposable)
					{
						((IDisposable)enumerator).Dispose();
					}
				}
			}

			if (iStaticTiles != null)
			{
				var enumerator2 = iStaticTiles.GetEnumerator();

				try
				{
					while (enumerator2.MoveNext())
					{
						var value2 = (StaticTile)enumerator2.Current;
						GetStaticTiles.Add(value2);
					}
				}
				finally
				{
					if (enumerator2 is IDisposable)
					{
						((IDisposable)enumerator2).Dispose();
					}
				}
			}
		}

		public Transition(string iDescription, ClsTerrain iGroupA, ClsTerrain iGroupB, ClsTerrain iGroupC, string iHashKey)
		{
			GetHaskKeyTable = [];
			GetStaticTiles = [];
			GetMapTiles = [];
			m_RandomTiles = null;
			File = null;
			Description = iDescription;
			var b = 0;
			do
			{
				var sLeft = Strings.Mid(iHashKey, b + 1, 1);
				if (StringType.StrCmp(sLeft, "A", false) == 0)
				{
					GetHaskKeyTable.Add(new HashKey(iGroupA.GroupID));
				}
				else if (StringType.StrCmp(sLeft, "B", false) == 0)
				{
					GetHaskKeyTable.Add(new HashKey(iGroupB.GroupID));
				}
				else if (StringType.StrCmp(sLeft, "C", false) == 0)
				{
					GetHaskKeyTable.Add(new HashKey(iGroupC.GroupID));
				}
			}
			while (++b <= 8);
		}

		public Transition(string iDescription, string iHashKey)
		{
			GetHaskKeyTable = [];
			GetStaticTiles = [];
			GetMapTiles = [];
			m_RandomTiles = null;
			File = null;
			Description = iDescription;
			var b = 0;
			do
			{
				GetHaskKeyTable.Add(new HashKey(Strings.Mid(iHashKey, (b * 2) + 1, 2)));
			}
			while (++b <= 8);
		}

		public Transition(string iDescription, ClsTerrain iGroupA, ClsTerrain iGroupB, string iHashKey, MapTile iMapTile)
		{
			GetHaskKeyTable = [];
			GetStaticTiles = [];
			GetMapTiles = [];
			m_RandomTiles = null;
			File = null;
			Description = iDescription;
			var b = 0;
			do
			{
				var sLeft = Strings.Mid(iHashKey, b + 1, 1);
				if (StringType.StrCmp(sLeft, "A", false) == 0)
				{
					GetHaskKeyTable.Add(new HashKey(iGroupA.GroupID));
				}
				else if (StringType.StrCmp(sLeft, "B", false) == 0)
				{
					GetHaskKeyTable.Add(new HashKey(iGroupB.GroupID));
				}
			}
			while (++b <= 8);
			GetMapTiles.Add(iMapTile);
		}
	}

	/// TransitionTable
	public class TransitionTable
	{
		#region Getters And Setters

		public Hashtable GetTransitionTable { get; }

		#endregion

		public Transition Transition(int iKey)
		{
			return (Transition)GetTransitionTable[iKey];
		}

		public TransitionTable()
		{
			GetTransitionTable = [];
		}

		public void Clear()
		{
			GetTransitionTable.Clear();
		}

		public void Add(Transition iValue)
		{
			try
			{
				GetTransitionTable.Add(iValue.HashKey, iValue);
			}
			catch (Exception expr_17)
			{
				ProjectData.SetProjectError(expr_17);
				var ex = expr_17;
				_ = Interaction.MsgBox(ex.Message, MsgBoxStyle.OkOnly, null);
				ProjectData.ClearProjectError();
			}
		}

		public void Remove(Transition iValue)
		{
			GetTransitionTable.Remove(iValue.HashKey);
		}

		public void Display(ListBox iList)
		{
			iList.Items.Clear();

			var enumerator = GetTransitionTable.Values.GetEnumerator();

			try
			{
				while (enumerator.MoveNext())
				{
					var item = (Transition)enumerator.Current;
					_ = iList.Items.Add(item);
				}
			}
			finally
			{
				if (enumerator is IDisposable)
				{
					((IDisposable)enumerator).Dispose();
				}
			}
		}

		public void Load(string iFilename)
		{
			var xmlDocument = new XmlDocument();
			try
			{
				xmlDocument.Load(iFilename);

				var enumerator = xmlDocument.SelectNodes("//Trans/TransInfo").GetEnumerator();

				try
				{
					while (enumerator.MoveNext())
					{
						var xmlInfo = (XmlElement)enumerator.Current;
						var transition = new Transition(xmlInfo);
						GetTransitionTable.Add(transition.HashKey, transition);
					}
				}
				finally
				{
					if (enumerator is IDisposable)
					{
						((IDisposable)enumerator).Dispose();
					}
				}
			}
			catch (Exception expr_74)
			{
				ProjectData.SetProjectError(expr_74);
				_ = Interaction.MsgBox(String.Format("XMLFile:{0}", iFilename), MsgBoxStyle.OkOnly, null);
				ProjectData.ClearProjectError();
			}
		}

		public void Save(string iFilename)
		{
			var saveFileDialog = new SaveFileDialog
			{
				FileName = iFilename,
				Filter = "xml files (*.xml)|*.xml"
			};
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				var xmlTextWriter = new XmlTextWriter(saveFileDialog.FileName, Encoding.UTF8)
				{
					Indentation = 2,
					Formatting = Formatting.Indented
				};
				xmlTextWriter.WriteStartDocument();
				xmlTextWriter.WriteStartElement("Trans");

				var enumerator = GetTransitionTable.Values.GetEnumerator();

				try
				{
					while (enumerator.MoveNext())
					{
						var transition = (Transition)enumerator.Current;
						transition.Save(xmlTextWriter);
					}
				}
				finally
				{
					if (enumerator is IDisposable)
					{
						((IDisposable)enumerator).Dispose();
					}
				}

				xmlTextWriter.WriteEndElement();
				xmlTextWriter.WriteEndDocument();
				xmlTextWriter.Close();
			}
		}

		public void Save(string iPath, string iFilename)
		{
			var xmlTextWriter = new XmlTextWriter(String.Format("{0}/{1}.xml", iPath, iFilename), Encoding.UTF8)
			{
				Indentation = 2,
				Formatting = Formatting.Indented
			};
			xmlTextWriter.WriteStartDocument();
			xmlTextWriter.WriteStartElement("Trans");

			var enumerator = GetTransitionTable.Values.GetEnumerator();

			try
			{
				while (enumerator.MoveNext())
				{
					var transition = (Transition)enumerator.Current;
					transition.Save(xmlTextWriter);
				}
			}
			finally
			{
				if (enumerator is IDisposable)
				{
					((IDisposable)enumerator).Dispose();
				}
			}

			xmlTextWriter.WriteEndElement();
			xmlTextWriter.WriteEndDocument();
			xmlTextWriter.Close();
		}

		public void MassLoad(string iPath)
		{
			ProcessDirectory(iPath);
		}

		public void ProcessDirectory(string targetDirectory)
		{
			var files = Directory.GetFiles(targetDirectory, "*.xml");
			var array = files;

			for (var i = 0; i < array.Length; i++)
			{
				var iFilename = array[i];
				Load(iFilename);
			}

			var directories = Directory.GetDirectories(targetDirectory);
			var array2 = directories;
			for (var j = 0; j < array2.Length; j++)
			{
				var targetDirectory2 = array2[j];
				ProcessDirectory(targetDirectory2);
			}
		}
	}

	/// HashKey
	public class HashKey
	{
		#region Getters And Setters

		public byte Key { get; set; }

		#endregion

		public HashKey()
		{
			Key = 0;
		}

		public HashKey(int Key)
		{
			this.Key = Convert.ToByte(Key);
		}

		public HashKey(byte Key)
		{
			this.Key = Key;
		}

		public HashKey(string Key)
		{
			this.Key = Byte.Parse(Key);
		}

		public override string ToString()
		{
			return String.Format("{0:X2}", Key);
		}
	}

	/// HashKeyCollection
	public class HashKeyCollection : CollectionBase
	{
		#region Getters And Setters

		public HashKey this[int index]
		{
			get => (HashKey)List[index];
			set => List[index] = value;
		}

		#endregion

		public HashKeyCollection()
		{
		}

		public void Add(HashKey Value)
		{
			if (InnerList.Count <= 9)
			{
				_ = InnerList.Add(Value);
			}
		}

		public void AddHashKey(string Value)
		{
			InnerList.Clear();
			var num = 0;
			do
			{
				Add(new HashKey(Strings.Mid(Value, (num * 2) + 1, 2)));
			}
			while (++num <= 8);
		}

		public void Remove(HashKey Value)
		{
			InnerList.Remove(Value);
		}

		public override string ToString()
		{
			var key = new object[] { this[0].Key, this[1].Key, this[2].Key, this[3].Key, this[4].Key, this[5].Key, this[6].Key, this[7].Key, this[8].Key };
			return String.Format("{0:X2}{1:X2}{2:X2}{3:X2}{4:X2}{5:X2}{6:X2}{7:X2}{8:X2}", key);
		}
	}

	/// ImportTiles
	public class ImportTiles
	{
		public ImportTiles(Collection[,] StaticMap, string iPath)
		{
			iPath += "\\Import\\";
			if (!Directory.Exists(iPath))
			{
				_ = Interaction.MsgBox("No Import Directory Was Found!", MsgBoxStyle.OkOnly, null);
			}
			else
			{
				ProcessDirectory(StaticMap, iPath);
			}
		}

		public void Load(Collection[,] StaticMap, string iFilename)
		{
			var xmlDocument = new XmlDocument();
			try
			{
				xmlDocument.Load(iFilename);
				var xmlElement = (XmlElement)xmlDocument.SelectSingleNode("//Static_Tiles");

				var enumerator = xmlElement.SelectNodes("Tile").GetEnumerator();

				try
				{
					while (enumerator.MoveNext())
					{
						var xmlElement2 = (XmlElement)enumerator.Current;
						var iTileID = Utility.Parse<ushort>(xmlElement2.GetAttribute("TileID"));
						var iX = Utility.Parse<byte>(xmlElement2.GetAttribute("X"));
						var iY = Utility.Parse<byte>(xmlElement2.GetAttribute("Y"));
						var iZ = Utility.Parse<sbyte>(xmlElement2.GetAttribute("Z"));
						var iHue = Utility.Parse<ushort>(xmlElement2.GetAttribute("Hue"));
						var item = checked(new StaticCell(iTileID, (byte)(iX % 8), (byte)(iY % 8), iZ, iHue));
						StaticMap[iX >> 3, iY >> 3].Add(item, null, null, null);
					}
				}
				finally
				{
					if (enumerator is IDisposable)
					{
						((IDisposable)enumerator).Dispose();
					}
				}
			}
			catch (Exception expr_FB)
			{
				ProjectData.SetProjectError(expr_FB);
				_ = Interaction.MsgBox("Can not find:" + iFilename, MsgBoxStyle.OkOnly, null);
				ProjectData.ClearProjectError();
			}
		}

		public void ProcessDirectory(Collection[,] StaticMap, string targetDirectory)
		{
			var files = Directory.GetFiles(targetDirectory, "*.xml");
			var array = files;
			for (var i = 0; i < array.Length; i++)
			{
				var iFilename = array[i];
				Load(StaticMap, iFilename);
			}

			var directories = Directory.GetDirectories(targetDirectory);
			var array2 = directories;
			for (var j = 0; j < array2.Length; j++)
			{
				var targetDirectory2 = array2[j];
				ProcessDirectory(StaticMap, targetDirectory2);
			}
		}
	}

	/// MapCell
	public class MapCell
	{
		#region Getters And Setters

		public byte GroupID { get; }

		public ushort TileID { get; set; }

		public sbyte AltID { get; set; }

		#endregion

		public void ChangeAltID(int AltMOD)
		{
			AltID = (sbyte)unchecked(AltID + AltMOD);
		}

		public MapCell(byte i_Terrian, sbyte i_Alt)
		{
			GroupID = i_Terrian;
			TileID = 0;
			AltID = i_Alt;
		}

		public void WriteMapMul(BinaryWriter i_MapFile)
		{
			i_MapFile.Write(TileID);
			if (AltID <= -127)
			{
				AltID = -127;
			}

			if (AltID >= 127)
			{
				AltID = 127;
			}

			i_MapFile.Write(AltID);
		}
	}

	/// MapTile
	public class MapTile
	{
		#region Getters And Setters

		public ushort TileID { get; set; }

		public sbyte AltIDMod { get; set; }

		#endregion

		public override string ToString()
		{
			return String.Format("{0:X4} [{1}]", TileID, AltIDMod);
		}

		public MapTile(ushort tileID, sbyte AltID)
		{
			TileID = tileID;
			AltIDMod = AltID;
		}

		public MapTile()
		{
		}

		public MapTile(XmlElement xmlInfo)
		{
			TileID = Utility.Parse<ushort>(xmlInfo.GetAttribute("TileID"));
			AltIDMod = unchecked((sbyte)Utility.Parse<int>(xmlInfo.GetAttribute("AltIDMod")));
		}

		public void Save(XmlTextWriter xmlInfo)
		{
			xmlInfo.WriteStartElement("MapTile");
			xmlInfo.WriteAttributeString("TileID", Convert.ToString(TileID));
			xmlInfo.WriteAttributeString("AltIDMod", Convert.ToString(AltIDMod));
			xmlInfo.WriteEndElement();
		}
	}

	/// MapTileCollection
	public class MapTileCollection : CollectionBase
	{
		#region Getters And Setters

		public MapTile this[int index]
		{
			get => (MapTile)List[index];
			set => List[index] = value;
		}

		public MapTile RandomTile
		{
			get
			{
				var num = (int)Math.Round(VBMath.Rnd() * (List.Count - 1));
				return (MapTile)List[num];
			}
		}

		#endregion

		public void Add(MapTile Value)
		{
			_ = InnerList.Add(Value);
		}

		public void Remove(MapTile Value)
		{
			InnerList.Remove(Value);
		}

		public void Save(XmlTextWriter xmlInfo)
		{
			xmlInfo.WriteStartElement("MapTiles");

			var enumerator = InnerList.GetEnumerator();

			try
			{
				while (enumerator.MoveNext())
				{
					var mapTile = (MapTile)enumerator.Current;
					mapTile.Save(xmlInfo);
				}
			}
			finally
			{
				if (enumerator is IDisposable)
				{
					((IDisposable)enumerator).Dispose();
				}
			}

			xmlInfo.WriteEndElement();
		}

		public void Load(XmlElement xmlInfo)
		{

			var enumerator = xmlInfo.SelectNodes("MapTiles").GetEnumerator();

			try
			{
				while (enumerator.MoveNext())
				{
					var xmlElement = (XmlElement)enumerator.Current;

					var enumerator2 = xmlElement.SelectNodes("MapTile").GetEnumerator();

					try
					{
						while (enumerator2.MoveNext())
						{
							var xmlInfo2 = (XmlElement)enumerator2.Current;
							_ = InnerList.Add(new MapTile(xmlInfo2));
						}
					}
					finally
					{
						if (enumerator2 is IDisposable)
						{
							((IDisposable)enumerator2).Dispose();
						}
					}
				}
			}
			finally
			{
				if (enumerator is IDisposable)
				{
					((IDisposable)enumerator).Dispose();
				}
			}
		}

		public void Display(ListBox iList)
		{
			iList.Items.Clear();

			var enumerator = InnerList.GetEnumerator();

			try
			{
				while (enumerator.MoveNext())
				{
					var item = (MapTile)enumerator.Current;
					_ = iList.Items.Add(item);
				}
			}
			finally
			{
				if (enumerator is IDisposable)
				{
					((IDisposable)enumerator).Dispose();
				}
			}
		}
	}

	/// RandomStatic
	public class RandomStatic : INotifyPropertyChanged
	{
		private ushort _TileID;

		public ushort TileID
		{
			get => _TileID;
			set
			{
				_TileID = value;

				OnPropertyChanged();
			}
		}

		private sbyte _X;

		public sbyte X
		{
			get => _X;
			set
			{
				_X = value;

				OnPropertyChanged();
			}
		}

		private sbyte _Y;

		public sbyte Y
		{
			get => _Y;
			set
			{
				_Y = value;

				OnPropertyChanged();
			}
		}

		private sbyte _Z;

		public sbyte Z
		{
			get => _Z;
			set
			{
				_Z = value;

				OnPropertyChanged();
			}
		}

		private ushort _Hue;

		public ushort Hue
		{
			get => _Hue;
			set
			{
				_Hue = value;

				OnPropertyChanged();
			}
		}

		public ref ItemData Data => ref TileData.ItemTable[TileID];

		public event PropertyChangedEventHandler PropertyChanged;

		public RandomStatic()
		{
		}

		public RandomStatic(ushort iTileID, sbyte iXMod, sbyte iYMod, sbyte iZMod, ushort iHueMod)
		{
			_TileID = iTileID;
			_X = iXMod;
			_Y = iYMod;
			_Z = iZMod;
			_Hue = iHueMod;
		}

		public RandomStatic(XmlElement xmlInfo)
		{
			Load(xmlInfo);
		}

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public void Load(XmlElement xmlInfo)
		{
			TileID = Utility.Parse<ushort>(xmlInfo.GetAttribute("TileID"));
			X = Utility.Parse<sbyte>(xmlInfo.GetAttribute("X"));
			Y = Utility.Parse<sbyte>(xmlInfo.GetAttribute("Y"));
			Z = Utility.Parse<sbyte>(xmlInfo.GetAttribute("Z"));
			Hue = Utility.Parse<ushort>(xmlInfo.GetAttribute("Hue"));
		}

		public void Save(XmlTextWriter xmlInfo)
		{
			xmlInfo.WriteStartElement("Static");
			xmlInfo.WriteAttributeString("TileID", Convert.ToString(TileID));
			xmlInfo.WriteAttributeString("X", Convert.ToString(X));
			xmlInfo.WriteAttributeString("Y", Convert.ToString(Y));
			xmlInfo.WriteAttributeString("Z", Convert.ToString(Z));
			xmlInfo.WriteAttributeString("Hue", Convert.ToString(Hue));
			xmlInfo.WriteEndElement();
		}

		public override string ToString()
		{
			var name = Data.Name;

			if (String.IsNullOrWhiteSpace(name))
			{
				name = "Static";
			}

			return $"{name} [0x{TileID:X4} | {TileID:D5}] ({X}, {Y}, {Z})";
		}
	}

	/// RandomStaticCollection
	public class RandomStaticCollection : BindingList<RandomStatic>, INotifyPropertyChanged
	{
		private string _Description = "Statics Group";

		public string Description
		{
			get => _Description;
			set
			{
				if (String.IsNullOrWhiteSpace(value))
				{
					value = "Statics Group";
				}

				_Description = value;

				OnPropertyChanged();
			}
		}

		private int _Frequency = 100;

		public int Frequency
		{
			get => _Frequency;
			set
			{
				_Frequency = Math.Clamp(value, 0, 100);

				OnPropertyChanged();
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public RandomStaticCollection()
		{
		}

		public RandomStaticCollection(string iDescription, int iFreq)
		{
			Description = iDescription;
			Frequency = iFreq;
		}

		public RandomStaticCollection(XmlElement xmlInfo)
		{
			Load(xmlInfo);
		}

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public void Load(XmlElement xmlInfo)
		{
			Description = xmlInfo.GetAttribute("Description");
			Frequency = Utility.Parse<ushort>(xmlInfo.GetAttribute("Freq"));

			foreach (XmlElement node in xmlInfo.SelectNodes("Static"))
			{
				Add(new RandomStatic(node));
			}
		}

		public void Save(XmlTextWriter xmlInfo)
		{
			xmlInfo.WriteStartElement("Statics");
			xmlInfo.WriteAttributeString("Description", _Description);
			xmlInfo.WriteAttributeString("Freq", Convert.ToString(_Frequency));

			foreach (var o in this)
			{
				o.Save(xmlInfo);
			}

			xmlInfo.WriteEndElement();
		}

		public void RandomStatic(byte X, byte Y, sbyte Z, Collection[,] staticMap)
		{
			foreach (var o in this)
			{
				var x = (X + o.X) >> 3;
				var y = (Y + o.Y) >> 3;

				if (x >= staticMap.GetLength(0))
				{
					continue;
				}

				if (y >= staticMap.GetLength(1))
				{
					continue;
				}

				var item = new StaticCell(o.TileID, (byte)((X + o.X) % 8), (byte)((Y + o.Y) % 8), (sbyte)(Z + o.Z));

				staticMap[x, y].Add(item, null, null, null);
			}
		}

		public override string ToString()
		{
			var desc = Description;

			if (String.IsNullOrWhiteSpace(desc))
			{
				desc = GetType().Name;
			}

			return $"{desc} [{Frequency:P}] ({Count:N})";
		}
	}

	/// RandomStatics
	public class RandomStatics : BindingList<RandomStaticCollection>, INotifyPropertyChanged
	{
		private int _Chance;

		public int Chance
		{
			get => _Chance;
			set
			{
				_Chance = Math.Clamp(value, 0, 100);

				OnPropertyChanged();
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public RandomStatics()
		{
		}

		public RandomStatics(string iFileName)
		{
			Load(iFileName);
		}

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public void Load(string iFileName)
		{
			Chance = 0;

			Clear();

			var xmlDocument = new XmlDocument();

			var filename = Path.Combine(Environment.CurrentDirectory, "MapCompiler", "Engine", "TerrainTypes", iFileName);

			xmlDocument.Load(filename);

			var xmlElement = (XmlElement)xmlDocument.SelectSingleNode("//RandomStatics");

			Chance = Utility.Parse<int>(xmlElement.GetAttribute("Chance"));

			foreach (XmlElement xmlInfo in xmlElement.SelectNodes("Statics"))
			{
				Add(new RandomStaticCollection(xmlInfo));
			}
		}

		public void Save(string iFileName)
		{
			var xmlTextWriter = new XmlTextWriter(iFileName, Encoding.UTF8)
			{
				Indentation = 2,
				Formatting = Formatting.Indented
			};

			xmlTextWriter.WriteStartDocument();
			xmlTextWriter.WriteStartElement("RandomStatics");
			xmlTextWriter.WriteAttributeString("Chance", Convert.ToString(Chance));

			foreach (var scol in this)
			{
				scol.Save(xmlTextWriter);
			}

			xmlTextWriter.WriteEndElement();
			xmlTextWriter.WriteEndDocument();
			xmlTextWriter.Close();
		}

		public void GetRandomStatic(byte X, byte Y, sbyte Z, Collection[,] StaticMap)
		{
			if (Count > 0 && Utility.Random(0, 100) <= Chance)
			{
				this[Utility.Random(Count)].RandomStatic(X, Y, Z, StaticMap);
			}
		}

		public override string ToString()
		{
			var name = GetType().Name;

			return $"{name} [{Chance:P}] ({Count:N})";
		}
	}

	/// StaticCell
	public readonly struct StaticCell
	{
		private readonly ushort m_TileID;
		private readonly byte m_X;
		private readonly byte m_Y;
		private readonly sbyte m_Z;
		private readonly ushort m_Hue;

		public StaticCell(ushort iTileID, byte iX, byte iY, sbyte iZ)
		{
			m_Hue = 0;
			m_TileID = iTileID;
			m_X = iX;
			m_Y = iY;
			m_Z = iZ;
		}

		public StaticCell(ushort iTileID, byte iX, byte iY, sbyte iZ, ushort iHue)
		{
			m_Hue = 0;
			m_TileID = iTileID;
			m_X = iX;
			m_Y = iY;
			m_Z = iZ;
			m_Hue = iHue;
		}

		public void Write(BinaryWriter i_StaticFile)
		{
			try
			{
				i_StaticFile.Write(m_TileID);
				i_StaticFile.Write(m_X);
				i_StaticFile.Write(m_Y);
				i_StaticFile.Write(m_Z);
				i_StaticFile.Write(m_Hue);
			}
			catch (Exception expr_45)
			{
				ProjectData.SetProjectError(expr_45);
				_ = Interaction.MsgBox(String.Format("Error [{0}] X:{1} Y:{2} Z:{3} Hue:{4}", m_TileID, m_X, m_Y, m_Z, m_Hue), MsgBoxStyle.OkOnly, null);
				ProjectData.ClearProjectError();
			}
		}
	}

	/// StaticTile
	public class StaticTile
	{
		#region Getters And Setters

		public ushort TileID { get; set; }

		public sbyte AltIDMod { get; set; }

		#endregion

		public override string ToString()
		{
			return String.Format("{0:X4} [{1}]", TileID, AltIDMod);
		}

		public StaticTile()
		{
		}

		public StaticTile(ushort TileID, sbyte AltIDMod)
		{
			this.TileID = TileID;
			this.AltIDMod = AltIDMod;
		}

		public StaticTile(XmlElement xmlInfo)
		{
			TileID = Utility.Parse<ushort>(xmlInfo.GetAttribute("TileID"));
			AltIDMod = unchecked((sbyte)Utility.Parse<int>(xmlInfo.GetAttribute("AltIDMod")));
		}

		public void Save(XmlTextWriter xmlInfo)
		{
			xmlInfo.WriteStartElement("StaticTile");
			xmlInfo.WriteAttributeString("TileID", Convert.ToString(TileID));
			xmlInfo.WriteAttributeString("AltIDMod", Convert.ToString(AltIDMod));
			xmlInfo.WriteEndElement();
		}
	}

	/// StaticTileCollection
	public class StaticTileCollection : CollectionBase
	{
		#region Getters And Setters

		public StaticTile this[int index]
		{
			get => (StaticTile)List[index];
			set => List[index] = value;
		}

		public StaticTile RandomTile
		{
			get
			{
				var num = (int)Math.Round(VBMath.Rnd() * (List.Count - 1));
				return (StaticTile)List[num];
			}
		}

		#endregion

		public void Add(StaticTile Value)
		{
			_ = InnerList.Add(Value);
		}

		public void Remove(StaticTile Value)
		{
			InnerList.Remove(Value);
		}

		public void Save(XmlTextWriter xmlInfo)
		{
			xmlInfo.WriteStartElement("StaticTiles");

			var enumerator = InnerList.GetEnumerator();

			try
			{
				while (enumerator.MoveNext())
				{
					var staticTile = (StaticTile)enumerator.Current;
					staticTile.Save(xmlInfo);
				}
			}
			finally
			{
				if (enumerator is IDisposable)
				{
					((IDisposable)enumerator).Dispose();
				}
			}

			xmlInfo.WriteEndElement();
		}

		public void Load(XmlElement xmlInfo)
		{
			var enumerator = xmlInfo.SelectNodes("StaticTiles").GetEnumerator();

			try
			{
				while (enumerator.MoveNext())
				{
					var xmlElement = (XmlElement)enumerator.Current;

					var enumerator2 = xmlElement.SelectNodes("StaticTile").GetEnumerator();

					try
					{
						while (enumerator2.MoveNext())
						{
							var xmlInfo2 = (XmlElement)enumerator2.Current;
							_ = InnerList.Add(new StaticTile(xmlInfo2));
						}
					}
					finally
					{
						if (enumerator2 is IDisposable)
						{
							((IDisposable)enumerator2).Dispose();
						}
					}
				}
			}
			finally
			{
				if (enumerator is IDisposable)
				{
					((IDisposable)enumerator).Dispose();
				}
			}
		}

		public void Display(ListBox iList)
		{
			iList.Items.Clear();
			var enumerator = InnerList.GetEnumerator();

			try
			{
				while (enumerator.MoveNext())
				{
					var item = (StaticTile)enumerator.Current;
					_ = iList.Items.Add(item);
				}
			}
			finally
			{
				if (enumerator is IDisposable)
				{
					((IDisposable)enumerator).Dispose();
				}
			}
		}
	}
}