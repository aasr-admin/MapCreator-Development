using System.Xml;

namespace Cartography
{
	public sealed class Facet : FacetMatrix, IXmlEntry, IComparable<Facet>
	{
		public byte Index { get; set; }

		public string Name { get; set; } = String.Empty;

		public bool IsValid => Area > 0 && !String.IsNullOrWhiteSpace(Name);

		public override string ToString()
		{
			return $"{Index} {Name} ({Width} x {Height})";
		}

		public int CompareTo(Facet? other)
		{
			return Index.CompareTo(other?.Index);
		}

		public IEnumerable<LandCell> GetLandSequence(int x, int y)
		{
			LandCell land;

			if (GetLandSafe(x - 1, y - 1, out land))
			{
				yield return land;
			}

			if (GetLandSafe(x, y - 1, out land))
			{
				yield return land;
			}

			if (GetLandSafe(x + 1, y - 1, out land))
			{
				yield return land;
			}

			if (GetLandSafe(x - 1, y, out land))
			{
				yield return land;
			}

			if (GetLandSafe(x, y, out land))
			{
				yield return land;
			}

			if (GetLandSafe(x + 1, y, out land))
			{
				yield return land;
			}

			if (GetLandSafe(x - 1, y + 1, out land))
			{
				yield return land;
			}

			if (GetLandSafe(x, y + 1, out land))
			{
				yield return land;
			}

			if (GetLandSafe(x + 1, y + 1, out land))
			{
				yield return land;
			}
		}

		private bool GetLandSafe(int x, int y, out LandCell land)
		{
			if (x >= 0 && x < Width && y >= 0 && y < Height)
			{
				land = GetLand(x, y);

				return true;
			}

			land = default;

			return false;
		}

		public void SaveLandMatrix(string mulPath, Action<int, int>? progressCallback = null)
		{
			var processIndex = 0;
			var processCount = Area / 8;

			using var mulStream = new FileStream(mulPath, FileMode.Create);
			using var mulWriter = new BinaryWriter(mulStream);

			try
			{
				for (var x = 0; x < Width; x += 8)
				{
					for (var y = 0; y < Height; y += 8)
					{
						mulWriter.Write(1);

						for (var by = 0; by < 8; by++)
						{
							for (var bx = 0; bx < 8; bx++)
							{
								ref var tile = ref GetLand(x + bx, y + by);

								mulWriter.Write(tile.ID);
								mulWriter.Write(tile.Z);
							}
						}

						if (++processIndex < processCount)
						{
							progressCallback?.Invoke(processIndex, processCount);
						}
					}
				}
			}
			finally
			{
				mulWriter.Flush();
			}
		}

		public void SaveStaticMatrix(string idxFilePath, string mulFilePath, Action<int, int>? progressCallback = null)
		{
			var processIndex = 0;
			var processCount = Area;

			using var staIdxFile = new FileStream(idxFilePath, FileMode.Create);
			using var staIdxWriter = new BinaryWriter(staIdxFile);

			using var staticsStream = new FileStream(mulFilePath, FileMode.Create);
			using var staticsWriter = new BinaryWriter(staticsStream);

			try
			{
				var staticsPosition = 0;

				for (var x = 0; x < Width; x++)
				{
					for (var y = 0; y < Height; y++)
					{
						var length = 0;

						ref var tiles = ref GetStatics(x, y);

						for (var i = 0; i < tiles.Length; i++)
						{
							ref var tile = ref tiles[i];

							staticsWriter.Write(tile.ID);
							staticsWriter.Write((byte)tile.X);
							staticsWriter.Write((byte)tile.Y);
							staticsWriter.Write(tile.Z);
							staticsWriter.Write(tile.Hue);

							length += 7;
						}

						if (length > 0)
						{
							staIdxWriter.Write(staticsPosition);

							staticsPosition += length;
						}
						else
						{
							staIdxWriter.Write(-1);
						}

						staIdxWriter.Write(length);
						staIdxWriter.Write(1);

						if (++processIndex < processCount)
						{
							progressCallback?.Invoke(processIndex, processCount);
						}
					}
				}
			}
			finally
			{
				staIdxWriter.Flush();
				staticsWriter.Flush();
			}
		}

		public void SaveXml(string filePath)
		{
			XmlHelper.Save(filePath, "Facet", this);
		}

		public void Save(XmlDocument doc)
		{
			XmlHelper.Save(doc, "Facet", this);
		}

		public void Save(XmlElement node)
		{
			node.SetAttribute("Index", $"{Index}");

			node.SetAttribute("Name", $"{Name}");

			node.SetAttribute("Width", $"{Width}");
			node.SetAttribute("Height", $"{Height}");
		}

		public bool LoadXml(string filePath)
		{
			return XmlHelper.Load(filePath, "Facet", this);
		}

		public bool Load(XmlDocument doc)
		{
			return XmlHelper.Load(doc, "Facet", this);
		}

		public void Load(XmlElement node)
		{
			Index = XmlConvert.ToByte(node.GetAttribute("Index"));

			Name = node.GetAttribute("Name");

			var width = XmlConvert.ToInt32(node.GetAttribute("Width"));
			var height = XmlConvert.ToInt32(node.GetAttribute("Height"));

			Resize(width, height);
		}
	}
}