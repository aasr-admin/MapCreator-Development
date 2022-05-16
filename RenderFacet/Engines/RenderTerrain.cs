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
using Transition;

using UltimaSDK_v432;

namespace Terrain
{
    public class ClsTerrain
    {
        private int m_GroupID;
        private string m_Name;
        private short m_TileID;
        private bool m_RandAlt;
        private byte m_BaseAlt;

        private Color m_Color;

        [Category("Tile ID")]
        public byte AltID
        {
            get
            {
                return this.m_BaseAlt;
            }
            set
            {
                this.m_BaseAlt = value;
            }
        }

        [Category("Colour")]
        public Color Colour
        {
            get
            {
                return this.m_Color;
            }
            set
            {
                this.m_Color = value;
            }
        }

        [Category("Key")]
        public int GroupID
        {
            get
            {
                return this.m_GroupID;
            }
            set
            {
                this.m_GroupID = value;
            }
        }

        [Category("Group ID")]
        public string GroupIDHex
        {
            get
            {
                return string.Format("{0:X}", this.m_GroupID);
            }
        }

        [Category("Description")]
        public string Name
        {
            get
            {
                return this.m_Name;
            }
            set
            {
                this.m_Name = value;
            }
        }

        [Category("Tile ID")]
        public bool RandAlt
        {
            get
            {
                return this.m_RandAlt;
            }
            set
            {
                this.m_RandAlt = value;
            }
        }

        [Category("Tile ID")]
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

        public ClsTerrain(string iName, int iGroupID, short iTileID, Color iColor, byte iBase, bool iRandAlt)
        {
            this.m_Name = iName;
            this.m_GroupID = iGroupID;
            this.m_TileID = iTileID;
            this.m_Color = iColor;
            this.m_BaseAlt = iBase;
            this.m_RandAlt = iRandAlt;
        }

        public ClsTerrain(XmlElement xmlInfo)
        {
            this.m_Name = xmlInfo.GetAttribute("Name");
            this.m_GroupID = XmlConvert.ToInt32(xmlInfo.GetAttribute("ID"));
            this.m_TileID = XmlConvert.ToInt16(xmlInfo.GetAttribute("TileID"));
            this.m_Color = Color.FromArgb((int)XmlConvert.ToByte(xmlInfo.GetAttribute("R")), (int)XmlConvert.ToByte(xmlInfo.GetAttribute("G")), (int)XmlConvert.ToByte(xmlInfo.GetAttribute("B")));
            this.m_BaseAlt = XmlConvert.ToByte(xmlInfo.GetAttribute("Base"));
            string attribute = xmlInfo.GetAttribute("Random");
            if (StringType.StrCmp(attribute, "False", false) == 0)
            {
                this.m_RandAlt = false;
            }
            else if (StringType.StrCmp(attribute, "True", false) == 0)
            {
                this.m_RandAlt = true;
            }
        }

        public override string ToString()
        {
            string str;
            str = (!this.m_RandAlt ? string.Format("[{0:X2}] {1}", this.m_GroupID, this.m_Name) : string.Format("[{0:X2}] *{1}", this.m_GroupID, this.m_Name));
            return str;
        }

        #region Terrain Swatch And Color Table

        public void Save(XmlTextWriter xmlInfo)
        {
            xmlInfo.WriteStartElement("Terrain");
            xmlInfo.WriteAttributeString("Name", this.m_Name);
            xmlInfo.WriteAttributeString("ID", StringType.FromInteger(this.m_GroupID));
            xmlInfo.WriteAttributeString("TileID", StringType.FromInteger(this.m_TileID));
            xmlInfo.WriteAttributeString("R", StringType.FromByte(this.m_Color.R));
            xmlInfo.WriteAttributeString("G", StringType.FromByte(this.m_Color.G));
            xmlInfo.WriteAttributeString("B", StringType.FromByte(this.m_Color.B));
            xmlInfo.WriteAttributeString("Base", StringType.FromByte(this.m_BaseAlt));
            xmlInfo.WriteAttributeString("Random", StringType.FromBoolean(this.m_RandAlt));
            xmlInfo.WriteEndElement();
        }

        public void SaveACO(BinaryWriter iACTFile)
        {
            byte num = 0;
            iACTFile.Write(this.m_Color.R);
            iACTFile.Write(this.m_Color.R);
            iACTFile.Write(this.m_Color.G);
            iACTFile.Write(this.m_Color.G);
            iACTFile.Write(this.m_Color.B);
            iACTFile.Write(this.m_Color.B);
            iACTFile.Write(num);
            iACTFile.Write(num);
        }

        public void SaveACOText(BinaryWriter iACTFile)
        {
            byte num = 0;
            iACTFile.Write(this.m_Color.R);
            iACTFile.Write(this.m_Color.R);
            iACTFile.Write(this.m_Color.G);
            iACTFile.Write(this.m_Color.G);
            iACTFile.Write(this.m_Color.B);
            iACTFile.Write(this.m_Color.B);
            iACTFile.Write(num);
            iACTFile.Write(num);
            iACTFile.Write(num);
            iACTFile.Write(num);
            byte[] bytes = (new UnicodeEncoding(true, true)).GetBytes(this.m_Name);
            byte num1 = Convert.ToByte(bytes.Length);
            byte num2 = checked((byte)Math.Round((double)num1 / 2 + 1));
            iACTFile.Write(num);
            iACTFile.Write(num2);
            byte[] numArray = bytes;
            for (int i = 0; i < (int)numArray.Length; i++)
            {
                iACTFile.Write(numArray[i]);
            }
            iACTFile.Write(num);
            iACTFile.Write(num);
        }

        public void SaveACT(BinaryWriter iACTFile)
        {
            iACTFile.Write(this.m_Color.R);
            iACTFile.Write(this.m_Color.G);
            iACTFile.Write(this.m_Color.B);
        }

        #endregion
    }

    public class ClsTerrainTable
    {
        private Hashtable i_TerrainTable;

        public Hashtable TerrainHash
        {
            get
            {
                return this.i_TerrainTable;
            }
        }

        public ClsTerrain TerrianGroup(int iKey)
        {
            return (ClsTerrain)this.i_TerrainTable[iKey];
        }

        public ClsTerrainTable()
        {
            this.i_TerrainTable = new Hashtable();
        }

        public void Display(ListBox iList)
        {
            IEnumerator enumerator = null;
            iList.Items.Clear();
            try
            {
                enumerator = this.i_TerrainTable.Values.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    ClsTerrain current = (ClsTerrain)enumerator.Current;
                    iList.Items.Add(current);
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

        public void Display(ComboBox iCombo)
        {
            IEnumerator enumerator = null;
            iCombo.Items.Clear();
            try
            {
                enumerator = this.i_TerrainTable.Values.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    ClsTerrain current = (ClsTerrain)enumerator.Current;
                    iCombo.Items.Add(current);
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

        public ColorPalette GetPalette()
        {
            ColorPalette palette = (new Bitmap(2, 2, PixelFormat.Format8bppIndexed)).Palette;
            byte num = 0;
            do
            {
                if (this.TerrianGroup(num) == null)
                {
                    palette.Entries[num] = Color.Black;
                }
                else
                {
                    palette.Entries[num] = this.TerrianGroup(num).Colour;
                }
                num = checked((byte)(num + 1));
            }
            while (num <= 254);
            palette.Entries[255] = this.TerrianGroup(255).Colour;
            return palette;
        }

        #region Terrain Swatch And Color Table

        public void Load()
        {
            IEnumerator enumerator = null;
            IEnumerator enumerator1 = null;

            #region Data Directory Modification

            string str = string.Format("{0}\\MapCompiler\\Engine\\Terrain.xml", AppDomain.CurrentDomain.BaseDirectory);

            #endregion

            XmlDocument xmlDocument = new XmlDocument();
            try
            {
                xmlDocument.Load(str);
                this.i_TerrainTable.Clear();
                try
                {
                    enumerator1 = xmlDocument.SelectNodes("Terrains").GetEnumerator();
                    while (enumerator1.MoveNext())
                    {
                        XmlElement current = (XmlElement)enumerator1.Current;
                        try
                        {
                            enumerator = current.SelectNodes("Terrain").GetEnumerator();
                            while (enumerator.MoveNext())
                            {
                                ClsTerrain clsTerrain = new ClsTerrain((XmlElement)enumerator.Current);
                                this.i_TerrainTable.Add(clsTerrain.GroupID, clsTerrain);
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
                finally
                {
                    if (enumerator1 is IDisposable)
                    {
                        ((IDisposable)enumerator1).Dispose();
                    }
                }
            }
            catch (Exception exception)
            {
                ProjectData.SetProjectError(exception);
                Interaction.MsgBox(exception.Message, MsgBoxStyle.OkOnly, null);
                Interaction.MsgBox(string.Format("XMLFile:{0}", str), MsgBoxStyle.OkOnly, null);
                ProjectData.ClearProjectError();
            }
        }

        public void Save()
        {
            IEnumerator enumerator = null;

            #region Data Directory Modification

            string str = string.Format("{0}/MapCompiler/Engine/Terrain.xml", Directory.GetCurrentDirectory());

            #endregion

            XmlTextWriter xmlTextWriter = new XmlTextWriter(str, Encoding.UTF8)
            {
                Indentation = 2,
                Formatting = Formatting.Indented
            };
            xmlTextWriter.WriteStartDocument();
            xmlTextWriter.WriteStartElement("Terrains");
            try
            {
                enumerator = this.i_TerrainTable.Values.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    ((ClsTerrain)enumerator.Current).Save(xmlTextWriter);
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

        public void SaveACO()
        {
            byte num = Convert.ToByte(this.i_TerrainTable.Count);

            #region Data Directory Modification

            //string str = string.Format("{0}/../Development/DrawingTools/AdobePhotoshop/Presets/ColorSwatches/Terrain.aco", Directory.GetCurrentDirectory());

            string str = string.Format("Development/DrawingTools/AdobePhotoshop/Presets/Color Swatches/Terrain.ACO", Directory.GetCurrentDirectory());

            #endregion

            FileStream fileStream = new FileStream(str, FileMode.Create);
            BinaryWriter binaryWriter = new BinaryWriter(fileStream);
            binaryWriter.Write((byte)0);
            binaryWriter.Write((byte)1);
            binaryWriter.Write((byte)0);
            binaryWriter.Write(num);
            int num1 = 0;
            do
            {
                if (this.i_TerrainTable[num1] != null)
                {
                    binaryWriter.Write((byte)0);
                    binaryWriter.Write((byte)0);
                    ((ClsTerrain)this.i_TerrainTable[num1]).SaveACO(binaryWriter);
                }
                num1++;
            }
            while (num1 <= 255);
            binaryWriter.Write((byte)0);
            binaryWriter.Write((byte)2);
            binaryWriter.Write((byte)0);
            binaryWriter.Write(num);
            int num2 = 0;
            do
            {
                if (this.i_TerrainTable[num2] != null)
                {
                    binaryWriter.Write((byte)0);
                    binaryWriter.Write((byte)0);
                    ((ClsTerrain)this.i_TerrainTable[num2]).SaveACOText(binaryWriter);
                }
                num2++;
            }
            while (num2 <= 255);
            binaryWriter.Close();
            fileStream.Close();
            Interaction.MsgBox("Terrain.ACO Saved", MsgBoxStyle.OkOnly, null);
        }

        public void SaveACT()
        {
            #region Data Directory Modification

            //string str = string.Format("{0}/../Development/DrawingTools/AdobePhotoshop/Presets/OptimizedColors/Terrain.act", Directory.GetCurrentDirectory());

            string str = string.Format("Development/DrawingTools/AdobePhotoshop/Presets/Optimized Colors/Terrain.ACT", Directory.GetCurrentDirectory());

            #endregion

            FileStream fileStream = new FileStream(str, FileMode.Create);
            BinaryWriter binaryWriter = new BinaryWriter(fileStream);
            byte num = 0;
            int num1 = 0;
            do
            {
                if (this.i_TerrainTable[num1] != null)
                {
                    ((ClsTerrain)this.i_TerrainTable[num1]).SaveACT(binaryWriter);
                }
                else
                {
                    binaryWriter.Write(num);
                    binaryWriter.Write(num);
                    binaryWriter.Write(num);
                }
                num1++;
            }
            while (num1 <= 255);
            binaryWriter.Close();
            fileStream.Close();
            Interaction.MsgBox("Terrain.ACT Saved", MsgBoxStyle.OkOnly, null);
        }

        #endregion
    }
}