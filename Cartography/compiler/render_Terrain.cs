using System.Collections;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Text;
using System.Xml;

using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Cartography.compiler
{
    public class ClsTerrain
    {
        [Category("Tile Altitude")]
        public sbyte AltID { get; set; }

        [Category("Colour")]
        public Color Colour { get; set; }

        [Category("Key")]
        public byte GroupID { get; set; }

        [Category("Group ID")]
        public string GroupIDHex => string.Format("{0:X}", GroupID);

        [Category("Description")]
        public string Name { get; set; }

        [Category("Random Altitude")]
        public bool RandAlt { get; set; }

        [Category("Tile ID")]
        public ushort TileID { get; set; }

        public ClsTerrain(string iName, byte iGroupID, ushort iTileID, Color iColor, sbyte iBase, bool iRandAlt)
        {
            Name = iName;
            GroupID = iGroupID;
            TileID = iTileID;
            Colour = iColor;
            AltID = iBase;
            RandAlt = iRandAlt;
        }

        public ClsTerrain(XmlElement xmlInfo)
        {
            Name = xmlInfo.GetAttribute("Name");
            GroupID = XmlConvert.ToByte(xmlInfo.GetAttribute("ID"));
            TileID = XmlConvert.ToUInt16(xmlInfo.GetAttribute("TileID"));
            Colour = Color.FromArgb(XmlConvert.ToByte(xmlInfo.GetAttribute("R")), XmlConvert.ToByte(xmlInfo.GetAttribute("G")), XmlConvert.ToByte(xmlInfo.GetAttribute("B")));
            AltID = unchecked((sbyte)XmlConvert.ToInt32(xmlInfo.GetAttribute("Base")));
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

        public override string ToString()
        {
            string str;
            str = !RandAlt ? string.Format("[{0:X2}] {1}", GroupID, Name) : string.Format("[{0:X2}] *{1}", GroupID, Name);
            return str;
        }

        #region Terrain Swatch And Color Table

        public void Save(XmlTextWriter xmlInfo)
        {
            xmlInfo.WriteStartElement("Terrain");
            xmlInfo.WriteAttributeString("Name", Name);
            xmlInfo.WriteAttributeString("ID", Convert.ToString(GroupID));
            xmlInfo.WriteAttributeString("TileID", Convert.ToString(TileID));
            xmlInfo.WriteAttributeString("R", Convert.ToString(Colour.R));
            xmlInfo.WriteAttributeString("G", Convert.ToString(Colour.G));
            xmlInfo.WriteAttributeString("B", Convert.ToString(Colour.B));
            xmlInfo.WriteAttributeString("Base", Convert.ToString(AltID));
            xmlInfo.WriteAttributeString("Random", Convert.ToString(RandAlt));
            xmlInfo.WriteEndElement();
        }

        public void SaveACO(BinaryWriter iACTFile)
        {
            iACTFile.Write(Colour.R);
            iACTFile.Write(Colour.R);
            iACTFile.Write(Colour.G);
            iACTFile.Write(Colour.G);
            iACTFile.Write(Colour.B);
            iACTFile.Write(Colour.B);
            iACTFile.Write((byte)0);
            iACTFile.Write((byte)0);
        }

        public void SaveACOText(BinaryWriter iACTFile)
        {
            iACTFile.Write(Colour.R);
            iACTFile.Write(Colour.R);
            iACTFile.Write(Colour.G);
            iACTFile.Write(Colour.G);
            iACTFile.Write(Colour.B);
            iACTFile.Write(Colour.B);
            iACTFile.Write((byte)0);
            iACTFile.Write((byte)0);
            iACTFile.Write((byte)0);
            iACTFile.Write((byte)0);
            iACTFile.Write((byte)0);
            var bytes = new UnicodeEncoding(true, true).GetBytes(Name);
            iACTFile.Write((byte)Math.Round((bytes.Length / 2.0) + 1));
            var numArray = bytes;
            for (var i = 0; i < numArray.Length; i++)
            {
                iACTFile.Write(numArray[i]);
            }

            iACTFile.Write((byte)0);
            iACTFile.Write((byte)0);
        }

        public void SaveACT(BinaryWriter iACTFile)
        {
            iACTFile.Write(Colour.R);
            iACTFile.Write(Colour.G);
            iACTFile.Write(Colour.B);
        }

        #endregion
    }

    public class ClsTerrainTable
    {
        public Hashtable TerrainHash { get; }

        public ClsTerrain TerrianGroup(int iKey)
        {
            return (ClsTerrain)TerrainHash[iKey];
        }

        public ClsTerrainTable()
        {
            TerrainHash = new Hashtable();
        }

        public void Display(ListBox iList)
        {
            IEnumerator enumerator = null;
            iList.Items.Clear();
            try
            {
                enumerator = TerrainHash.Values.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    var current = (ClsTerrain)enumerator.Current;
                    _ = iList.Items.Add(current);
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
                enumerator = TerrainHash.Values.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    var current = (ClsTerrain)enumerator.Current;
                    _ = iCombo.Items.Add(current);
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
            var palette = new Bitmap(2, 2, PixelFormat.Format8bppIndexed).Palette;
            var num = 0;
            do
            {
                if (TerrianGroup(num) == null)
                {
                    palette.Entries[num] = Color.Black;
                }
                else
                {
                    palette.Entries[num] = TerrianGroup(num).Colour;
                }
            }
            while (++num < 255);
            palette.Entries[255] = TerrianGroup(255).Colour;
            return palette;
        }

        #region Terrain Swatch And Color Table

        public void Load()
        {
            IEnumerator enumerator = null;
            IEnumerator enumerator1 = null;

            #region Data Directory Modification

            var str = string.Format("{0}\\MapCompiler\\Engine\\Terrain.xml", AppDomain.CurrentDomain.BaseDirectory);

            #endregion

            var xmlDocument = new XmlDocument();
            try
            {
                xmlDocument.Load(str);
                TerrainHash.Clear();
                try
                {
                    enumerator1 = xmlDocument.SelectNodes("Terrains").GetEnumerator();
                    while (enumerator1.MoveNext())
                    {
                        var current = (XmlElement)enumerator1.Current;
                        try
                        {
                            enumerator = current.SelectNodes("Terrain").GetEnumerator();
                            while (enumerator.MoveNext())
                            {
                                var clsTerrain = new ClsTerrain((XmlElement)enumerator.Current);
                                TerrainHash.Add(clsTerrain.GroupID, clsTerrain);
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
                _ = Interaction.MsgBox(exception.Message, MsgBoxStyle.OkOnly, null);
                _ = Interaction.MsgBox(string.Format("XMLFile:{0}", str), MsgBoxStyle.OkOnly, null);
                ProjectData.ClearProjectError();
            }
        }

        public void Save()
        {
            IEnumerator enumerator = null;

            #region Data Directory Modification

            var str = string.Format("{0}/MapCompiler/Engine/Terrain.xml", Directory.GetCurrentDirectory());

            #endregion

            var xmlTextWriter = new XmlTextWriter(str, Encoding.UTF8)
            {
                Indentation = 2,
                Formatting = Formatting.Indented
            };
            xmlTextWriter.WriteStartDocument();
            xmlTextWriter.WriteStartElement("Terrains");
            try
            {
                enumerator = TerrainHash.Values.GetEnumerator();
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
            var num = TerrainHash.Count;

            #region Data Directory Modification

            var str = string.Format("{0}/Development/DrawingTools/AdobePhotoshop/ColorSwatches/Terrain.aco", Directory.GetCurrentDirectory());

            #endregion

            var fileStream = new FileStream(str, FileMode.Create);
            var binaryWriter = new BinaryWriter(fileStream);
            binaryWriter.Write((byte)0);
            binaryWriter.Write((byte)1);
            binaryWriter.Write((byte)0);
            binaryWriter.Write((byte)num);
            var num1 = 0;
            do
            {
                if (TerrainHash[num1] != null)
                {
                    binaryWriter.Write((byte)0);
                    binaryWriter.Write((byte)0);
                    ((ClsTerrain)TerrainHash[num1]).SaveACO(binaryWriter);
                }
            }
            while (++num1 <= 255);
            binaryWriter.Write((byte)0);
            binaryWriter.Write((byte)2);
            binaryWriter.Write((byte)0);
            binaryWriter.Write((byte)num);
            var num2 = 0;
            do
            {
                if (TerrainHash[num2] != null)
                {
                    binaryWriter.Write((byte)0);
                    binaryWriter.Write((byte)0);
                    ((ClsTerrain)TerrainHash[num2]).SaveACOText(binaryWriter);
                }
            }
            while (++num2 <= 255);
            binaryWriter.Close();
            fileStream.Close();
            _ = Interaction.MsgBox("Terrain.aco Saved", MsgBoxStyle.OkOnly, null);
        }

        public void SaveACT()
        {
            #region Data Directory Modification

            var str = string.Format("{0}/Development/DrawingTools/AdobePhotoshop/OptimizedColors/Terrain.act", Directory.GetCurrentDirectory());

            #endregion

            var fileStream = new FileStream(str, FileMode.Create);
            var binaryWriter = new BinaryWriter(fileStream);
            var num = 0;
            var num1 = 0;
            do
            {
                if (TerrainHash[num1] != null)
                {
                    ((ClsTerrain)TerrainHash[num1]).SaveACT(binaryWriter);
                }
                else
                {
                    binaryWriter.Write((byte)num);
                    binaryWriter.Write((byte)num);
                    binaryWriter.Write((byte)num);
                }
            }
            while (++num1 <= 255);
            binaryWriter.Close();
            fileStream.Close();
            _ = Interaction.MsgBox("Terrain.act Saved", MsgBoxStyle.OkOnly, null);
        }

        #endregion
    }
}