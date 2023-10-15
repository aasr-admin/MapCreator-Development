using MapCreator.userPlugin;

namespace MapCreator
{
	public partial class UserPlugins : Form
	{
		public UserPlugins()
		{
			InitializeComponent();

			// Set position on top of your panel
			userPlugins_panel_formLauncher.AutoScrollPosition = new Point(0, 0);

			// Set maximum position of your panel beyond the point your panel items reach.
			// You'll have to change this size depending on the total size of items for your case.
			userPlugins_panel_formLauncher.VerticalScroll.Maximum = 1000;
		}

		private void userPlugins_Load(object sender, EventArgs e)
		{
			userPlugins_pictureBox_pluginDescriptionDisplay.Show();

			userPlugins_pictureBox_pluginDescriptionDisplay_createTileTransitions.Hide();
			userPlugins_pictureBox_pluginDescriptionDisplay_createTerrainTypes.Hide();
			userPlugins_pictureBox_pluginDescriptionDisplay_viewFacetAsPlanet.Hide();
			userPlugins_pictureBox_pluginDescriptionDisplay_fileTypeConverters.Hide();
		}

		private void userPlugins_menuStrip_button_facetBuilder_Click(object sender, EventArgs e)
		{
			Hide();

			var facetBuilderForm = new FacetBuilder();
			facetBuilderForm.Show();
		}

		private void userPlugins_menuStrip_button_information_Click(object sender, EventArgs e)
		{
			var communityCreditsForm = new CommunityCredits();
			communityCreditsForm.Show();
		}

		private void userPlugins_panel_formLauncher_button_createTileTransitions_MouseEnter(object sender, EventArgs e)
		{

			userPlugins_pictureBox_pluginDescriptionDisplay.Hide();

			userPlugins_pictureBox_pluginDescriptionDisplay_createTileTransitions.Show();
			userPlugins_pictureBox_pluginDescriptionDisplay_createTerrainTypes.Hide();
			userPlugins_pictureBox_pluginDescriptionDisplay_viewFacetAsPlanet.Hide();
			userPlugins_pictureBox_pluginDescriptionDisplay_fileTypeConverters.Hide();

			userPlugins_panel_formLauncher_button_createTileTransitions.ForeColor = Color.LimeGreen;
		}

		private void userPlugins_panel_formLauncher_button_createTileTransitions_Click(object sender, EventArgs e)
		{

		}

		private void userPlugins_panel_formLauncher_button_createTileTransitions_MouseLeave(object sender, EventArgs e)
		{
			userPlugins_pictureBox_pluginDescriptionDisplay.Show();

			userPlugins_pictureBox_pluginDescriptionDisplay_createTileTransitions.Hide();
			userPlugins_pictureBox_pluginDescriptionDisplay_createTerrainTypes.Hide();
			userPlugins_pictureBox_pluginDescriptionDisplay_viewFacetAsPlanet.Hide();
			userPlugins_pictureBox_pluginDescriptionDisplay_fileTypeConverters.Hide();

			userPlugins_panel_formLauncher_button_createTileTransitions.ForeColor = Color.SlateGray;
		}

		private void userPlugins_panel_formLauncher_button_createTerrainTypes_MouseEnter(object sender, EventArgs e)
		{
			userPlugins_pictureBox_pluginDescriptionDisplay.Hide();

			userPlugins_pictureBox_pluginDescriptionDisplay_createTileTransitions.Hide();
			userPlugins_pictureBox_pluginDescriptionDisplay_createTerrainTypes.Show();
			userPlugins_pictureBox_pluginDescriptionDisplay_viewFacetAsPlanet.Hide();
			userPlugins_pictureBox_pluginDescriptionDisplay_fileTypeConverters.Hide();

			userPlugins_panel_formLauncher_button_createTerrainTypes.ForeColor = Color.LimeGreen;
		}

		private void userPlugins_panel_formLauncher_button_createTerrainTypes_Click(object sender, EventArgs e)
		{
			Hide();

			var createTerrainTypesForm = new CreateTerrainTypes();
			createTerrainTypesForm.Show();
		}

		private void userPlugins_panel_formLauncher_button_createTerrainTypes_MouseLeave(object sender, EventArgs e)
		{
			userPlugins_pictureBox_pluginDescriptionDisplay.Show();

			userPlugins_pictureBox_pluginDescriptionDisplay_createTileTransitions.Hide();
			userPlugins_pictureBox_pluginDescriptionDisplay_createTerrainTypes.Hide();
			userPlugins_pictureBox_pluginDescriptionDisplay_viewFacetAsPlanet.Hide();
			userPlugins_pictureBox_pluginDescriptionDisplay_fileTypeConverters.Hide();

			userPlugins_panel_formLauncher_button_createTerrainTypes.ForeColor = Color.SlateGray;
		}

		private void userPlugins_panel_formLauncher_button_viewFacetAsPlanet_MouseEnter(object sender, EventArgs e)
		{
			userPlugins_pictureBox_pluginDescriptionDisplay.Hide();

			userPlugins_pictureBox_pluginDescriptionDisplay_createTileTransitions.Hide();
			userPlugins_pictureBox_pluginDescriptionDisplay_createTerrainTypes.Hide();
			userPlugins_pictureBox_pluginDescriptionDisplay_viewFacetAsPlanet.Show();
			userPlugins_pictureBox_pluginDescriptionDisplay_fileTypeConverters.Hide();

			userPlugins_panel_formLauncher_button_viewFacetAsPlanet.ForeColor = Color.LimeGreen;
		}

		private void userPlugins_panel_formLauncher_button_viewFacetAsPlanet_Click(object sender, EventArgs e)
		{

		}

		private void userPlugins_panel_formLauncher_button_viewFacetAsPlanet_MouseLeave(object sender, EventArgs e)
		{
			userPlugins_pictureBox_pluginDescriptionDisplay.Show();

			userPlugins_pictureBox_pluginDescriptionDisplay_createTileTransitions.Hide();
			userPlugins_pictureBox_pluginDescriptionDisplay_createTerrainTypes.Hide();
			userPlugins_pictureBox_pluginDescriptionDisplay_viewFacetAsPlanet.Hide();
			userPlugins_pictureBox_pluginDescriptionDisplay_fileTypeConverters.Hide();

			userPlugins_panel_formLauncher_button_viewFacetAsPlanet.ForeColor = Color.SlateGray;
		}

		private void userPlugins_panel_formLauncher_button_fileTypeConverters_MouseEnter(object sender, EventArgs e)
		{
			userPlugins_pictureBox_pluginDescriptionDisplay.Hide();

			userPlugins_pictureBox_pluginDescriptionDisplay_createTileTransitions.Hide();
			userPlugins_pictureBox_pluginDescriptionDisplay_createTerrainTypes.Hide();
			userPlugins_pictureBox_pluginDescriptionDisplay_viewFacetAsPlanet.Hide();
			userPlugins_pictureBox_pluginDescriptionDisplay_fileTypeConverters.Show();

			userPlugins_panel_formLauncher_button_fileTypeConverters.ForeColor = Color.LimeGreen;
		}

		private void userPlugins_panel_formLauncher_button_fileTypeConverters_Click(object sender, EventArgs e)
		{
		}

		private void userPlugins_panel_formLauncher_button_fileTypeConverters_MouseLeave(object sender, EventArgs e)
		{
			userPlugins_pictureBox_pluginDescriptionDisplay.Show();

			userPlugins_pictureBox_pluginDescriptionDisplay_createTileTransitions.Hide();
			userPlugins_pictureBox_pluginDescriptionDisplay_createTerrainTypes.Hide();
			userPlugins_pictureBox_pluginDescriptionDisplay_viewFacetAsPlanet.Hide();
			userPlugins_pictureBox_pluginDescriptionDisplay_fileTypeConverters.Hide();

			userPlugins_panel_formLauncher_button_fileTypeConverters.ForeColor = Color.SlateGray;
		}
	}
}
