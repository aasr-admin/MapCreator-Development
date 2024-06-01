using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace System.Drawing
{
	public static class Extensions
	{
		public static void Fill(this Bitmap image, byte indexedColor)
		{
			var b = new Rectangle(0, 0, image.Width, image.Height);
			var bd = image.LockBits(b, ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);

			try
			{
				var matrix = new byte[bd.Width * bd.Height];

				Array.Fill(matrix, indexedColor);

				Marshal.Copy(matrix, 0, bd.Scan0, matrix.Length);
			}
			finally
			{
				image.UnlockBits(bd);
			}
		}

		public static Color Interpolate(this Color source, Color target, float percent)
		{
			if (percent <= 0.0)
			{
				return source;
			}

			if (percent >= 1.0)
			{
				return target;
			}

			var r = (int)(source.R + ((target.R - source.R) * percent));
			var g = (int)(source.G + ((target.G - source.G) * percent));
			var b = (int)(source.B + ((target.B - source.B) * percent));

			return Color.FromArgb(255, r, g, b);
		}
	}
}