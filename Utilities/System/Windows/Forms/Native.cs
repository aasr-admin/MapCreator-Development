using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	[SuppressMessage("Interoperability", "SYSLIB1054", Justification = "Incompatible")]
	internal static class Native
	{
		private const uint WM_NCLBUTTONDOWN = 0xA1;
		private const int HT_CAPTION = 0x2;

		[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		internal static extern int SendMessage(nint hWnd, uint wMsg, nint wParam, nint lParam);

		[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		internal static extern bool ReleaseCapture();

		internal static void BeginMoveWindow(object? sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				_ = ReleaseCapture();

				if (sender is Control c)
				{
					var f = c.FindForm();

					if (f != null)
					{
						_ = SendMessage(f.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
					}
				}
			}
		}
	}
}