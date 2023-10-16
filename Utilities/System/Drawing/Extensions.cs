using System.Runtime.InteropServices;

namespace System.Drawing.Imaging
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
	}
}