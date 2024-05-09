
using System.Numerics;

namespace MapCreator
{
    public static class Utility
    {
        [ThreadStatic]
        private static Random _random;

        public static T Random<T>() where T : INumber<T>, IMinMaxValue<T>
        {
            return Random(T.MinValue, T.MaxValue);
        }

        public static T Random<T>(T min, T maxExclusive) where T : INumber<T>
        {
            return min + Random(maxExclusive - min);
        }

        public static T Random<T>(T maxExclusive) where T : INumber<T>
        {
            _random ??= new Random();

            return T.CreateTruncating(Math.Floor(double.CreateTruncating(maxExclusive) * _random.NextDouble()));
        }

        public static T Parse<T>(string input) where T : INumber<T>
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return default;
            }

            return Parse<T>(input.AsSpan());
        }

        public static T Parse<T>(ReadOnlySpan<char> input) where T : INumber<T>
        {
            if (input.IsEmpty || input.IsWhiteSpace())
            {
                return default;
            }

            if (!T.TryParse(input, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out var result))
            {
                result = T.Parse(input, System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture);
            }

            return result;
        }
    }
}