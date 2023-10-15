namespace System.Xml
{
	public static class XmlHelper
	{
		public static int SaveChildren<T>(XmlElement node, string childName, IEnumerable<T> entries) where T : IXmlEntry
		{
			var count = 0;

			foreach (var entry in entries)
			{
				var child = node.OwnerDocument.CreateElement(childName);

				entry.Save(child);

				node.AppendChild(child);

				++count;
			}

			return count;
		}

		public static IEnumerable<T> LoadChildren<T>(XmlElement node, string childName) where T : IXmlEntry, new()
		{
			foreach (XmlElement entry in node.SelectNodes(childName))
			{
				var o = new T();

				o.Load(entry);

				yield return o;
			}
		}

		public static IEnumerable<T> LoadDirectory<T>(string directoryPath, string rootName, string search = "*.xml") where T : IXmlEntry, new()
		{
			foreach (var filePath in Directory.EnumerateFiles(directoryPath, search, SearchOption.AllDirectories))
			{
				yield return Load<T>(filePath, rootName);
			}
		}

		public static void Save<T>(string filePath, string rootName, T entry) where T : IXmlEntry
		{
			var doc = new XmlDocument();

			Save(doc, rootName, entry);

			doc.Save(filePath);
		}

		public static void Save<T>(XmlDocument doc, string rootName, T entry) where T : IXmlEntry
		{
			var root = doc.CreateElement(rootName);

			entry.Save(root);

			doc.AppendChild(root);
		}

		public static T Load<T>(string filePath, string rootName) where T : IXmlEntry, new()
		{
			var doc = new XmlDocument();

			doc.Load(filePath);

			return Load<T>(doc, rootName);
		}

		public static T Load<T>(XmlDocument doc, string rootName) where T : IXmlEntry, new()
		{
			if (doc.SelectSingleNode(rootName) is XmlElement root)
			{
				var o = new T();

				o.Load(root);

				return o;
			}

			return default;
		}

		public static bool Load<T>(string filePath, string rootName, T entry) where T : IXmlEntry
		{
			var doc = new XmlDocument();

			doc.Load(filePath);

			return Load(doc, rootName, entry);
		}

		public static bool Load<T>(XmlDocument doc, string rootName, T entry) where T : IXmlEntry
		{
			if (doc.SelectSingleNode(rootName) is XmlElement root)
			{
				entry.Load(root);

				return true;
			}

			return false;
		}

		public static void WriteNode(XmlElement root, string childName, object value)
		{
			var node = root.OwnerDocument.CreateElement(childName);

			node.Value = $"{value}";

			root.AppendChild(node);
		}

		public static T ReadNode<T>(XmlElement root, string childName, Func<string, T> parser)
		{
			if (root.SelectSingleNode(childName) is XmlElement node)
			{
				return parser(node.Value);
			}

			return default;
		}
	}
}