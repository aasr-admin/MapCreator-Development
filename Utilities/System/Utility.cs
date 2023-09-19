namespace System
{
	public static class Utility
	{
		private static readonly Random _Random = new();

		public static double RandomDouble()
		{
			return _Random.NextDouble();
		}

		public static int Random()
		{
			return _Random.Next();
		}

		public static int Random(int maxValue)
		{
			return _Random.Next(maxValue);
		}

		public static int Random(int minValue, int maxValue)
		{
			return _Random.Next(minValue, maxValue);
		}

		public static int RandomMinMax(int minValue, int maxValue)
		{
			return _Random.Next(minValue, maxValue + 1);
		}

		public static bool RandomBool()
		{
			return _Random.Next() % 2 == 0;
		}

		public static T RandomList<T>(T[] list)
		{
			if (list?.Length > 0)
			{
				return list[Random(list.Length)];
			}

			return default;
		}

		public static T RandomList<T>(IList<T> list)
		{
			if (list?.Count > 0)
			{
				return list[Random(list.Count)];
			}

			return default;
		}

		public static T GetRandom<T>(IEnumerable<T> collection)
		{
			if (collection is T[] arr)
			{
				return RandomList(arr);
			}

			if (collection is IList<T> list)
			{
				return RandomList(list);
			}

			foreach (var entry in collection.OrderBy(o => Random(o.GetHashCode())))
			{
				return entry;
			}

			return default;
        }

        public static T Min<T>(T value, params T[] values) where T : struct, IComparable<T>
        {
            foreach (var v in values)
            {
                if (v.CompareTo(value) < 0)
                {
                    value = v;
                }
            }

            return value;
        }

        public static T Max<T>(T value, params T[] values) where T : struct, IComparable<T>
        {
			foreach (var v in values)
			{
				if (v.CompareTo(value) > 0)
				{
					value = v;
				}
			}

			return value;
		}

		public static string FindDataFile(string dataPath, string search)
		{
			var fullName = Path.Combine(dataPath, search);

			if (File.Exists(fullName))
			{
				return fullName;
			}

			foreach (var file in Directory.EnumerateFiles(dataPath, search, SearchOption.AllDirectories))
			{
				return file;
			}

			return null;
		}
	}
}
