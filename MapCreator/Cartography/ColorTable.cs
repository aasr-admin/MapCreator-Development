using Photoshop;

using System.Drawing.Imaging;
using System.Xml;

namespace Cartography
{
	public abstract class ColorTable<T> : ColorCollection<T>, IXmlEntry where T : IColorTableEntry, new()
	{
		protected abstract string RootNodeName { get; }
		protected abstract string EntryNodeName { get; }

		public ColorTable()
			: base(Byte.MaxValue + 1)
		{
		}

		public virtual ColorPalette GetPalette()
		{
			using var bmp = new Bitmap(2, 2, PixelFormat.Format8bppIndexed);

			var palette = bmp.Palette;

			bmp.Palette = null;

			for (var i = 0; i < Length; i++)
			{
				palette.Entries[i] = this[i].Color;
			}

			return palette;
		}

		public void SaveXml(string filePath)
		{
			XmlHelper.Save(filePath, RootNodeName, this);
		}

		public void Save(XmlDocument doc)
		{
			XmlHelper.Save(doc, RootNodeName, this);
		}

		public virtual void Save(XmlElement node)
		{
			node.SetAttribute("Count", $"{Length:N0}");

			foreach (var entry in this)
			{
				var child = node.OwnerDocument.CreateElement(EntryNodeName);

				entry.Save(child);

				_ = node.AppendChild(child);
			}
		}

		public void LoadXml(string filePath)
		{
			_ = XmlHelper.Load(filePath, RootNodeName, this);
		}

		public void Load(XmlDocument doc)
		{
			_ = XmlHelper.Load(doc, RootNodeName, this);
		}

		public virtual void Load(XmlElement node)
		{
			var index = -1;

			foreach (XmlElement entry in node.SelectNodes(EntryNodeName))
			{
				if (++index < Length)
				{
					this[index].Load(node);
				}
				else
				{
					break;
				}
			}
		}
	}
}