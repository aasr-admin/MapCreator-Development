using System.Timers;

namespace MapCreator.Interface
{
	public partial class ProjectBrowser : ContentPage
	{
		public event EventHandler<ProjectEventArgs>? ProjectSelected;
		public event EventHandler<ProjectEventArgs>? ProjectCreated;
		public event EventHandler<ProjectEventArgs>? ProjectDeleted;
		public event EventHandler<ProjectEventArgs>? ProjectOpened;

		public ComboBox.ObjectCollection Projects => ProjectSelect.Items;

		public int ProjectCount => Projects.Count;

		public int SelectedProjectIndex
		{
			get => ProjectSelect.SelectedIndex;
			set
			{
				if (value >= 0 && value < ProjectCount)
				{
					ProjectSelect.SelectedIndex = value;
				}
			}
		}

		public Project? SelectedProject
		{
			get => Projects[SelectedProjectIndex] as Project;
			set => SelectedProjectIndex = Projects.IndexOf(value);
		}

		public ProjectBrowser()
		{
			InitializeComponent();

			ProjectSelect.SuspendLayout();

			ProjectSelect.SelectedIndex = Projects.Add("<New Project>");

			ProjectSelect.ResumeLayout(false);

			ProjectRefreshButton.SetToolTip(ToolTipIcon.Info, "Refresh", "Refresh the project list");
			ProjectCreateButton.SetToolTip(ToolTipIcon.Info, "Create", "Create a new project");
			ProjectOpenButton.SetToolTip(ToolTipIcon.Info, "Open", "Open the selected project");
			ProjectDeleteButton.SetToolTip(ToolTipIcon.Error, "Delete", "Delete the selected project");

			ProjectSelect.SelectedIndexChanged += HandleProjectSelection;

			ProjectRefreshButton.Click += HandleClickRefresh;
			ProjectCreateButton.Click += HandleClickCreate;
			ProjectOpenButton.Click += HandleClickOpen;
			ProjectDeleteButton.Click += HandleClickDelete;

			this.PreventFocusOutline<Button>();

			InvalidateProjects();
		}

		public void InvalidateProjects()
		{
			Project.RefreshProjects();

			ProjectSelect.SuspendLayout();
			ProjectSelect.BeginUpdate();

			var selected = SelectedProject;

			var index = ProjectCount;

			while (--index >= 0)
			{
				if (Projects[index] is Project)
				{
					Projects.RemoveAt(index);
				}
			}

			foreach (var project in Project.Projects)
			{
				Projects.Add(project);
			}

			SelectedProjectIndex = Math.Max(0, Projects.IndexOf(selected));

			ProjectSelect.EndUpdate();
			ProjectSelect.ResumeLayout(false);

			InvalidatePreviews();
			InvalidateToolbar();
		}

		private void InvalidateToolbar()
		{
			var project = SelectedProject;

			if (project != null)
			{
				ProjectCreateButton.Hide();
				ProjectNameInput.Hide();

				ProjectOpenButton.Show();
				ProjectDeleteButton.Show();
			}
			else
			{
				ProjectDeleteButton.Hide();
				ProjectOpenButton.Hide();

				ProjectNameInput.Show();
				ProjectCreateButton.Show();
			}
		}

		private void InvalidatePreviews()
		{
			/*
			var box = new PictureBox();

			((ISupportInitialize)box).BeginInit();

			box.ErrorImage = (Image)resources.GetObject("pictureBox1.ErrorImage");
			box.InitialImage = (Image)resources.GetObject("pictureBox1.InitialImage");
			box.Location = new Point(3, 3);
			box.Name = "pictureBox1";
			box.Size = new Size(100, 50);
			box.SizeMode = PictureBoxSizeMode.Zoom;
			box.TabIndex = 0;
			box.TabStop = false;

			((ISupportInitialize)box).EndInit();
			*/
		}

		private void HandleClickRefresh(object? sender, EventArgs e)
		{
			InvalidateProjects();
		}

		private void HandleClickCreate(object? sender, EventArgs e)
		{
			if (String.IsNullOrWhiteSpace(ProjectNameInput.Text))
			{
				var oldColor = ProjectNameInput.BackColor;
				var newColor = Color.FromArgb(63, Color.OrangeRed);

				_ = TaskTimer.DelayCall(TimeSpan.Zero, TimeSpan.FromMilliseconds(200), 6, Invoke, () =>
				{
					if (ProjectNameInput.BackColor == oldColor)
					{
						ProjectNameInput.BackColor = Color.Red;
					}
					else
					{
						ProjectNameInput.BackColor = oldColor;
					}
				});
			}
			else
			{
				var projectFilePath = Project.GetFilePath(ProjectNameInput.Text);

				var project = Project.OpenOrCreate(projectFilePath, out var created);

				if (created)
				{
					OnProjectCreated(new ProjectEventArgs(project));
				}

				var index = Projects.IndexOf(project);

				if (index >= 0)
				{
					ProjectSelect.SelectedIndex = index;
				}
				else
				{
					ProjectSelect.SelectedIndex = Projects.Add(project);
				}
			}
		}

		private void HandleClickOpen(object? sender, EventArgs e)
		{
			var project = SelectedProject;

			if (project != null)
			{
				OnProjectOpened(new ProjectEventArgs(project));
			}
		}

		private void HandleClickDelete(object? sender, EventArgs e)
		{
			var project = SelectedProject;

			if (project?.DeleteFiles() == true)
			{
				ProjectSelect.Items.Remove(project);

				OnProjectDeleted(new ProjectEventArgs(project));

				ProjectSelect.SelectedIndex = 0;
			}
		}

		private void HandleProjectSelection(object? sender, EventArgs e)
		{
			var project = SelectedProject;

			if (project != null)
			{
				OnProjectSelected(new ProjectEventArgs(project));
			}

			InvalidateToolbar();
		}

		protected virtual void OnProjectSelected(ProjectEventArgs e)
		{
			ProjectSelected?.Invoke(this, e);
		}

		protected virtual void OnProjectCreated(ProjectEventArgs e)
		{
			ProjectCreated?.Invoke(this, e);
		}

		protected virtual void OnProjectDeleted(ProjectEventArgs e)
		{
			ProjectDeleted?.Invoke(this, e);
		}

		protected virtual void OnProjectOpened(ProjectEventArgs e)
		{
			ProjectOpened?.Invoke(this, e);
		}
	}
}
