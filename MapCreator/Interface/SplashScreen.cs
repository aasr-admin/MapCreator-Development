namespace MapCreator
{
	public partial class SplashScreen : Form
	{
		public SplashScreen()
		{
			InitializeComponent();

			var facetBuilderForm = new FacetBuilder();
			facetBuilderForm.Show();
		}
	}
}