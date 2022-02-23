using Altitude;

using BuildLogger;

using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

using Terrain;

using Ultima;

namespace Transition
{
    /// Transition
	public class Transition
    {
        private string m_Description;
        private HashKeyCollection m_HashKey;
        private StaticTileCollection m_StaticTiles;
        private MapTileCollection m_MapTiles;
        private RandomStatics m_RandomTiles;
        private string m_File;

        #region Getters And Setters

        public string File
        {
            get
            {
                return this.m_File;
            }
            set
            {
                this.m_File = value;
            }
        }

        public string Description
        {
            get
            {
                return this.m_Description;
            }
            set
            {
                this.m_Description = value;
            }
        }

        public string HashKey
        {
            get
            {
                return this.m_HashKey.ToString();
            }
            set
            {
                byte b = 0;
                do
                {
                    this.m_HashKey.Add(new HashKey(Strings.Mid(value, (int)checked(b * 2 + 1), 2)));
                    b += 1;
                }
                while (b <= 8);
            }
        }

        public MapTileCollection GetMapTiles
        {
            get
            {
                return this.m_MapTiles;
            }
            set
            {
                this.m_MapTiles = value;
            }
        }

        public StaticTileCollection GetStaticTiles
        {
            get
            {
                return this.m_StaticTiles;
            }
            set
            {
                this.m_StaticTiles = value;
            }
        }

        public HashKeyCollection GetHaskKeyTable
        {
            get
            {
                return this.m_HashKey;
            }
        }

        #endregion

        public byte GetKey(int Index)
        {
            return this.m_HashKey[Index].Key;
        }

        public virtual MapTile GetRandomMapTile()
        {
            MapTile randomTile = null;
            if (this.GetMapTiles.Count > 0)
            {
                randomTile = this.m_MapTiles.RandomTile;
            }
            return randomTile;
        }

        public virtual void GetRandomStaticTiles(short X, short Y, short Z, Collection[,] StaticMap, bool iRandom)
        {
            if (this.m_StaticTiles.Count > 0)
            {
                StaticTile randomTile = this.m_StaticTiles.RandomTile;
                StaticMap[(int)((short)(X >> 3)), (int)((short)(Y >> 3))].Add(new StaticCell(randomTile.TileID, checked((byte)(X % 8)), checked((byte)(Y % 8)), (short)(Z + randomTile.AltIDMod)), null, null, null);
            }
            if (iRandom)
            {
                if (this.m_RandomTiles != null)
                {
                    this.m_RandomTiles.GetRandomStatic(X, Y, Z, StaticMap);
                }
            }
        }

        public void Clone(ClsTerrain iGroupA, ClsTerrain iGroupB)
        {
            this.m_Description = this.m_Description.Replace(iGroupA.Name, iGroupB.Name);
            int num = 0;
            checked
            {
                do
                {
                    if ((int)this.m_HashKey[num].Key == iGroupA.GroupID)
                    {
                        this.m_HashKey[num].Key = (byte)iGroupB.GroupID;
                    }
                    num++;
                }
                while (num <= 8);
            }
        }

        public void SetHashKey(int iKey, byte iKeyHash)
        {
            this.m_HashKey[iKey].Key = iKeyHash;
        }

        public void AddMapTile(short TileID, short AltIDMod)
        {
            this.m_MapTiles.Add(new MapTile(TileID, AltIDMod));
        }

        public void RemoveMapTile(MapTile iMapTile)
        {
            this.m_MapTiles.Remove(iMapTile);
        }

        public void AddStaticTile(short TileID, short AltIDMod)
        {
            this.m_StaticTiles.Add(new StaticTile(TileID, AltIDMod));
        }

        public void RemoveStaticTile(StaticTile iStaticTile)
        {
            this.m_StaticTiles.Remove(iStaticTile);
        }

        public override string ToString()
        {
            return string.Format("[{0}] {1}", this.m_HashKey.ToString(), this.m_Description);
        }

        public Bitmap TransitionImage(ClsTerrainTable iTerrain)
        {
            Bitmap bitmap = new Bitmap(400, 168, PixelFormat.Format16bppRgb555);
            Graphics graphics = Graphics.FromImage(bitmap);
            Font font = new Font("Arial", 10f);
            Graphics graphics2 = graphics;
            graphics2.Clear(Color.White);
            Graphics arg_5E_0 = graphics2;
            Image arg_5E_1 = Art.GetLand((int)iTerrain.TerrianGroup(0).TileID);
            Point point = new Point(61, 15);
            arg_5E_0.DrawImage(arg_5E_1, point);
            Graphics arg_85_0 = graphics2;
            Image arg_85_1 = Art.GetLand((int)iTerrain.TerrianGroup(1).TileID);
            point = new Point(84, 38);
            arg_85_0.DrawImage(arg_85_1, point);
            Graphics arg_AC_0 = graphics2;
            Image arg_AC_1 = Art.GetLand((int)iTerrain.TerrianGroup(2).TileID);
            point = new Point(107, 61);
            arg_AC_0.DrawImage(arg_AC_1, point);
            Graphics arg_D3_0 = graphics2;
            Image arg_D3_1 = Art.GetLand((int)iTerrain.TerrianGroup(3).TileID);
            point = new Point(38, 38);
            arg_D3_0.DrawImage(arg_D3_1, point);
            Graphics arg_FA_0 = graphics2;
            Image arg_FA_1 = Art.GetLand((int)iTerrain.TerrianGroup(4).TileID);
            point = new Point(61, 61);
            arg_FA_0.DrawImage(arg_FA_1, point);
            Graphics arg_121_0 = graphics2;
            Image arg_121_1 = Art.GetLand((int)iTerrain.TerrianGroup(5).TileID);
            point = new Point(84, 84);
            arg_121_0.DrawImage(arg_121_1, point);
            Graphics arg_148_0 = graphics2;
            Image arg_148_1 = Art.GetLand((int)iTerrain.TerrianGroup(6).TileID);
            point = new Point(15, 61);
            arg_148_0.DrawImage(arg_148_1, point);
            Graphics arg_16F_0 = graphics2;
            Image arg_16F_1 = Art.GetLand((int)iTerrain.TerrianGroup(7).TileID);
            point = new Point(38, 84);
            arg_16F_0.DrawImage(arg_16F_1, point);
            Graphics arg_196_0 = graphics2;
            Image arg_196_1 = Art.GetLand((int)iTerrain.TerrianGroup(8).TileID);
            point = new Point(61, 107);
            arg_196_0.DrawImage(arg_196_1, point);
            graphics2.DrawString(this.ToString(), font, Brushes.Black, 151f, 2f);
            graphics.Dispose();
            return bitmap;
        }

        public void Save(XmlTextWriter xmlInfo)
        {
            xmlInfo.WriteStartElement("TransInfo");
            xmlInfo.WriteAttributeString("Description", this.m_Description);
            xmlInfo.WriteAttributeString("HashKey", this.m_HashKey.ToString());
            if (this.m_File != null)
            {
                xmlInfo.WriteAttributeString("File", this.m_File);
            }
            this.m_MapTiles.Save(xmlInfo);
            this.m_StaticTiles.Save(xmlInfo);
            xmlInfo.WriteEndElement();
        }

        public Transition(XmlElement xmlInfo)
        {
            this.m_HashKey = new HashKeyCollection();
            this.m_StaticTiles = new StaticTileCollection();
            this.m_MapTiles = new MapTileCollection();
            this.m_RandomTiles = null;
            this.m_File = null;
            this.m_Description = xmlInfo.GetAttribute("Description");
            this.m_HashKey.AddHashKey(xmlInfo.GetAttribute("HashKey"));
            if (StringType.StrCmp(xmlInfo.GetAttribute("File"), string.Empty, false) != 0)
            {
                this.m_RandomTiles = new RandomStatics(xmlInfo.GetAttribute("File"));
                this.m_File = xmlInfo.GetAttribute("File");
            }
            this.m_MapTiles.Load(xmlInfo);
            this.m_StaticTiles.Load(xmlInfo);
        }

        public Transition()
        {
            this.m_HashKey = new HashKeyCollection();
            this.m_StaticTiles = new StaticTileCollection();
            this.m_MapTiles = new MapTileCollection();
            this.m_RandomTiles = null;
            this.m_File = null;
            this.m_Description = "<New Transition>";
            this.m_HashKey.Clear();
            byte b = 0;
            do
            {
                this.m_HashKey.Add(new HashKey());
                b += 1;
            }
            while (b <= 8);
        }

        public Transition(string iDescription, string iHashKey, MapTileCollection iMapTiles, StaticTileCollection iStaticTiles)
        {
            this.m_HashKey = new HashKeyCollection();
            this.m_StaticTiles = new StaticTileCollection();
            this.m_MapTiles = new MapTileCollection();
            this.m_RandomTiles = null;
            this.m_File = null;
            this.m_Description = iDescription;
            this.m_HashKey.AddHashKey(iHashKey);

            IEnumerator enumerator = iMapTiles.GetEnumerator();

            try
            {
                while (enumerator.MoveNext())
                {
                    MapTile value = (MapTile)enumerator.Current;
                    this.m_MapTiles.Add(value);
                }
            }
            finally
            {
                if (enumerator is IDisposable)
                {
                    ((IDisposable)enumerator).Dispose();
                }
            }

            IEnumerator enumerator2 = iStaticTiles.GetEnumerator();

            try
            {
                while (enumerator2.MoveNext())
                {
                    StaticTile value2 = (StaticTile)enumerator2.Current;
                    this.m_StaticTiles.Add(value2);
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
            this.m_HashKey = new HashKeyCollection();
            this.m_StaticTiles = new StaticTileCollection();
            this.m_MapTiles = new MapTileCollection();
            this.m_RandomTiles = null;
            this.m_File = null;
            this.m_Description = iDescription;
            byte b = 0;
            do
            {
                string sLeft = Strings.Mid(iHashKey, (int)checked(b + 1), 1);
                if (StringType.StrCmp(sLeft, "A", false) == 0)
                {
                    this.m_HashKey.Add(new HashKey(iGroupA.GroupID));
                }
                else
                {
                    if (StringType.StrCmp(sLeft, "B", false) == 0)
                    {
                        this.m_HashKey.Add(new HashKey(iGroupB.GroupID));
                    }
                }
                b += 1;
            }
            while (b <= 8);
        }

        public Transition(string iDescription, string iHashKey, ClsTerrain iGroupA, ClsTerrain iGroupB, MapTileCollection iMapTiles, StaticTileCollection iStaticTiles)
        {
            this.m_HashKey = new HashKeyCollection();
            this.m_StaticTiles = new StaticTileCollection();
            this.m_MapTiles = new MapTileCollection();
            this.m_RandomTiles = null;
            this.m_File = null;
            this.m_Description = iDescription;
            byte b = 0;
            do
            {
                string sLeft = Strings.Mid(iHashKey, (int)checked(b + 1), 1);
                if (StringType.StrCmp(sLeft, "A", false) == 0)
                {
                    this.m_HashKey.Add(new HashKey(iGroupA.GroupID));
                }
                else
                {
                    if (StringType.StrCmp(sLeft, "B", false) == 0)
                    {
                        this.m_HashKey.Add(new HashKey(iGroupB.GroupID));
                    }
                }
                b += 1;
            }
            while (b <= 8);
            if (iMapTiles != null)
            {
                IEnumerator enumerator = iMapTiles.GetEnumerator();
                try
                {
                    while (enumerator.MoveNext())
                    {
                        MapTile value = (MapTile)enumerator.Current;
                        this.m_MapTiles.Add(value);
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
                IEnumerator enumerator2 = iStaticTiles.GetEnumerator();

                try
                {
                    while (enumerator2.MoveNext())
                    {
                        StaticTile value2 = (StaticTile)enumerator2.Current;
                        this.m_StaticTiles.Add(value2);
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
            this.m_HashKey = new HashKeyCollection();
            this.m_StaticTiles = new StaticTileCollection();
            this.m_MapTiles = new MapTileCollection();
            this.m_RandomTiles = null;
            this.m_File = null;
            this.m_Description = iDescription;
            byte b = 0;
            do
            {
                string sLeft = Strings.Mid(iHashKey, (int)checked(b + 1), 1);
                if (StringType.StrCmp(sLeft, "A", false) == 0)
                {
                    this.m_HashKey.Add(new HashKey(iGroupA.GroupID));
                }
                else
                {
                    if (StringType.StrCmp(sLeft, "B", false) == 0)
                    {
                        this.m_HashKey.Add(new HashKey(iGroupB.GroupID));
                    }
                    else
                    {
                        if (StringType.StrCmp(sLeft, "C", false) == 0)
                        {
                            this.m_HashKey.Add(new HashKey(iGroupC.GroupID));
                        }
                    }
                }
                b += 1;
            }
            while (b <= 8);
        }

        public Transition(string iDescription, string iHashKey)
        {
            this.m_HashKey = new HashKeyCollection();
            this.m_StaticTiles = new StaticTileCollection();
            this.m_MapTiles = new MapTileCollection();
            this.m_RandomTiles = null;
            this.m_File = null;
            this.m_Description = iDescription;
            byte b = 0;
            do
            {
                this.m_HashKey.Add(new HashKey(Strings.Mid(iHashKey, (int)checked(b * 2 + 1), 2)));
                b += 1;
            }
            while (b <= 8);
        }

        public Transition(string iDescription, ClsTerrain iGroupA, ClsTerrain iGroupB, string iHashKey, MapTile iMapTile)
        {
            this.m_HashKey = new HashKeyCollection();
            this.m_StaticTiles = new StaticTileCollection();
            this.m_MapTiles = new MapTileCollection();
            this.m_RandomTiles = null;
            this.m_File = null;
            this.m_Description = iDescription;
            byte b = 0;
            do
            {
                string sLeft = Strings.Mid(iHashKey, (int)checked(b + 1), 1);
                if (StringType.StrCmp(sLeft, "A", false) == 0)
                {
                    this.m_HashKey.Add(new HashKey(iGroupA.GroupID));
                }
                else
                {
                    if (StringType.StrCmp(sLeft, "B", false) == 0)
                    {
                        this.m_HashKey.Add(new HashKey(iGroupB.GroupID));
                    }
                }
                b += 1;
            }
            while (b <= 8);
            this.m_MapTiles.Add(iMapTile);
        }
    }

    /// TransitionTable
    public class TransitionTable
    {
        private Hashtable i_Transitions;

        #region Getters And Setters

        public Hashtable GetTransitionTable
        {
            get
            {
                return this.i_Transitions;
            }
        }

        #endregion

        public Transition Transition(int iKey)
        {
            return (Transition)this.i_Transitions[iKey];
        }

        public TransitionTable()
        {
            this.i_Transitions = new Hashtable();
        }

        public void Clear()
        {
            this.i_Transitions.Clear();
        }

        public void Add(Transition iValue)
        {
            try
            {
                this.i_Transitions.Add(iValue.HashKey, iValue);
            }
            catch (Exception expr_17)
            {
                ProjectData.SetProjectError(expr_17);
                Exception ex = expr_17;
                Interaction.MsgBox(ex.Message, MsgBoxStyle.OkOnly, null);
                ProjectData.ClearProjectError();
            }
        }

        public void Remove(Transition iValue)
        {
            this.i_Transitions.Remove(iValue.HashKey);
        }

        public void Display(ListBox iList)
        {
            iList.Items.Clear();

            IEnumerator enumerator = this.i_Transitions.Values.GetEnumerator();

            try
            {
                while (enumerator.MoveNext())
                {
                    Transition item = (Transition)enumerator.Current;
                    iList.Items.Add(item);
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
            XmlDocument xmlDocument = new XmlDocument();
            try
            {
                xmlDocument.Load(iFilename);

                IEnumerator enumerator = xmlDocument.SelectNodes("//Trans/TransInfo").GetEnumerator();

                try
                {
                    while (enumerator.MoveNext())
                    {
                        XmlElement xmlInfo = (XmlElement)enumerator.Current;
                        Transition transition = new Transition(xmlInfo);
                        this.i_Transitions.Add(transition.HashKey, transition);
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
                Interaction.MsgBox(string.Format("XMLFile:{0}", iFilename), MsgBoxStyle.OkOnly, null);
                ProjectData.ClearProjectError();
            }
        }

        public void Save(string iFilename)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = iFilename;
            saveFileDialog.Filter = "xml files (*.xml)|*.xml";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                XmlTextWriter xmlTextWriter = new XmlTextWriter(saveFileDialog.FileName, Encoding.UTF8);
                xmlTextWriter.Indentation = 2;
                xmlTextWriter.Formatting = Formatting.Indented;
                xmlTextWriter.WriteStartDocument();
                xmlTextWriter.WriteStartElement("Trans");

                IEnumerator enumerator = this.i_Transitions.Values.GetEnumerator();

                try
                {
                    while (enumerator.MoveNext())
                    {
                        Transition transition = (Transition)enumerator.Current;
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
            XmlTextWriter xmlTextWriter = new XmlTextWriter(string.Format("{0}/{1}.xml", iPath, iFilename), Encoding.UTF8);
            xmlTextWriter.Indentation = 2;
            xmlTextWriter.Formatting = Formatting.Indented;
            xmlTextWriter.WriteStartDocument();
            xmlTextWriter.WriteStartElement("Trans");

            IEnumerator enumerator = this.i_Transitions.Values.GetEnumerator();

            try
            {
                while (enumerator.MoveNext())
                {
                    Transition transition = (Transition)enumerator.Current;
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
            this.ProcessDirectory(iPath);
        }

        public void ProcessDirectory(string targetDirectory)
        {
            string[] files = Directory.GetFiles(targetDirectory, "*.xml");
            string[] array = files;
            checked
            {
                for (int i = 0; i < array.Length; i++)
                {
                    string iFilename = array[i];
                    this.Load(iFilename);
                }
                string[] directories = Directory.GetDirectories(targetDirectory);
                string[] array2 = directories;
                for (int j = 0; j < array2.Length; j++)
                {
                    string targetDirectory2 = array2[j];
                    this.ProcessDirectory(targetDirectory2);
                }
            }
        }
    }

    /// HashKey
	public class HashKey
    {
        private byte m_Key;

        #region Getters And Setters

        public byte Key
        {
            get
            {
                return this.m_Key;
            }
            set
            {
                this.m_Key = value;
            }
        }

        #endregion

        public HashKey()
        {
            this.m_Key = 0;
        }

        public HashKey(int Key)
        {
            this.m_Key = Convert.ToByte(Key);
        }

        public HashKey(byte Key)
        {
            this.m_Key = Key;
        }

        public HashKey(string Key)
        {
            this.m_Key = ByteType.FromString("&H" + Key);
        }

        public override string ToString()
        {
            return string.Format("{0:X2}", this.m_Key);
        }
    }

    /// HashKeyCollection
    public class HashKeyCollection : CollectionBase
    {
        #region Getters And Setters

        public HashKey this[int index]
        {
            get
            {
                return (HashKey)this.List[index];
            }
            set
            {
                this.List[index] = value;
            }
        }

        #endregion

        public HashKeyCollection()
        {
        }

        public void Add(HashKey Value)
        {
            if (this.InnerList.Count <= 9)
            {
                this.InnerList.Add(Value);
            }
        }

        public void AddHashKey(string Value)
        {
            this.InnerList.Clear();
            byte num = 0;
            do
            {
                this.Add(new HashKey(Strings.Mid(Value, checked(checked(num * 2) + 1), 2)));
                num = checked((byte)(num + 1));
            }
            while (num <= 8);
        }

        public void Remove(HashKey Value)
        {
            this.InnerList.Remove(Value);
        }

        public override string ToString()
        {
            object[] key = new object[] { this[0].Key, this[1].Key, this[2].Key, this[3].Key, this[4].Key, this[5].Key, this[6].Key, this[7].Key, this[8].Key };
            return string.Format("{0:X2}{1:X2}{2:X2}{3:X2}{4:X2}{5:X2}{6:X2}{7:X2}{8:X2}", key);
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
                Interaction.MsgBox("No Import Directory Was Found!", MsgBoxStyle.OkOnly, null);
            }
            else
            {
                this.ProcessDirectory(StaticMap, iPath);
            }
        }

        public void Load(Collection[,] StaticMap, string iFilename)
        {
            XmlDocument xmlDocument = new XmlDocument();
            try
            {
                xmlDocument.Load(iFilename);
                XmlElement xmlElement = (XmlElement)xmlDocument.SelectSingleNode("//Static_Tiles");

                IEnumerator enumerator = xmlElement.SelectNodes("Tile").GetEnumerator();

                try
                {
                    while (enumerator.MoveNext())
                    {
                        XmlElement xmlElement2 = (XmlElement)enumerator.Current;
                        short iTileID = XmlConvert.ToInt16(xmlElement2.GetAttribute("TileID"));
                        short num = XmlConvert.ToInt16(xmlElement2.GetAttribute("X"));
                        short num2 = XmlConvert.ToInt16(xmlElement2.GetAttribute("Y"));
                        short iZ = XmlConvert.ToInt16(xmlElement2.GetAttribute("Z"));
                        short iHue = XmlConvert.ToInt16(xmlElement2.GetAttribute("Hue"));
                        StaticCell item = checked(new StaticCell(iTileID, (byte)(num % 8), (byte)(num2 % 8), iZ, iHue));
                        StaticMap[(int)((short)(num >> 3)), (int)((short)(num2 >> 3))].Add(item, null, null, null);
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
                Interaction.MsgBox("Can not find:" + iFilename, MsgBoxStyle.OkOnly, null);
                ProjectData.ClearProjectError();
            }
        }

        public void ProcessDirectory(Collection[,] StaticMap, string targetDirectory)
        {
            string[] files = Directory.GetFiles(targetDirectory, "*.xml");
            string[] array = files;
            checked
            {
                for (int i = 0; i < array.Length; i++)
                {
                    string iFilename = array[i];
                    this.Load(StaticMap, iFilename);
                }
                string[] directories = Directory.GetDirectories(targetDirectory);
                string[] array2 = directories;
                for (int j = 0; j < array2.Length; j++)
                {
                    string targetDirectory2 = array2[j];
                    this.ProcessDirectory(StaticMap, targetDirectory2);
                }
            }
        }
    }

    /// MapCell
	public class MapCell
    {
        private byte m_GroupID;
        private short m_TileID;
        private short m_Alt;

        #region Getters And Setters

        public byte GroupID
        {
            get
            {
                return this.m_GroupID;
            }
        }

        public short TileID
        {
            get
            {
                return this.m_TileID;
            }
            set
            {
                this.m_TileID = value;
            }
        }

        public short AltID
        {
            get
            {
                return this.m_Alt;
            }
            set
            {
                this.m_Alt = value;
            }
        }

        #endregion

        public void ChangeAltID(short AltMOD)
        {
            this.m_Alt += AltMOD;
        }

        public MapCell(byte i_Terrian, short i_Alt)
        {
            this.m_GroupID = i_Terrian;
            this.m_TileID = 0;
            this.m_Alt = i_Alt;
        }

        public void WriteMapMul(BinaryWriter i_MapFile)
        {
            i_MapFile.Write(this.m_TileID);
            if (this.m_Alt <= -127)
            {
                this.m_Alt = -127;
            }
            if (this.m_Alt >= 127)
            {
                this.m_Alt = 127;
            }
            sbyte b = Convert.ToSByte(this.m_Alt);
            i_MapFile.Write(b);
        }
    }

    /// MapTile
	public class MapTile
    {
        private short m_TileID;
        private short m_AltID;

        #region Getters And Setters

        public short TileID
        {
            get
            {
                return this.m_TileID;
            }
            set
            {
                this.m_TileID = value;
            }
        }

        public short AltIDMod
        {
            get
            {
                return this.m_AltID;
            }
            set
            {
                this.m_AltID = value;
            }
        }

        #endregion

        public override string ToString()
        {
            return string.Format("{0:X4} [{1}]", this.m_TileID, this.m_AltID);
        }

        public MapTile(short TileID, short AltID)
        {
            this.m_TileID = TileID;
            this.m_AltID = AltID;
        }

        public MapTile()
        {
        }

        public MapTile(XmlElement xmlInfo)
        {
            try
            {
                this.m_TileID = XmlConvert.ToInt16(xmlInfo.GetAttribute("TileID"));
            }
            catch (Exception expr_21)
            {
                ProjectData.SetProjectError(expr_21);
                this.m_TileID = ShortType.FromString("&H" + xmlInfo.GetAttribute("TileID"));
                ProjectData.ClearProjectError();
            }
            this.m_AltID = XmlConvert.ToInt16(xmlInfo.GetAttribute("AltIDMod"));
        }

        public void Save(XmlTextWriter xmlInfo)
        {
            xmlInfo.WriteStartElement("MapTile");
            xmlInfo.WriteAttributeString("TileID", StringType.FromInteger((int)this.m_TileID));
            xmlInfo.WriteAttributeString("AltIDMod", StringType.FromInteger((int)this.m_AltID));
            xmlInfo.WriteEndElement();
        }
    }

    /// MapTileCollection
    public class MapTileCollection : CollectionBase
    {
        #region Getters And Setters

        public MapTile this[int index]
        {
            get
            {
                return (MapTile)this.List[index];
            }
            set
            {
                this.List[index] = value;
            }
        }

        public MapTile RandomTile
        {
            get
            {
                int num = checked((int)Math.Round((double)unchecked(VBMath.Rnd() * (float)checked(this.List.Count - 1))));
                return (MapTile)this.List[num];
            }
        }

        #endregion

        public void Add(MapTile Value)
        {
            this.InnerList.Add(Value);
        }

        public void Remove(MapTile Value)
        {
            this.InnerList.Remove(Value);
        }

        public void Save(XmlTextWriter xmlInfo)
        {
            xmlInfo.WriteStartElement("MapTiles");

            IEnumerator enumerator = this.InnerList.GetEnumerator();

            try
            {
                while (enumerator.MoveNext())
                {
                    MapTile mapTile = (MapTile)enumerator.Current;
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

            IEnumerator enumerator = xmlInfo.SelectNodes("MapTiles").GetEnumerator();

            try
            {
                while (enumerator.MoveNext())
                {
                    XmlElement xmlElement = (XmlElement)enumerator.Current;

                    IEnumerator enumerator2 = xmlElement.SelectNodes("MapTile").GetEnumerator();

                    try
                    {
                        while (enumerator2.MoveNext())
                        {
                            XmlElement xmlInfo2 = (XmlElement)enumerator2.Current;
                            this.InnerList.Add(new MapTile(xmlInfo2));
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

            IEnumerator enumerator = this.InnerList.GetEnumerator();

            try
            {
                while (enumerator.MoveNext())
                {
                    MapTile item = (MapTile)enumerator.Current;
                    iList.Items.Add(item);
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
        private short m_TileID;
        private short m_XMod;
        private short m_YMod;
        private short m_ZMod;
        private short m_HueMod;

        #region Getters And Setters

        public short TileID
        {
            get
            {
                return this.m_TileID;
            }
            set
            {
                this.m_TileID = value;
            }
        }

        public short X
        {
            get
            {
                return this.m_XMod;
            }
            set
            {
                this.m_XMod = value;
            }
        }

        public short Y
        {
            get
            {
                return this.m_YMod;
            }
            set
            {
                this.m_YMod = value;
            }
        }

        public short Z
        {
            get
            {
                return this.m_ZMod;
            }
            set
            {
                this.m_ZMod = value;
            }
        }

        public short Hue
        {
            get
            {
                return this.m_HueMod;
            }
            set
            {
                this.m_HueMod = value;
            }
        }

        #endregion

        public RandomStatic()
        {
        }

        public RandomStatic(short iTileID, short iXMod, short iYMod, short iZMod, short iHueMod)
        {
            this.m_TileID = iTileID;
            this.m_XMod = iXMod;
            this.m_YMod = iYMod;
            this.m_ZMod = iZMod;
            this.m_HueMod = iHueMod;
        }

        public RandomStatic(XmlElement xmlInfo)
        {
            try
            {
                try
                {
                    this.m_TileID = XmlConvert.ToInt16(xmlInfo.GetAttribute("TileID"));
                }
                catch (Exception expr_22)
                {
                    ProjectData.SetProjectError(expr_22);
                    this.m_TileID = ShortType.FromString("&H" + xmlInfo.GetAttribute("TileID"));
                    ProjectData.ClearProjectError();
                }
                this.m_XMod = XmlConvert.ToInt16(xmlInfo.GetAttribute("X"));
                this.m_YMod = XmlConvert.ToInt16(xmlInfo.GetAttribute("Y"));
                this.m_ZMod = XmlConvert.ToInt16(xmlInfo.GetAttribute("Z"));
                this.m_HueMod = XmlConvert.ToInt16(xmlInfo.GetAttribute("Hue"));
            }
            catch (Exception expr_AC)
            {
                ProjectData.SetProjectError(expr_AC);
                Interaction.MsgBox(string.Format("Error\r\n{0}", xmlInfo.OuterXml), MsgBoxStyle.OkOnly, null);
                ProjectData.ClearProjectError();
            }
        }

        public override string ToString()
        {
            return string.Format("Tile:{0:X4} X:{1} Y:{2} Z:{3} Hue:{4}", new object[]
            {
                this.m_TileID,
                this.m_XMod,
                this.m_YMod,
                this.m_ZMod,
                this.m_HueMod
            });
        }

        public void Save(XmlTextWriter xmlInfo)
        {
            xmlInfo.WriteStartElement("Static");
            xmlInfo.WriteAttributeString("TileID", StringType.FromInteger((int)this.m_TileID));
            xmlInfo.WriteAttributeString("X", StringType.FromInteger((int)this.m_XMod));
            xmlInfo.WriteAttributeString("Y", StringType.FromInteger((int)this.m_YMod));
            xmlInfo.WriteAttributeString("Z", StringType.FromInteger((int)this.m_ZMod));
            xmlInfo.WriteAttributeString("Hue", StringType.FromInteger((int)this.m_HueMod));
            xmlInfo.WriteEndElement();
        }
    }

    /// RandomStaticCollection
	public class RandomStaticCollection : CollectionBase
    {
        private string m_Description;
        private int m_Freq;

        #region Getters And Setters

        public Collection iCollection
        {
            get
            {
                return (Collection)this.List;
            }
        }

        public string Description
        {
            get
            {
                return this.m_Description;
            }
            set
            {
                this.m_Description = value;
            }
        }

        public int Freq
        {
            get
            {
                return this.m_Freq;
            }
            set
            {
                this.m_Freq = value;
            }
        }

        public RandomStatic this[int index]
        {
            get
            {
                return (RandomStatic)this.List[index];
            }
            set
            {
                this.List[index] = value;
            }
        }

        #endregion

        public void Add(RandomStatic Value)
        {
            this.InnerList.Add(Value);
        }

        public void Remove(RandomStatic Value)
        {
            this.InnerList.Remove(Value);
        }

        public void Save(XmlTextWriter xmlInfo)
        {
            xmlInfo.WriteStartElement("Statics");
            xmlInfo.WriteAttributeString("Description", this.m_Description);
            xmlInfo.WriteAttributeString("Freq", StringType.FromInteger(this.m_Freq));

            IEnumerator enumerator = this.InnerList.GetEnumerator();

            try
            {
                while (enumerator.MoveNext())
                {
                    RandomStatic randomStatic = (RandomStatic)enumerator.Current;
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
                    iList.Items.Add(item);
                }
            }
            catch
            {

            }
        }

        public void RandomStatic(short X, short Y, short Z, Collection[,] StaticMap)
        {
            IEnumerator enumerator = this.InnerList.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    RandomStatic randomStatic = (RandomStatic)enumerator.Current;
                    StaticCell item = new StaticCell(randomStatic.TileID, checked((byte)(unchecked(X + randomStatic.X) % 8)), checked((byte)(unchecked(Y + randomStatic.Y) % 8)), (short)(Z + randomStatic.Z));
                    StaticMap[(int)((short)(X + randomStatic.X >> 3)), (int)((short)(Y + randomStatic.Y >> 3))].Add(item, null, null, null);
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
            return string.Format("{0} Freq:{1}", this.m_Description, this.m_Freq);
        }

        public RandomStaticCollection()
        {
        }

        public RandomStaticCollection(string iDescription, int iFreq)
        {
            this.m_Description = iDescription;
            this.m_Freq = iFreq;
        }

        public RandomStaticCollection(XmlElement xmlInfo)
        {
            this.m_Description = xmlInfo.GetAttribute("Description");
            this.m_Freq = (int)XmlConvert.ToInt16(xmlInfo.GetAttribute("Freq"));

            IEnumerator enumerator = xmlInfo.SelectNodes("Static").GetEnumerator();

            try
            {
                while (enumerator.MoveNext())
                {
                    XmlElement xmlInfo2 = (XmlElement)enumerator.Current;
                    this.InnerList.Add(new RandomStatic(xmlInfo2));
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
        private int m_Freq;
        private Collection m_Random;

        #region Getters And Setters

        public int Freq
        {
            get
            {
                return this.m_Freq;
            }
            set
            {
                this.m_Freq = value;
            }
        }

        public RandomStaticCollection this[int index]
        {
            get
            {
                return (RandomStaticCollection)this.List[index];
            }
            set
            {
                this.List[index] = value;
            }
        }

        #endregion

        public void Add(RandomStaticCollection Value)
        {
            this.InnerList.Add(Value);
            byte arg_17_0 = 0;
            byte b = checked((byte)Value.Count);
            for (byte b2 = arg_17_0; b2 <= b; b2 += 1)
            {
                this.m_Random.Add(Value, null, null, null);
            }
        }

        public void Remove(RandomStaticCollection Value)
        {
            this.InnerList.Remove(Value);
        }

        public RandomStatics()
        {
            this.m_Random = new Collection();
        }

        public RandomStatics(string iFileName)
        {
            this.m_Random = new Collection();
            XmlDocument xmlDocument = new XmlDocument();
            try
            {
                #region Data Directory Modification

                string filename = string.Format("{0}Data\\Engine\\TerrainTypes\\{1}", AppDomain.CurrentDomain.BaseDirectory, iFileName);

                #endregion

                xmlDocument.Load(filename);
                XmlElement xmlElement = (XmlElement)xmlDocument.SelectSingleNode("//RandomStatics");
                this.m_Freq = (int)XmlConvert.ToInt16(xmlElement.GetAttribute("Chance"));

                IEnumerator enumerator = xmlElement.SelectNodes("Statics").GetEnumerator();

                try
                {
                    while (enumerator.MoveNext())
                    {
                        XmlElement xmlInfo = (XmlElement)enumerator.Current;
                        RandomStaticCollection randomStaticCollection = new RandomStaticCollection(xmlInfo);
                        this.InnerList.Add(randomStaticCollection);
                        if (randomStaticCollection.Freq > 0)
                        {
                            byte arg_AC_0 = 1;
                            byte b = checked((byte)randomStaticCollection.Freq);
                            for (byte b2 = arg_AC_0; b2 <= b; b2 += 1)
                            {
                                this.m_Random.Add(randomStaticCollection, null, null, null);
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
                Interaction.MsgBox("Can not find:" + iFileName, MsgBoxStyle.OkOnly, null);
                ProjectData.ClearProjectError();
            }
        }

        public void Save(string iFileName)
        {
            XmlTextWriter xmlTextWriter = new XmlTextWriter(iFileName, Encoding.UTF8);
            xmlTextWriter.Indentation = 2;
            xmlTextWriter.Formatting = Formatting.Indented;
            xmlTextWriter.WriteStartDocument();
            xmlTextWriter.WriteStartElement("RandomStatics");
            xmlTextWriter.WriteAttributeString("Chance", StringType.FromInteger(this.m_Freq));

            IEnumerator enumerator = this.InnerList.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    RandomStaticCollection randomStaticCollection = (RandomStaticCollection)enumerator.Current;
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

            IEnumerator enumerator = this.InnerList.GetEnumerator();

            try
            {
                while (enumerator.MoveNext())
                {
                    RandomStaticCollection item = (RandomStaticCollection)enumerator.Current;
                    iList.Items.Add(item);
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

        public void GetRandomStatic(short X, short Y, short Z, Collection[,] StaticMap)
        {
            checked
            {
                if (this.m_Random.Count != 0)
                {
                    VBMath.Randomize();
                    if ((int)Math.Round((double)Conversion.Int(unchecked(100f * VBMath.Rnd()))) <= this.m_Freq)
                    {
                        int index = (int)Math.Round((double)unchecked((float)Conversion.Int(checked(this.m_Random.Count - 1)) * VBMath.Rnd())) + 1;
                        ((RandomStaticCollection)this.m_Random[index]).RandomStatic(X, Y, Z, StaticMap);
                    }
                }
            }
        }
    }

    /// StaticCell
	public class StaticCell
    {
        private short m_TileID;
        private byte m_X;
        private byte m_Y;
        private sbyte m_Z;
        private short m_Hue;

        public StaticCell(short iTileID, byte iX, byte iY, short iZ)
        {
            this.m_Hue = 0;
            this.m_TileID = iTileID;
            this.m_X = iX;
            this.m_Y = iY;
            this.m_Z = Convert.ToSByte(iZ);
        }

        public StaticCell(short iTileID, byte iX, byte iY, short iZ, short iHue)
        {
            this.m_Hue = 0;
            this.m_TileID = iTileID;
            this.m_X = iX;
            this.m_Y = iY;
            this.m_Z = Convert.ToSByte(iZ);
            this.m_Hue = iHue;
        }

        public void Write(BinaryWriter i_StaticFile)
        {
            try
            {
                i_StaticFile.Write(this.m_TileID);
                i_StaticFile.Write(this.m_X);
                i_StaticFile.Write(this.m_Y);
                i_StaticFile.Write(this.m_Z);
                i_StaticFile.Write(this.m_Hue);
            }
            catch (Exception expr_45)
            {
                ProjectData.SetProjectError(expr_45);
                Interaction.MsgBox(string.Format("Error [{0}] X:{1} Y:{2} Z:{3} Hue:{4}", new object[]
                {
                    this.m_TileID,
                    this.m_X,
                    this.m_Y,
                    this.m_Z,
                    this.m_Hue
                }), MsgBoxStyle.OkOnly, null);
                ProjectData.ClearProjectError();
            }
        }
    }

    /// StaticTile
	public class StaticTile
    {
        private short m_TileID;
        private short m_AltIDMod;

        #region Getters And Setters

        public short TileID
        {
            get
            {
                return this.m_TileID;
            }
            set
            {
                this.m_TileID = value;
            }
        }

        public short AltIDMod
        {
            get
            {
                return this.m_AltIDMod;
            }
            set
            {
                this.m_AltIDMod = value;
            }
        }

        #endregion

        public override string ToString()
        {
            return string.Format("{0:X4} [{1}]", this.m_TileID, this.m_AltIDMod);
        }

        public StaticTile()
        {
        }

        public StaticTile(short TileID, short AltIDMod)
        {
            this.m_TileID = TileID;
            this.m_AltIDMod = AltIDMod;
        }

        public StaticTile(XmlElement xmlInfo)
        {
            this.m_TileID = XmlConvert.ToInt16(xmlInfo.GetAttribute("TileID"));
            this.m_AltIDMod = XmlConvert.ToInt16(xmlInfo.GetAttribute("AltIDMod"));
        }

        public void Save(XmlTextWriter xmlInfo)
        {
            xmlInfo.WriteStartElement("StaticTile");
            xmlInfo.WriteAttributeString("TileID", StringType.FromInteger((int)this.m_TileID));
            xmlInfo.WriteAttributeString("AltIDMod", StringType.FromInteger((int)this.m_AltIDMod));
            xmlInfo.WriteEndElement();
        }
    }

    /// StaticTileCollection
    public class StaticTileCollection : CollectionBase
    {
        #region Getters And Setters

        public StaticTile this[int index]
        {
            get
            {
                return (StaticTile)this.List[index];
            }
            set
            {
                this.List[index] = value;
            }
        }

        public StaticTile RandomTile
        {
            get
            {
                int num = checked((int)Math.Round((double)unchecked(VBMath.Rnd() * (float)checked(this.List.Count - 1))));
                return (StaticTile)this.List[num];
            }
        }

        #endregion

        public void Add(StaticTile Value)
        {
            this.InnerList.Add(Value);
        }

        public void Remove(StaticTile Value)
        {
            this.InnerList.Remove(Value);
        }

        public void Save(XmlTextWriter xmlInfo)
        {
            xmlInfo.WriteStartElement("StaticTiles");

            IEnumerator enumerator = this.InnerList.GetEnumerator();

            try
            {
                while (enumerator.MoveNext())
                {
                    StaticTile staticTile = (StaticTile)enumerator.Current;
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
            IEnumerator enumerator = xmlInfo.SelectNodes("StaticTiles").GetEnumerator();

            try
            {
                while (enumerator.MoveNext())
                {
                    XmlElement xmlElement = (XmlElement)enumerator.Current;

                    IEnumerator enumerator2 = xmlElement.SelectNodes("StaticTile").GetEnumerator();

                    try
                    {
                        while (enumerator2.MoveNext())
                        {
                            XmlElement xmlInfo2 = (XmlElement)enumerator2.Current;
                            this.InnerList.Add(new StaticTile(xmlInfo2));
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
            IEnumerator enumerator = this.InnerList.GetEnumerator();

            try
            {
                while (enumerator.MoveNext())
                {
                    StaticTile item = (StaticTile)enumerator.Current;
                    iList.Items.Add(item);
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