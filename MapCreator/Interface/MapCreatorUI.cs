namespace MapCreator.Interface
{
	public partial class MapCreatorUI : Form
	{
		private static MapCreatorUI? _Instance;

		public static MapCreatorUI Instance => _Instance ??= new();

		public ProjectBrowser ProjectBrowser { get; } = new();
		public ProjectView ProjectView { get; } = new();

		public int ContentPageCount => Content.ContentCount;
		public int ContentPageIndex { get => Content.ContentIndex; set => Content.ContentIndex = value; }
		public Control? ContentPage { get => Content.ContentValue; set => Content.ContentValue = value; }

		private MapCreatorUI()
		{
			AllowTransparency = true;

			InitializeComponent();
			InitializeContent();

			Menu.SetWindowDragHandle();

			Menu_Icon.Click += HandleClickIcon;
			Menu_Minimize.Click += HandleClickMinimize;
			Menu_Exit.Click += HandleClickExit;

			this.PreventFocusOutline<Button>();
		}

		private void InitializeContent()
		{
			ProjectBrowser.ProjectCreated += HandleProjectCreated;
			ProjectBrowser.ProjectDeleted += HandleProjectDeleted;
			ProjectBrowser.ProjectOpened += HandleProjectOpened;
			ProjectBrowser.ProjectSelected += HandleProjectSelected;

			ProjectView.ProjectChanged += HandleProjectChanged;

			Content.AddContent(ProjectBrowser);
			Content.AddContent(ProjectView);

			Content.ResumeLayout(false);

			ResumeLayout(false);
		}

		private void HandleProjectChanged(object? sender, ProjectEventArgs e)
		{
			if (e.Project == null)
			{
				ContentPage = ProjectBrowser;
			}
			else
			{
				ContentPage = ProjectView;
			}
		}

		private void HandleProjectCreated(object? sender, ProjectEventArgs e)
		{
			ProjectView.Project = e.Project;

			ContentPage = ProjectView;
		}

		private void HandleProjectDeleted(object? sender, ProjectEventArgs e)
		{
			if (ProjectView.Project == e.Project)
			{
				ProjectView.Project = null;
			}
		}

		private void HandleProjectOpened(object? sender, ProjectEventArgs e)
		{
			ProjectView.Project = e.Project;
		}

		private void HandleProjectSelected(object? sender, ProjectEventArgs e)
		{
		}

		private void HandleClickIcon(object? sender, EventArgs e)
		{
			// launch github link?
		}

		private void HandleClickMinimize(object? sender, EventArgs e)
		{
			WindowState = FormWindowState.Minimized;
		}

		private void HandleClickExit(object? sender, EventArgs e)
		{
			Close();

			Application.Exit();
		}
	}
}