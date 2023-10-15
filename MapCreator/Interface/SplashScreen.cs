namespace MapCreator
{
	public partial class SplashScreen : Form
	{
		private readonly int _NewProjectIndex;

		public SplashScreen()
		{
			AllowTransparency = true;

			InitializeComponent();

			Menu.SetWindowDragHandle();

			Menu_Minimize.Click += OnClickMinimize;
			Menu_Exit.Click += OnClickExit;

			Menu_Minimize.SetToolTip(ToolTipIcon.None, "HELLO world", "TEST");
			Menu_Exit.SetToolTip(ToolTipIcon.Error, "TEST", "HELLO world");

			Toolbar_Create.Click += OnClickCreate;
			Toolbar_Open.Click += OnClickOpen;
			Toolbar_Delete.Click += OnClickDelete;

			_NewProjectIndex = Toolbar_Select.Items.Add("<New Project>");

			foreach (var project in Project.Projects)
			{
				Toolbar_Select.Items.Add(project);
			}

			Toolbar_Select.SelectedIndex = _NewProjectIndex;

			Toolbar_Select.SelectedIndexChanged += OnProjectSelection;

			InvalidateToolbar();
		}

		private void OnClickMinimize(object? sender, EventArgs e)
		{
			WindowState = FormWindowState.Minimized;
		}

		private void OnClickExit(object? sender, EventArgs e)
		{
			Close();

			Application.Exit();
		}

		private void OnClickCreate(object? sender, EventArgs e)
		{
		}

		private void OnClickOpen(object? sender, EventArgs e)
		{
			//var facetBuilderForm = new FacetBuilder();
			//facetBuilderForm.Show();
		}

		private void OnClickDelete(object? sender, EventArgs e)
		{
		}

		private void OnProjectSelection(object? sender, EventArgs e)
		{
			InvalidateToolbar();
		}

		private void InvalidateToolbar()
		{
			if (Toolbar_Select.SelectedValue is Project)
			{
				Toolbar_Create.Hide();
				Toolbar_Input.Hide();

				Toolbar_Open.Show();
				Toolbar_Delete.Show();
			}
			else
			{
				Toolbar_Delete.Hide();
				Toolbar_Open.Hide();

				Toolbar_Input.Show();
				Toolbar_Create.Show();
			}
		}
	}
}