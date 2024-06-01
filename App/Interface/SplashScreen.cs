
using System.Timers;

namespace MapCreator
{
	public partial class SplashScreen : Form
	{
		public SplashScreen()
		{
			InitializeComponent();
		}

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);

			// This Screen Will Pop-Up For (1.5) Seconds
			_ = TaskTimer.DelayCall(TimeSpan.FromSeconds(1.5), SplashScreen_closeTimer_Tick);
		}

		private void SplashScreen_closeTimer_Tick()
		{
			Hide();

			_ = StaticForm<FacetBuilder>.Open();
		}
	}
}