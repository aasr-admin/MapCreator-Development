using System.Diagnostics;

namespace MapCreator
{
	public partial class CommunityCredits : Form
	{
		public CommunityCredits()
		{
			InitializeComponent();
		}

		private void communityCredits_button_close_MouseEnter(object sender, EventArgs e)
		{
			communityCredits_button_close.ForeColor = Color.LimeGreen;
		}

		private void communityCredits_button_close_Click(object sender, EventArgs e)
		{
			Hide();
		}

		private void communityCredits_button_close_MouseLeave(object sender, EventArgs e)
		{
			communityCredits_button_close.ForeColor = Color.SlateGray;
		}

		private void communityCredits_linkLabel_uoAvocation_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			var uoAvocation = new ProcessStartInfo
			{
				FileName = "http://www.uoavocation.net",
				UseShellExecute = true
			};

			_ = Process.Start(uoAvocation);
		}
	}
}
