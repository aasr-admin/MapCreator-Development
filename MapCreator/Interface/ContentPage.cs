using System.ComponentModel;
using System.Timers;

namespace MapCreator.Interface
{
	[ToolboxItem(false)]
	public partial class ContentPage : UserControl
	{
		protected override Size DefaultSize { get; } = new Size(548, 186);
		protected override Size DefaultMinimumSize { get; } = new Size(548, 186);

		public ContentPage()
		{
			InitializeComponent();
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			_ = TaskTimer.DelayCall(TimeSpan.Zero, this.PreventFocusOutline<Button>);
		}
	}
}
