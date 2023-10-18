namespace MapCreator.Interface
{
	public partial class ProjectView : ContentPage
	{
		public event EventHandler<ProjectEventArgs>? ProjectChanging;
		public event EventHandler<ProjectEventArgs>? ProjectChanged;

		private Project? _Project;

		public Project? Project
		{
			get => _Project;
			set
			{
				if (_Project != value)
				{
					OnProjectChanging(new ProjectEventArgs(value));

					_Project = value;

					OnProjectChanged(new ProjectEventArgs(value));
				}
			}
		}

		public ProjectView()
		{
			InitializeComponent();
		}

		protected virtual void OnProjectChanging(ProjectEventArgs e)
		{
			ProjectChanging?.Invoke(this, e);
		}

		protected virtual void OnProjectChanged(ProjectEventArgs e)
		{
			ProjectChanged?.Invoke(this, e);
		}
	}
}
