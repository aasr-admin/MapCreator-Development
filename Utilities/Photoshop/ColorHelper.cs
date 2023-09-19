using System.Drawing;

namespace Photoshop
{
	public static class ColorHelper
    {
        private const ushort NULL16 = 0;

        public static void Convert(ushort v0, ushort v1, ushort v2, ushort v3, ColorFormat inputFormat, out Color color)
        {
            color = Color.Empty;

            switch (inputFormat)
            {
                case ColorFormat.RGB:
                {
                    var r = (byte)Math.Ceiling(v0 / 65535f * 255f);
                    var g = (byte)Math.Ceiling(v1 / 65535f * 255f);
                    var b = (byte)Math.Ceiling(v2 / 65535f * 255f);

                    color = Color.FromArgb(r, g, b);
                }
                break;

                case ColorFormat.HSB:
                {
                    var h = v0 / 65535f * 360f;
                    var s = v1 / 65535f;
                    var b = v2 / 65535f;

                    if (Math.Abs(s - 0) < Double.Epsilon)
                    {
                        var v = (byte)Math.Ceiling(b * 255f);

                        color = Color.FromArgb(v, v, v);
                    }
                    else
                    {
                        var sp = h / 60f;
                        var sn = (byte)Math.Truncate(sp);

                        v0 = (byte)Math.Ceiling(b * 255f);
                        v1 = (byte)Math.Ceiling(b * (1f - s) * 255f);
                        v2 = (byte)Math.Ceiling(b * (1f - s * (sp - sn)) * 255f);
                        v3 = (byte)Math.Ceiling(b * (1f - s * (1f - (sp - sn))) * 255f);

                        switch (sn)
                        {
                            case 0:
                            color = Color.FromArgb(v0, v3, v1);
                            break;

                            case 1:
                            color = Color.FromArgb(v2, v0, v1);
                            break;

                            case 2:
                            color = Color.FromArgb(v1, v0, v3);
                            break;

                            case 3:
                            color = Color.FromArgb(v1, v2, v0);
                            break;

                            case 4:
                            color = Color.FromArgb(v3, v1, v0);
                            break;

                            case 5:
                            color = Color.FromArgb(v0, v1, v2);
                            break;
                        }
                    }
                }
                break;

                case ColorFormat.CMYK:
                {
                    var c = Math.Ceiling(v0 / 65535f * 255f);
                    var m = Math.Ceiling(v1 / 65535f * 255f);
                    var y = Math.Ceiling(v2 / 65535f * 255f);
                    var k = Math.Ceiling(v3 / 65535f);

                    var r = (byte)(255f * (65535f - c) * k);
                    var g = (byte)(255f * (65535f - m) * k);
                    var b = (byte)(255f * (65535f - y) * k);

                    color = Color.FromArgb(r, g, b);
                }
                break;

                case ColorFormat.GreyScale:
                {
                    var v = (byte)Math.Ceiling(v0 / 10000f * 255f);

                    color = Color.FromArgb(v, v, v);
                }
                break;

                default:
                throw new NotSupportedException($"{inputFormat} is not supported.");
            }
        }

        public static void Convert(Color color, ColorFormat outputFormat, out ushort v0, out ushort v1, out ushort v2, out ushort v3)
        {
            v0 = v1 = v2 = v3 = NULL16;

            switch (outputFormat)
            {
                case ColorFormat.RGB:
                {
                    v0 = (ushort)Math.Ceiling(color.R / 255f * 65535f);
                    v1 = (ushort)Math.Ceiling(color.G / 255f * 65535f);
                    v2 = (ushort)Math.Ceiling(color.B / 255f * 65535f);
                }
                break;

                case ColorFormat.HSB:
                {
                    v0 = (ushort)Math.Ceiling(color.GetHue() / 360f * 65535f);
                    v1 = (ushort)Math.Ceiling(color.GetSaturation() * 65535f);
                    v2 = (ushort)Math.Ceiling(color.GetBrightness() * 65535f);
                }
                break;

                case ColorFormat.CMYK:
                {
                    var r = (ushort)Math.Ceiling(color.R / 255f * 65535f);
                    var g = (ushort)Math.Ceiling(color.G / 255f * 65535f);
                    var b = (ushort)Math.Ceiling(color.B / 255f * 65535f);

                    v3 = (ushort)Math.Max(0, 65535f - Math.Max(Math.Max(r, g), b));

                    var d = 65535f - v3;

                    v0 = (ushort)Math.Max(0, (d - r) / d);
                    v1 = (ushort)Math.Max(0, (d - g) / d);
                    v2 = (ushort)Math.Max(0, (d - b) / d);
                }
                break;

                case ColorFormat.GreyScale:
                {
                    if (color.R == color.G && color.G == color.B)
                    {
                        v0 = (ushort)Math.Ceiling(color.R / 255f * 10000);
                    }
                    else
                    {
                        v0 = (ushort)Math.Ceiling(color.GetBrightness() * 10000);
                    }
                }
                break;

                default:
                throw new NotSupportedException($"{outputFormat} is not supported.");
            }
        }
    }
}
