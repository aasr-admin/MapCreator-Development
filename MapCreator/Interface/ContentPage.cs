using System.ComponentModel;

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
	}
}
