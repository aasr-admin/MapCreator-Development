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

namespace MapCreator
{
    public class MapInfo
    {
        private string m_Name;
        private byte m_Num;
        private int m_XSize;
        private int m_YSize;

        #region Getters And Setters

        public string MapName
        {
            get
            {
                return this.m_Name;
            }
        }

        public byte MapNumber
        {
            get
            {
                return this.m_Num;
            }
        }

        public int XSize
        {
            get
            {
                return this.m_XSize;
            }
        }

        public int YSize
        {
            get
            {
                return this.m_YSize;
            }
        }

        #endregion

        public MapInfo(XmlElement iXml)
        {
            this.m_Name = iXml.GetAttribute("Name");
            this.m_Num = ByteType.FromString(iXml.GetAttribute("Num"));
            this.m_XSize = IntegerType.FromString(iXml.GetAttribute("XSize"));
            this.m_YSize = IntegerType.FromString(iXml.GetAttribute("YSize"));
        }

        public override string ToString()
        {
            return string.Format("{0}", this.m_Name);
        }
    }
}
