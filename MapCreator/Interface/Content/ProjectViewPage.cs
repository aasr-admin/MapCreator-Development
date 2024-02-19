namespace MapCreator.Interface.Content
{
	public partial class ProjectViewPage : ContentPage
	{
		public event EventHandler<ProjectChangingEventArgs>? ProjectChanging;
		public event EventHandler<ProjectEventArgs>? ProjectChanged;
		
		private Project? _Project;

		public Project? Project
		{
			get => _Project;
			set
			{
				if (_Project != value)
				{
					var changingArgs = new ProjectChangingEventArgs(_Project, value);

					OnProjectChanging(changingArgs);

					if (changingArgs.PreventChange)
					{
						return;
					}

					_Project = value;

					OnProjectChanged(new ProjectEventArgs(value));
				}
			}
		}

		public ProjectViewPage()
		{
			InitializeComponent();

			ProjectUndoButton.Click += HandleClickUndo;
		}

		private void HandleClickUndo(object? sender, EventArgs e)
		{
			Project = null;
		}

		protected virtual void OnProjectChanging(ProjectChangingEventArgs e)
		{
			ProjectChanging?.Invoke(this, e);
		}

		protected virtual void OnProjectChanged(ProjectEventArgs e)
		{
			ProjectChanged?.Invoke(this, e);
		}
	}
}
