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

				_ = node.AppendChild(child);

				++count;
			}

			return count;
		}

		public static IEnumerable<T> LoadChildren<T>(XmlElement node, string childName) where T : IXmlEntry, new()
		{
			var nodes = node.SelectNodes(childName);

			if (nodes?.Count > 0)
			{
				foreach (XmlElement entry in nodes)
				{
					var o = new T();

					o.Load(entry);

					yield return o;
				}
			}
		}

		public static IEnumerable<T> LoadDirectory<T>(string directoryPath, string rootName, string search = "*.xml") where T : IXmlEntry, new()
		{
			foreach (var filePath in Directory.EnumerateFiles(directoryPath, search, SearchOption.AllDirectories))
			{
				var loaded = Load<T>(filePath, rootName);

				if (loaded != null)
				{
					yield return loaded;
				}
			}
		}

		public static void Save<T>(string filePath, T entry) where T : IXmlEntry
		{
			var doc = new XmlDocument();

			var rootName = Path.GetFileNameWithoutExtension(filePath);

			rootName = rootName.Replace("-", String.Empty);
			rootName = rootName.Replace("_", String.Empty);
			rootName = rootName.Replace(" ", String.Empty);

			Save(doc, rootName, entry);

			doc.Save(filePath);
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

			_ = doc.AppendChild(root);
		}

		public static void Save<T>(XmlElement parent, string childName, T entry) where T : IXmlEntry
		{
			var child = parent.OwnerDocument.CreateElement(childName);

			entry.Save(child);

			_ = parent.AppendChild(child);
		}

		public static T? Load<T>(string filePath) where T : IXmlEntry, new()
		{
			var doc = new XmlDocument();

			doc.Load(filePath);

			var rootName = doc.DocumentElement?.Name;

			if (rootName != null)
			{
				return Load<T>(doc, rootName);
			}

			return default;
		}

		public static T? Load<T>(string filePath, string rootName) where T : IXmlEntry, new()
		{
			var doc = new XmlDocument();

			doc.Load(filePath);

			return Load<T>(doc, rootName);
		}

		public static T? Load<T>(XmlDocument doc, string rootName) where T : IXmlEntry, new()
		{
			if (doc.SelectSingleNode(rootName) is XmlElement root)
			{
				var o = new T();

				o.Load(root);

				return o;
			}

			return default;
		}

		public static T? Load<T>(XmlElement parent, string childName) where T : IXmlEntry, new()
		{
			if (parent.SelectSingleNode(childName) is XmlElement child)
			{
				var o = new T();

				o.Load(child);

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

		public static bool Load<T>(string filePath, T entry) where T : IXmlEntry
		{
			var doc = new XmlDocument();

			doc.Load(filePath);

			var rootName = doc.DocumentElement?.Name;

			if (rootName != null)
			{
				return Load(doc, rootName, entry);
			}

			return false;
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

		public static bool Load<T>(XmlElement parent, string childName, T entry) where T : IXmlEntry
		{
			if (parent.SelectSingleNode(childName) is XmlElement child)
			{
				entry.Load(child);

				return true;
			}

			return false;
		}

		public static void WriteNode<T>(XmlElement parent, string childName, T? value)
		{
			var node = parent.OwnerDocument.CreateElement(childName);

			node.SetAttribute("Value", $"{value}");

			_ = parent.AppendChild(node);
		}

		public static T? ReadNode<T>(XmlElement parent, string childName, Func<string, T> parser)
		{
			if (parent.SelectSingleNode(childName) is XmlElement node)
			{
				return parser(node.GetAttribute("Value"));
			}

			return default;
		}
	}
}