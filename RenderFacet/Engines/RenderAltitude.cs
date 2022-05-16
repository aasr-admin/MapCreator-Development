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

namespace Altitude
{
	public class ClsAltitude
	{
		private int m_Key;
		private string m_Type;
		private short m_Alt;

		private Color m_AltColor;

        [Category("ID")]
		public Color AltitudeColor
		{
			get
			{
				return this.m_AltColor;
			}
			set
			{
				this.m_AltColor = value;
			}
		}

		[Category("Method")]
		public short GetAltitude
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

		[Category("Key")]
		public int Key
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

		[Category("ID")]
		public string Type
		{
			get
			{
				return this.m_Type;
			}
			set
			{
				this.m_Type = value;
			}
		}

        public ClsAltitude(int iKey, string iType, short iAlt, Color iAltColor)
        {
            this.m_Key = iKey;
            this.m_Type = iType;
            this.m_Alt = iAlt;
            this.m_AltColor = iAltColor;
        }

        public ClsAltitude(XmlElement xmlInfo)
        {
            this.m_Key = XmlConvert.ToInt32(xmlInfo.GetAttribute("Key"));
            this.m_Type = xmlInfo.GetAttribute("Type");
            this.m_Alt = XmlConvert.ToInt16(xmlInfo.GetAttribute("Altitude"));
            this.m_AltColor = Color.FromArgb((int)XmlConvert.ToByte(xmlInfo.GetAttribute("R")), (int)XmlConvert.ToByte(xmlInfo.GetAttribute("G")), (int)XmlConvert.ToByte(xmlInfo.GetAttribute("B")));
        }

        public override string ToString()
		{
			string str = string.Format("[{0:X3}] {1} {2}", this.m_Key, this.m_Type, this.m_Alt);
			return str;
		}

		# region Altitude Swatch And Color Table

        public void Save(XmlTextWriter xmlInfo)
		{
			xmlInfo.WriteStartElement("Altitude");
			xmlInfo.WriteAttributeString("Key", StringType.FromInteger(this.m_Key));
			xmlInfo.WriteAttributeString("Type", this.m_Type);
			xmlInfo.WriteAttributeString("Altitude", StringType.FromInteger(this.m_Alt));
			xmlInfo.WriteAttributeString("R", StringType.FromByte(this.m_AltColor.R));
			xmlInfo.WriteAttributeString("G", StringType.FromByte(this.m_AltColor.G));
			xmlInfo.WriteAttributeString("B", StringType.FromByte(this.m_AltColor.B));
			xmlInfo.WriteEndElement();
		}

		public void SaveACO(BinaryWriter iACTFile)
		{
			byte num = 0;
			iACTFile.Write(this.m_AltColor.R);
			iACTFile.Write(this.m_AltColor.R);
			iACTFile.Write(this.m_AltColor.G);
			iACTFile.Write(this.m_AltColor.G);
			iACTFile.Write(this.m_AltColor.B);
			iACTFile.Write(this.m_AltColor.B);
			iACTFile.Write(num);
			iACTFile.Write(num);
		}

		public void SaveACOText(BinaryWriter iACTFile)
		{
			byte num = 0;
			iACTFile.Write(this.m_AltColor.R);
			iACTFile.Write(this.m_AltColor.R);
			iACTFile.Write(this.m_AltColor.G);
			iACTFile.Write(this.m_AltColor.G);
			iACTFile.Write(this.m_AltColor.B);
			iACTFile.Write(this.m_AltColor.B);
			iACTFile.Write(num);
			iACTFile.Write(num);
			iACTFile.Write(num);
			iACTFile.Write(num);
			UnicodeEncoding unicodeEncoding = new UnicodeEncoding(true, true);
			string str = string.Format("{0} {1}", this.m_Type, this.m_Alt);
			byte[] bytes = unicodeEncoding.GetBytes(str);
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
			iACTFile.Write(this.m_AltColor.R);
			iACTFile.Write(this.m_AltColor.G);
			iACTFile.Write(this.m_AltColor.B);
		}

		#endregion
	}

    public class ClsAltitudeTable
    {
        private Hashtable i_AltitudeTable;

        public Hashtable AltitudeHash
        {
            get
            {
                return this.i_AltitudeTable;
            }
        }

        public void SetAltitude(int Index, ClsAltitude Value)
        {
            this.i_AltitudeTable[Index] = Value;
        }

        public ClsAltitude GetAltitude(int Index)
        {
            return (ClsAltitude)this.i_AltitudeTable[Index];
        }

        public ClsAltitudeTable()
        {
            this.i_AltitudeTable = new Hashtable();
        }

        public void Display(ListBox iList)
        {
            IEnumerator enumerator = null;
            iList.Items.Clear();
            try
            {
                enumerator = this.i_AltitudeTable.Values.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    ClsAltitude current = (ClsAltitude)enumerator.Current;
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

        public ColorPalette GetAltPalette()
        {
            IEnumerator enumerator = null;
            ColorPalette palette = (new Bitmap(2, 2, PixelFormat.Format8bppIndexed)).Palette;
            try
            {
                enumerator = this.i_AltitudeTable.Values.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    ClsAltitude current = (ClsAltitude)enumerator.Current;
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

            string str = string.Format("{0}MapCompiler/Engine/Altitude.xml", AppDomain.CurrentDomain.BaseDirectory);

            #endregion

            XmlDocument xmlDocument = new XmlDocument();
            try
            {
                xmlDocument.Load(str);
                this.i_AltitudeTable.Clear();
                try
                {
                    enumerator1 = xmlDocument.SelectNodes("Altitudes").GetEnumerator();
                    while (enumerator1.MoveNext())
                    {
                        XmlElement current = (XmlElement)enumerator1.Current;
                        try
                        {
                            enumerator = current.SelectNodes("Altitude").GetEnumerator();
                            while (enumerator.MoveNext())
                            {
                                ClsAltitude clsAltitude = new ClsAltitude((XmlElement)enumerator.Current);
                                this.i_AltitudeTable.Add(clsAltitude.Key, clsAltitude);
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
                Interaction.MsgBox(string.Format("XMLFile:{0}", str), MsgBoxStyle.OkOnly, null);
                ProjectData.ClearProjectError();
            }
        }

        public void Save()
        {
            IEnumerator enumerator = null;

            #region Data Directory Modification

            string str = string.Format("{0}MapCompiler\\Engine\\Altitude.xml", AppDomain.CurrentDomain.BaseDirectory);

            #endregion

            XmlTextWriter xmlTextWriter = new XmlTextWriter(str, Encoding.UTF8)
            {
                Indentation = 2,
                Formatting = Formatting.Indented
            };
            xmlTextWriter.WriteStartDocument();
            xmlTextWriter.WriteStartElement("Altitudes");
            try
            {
                enumerator = this.i_AltitudeTable.Values.GetEnumerator();
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
            byte num = Convert.ToByte(this.i_AltitudeTable.Count);

            #region Data Directory Modification

            // string str = string.Format("{0}/../Utilities/AdobePhotoshopCC/Altitude.aco", Directory.GetCurrentDirectory());

            string str = string.Format("Development/DrawingTools/AdobePhotoshop/Presets/Color Swatches/Altitude.ACO", Directory.GetCurrentDirectory());

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
                if (this.i_AltitudeTable[num1] != null)
                {
                    binaryWriter.Write((byte)0);
                    binaryWriter.Write((byte)0);
                    ((ClsAltitude)this.i_AltitudeTable[num1]).SaveACO(binaryWriter);
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
                if (this.i_AltitudeTable[num2] != null)
                {
                    binaryWriter.Write((byte)0);
                    binaryWriter.Write((byte)0);
                    ((ClsAltitude)this.i_AltitudeTable[num2]).SaveACOText(binaryWriter);
                }
                num2++;
            }
            while (num2 <= 255);
            binaryWriter.Close();
            fileStream.Close();
            Interaction.MsgBox("Altitude.ACO Saved", MsgBoxStyle.OkOnly, null);
        }

        public void SaveACT()
        {
            #region Data Directory Modification

            // string str = string.Format("{0}/../Utilities/AdobePhotoshopCC/Altitude.act", Directory.GetCurrentDirectory());

            string str = string.Format("Development/DrawingTools/AdobePhotoshop/Presets/Optimized Colors/Altitude.ACT", Directory.GetCurrentDirectory());

            #endregion

            FileStream fileStream = new FileStream(str, FileMode.Create);
            BinaryWriter binaryWriter = new BinaryWriter(fileStream);
            byte num = 0;
            int num1 = 0;
            do
            {
                if (this.i_AltitudeTable[num1] != null)
                {
                    ((ClsAltitude)this.i_AltitudeTable[num1]).SaveACT(binaryWriter);
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
            Interaction.MsgBox("Altitude.ACT Saved", MsgBoxStyle.OkOnly, null);
        }

        #endregion
    }
}