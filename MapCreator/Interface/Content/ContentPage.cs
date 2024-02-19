using System.ComponentModel;
using System.Timers;

namespace MapCreator.Interface.Content
{
	[ToolboxItem(false)]
	public class ContentPage : UserControl
	{
		private Size _InitialMinimumSize, _InitialSize;

		protected override Size DefaultSize => _InitialSize;
		protected override Size DefaultMinimumSize => _InitialMinimumSize;

		public ContentPage()
		{
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			SuspendLayout();

			Name = "ContentPage";

			DoubleBuffered = true;

			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;

			BackColor = Color.Transparent;

			Margin = Padding.Empty;
			Padding = Padding.Empty;

			Size = new Size(548, 186);

			ResumeLayout(false);
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			_ = TaskTimer.DelayCall(TimeSpan.Zero, () =>
			{
				_InitialMinimumSize = MinimumSize;
				_InitialSize = Size;

				this.PreventFocusOutline<Button>();
			});
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				components?.Dispose();
			}

			base.Dispose(disposing);
		}

#pragma warning disable IDE0044 // Add readonly modifier
		private IContainer components = null!;
#pragma warning restore IDE0044 // Add readonly modifier
	}
}
