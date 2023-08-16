using System.Collections;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Text;
using System.Xml;

using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Cartography.compiler
{
    public class ClsAltitude
    {
        [Category("ID")]
        public Color AltitudeColor { get; set; }

        [Category("Method")]
        public sbyte GetAltitude { get; set; }

        [Category("Key")]
        public int Key { get; set; }

        [Category("ID")]
        public string Type { get; set; }

        public ClsAltitude(int iKey, string iType, sbyte iAlt, Color iAltColor)
        {
            Key = iKey;
            Type = iType;
            GetAltitude = iAlt;
            AltitudeColor = iAltColor;
        }

        public ClsAltitude(XmlElement xmlInfo)
        {
            Key = XmlConvert.ToInt32(xmlInfo.GetAttribute("Key"));
            Type = xmlInfo.GetAttribute("Type");
            GetAltitude = XmlConvert.ToSByte(xmlInfo.GetAttribute("Altitude"));
            AltitudeColor = Color.FromArgb(XmlConvert.ToByte(xmlInfo.GetAttribute("R")), XmlConvert.ToByte(xmlInfo.GetAttribute("G")), XmlConvert.ToByte(xmlInfo.GetAttribute("B")));
        }

        public override string ToString()
        {
            var str = string.Format("[{0:X3}] {1} {2}", Key, Type, GetAltitude);
            return str;
        }

        #region Altitude Swatch And Color Table

        public void Save(XmlTextWriter xmlInfo)
        {
            xmlInfo.WriteStartElement("Altitude");
            xmlInfo.WriteAttributeString("Key", Convert.ToString(Key));
            xmlInfo.WriteAttributeString("Type", Type);
            xmlInfo.WriteAttributeString("Altitude", Convert.ToString(GetAltitude));
            xmlInfo.WriteAttributeString("R", Convert.ToString(AltitudeColor.R));
            xmlInfo.WriteAttributeString("G", Convert.ToString(AltitudeColor.G));
            xmlInfo.WriteAttributeString("B", Convert.ToString(AltitudeColor.B));
            xmlInfo.WriteEndElement();
        }

        public void SaveACO(BinaryWriter iACTFile)
        {
            byte num = 0;
            iACTFile.Write(AltitudeColor.R);
            iACTFile.Write(AltitudeColor.R);
            iACTFile.Write(AltitudeColor.G);
            iACTFile.Write(AltitudeColor.G);
            iACTFile.Write(AltitudeColor.B);
            iACTFile.Write(AltitudeColor.B);
            iACTFile.Write(num);
            iACTFile.Write(num);
        }

        public void SaveACOText(BinaryWriter iACTFile)
        {
            byte num = 0;
            iACTFile.Write(AltitudeColor.R);
            iACTFile.Write(AltitudeColor.R);
            iACTFile.Write(AltitudeColor.G);
            iACTFile.Write(AltitudeColor.G);
            iACTFile.Write(AltitudeColor.B);
            iACTFile.Write(AltitudeColor.B);
            iACTFile.Write(num);
            iACTFile.Write(num);
            iACTFile.Write(num);
            iACTFile.Write(num);
            var unicodeEncoding = new UnicodeEncoding(true, true);
            var str = string.Format("{0} {1}", Type, GetAltitude);
            var bytes = unicodeEncoding.GetBytes(str);
            var num1 = Convert.ToByte(bytes.Length);
            var num2 = checked((byte)Math.Round(((double)num1 / 2) + 1));
            iACTFile.Write(num);
            iACTFile.Write(num2);
            var numArray = bytes;
            for (var i = 0; i < numArray.Length; i++)
            {
                iACTFile.Write(numArray[i]);
            }

            iACTFile.Write(num);
            iACTFile.Write(num);
        }

        public void SaveACT(BinaryWriter iACTFile)
        {
            iACTFile.Write(AltitudeColor.R);
            iACTFile.Write(AltitudeColor.G);
            iACTFile.Write(AltitudeColor.B);
        }

        #endregion
    }

    public class ClsAltitudeTable
    {
        public Hashtable AltitudeHash { get; }

        public void SetAltitude(int Index, ClsAltitude Value)
        {
            AltitudeHash[Index] = Value;
        }

        public ClsAltitude GetAltitude(int Index)
        {
            return (ClsAltitude)AltitudeHash[Index];
        }

        public ClsAltitudeTable()
        {
            AltitudeHash = new Hashtable();
        }

        public void Display(ListBox iList)
        {
            IEnumerator enumerator = null;
            iList.Items.Clear();
            try
            {
                enumerator = AltitudeHash.Values.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    var current = (ClsAltitude)enumerator.Current;
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

        public ColorPalette GetAltPalette()
        {
            IEnumerator enumerator = null;
            var palette = new Bitmap(2, 2, PixelFormat.Format8bppIndexed).Palette;
            try
            {
                enumerator = AltitudeHash.Values.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    var current = (ClsAltitude)enumerator.Current;
                    palette.Entries[current.Key] = current.AltitudeColor;
                }
            }
            finally
            {
                if (enumerator is IDisposable)
                {
                    ((IDisposable)enumerator).Dispose();
                }
            }

            return palette;
        }

        #region Altitude Swatch And Color Table

        public void Load()
        {
            IEnumerator enumerator = null;
            IEnumerator enumerator1 = null;

            #region Data Directory Modification

            var str = string.Format("{0}MapCompiler/Engine/Altitude.xml", AppDomain.CurrentDomain.BaseDirectory);

            #endregion

            var xmlDocument = new XmlDocument();
            try
            {
                xmlDocument.Load(str);
                AltitudeHash.Clear();
                try
                {
                    enumerator1 = xmlDocument.SelectNodes("Altitudes").GetEnumerator();
                    while (enumerator1.MoveNext())
                    {
                        var current = (XmlElement)enumerator1.Current;
                        try
                        {
                            enumerator = current.SelectNodes("Altitude").GetEnumerator();
                            while (enumerator.MoveNext())
                            {
                                var clsAltitude = new ClsAltitude((XmlElement)enumerator.Current);
                                AltitudeHash.Add(clsAltitude.Key, clsAltitude);
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
                _ = Interaction.MsgBox(string.Format("XMLFile:{0}", str), MsgBoxStyle.OkOnly, null);
                ProjectData.ClearProjectError();
            }
        }

        public void Save()
        {
            IEnumerator enumerator = null;

            #region Data Directory Modification

            var str = string.Format("{0}MapCompiler\\Engine\\Altitude.xml", AppDomain.CurrentDomain.BaseDirectory);

            #endregion

            var xmlTextWriter = new XmlTextWriter(str, Encoding.UTF8)
            {
                Indentation = 2,
                Formatting = Formatting.Indented
            };
            xmlTextWriter.WriteStartDocument();
            xmlTextWriter.WriteStartElement("Altitudes");
            try
            {
                enumerator = AltitudeHash.Values.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    ((ClsAltitude)enumerator.Current).Save(xmlTextWriter);
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
            var num = AltitudeHash.Count;

            #region Data Directory Modification

            var str = string.Format("{0}/Development/DrawingTools/AdobePhotoshop/ColorSwatches/Altitude.aco", Directory.GetCurrentDirectory());

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
                if (AltitudeHash[num1] != null)
                {
                    binaryWriter.Write((byte)0);
                    binaryWriter.Write((byte)0);
                    ((ClsAltitude)AltitudeHash[num1]).SaveACO(binaryWriter);
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
                if (AltitudeHash[num2] != null)
                {
                    binaryWriter.Write((byte)0);
                    binaryWriter.Write((byte)0);
                    ((ClsAltitude)AltitudeHash[num2]).SaveACOText(binaryWriter);
                }
            }
            while (++num2 <= 255);
            binaryWriter.Close();
            fileStream.Close();
            _ = Interaction.MsgBox("Altitude.aco Saved", MsgBoxStyle.OkOnly, null);
        }

        public void SaveACT()
        {
            #region Data Directory Modification

            var str = string.Format("{0}/Development/DrawingTools/AdobePhotoshop/OptimizedColors/Altitude.act", Directory.GetCurrentDirectory());

            #endregion

            var fileStream = new FileStream(str, FileMode.Create);
            var binaryWriter = new BinaryWriter(fileStream);
            var num = 0;
            var num1 = 0;
            do
            {
                if (AltitudeHash[num1] != null)
                {
                    ((ClsAltitude)AltitudeHash[num1]).SaveACT(binaryWriter);
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
            _ = Interaction.MsgBox("Altitude.act Saved", MsgBoxStyle.OkOnly, null);
        }

        #endregion
    }
}