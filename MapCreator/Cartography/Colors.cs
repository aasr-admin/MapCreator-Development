using Photoshop;

using System.Xml;

namespace Cartography
{
	public abstract class Colors<T> : ColorCollection<T>, IXmlEntry where T : IColorEntry, IXmlEntry, new()
	{
		protected abstract string RootNodeName { get; }
		protected abstract string EntryNodeName { get; }

		public Colors()
			: base(Byte.MaxValue + 1)
		{
		}

		public virtual void SaveXml(string filePath)
		{
			XmlHelper.Save(filePath, RootNodeName, this);
		}

		public virtual void Save(XmlDocument doc)
		{
			XmlHelper.Save(doc, RootNodeName, this);
		}

		public virtual void Save(XmlElement node)
		{
			var count = XmlHelper.SaveChildren(node, EntryNodeName, this);

			node.SetAttribute("Count", $"{count:N0}");
		}

		public virtual bool LoadXml(string filePath)
		{
			return XmlHelper.Load(filePath, RootNodeName, this);
		}

		public virtual bool Load(XmlDocument doc)
		{
			return XmlHelper.Load(doc, RootNodeName, this);
		}

		public virtual void Load(XmlElement node)
		{
			var index = -1;

			foreach (var entry in XmlHelper.LoadChildren<T>(node, EntryNodeName))
			{
				if (++index < Length)
				{
					this[index] = entry;
				}
				else
				{
					break;
				}
			}
		}
	}
}