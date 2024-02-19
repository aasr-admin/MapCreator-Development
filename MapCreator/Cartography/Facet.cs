using System.Xml;

namespace Cartography
{
	public sealed class Facet : IXmlEntry, IComparable<Facet>
	{
		public const byte INTERNAL_INDEX = 127;

		public const int BLOCK_SIZE = 8;

		public const int DEFAULT_WIDTH = 7168;
		public const int DEFAULT_HEIGHT = 4096;

		public byte Index { get; set; }

		public string Name { get; set; } = String.Empty;

		public Rectangle Bounds { get; private set; }

		public Size Size => Bounds.Size;

		public int Width => Bounds.Width;
		public int Height => Bounds.Height;

		public int Area => Bounds.Width * Bounds.Height;

		public bool IsValid => Index != INTERNAL_INDEX;

		private readonly LandMatrix _LandMatrix;
		private readonly StaticMatrix _StaticMatrix;

		public Facet()
			: this(DEFAULT_WIDTH, DEFAULT_HEIGHT)
		{
		}

		public Facet(int width, int height)
		{
			Utility.RoundUp(ref width, BLOCK_SIZE);
			Utility.RoundUp(ref height, BLOCK_SIZE);

			Bounds = new Rectangle(0, 0, width, height);

			_LandMatrix = new LandMatrix(width, height);
			_StaticMatrix = new StaticMatrix(width, height);
		}

		public override string ToString()
		{
			return $"{Index} {Name} ({Width} x {Height})";
		}

		public int CompareTo(Facet? other)
		{
			return Index.CompareTo(other?.Index);
		}

		public void Clear()
		{
			_LandMatrix.Clear();
			_StaticMatrix.Clear();
		}

		public void Clear(int x, int y)
		{
			_LandMatrix.Clear(x, y);
			_StaticMatrix.Clear(x, y);
		}

		public void Resize(int width, int height)
		{
			_LandMatrix.Resize(width, height);
			_StaticMatrix.Resize(width, height);
		}

		public ref LandCell GetLand(int x, int y)
		{
			return ref _LandMatrix[x, y];
		}

		public void SetLand(int x, int y, LandCell tile)
		{
			_LandMatrix[x, y] = tile;
		}

		public void SetLand(int x, int y, sbyte z)
		{
			_ = _LandMatrix.Set(x, y, z);
		}

		public void SetLand(int x, int y, sbyte z, ushort tileID)
		{
			_ = _LandMatrix.Set(x, y, z, tileID);
		}

		public void ClearStatics(int x, int y)
		{
			_StaticMatrix.Clear(x, y);
		}

		public ref StaticCell[] GetStatics(int x, int y)
		{
			return ref _StaticMatrix[x, y];
		}

		public void SetStatics(int x, int y, StaticCell[] tiles)
		{
			_StaticMatrix[x, y] = tiles;
		}

		public ref StaticCell AddStatic(int x, int y, sbyte z, ushort tileID)
		{
			return ref _StaticMatrix.Add(x, y, z, tileID);
		}

		public ref StaticCell AddStatic(int x, int y, sbyte z, ushort tileID, ushort hue)
		{
			return ref _StaticMatrix.Add(x, y, z, tileID, hue);
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
			var processCount = Area / BLOCK_SIZE;

			using var mulStream = new FileStream(mulPath, FileMode.Create);
			using var mulWriter = new BinaryWriter(mulStream);

			try
			{
				for (var x = 0; x < Width; x += BLOCK_SIZE)
				{
					for (var y = 0; y < Height; y += BLOCK_SIZE)
					{
						mulWriter.Write(1);

						for (var by = 0; by < BLOCK_SIZE; by++)
						{
							for (var bx = 0; bx < BLOCK_SIZE; bx++)
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

				for (var x = 0; x < Width; x += BLOCK_SIZE)
				{
					for (var y = 0; y < Height; y += BLOCK_SIZE)
					{
						var length = 0;

						for (var by = 0; by < BLOCK_SIZE; by++)
						{
							for (var bx = 0; bx < BLOCK_SIZE; bx++)
							{
								ref var tiles = ref GetStatics(x + bx, y + by);

								for (var i = 0; i < tiles.Length; i++)
								{
									ref var tile = ref tiles[i];

									staticsWriter.Write(tile.ID);
									staticsWriter.Write((byte)bx);
									staticsWriter.Write((byte)by);
									staticsWriter.Write(tile.Z);
									staticsWriter.Write(tile.Hue);

									length += 7;
								}
							}
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