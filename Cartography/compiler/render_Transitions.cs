using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

using System.Collections;
using System.Drawing.Imaging;
using System.Text;
using System.Xml;

using UltimaSDK;

namespace Cartography.compiler
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
			GetHaskKeyTable = new HashKeyCollection();
			GetStaticTiles = new StaticTileCollection();
			GetMapTiles = new MapTileCollection();
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
			GetHaskKeyTable = new HashKeyCollection();
			GetStaticTiles = new StaticTileCollection();
			GetMapTiles = new MapTileCollection();
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
			GetHaskKeyTable = new HashKeyCollection();
			GetStaticTiles = new StaticTileCollection();
			GetMapTiles = new MapTileCollection();
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
			GetHaskKeyTable = new HashKeyCollection();
			GetStaticTiles = new StaticTileCollection();
			GetMapTiles = new MapTileCollection();
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
			GetHaskKeyTable = new HashKeyCollection();
			GetStaticTiles = new StaticTileCollection();
			GetMapTiles = new MapTileCollection();
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
			GetHaskKeyTable = new HashKeyCollection();
			GetStaticTiles = new StaticTileCollection();
			GetMapTiles = new MapTileCollection();
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
			GetHaskKeyTable = new HashKeyCollection();
			GetStaticTiles = new StaticTileCollection();
			GetMapTiles = new MapTileCollection();
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
			GetHaskKeyTable = new HashKeyCollection();
			GetStaticTiles = new StaticTileCollection();
			GetMapTiles = new MapTileCollection();
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
			GetTransitionTable = new Hashtable();
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
						var iTileID = XmlConvert.ToUInt16(xmlElement2.GetAttribute("TileID"));
						var iX = XmlConvert.ToByte(xmlElement2.GetAttribute("X"));
						var iY = XmlConvert.ToByte(xmlElement2.GetAttribute("Y"));
						var iZ = XmlConvert.ToSByte(xmlElement2.GetAttribute("Z"));
						var iHue = XmlConvert.ToUInt16(xmlElement2.GetAttribute("Hue"));
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

		public void ChangeAltID(sbyte AltMOD)
		{
			AltID += AltMOD;
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
			TileID = XmlConvert.ToUInt16(xmlInfo.GetAttribute("TileID"));
			AltIDMod = XmlConvert.ToSByte(xmlInfo.GetAttribute("AltIDMod"));
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
	public class RandomStatic
	{
		#region Getters And Setters

		public ushort TileID { get; set; }

		public byte X { get; set; }

		public byte Y { get; set; }

		public sbyte Z { get; set; }

		public ushort Hue { get; set; }

		#endregion

		public RandomStatic()
		{
		}

		public RandomStatic(ushort iTileID, byte iXMod, byte iYMod, sbyte iZMod, ushort iHueMod)
		{
			TileID = iTileID;
			X = iXMod;
			Y = iYMod;
			Z = iZMod;
			Hue = iHueMod;
		}

		public RandomStatic(XmlElement xmlInfo)
		{
			try
			{
				TileID = XmlConvert.ToUInt16(xmlInfo.GetAttribute("TileID"));
				X = XmlConvert.ToByte(xmlInfo.GetAttribute("X"));
				Y = XmlConvert.ToByte(xmlInfo.GetAttribute("Y"));
				Z = XmlConvert.ToSByte(xmlInfo.GetAttribute("Z"));
				Hue = XmlConvert.ToUInt16(xmlInfo.GetAttribute("Hue"));
			}
			catch (Exception expr_AC)
			{
				ProjectData.SetProjectError(expr_AC);
				_ = Interaction.MsgBox(String.Format("Error\r\n{0}", xmlInfo.OuterXml), MsgBoxStyle.OkOnly, null);
				ProjectData.ClearProjectError();
			}
		}

		public override string ToString()
		{
			return String.Format("Tile:{0:X4} X:{1} Y:{2} Z:{3} Hue:{4}", TileID, X, Y, Z, Hue);
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
	}

	/// RandomStaticCollection
	public class RandomStaticCollection : CollectionBase
	{
		#region Getters And Setters

		public Collection iCollection => (Collection)List;

		public string Description { get; set; }

		public int Freq { get; set; }

		public RandomStatic this[int index]
		{
			get => (RandomStatic)List[index];
			set => List[index] = value;
		}

		#endregion

		public void Add(RandomStatic Value)
		{
			_ = InnerList.Add(Value);
		}

		public void Remove(RandomStatic Value)
		{
			InnerList.Remove(Value);
		}

		public void Save(XmlTextWriter xmlInfo)
		{
			xmlInfo.WriteStartElement("Statics");
			xmlInfo.WriteAttributeString("Description", Description);
			xmlInfo.WriteAttributeString("Freq", Convert.ToString(Freq));

			var enumerator = InnerList.GetEnumerator();

			try
			{
				while (enumerator.MoveNext())
				{
					var randomStatic = (RandomStatic)enumerator.Current;
					randomStatic.Save(xmlInfo);
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

		public void Display(ListBox iList)
		{
			iList.Items.Clear();
			try
			{
				foreach (RandomStatic item in InnerList)
				{
					_ = iList.Items.Add(item);
				}
			}
			catch
			{

			}
		}

		public void RandomStatic(byte X, byte Y, sbyte Z, Collection[,] StaticMap)
		{
			var enumerator = InnerList.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					var randomStatic = (RandomStatic)enumerator.Current;
					var item = new StaticCell(randomStatic.TileID, (byte)((X + randomStatic.X) % 8), (byte)((Y + randomStatic.Y) % 8), (sbyte)(Z + randomStatic.Z));

					var x = (X + randomStatic.X) >> 3;
					var y = (Y + randomStatic.Y) >> 3;

					if (x >= StaticMap.GetLength(0))
					{
						continue;
					}

					if (y >= StaticMap.GetLength(1))
					{
						continue;
					}

					StaticMap[x, y].Add(item, null, null, null);
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

		public override string ToString()
		{
			return String.Format("{0} Freq:{1}", Description, Freq);
		}

		public RandomStaticCollection()
		{
		}

		public RandomStaticCollection(string iDescription, int iFreq)
		{
			Description = iDescription;
			Freq = iFreq;
		}

		public RandomStaticCollection(XmlElement xmlInfo)
		{
			Description = xmlInfo.GetAttribute("Description");
			Freq = XmlConvert.ToUInt16(xmlInfo.GetAttribute("Freq"));

			var enumerator = xmlInfo.SelectNodes("Static").GetEnumerator();

			try
			{
				while (enumerator.MoveNext())
				{
					var xmlInfo2 = (XmlElement)enumerator.Current;
					_ = InnerList.Add(new RandomStatic(xmlInfo2));
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

	/// RandomStatics
	public class RandomStatics : CollectionBase
	{
		private readonly Collection m_Random;

		#region Getters And Setters

		public int Freq { get; set; }

		public RandomStaticCollection this[int index]
		{
			get => (RandomStaticCollection)List[index];
			set => List[index] = value;
		}

		#endregion

		public void Add(RandomStaticCollection Value)
		{
			_ = InnerList.Add(Value);
			var arg_17_0 = 0;
			var b = Value.Count;
			for (var b2 = arg_17_0; b2 <= b; b2++)
			{
				m_Random.Add(Value, null, null, null);
			}
		}

		public void Remove(RandomStaticCollection Value)
		{
			InnerList.Remove(Value);
		}

		public RandomStatics()
		{
			m_Random = new Collection();
		}

		public RandomStatics(string iFileName)
		{
			m_Random = new Collection();
			var xmlDocument = new XmlDocument();
			try
			{
				#region Data Directory Modification

				var filename = String.Format("{0}MapCompiler\\Engine\\TerrainTypes\\{1}", AppDomain.CurrentDomain.BaseDirectory, iFileName);

				#endregion

				xmlDocument.Load(filename);
				var xmlElement = (XmlElement)xmlDocument.SelectSingleNode("//RandomStatics");
				Freq = XmlConvert.ToInt32(xmlElement.GetAttribute("Chance"));

				var enumerator = xmlElement.SelectNodes("Statics").GetEnumerator();

				try
				{
					while (enumerator.MoveNext())
					{
						var xmlInfo = (XmlElement)enumerator.Current;
						var randomStaticCollection = new RandomStaticCollection(xmlInfo);
						_ = InnerList.Add(randomStaticCollection);
						if (randomStaticCollection.Freq > 0)
						{
							var arg_AC_0 = 1;
							var b = randomStaticCollection.Freq;
							for (var b2 = arg_AC_0; b2 <= b; b2++)
							{
								m_Random.Add(randomStaticCollection, null, null, null);
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
			catch (Exception expr_F8)
			{
				ProjectData.SetProjectError(expr_F8);
				_ = Interaction.MsgBox("Can not find:" + iFileName, MsgBoxStyle.OkOnly, null);
				ProjectData.ClearProjectError();
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
			xmlTextWriter.WriteAttributeString("Chance", Convert.ToString(Freq));

			var enumerator = InnerList.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					var randomStaticCollection = (RandomStaticCollection)enumerator.Current;
					randomStaticCollection.Save(xmlTextWriter);
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

		public void Display(ListBox iList)
		{
			iList.Items.Clear();

			var enumerator = InnerList.GetEnumerator();

			try
			{
				while (enumerator.MoveNext())
				{
					var item = (RandomStaticCollection)enumerator.Current;
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

		public void GetRandomStatic(byte X, byte Y, sbyte Z, Collection[,] StaticMap)
		{
			if (m_Random.Count != 0)
			{
				VBMath.Randomize();
				if ((int)Math.Round(100f * VBMath.Rnd()) <= Freq)
				{
					var index = (int)Math.Round((m_Random.Count - 1) * VBMath.Rnd()) + 1;

					if (m_Random[index] is RandomStaticCollection coll)
					{
						coll.RandomStatic(X, Y, Z, StaticMap);
					}
				}
			}
		}
	}

	/// StaticCell
	public class StaticCell
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
			TileID = XmlConvert.ToUInt16(xmlInfo.GetAttribute("TileID"));
			AltIDMod = XmlConvert.ToSByte(xmlInfo.GetAttribute("AltIDMod"));
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