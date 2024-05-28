
using Timer = System.Windows.Forms.Timer;

namespace MapCreator
{
	public partial class splashScreen : Form
	{
		public splashScreen()
		{
			InitializeComponent();
		}

		private void splashScreen_Shown(object sender, EventArgs e)
		{
			splashScreen_closeTimer = new Timer
			{
				Interval = 1500 // This Screen Will Pop-Up For (1.5) Seconds
			};
			splashScreen_closeTimer.Tick += splashScreen_closeTimer_Tick;

			splashScreen_closeTimer.Start();
		}

		private void splashScreen_closeTimer_Tick(object sender, EventArgs e)
		{
			splashScreen_closeTimer.Stop();

			Hide();

			_ = StaticForm<facetBuilder>.Open();
		}
	}
}