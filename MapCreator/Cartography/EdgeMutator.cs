using System.Xml;

namespace Cartography
{
	public enum EdgeType
	{
		Corner,
		Top,
		Left
	}

	public sealed class EdgeMutator : IXmlEntry
	{
		private static readonly EdgeType[] _Types = Enum.GetValues<EdgeType>();

		private readonly SortedSet<ushort>[] _Sets;

		public SortedSet<ushort> this[EdgeType type]
		{
			get => _Sets[(int)type];
			private set => _Sets[(int)type] = value;
		}

		public int Count => _Sets.Sum(s => s.Count);

		public EdgeMutator()
		{
			foreach (var type in _Types)
			{
				this[type] = new();
			}
		}

		public override string ToString()
		{
			return $"EdgeMutator ({Count:N0})";
		}

		public void SaveXml(string filePath)
		{
			XmlHelper.Save(filePath, "EdgeMutator", this);
		}

		public void Save(XmlDocument doc)
		{
			XmlHelper.Save(doc, "EdgeMutator", this);
		}

		public void Save(XmlElement node)
		{
			node.SetAttribute("Count", $"{Count:N0}");

			foreach (var type in _Types)
			{
				var name = $"{type}";
				var set = this[type];

				foreach (var id in set)
				{
					var entry = node.OwnerDocument.CreateElement(name);

					entry.SetAttribute("ID", $"{id}");

					node.AppendChild(entry);
				}
			}
		}

		public bool LoadXml(string filePath)
		{
			return XmlHelper.Load(filePath, "EdgeMutator", this);
		}

		public bool Load(XmlDocument doc)
		{
			return XmlHelper.Load(doc, "EdgeMutator", this);
		}

		public void Load(XmlElement node)
		{
			foreach (var type in _Types)
			{
				var name = $"{type}";
				var set = this[type];

				foreach (XmlElement entry in node.SelectNodes(name))
				{
					var id = UInt16.Parse(entry.GetAttribute("ID"));

					set.Add(id);
				}
			}
		}

		public bool Add(EdgeType type, ushort id)
		{
			return this[type].Add(id);
		}

		public bool Remove(EdgeType type, ushort id)
		{
			return this[type].Remove(id);
		}

		public bool Contains(EdgeType type, ushort id)
		{
			return this[type].Contains(id);
		}

		public void Clear()
		{
			foreach (var set in _Sets)
			{
				set.Clear();
			}
		}

		public sbyte Mutate(EdgeType type, ushort tileID)
		{
			if (!Contains(type, tileID))
			{
				return 0;
			}

			return type switch
			{
				EdgeType.Corner => -5,
				EdgeType.Top or EdgeType.Left => Utility.RandomMinMax(0, 15) switch
				{
					0 => -4,
					>= 1 and <= 3 => -3,
					>= 4 and <= 8 => -2,
					9 => -1,
					10 => 0,
					>= 11 and <= 13 => +1,
					14 => +2,
					15 => +3,
					_ => 0,
				},
				_ => 0
			};
		}

		public sbyte Mutate(ushort cornerID, ushort leftID, ushort topID)
		{
			sbyte z = 0;

			if (cornerID >= 0)
			{
				z = Mutate(EdgeType.Corner, cornerID);
			}

			if (leftID >= 0)
			{
				z = Mutate(EdgeType.Left, leftID);
			}

			if (topID >= 0)
			{
				z = Mutate(EdgeType.Top, topID);
			}

			return z;
		}

		public ref LandCell Mutate(FacetMatrix matrix, int x, int y)
		{
			ref var tile = ref matrix.GetLand(x, y);

			if (x > 0 && y > 0 && x < matrix.Width && y < matrix.Height)
			{
				ref var corner = ref matrix.GetLand(x - 1, y - 1);
				ref var left = ref matrix.GetLand(x - 1, y);
				ref var top = ref matrix.GetLand(x, y - 1);

				tile.Z = Mutate(corner.ID, left.ID, top.ID);
			}

			return ref tile;
		}
	}
}