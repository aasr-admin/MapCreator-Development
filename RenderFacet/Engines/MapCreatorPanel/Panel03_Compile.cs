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

namespace MapCreator
{
    public class RoughEdge
    {
        private Hashtable m_CornerEdge;
        private Hashtable m_LeftEdge;
        private Hashtable m_TopEdge;

        public RoughEdge()
        {
            string str;
            short num;
            IEnumerator enumerator = null;
            IEnumerator enumerator1 = null;
            IEnumerator enumerator2 = null;
            this.m_CornerEdge = new Hashtable();
            this.m_LeftEdge = new Hashtable();
            this.m_TopEdge = new Hashtable();
            XmlDocument xmlDocument = new XmlDocument();
            try
            {
                #region Data Directory Modification

                str = string.Format("{0}MapCompiler\\Engine\\RoughEdge\\Corner.xml", AppDomain.CurrentDomain.BaseDirectory);

                #endregion

                xmlDocument.Load(str);
                try
                {
                    enumerator2 = xmlDocument.SelectNodes("//Terrains/Corner").GetEnumerator();
                    while (enumerator2.MoveNext())
                    {
                        XmlElement current = (XmlElement)enumerator2.Current;
                        num = XmlConvert.ToInt16(current.GetAttribute("TileID"));
                        this.m_CornerEdge.Add(num, num);
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
            catch (Exception exception)
            {
                ProjectData.SetProjectError(exception);
                Interaction.MsgBox(exception.Message, MsgBoxStyle.OkOnly, null);
                ProjectData.ClearProjectError();
            }
            try
            {
                #region Data Directory Modification

                str = string.Format("{0}MapCompiler\\Engine\\RoughEdge\\Left.xml", AppDomain.CurrentDomain.BaseDirectory);

                #endregion

                xmlDocument.Load(str);
                try
                {
                    enumerator1 = xmlDocument.SelectNodes("//Terrains/Left").GetEnumerator();
                    while (enumerator1.MoveNext())
                    {
                        XmlElement xmlElement = (XmlElement)enumerator1.Current;
                        num = XmlConvert.ToInt16(xmlElement.GetAttribute("TileID"));
                        this.m_LeftEdge.Add(num, num);
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
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Interaction.MsgBox(exception1.Message, MsgBoxStyle.OkOnly, null);
                ProjectData.ClearProjectError();
            }
            try
            {
                #region Data Directory Modification

                str = string.Format("{0}MapCompiler\\Engine\\RoughEdge\\Top.xml", AppDomain.CurrentDomain.BaseDirectory);

                #endregion

                xmlDocument.Load(str);
                try
                {
                    enumerator = xmlDocument.SelectNodes("//Terrains/Top").GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        XmlElement current1 = (XmlElement)enumerator.Current;
                        num = XmlConvert.ToInt16(current1.GetAttribute("TileID"));
                        this.m_TopEdge.Add(num, num);
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
            catch (Exception exception2)
            {
                ProjectData.SetProjectError(exception2);
                Interaction.MsgBox(exception2.Message, MsgBoxStyle.OkOnly, null);
                ProjectData.ClearProjectError();
            }
        }

        public short CheckCorner(short TileID)
        {
            //short num;
            //num = (this.m_CornerEdge[TileID] != null ? -5 : 0);
            //return num;

            return this.m_CornerEdge[(object)TileID] == null ? (short)0 : (short)-5;
        }

        public short CheckLeft(short TileID)
        {
            short num = 0;
            if (this.m_LeftEdge[TileID] != null)
            {
                VBMath.Randomize();
                float single = VBMath.Rnd() * 15f;
                if (single == 0f)
                {
                    num = -4;
                }
                else if (single >= 1f && single <= 3f)
                {
                    num = -3;
                }
                else if (single >= 4f && single <= 8f)
                {
                    num = -2;
                }
                else if (single == 9f)
                {
                    num = -1;
                }
                else if (single == 10f)
                {
                    num = 0;
                }
                else if (single >= 11f && single <= 13f)
                {
                    num = 1;
                }
                else if (single == 14f)
                {
                    num = 2;
                }
                else if (single == 15f)
                {
                    num = 3;
                }
            }
            else
            {
                num = 0;
            }
            return num;
        }

        public short CheckTop(short TileID)
        {
            short num = 0;
            if (this.m_TopEdge[TileID] != null)
            {
                VBMath.Randomize();
                float single = VBMath.Rnd() * 15f;
                if (single == 0f)
                {
                    num = -4;
                }
                else if (single >= 1f && single <= 3f)
                {
                    num = -3;
                }
                else if (single >= 4f && single <= 8f)
                {
                    num = -2;
                }
                else if (single == 9f)
                {
                    num = -1;
                }
                else if (single == 10f)
                {
                    num = 0;
                }
                else if (single >= 11f && single <= 13f)
                {
                    num = 1;
                }
                else if (single == 14f)
                {
                    num = 2;
                }
                else if (single == 15f)
                {
                    num = 3;
                }
            }
            else
            {
                num = 0;
            }
            return num;
        }
    }
}