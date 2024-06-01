using System.Diagnostics;
using System.Globalization;
using System.Numerics;
using System.Text.RegularExpressions;

namespace System
{
	public static class Utility
	{
		[ThreadStatic]
		private static Random? _Random;

		public static void SeedRandom(int seed)
		{
			_Random = new Random(seed);
		}

		public static double RandomDouble()
		{
			_Random ??= new Random();

			return _Random.NextDouble();
		}

		public static int Random()
		{
			_Random ??= new Random();

			return _Random.Next();
		}

		public static int Random(int maxValue)
		{
			_Random ??= new Random();

			return _Random.Next(maxValue);
		}

		public static int Random(int minValue, int maxValue)
		{
			_Random ??= new Random();

			return _Random.Next(minValue, maxValue);
		}

		public static int RandomMinMax(int minValue, int maxValue)
		{
			_Random ??= new Random();

			return _Random.Next(minValue, maxValue + 1);
		}

		public static bool RandomBool()
		{
			_Random ??= new Random();

			return _Random.Next() % 2 == 0;
		}

		public static T RandomList<T>(T[] list)
		{
			if (list?.Length > 0)
			{
				return list[Random(list.Length)];
			}

			return default!;
		}

		public static T RandomList<T>(IList<T> list)
		{
			if (list?.Count > 0)
			{
				return list[Random(list.Count)];
			}

			return default!;
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

			foreach (var entry in collection.OrderBy(o => RandomDouble()))
			{
				return entry;
			}

			return default!;
		}

		public static T Min<T>(T value, params T[] values) where T : IComparable
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

		public static T Max<T>(T value, params T[] values) where T : IComparable
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

		public static N RoundUp<N>(N value, N multiple) where N : struct, INumber<N>
		{
			RoundUp(ref value, multiple);

			return value;
		}

		public static void RoundUp<N>(ref N value, N multiple) where N : struct, INumber<N>
		{
			if (!N.IsZero(multiple) && !N.IsZero(value % multiple))
			{
				var div = value / multiple;

				value = ++div * multiple;
			}
		}

		public static void InsertionSort<T>(T?[] array) where T : IComparable
		{
			var index1 = 0;

			while (index1 < array.Length)
			{
				var index2 = index1++;

				while (index2 >= 0)
				{
					if (array[index1]?.CompareTo(array[index2]) > 0)
					{
						array[index2 + 1] = array[index2];

						--index2;
					}
					else
					{
						break;
					}
				}

				array[index2 + 1] = array[index1];
			}
		}

		public static string? FindDataFile(string dataPath, string search)
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

		public static object? Launch(string uri, string? args = null, string? startIn = null, bool admin = false)
		{
			try
			{
				var isUri = IsWebUri(uri);

				if (!isUri && !File.Exists(uri))
				{
					return false;
				}

				var info = new ProcessStartInfo(uri, args ?? String.Empty);

				if (isUri)
				{
					info.UseShellExecute = true;
				}
				else
				{
					if (admin)
					{
						info.Verb = "runas";
					}

					if (String.IsNullOrWhiteSpace(startIn))
					{
						startIn = Path.GetDirectoryName(uri);
					}

					if (Directory.Exists(startIn))
					{
						info.WorkingDirectory = startIn;
					}
				}

				return Process.Start(info);
			}
			catch (Exception x)
			{
				return x;
			}
		}

		public static bool IsWebUri(string value)
		{
			if (Directory.Exists(value) || File.Exists(value))
			{
				return false;
			}

			if (Uri.IsWellFormedUriString(value, UriKind.Absolute))
			{
				return true;
			}

			if (Regex.IsMatch(value, String.Join("|", Uri.UriSchemeHttp, Uri.UriSchemeHttps)))
			{
				return true;
			}

			return false;
		}

		public static T? ParseNumber<T>(string input) where T : INumber<T>
		{
			if (String.IsNullOrWhiteSpace(input))
			{
				return default;
			}

			return ParseNumber<T>(input.AsSpan());
		}

		public static T? ParseNumber<T>(ReadOnlySpan<char> input) where T : INumber<T>
		{
			if (input.IsEmpty || input.IsWhiteSpace())
			{
				return default;
			}

			if (!T.TryParse(input, NumberStyles.Number, CultureInfo.InvariantCulture, out var result))
			{
				result = T.Parse(input, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
			}

			return result;
		}
	}
}
