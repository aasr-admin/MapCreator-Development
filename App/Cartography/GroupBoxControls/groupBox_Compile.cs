using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

using System.Collections;
using System.Xml;

namespace MapCreator
{
	public class RoughEdge
	{
		private readonly Hashtable m_CornerEdge;
		private readonly Hashtable m_LeftEdge;
		private readonly Hashtable m_TopEdge;

		public RoughEdge()
		{
			string str;
			ushort num;
			IEnumerator enumerator = null;
			IEnumerator enumerator1 = null;
			IEnumerator enumerator2 = null;
			m_CornerEdge = [];
			m_LeftEdge = [];
			m_TopEdge = [];
			var xmlDocument = new XmlDocument();
			try
			{
				#region Data Directory Modification

				str = String.Format("{0}MapCompiler\\Engine\\RoughEdge\\Corner.xml", AppDomain.CurrentDomain.BaseDirectory);

				#endregion

				xmlDocument.Load(str);
				try
				{
					enumerator2 = xmlDocument.SelectNodes("//Terrains/Corner").GetEnumerator();
					while (enumerator2.MoveNext())
					{
						var current = (XmlElement)enumerator2.Current;
						num = Utility.Parse<ushort>(current.GetAttribute("TileID"));
						m_CornerEdge.Add(num, num);
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
				_ = Interaction.MsgBox(exception.Message, MsgBoxStyle.OkOnly, null);
				ProjectData.ClearProjectError();
			}

			try
			{
				#region Data Directory Modification

				str = String.Format("{0}MapCompiler\\Engine\\RoughEdge\\Left.xml", AppDomain.CurrentDomain.BaseDirectory);

				#endregion

				xmlDocument.Load(str);
				try
				{
					enumerator1 = xmlDocument.SelectNodes("//Terrains/Left").GetEnumerator();
					while (enumerator1.MoveNext())
					{
						var xmlElement = (XmlElement)enumerator1.Current;
						num = Utility.Parse<ushort>(xmlElement.GetAttribute("TileID"));
						m_LeftEdge.Add(num, num);
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
				_ = Interaction.MsgBox(exception1.Message, MsgBoxStyle.OkOnly, null);
				ProjectData.ClearProjectError();
			}

			try
			{
				#region Data Directory Modification

				str = String.Format("{0}MapCompiler\\Engine\\RoughEdge\\Top.xml", AppDomain.CurrentDomain.BaseDirectory);

				#endregion

				xmlDocument.Load(str);
				try
				{
					enumerator = xmlDocument.SelectNodes("//Terrains/Top").GetEnumerator();
					while (enumerator.MoveNext())
					{
						var current1 = (XmlElement)enumerator.Current;
						num = Utility.Parse<ushort>(current1.GetAttribute("TileID"));
						m_TopEdge.Add(num, num);
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
				_ = Interaction.MsgBox(exception2.Message, MsgBoxStyle.OkOnly, null);
				ProjectData.ClearProjectError();
			}
		}

		public sbyte CheckCorner(ushort TileID)
		{
			if (m_CornerEdge[TileID] == null)
			{
				return 0;
			}

			return -5;
		}

		public sbyte CheckLeft(ushort TileID)
		{
			if (m_LeftEdge[TileID] != null)
			{
				VBMath.Randomize();
				var single = VBMath.Rnd() * 15f;
				if (single == 0f)
				{
					return -4;
				}
				else if (single is >= 1f and <= 3f)
				{
					return -3;
				}
				else if (single is >= 4f and <= 8f)
				{
					return -2;
				}
				else if (single == 9f)
				{
					return -1;
				}
				else if (single == 10f)
				{
					return 0;
				}
				else if (single is >= 11f and <= 13f)
				{
					return 1;
				}
				else if (single == 14f)
				{
					return 2;
				}
				else if (single == 15f)
				{
					return 3;
				}
			}

			return 0;
		}

		public sbyte CheckTop(ushort TileID)
		{
			if (m_TopEdge[TileID] != null)
			{
				VBMath.Randomize();
				var single = VBMath.Rnd() * 15f;
				if (single == 0f)
				{
					return -4;
				}
				else if (single is >= 1f and <= 3f)
				{
					return -3;
				}
				else if (single is >= 4f and <= 8f)
				{
					return -2;
				}
				else if (single == 9f)
				{
					return -1;
				}
				else if (single == 10f)
				{
					return 0;
				}
				else if (single is >= 11f and <= 13f)
				{
					return 1;
				}
				else if (single == 14f)
				{
					return 2;
				}
				else if (single == 15f)
				{
					return 3;
				}
			}

			return 0;
		}
	}
}