using Timer = System.Windows.Forms.Timer;

namespace MapCreator
{
	public partial class SplashScreen : Form
	{
		public SplashScreen()
		{
			InitializeComponent();
		}

		private void splashScreen_Shown(object? sender, EventArgs e)
		{
			splashScreen_closeTimer = new Timer
			{
				Interval = 1500 // This Screen Will Pop-Up For Two (1.5) Seconds
			};

			splashScreen_closeTimer.Start();
			splashScreen_closeTimer.Tick += new EventHandler(splashScreen_closeTimer_Tick);
		}

		private void splashScreen_closeTimer_Tick(object? sender, EventArgs e)
		{
			splashScreen_closeTimer.Stop();

			Hide();

			var facetBuilderForm = new FacetBuilder();
			facetBuilderForm.Show();
		}
	}
}