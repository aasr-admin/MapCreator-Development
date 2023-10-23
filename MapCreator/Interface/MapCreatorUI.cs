using System.Windows.Forms.Animation;

using MapCreator.Interface.Content;

namespace MapCreator.Interface
{
	public partial class MapCreatorUI : Form
	{
		private static MapCreatorUI? _Instance;

		public static MapCreatorUI Instance => _Instance ??= new();

		protected override Size DefaultSize { get; } = new Size(548, 224);
		protected override Size DefaultMinimumSize { get; } = new Size(548, 224);

		public ProjectBrowser ProjectBrowser { get; } = new();
		public ProjectView ProjectView { get; } = new();

		public int ContentPageCount => Content.ContentCount;
		public int ContentPageIndex { get => Content.ContentIndex; set => Content.ContentIndex = value; }
		public Control? ContentPage { get => Content.ContentValue; set => Content.ContentValue = value; }

		private MapCreatorUI()
		{
			AllowTransparency = true;

			InitializeComponent();

			SuspendLayout();

			InitializeContent();

			Menu.SetWindowDragHandle();

			Menu_Icon.Click += HandleClickIcon;
			Menu_Minimize.Click += HandleClickMinimize;
			Menu_Exit.Click += HandleClickExit;

			ResumeLayout(false);
		}

		private void InitializeContent()
		{
			Content.SuspendLayout();

			Content.AddContent(ProjectBrowser);
			Content.AddContent(ProjectView);

			Content.ResumeLayout(false);

			Content.ContentChanging += HandleContentChanging;
			Content.ContentChanged += HandleContentChanged;

			ProjectBrowser.ProjectCreated += HandleProjectCreated;
			ProjectBrowser.ProjectDeleted += HandleProjectDeleted;
			ProjectBrowser.ProjectOpened += HandleProjectOpened;
			ProjectBrowser.ProjectSelected += HandleProjectSelected;

			ProjectView.ProjectChanging += HandleProjectChanging;
			ProjectView.ProjectChanged += HandleProjectChanged;
		}

		private void HandleContentChanging(object? sender, ContentChangingEventArgs e)
		{
			Content.Hide();
		}

		private void HandleContentChanged(object? sender, ContentChangedEventArgs e)
		{
			_ = this.AnimateResize(Content.MinimumSize, state => Content.Show());
		}

		private void HandleProjectChanging(object? sender, ProjectEventArgs e)
		{
		}

		private void HandleProjectChanged(object? sender, ProjectEventArgs e)
		{
			if (e.Project == null)
			{
				BackgroundImage = Properties.Common.splash;
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
			_ = Utility.Launch("https://github.com/aasr-admin/MapCreator-Development");
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

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			this.PreventFocusOutline<Button>();
		}
	}
}