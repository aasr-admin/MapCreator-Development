using System.ComponentModel;
using System.Timers;

namespace MapCreator
{
	public partial class MapCreatorUI : Form
	{
		private static MapCreatorUI? _Instance;

		public static MapCreatorUI Instance => _Instance ??= new();

		private MapCreatorUI()
		{
			AllowTransparency = true;

			InitializeComponent();

			Menu.SetWindowDragHandle();

			Toolbar_Select.BeginUpdate();

			Toolbar_Select.SelectedIndex = Toolbar_Select.Items.Add("<New Project>");

			foreach (var project in Project.Projects)
			{
				Toolbar_Select.Items.Add(project);
			}

			Toolbar_Select.EndUpdate();

			Toolbar_Create.SetToolTip(ToolTipIcon.Info, "Create", "Create a new project");
			Toolbar_Open.SetToolTip(ToolTipIcon.Info, "Open", "Open the selected project");
			Toolbar_Delete.SetToolTip(ToolTipIcon.Error, "Delete", "Delete the selected project");

			Toolbar_Select.SelectedIndexChanged += OnProjectSelection;

			Toolbar_Refresh.Click += OnClickRefresh;
			Toolbar_Create.Click += OnClickCreate;
			Toolbar_Open.Click += OnClickOpen;
			Toolbar_Delete.Click += OnClickDelete;

			Menu_Icon.Click += OnClickIcon;
			Menu_Minimize.Click += OnClickMinimize;
			Menu_Exit.Click += OnClickExit;

			foreach (var button in this.FindChildren<Button>())
			{
				button.GotFocus += (s, e) =>
				{
					if (ActiveControl == button)
					{
						ActiveControl = ActiveControl?.Parent;
					}
				};
			}

			RefreshToolbar();
		}

		private void InvalidatePreviews()
		{
			var box = new PictureBox();

			((ISupportInitialize)box).BeginInit();

			/*
			box.ErrorImage = (Image)resources.GetObject("pictureBox1.ErrorImage");
			box.InitialImage = (Image)resources.GetObject("pictureBox1.InitialImage");
			box.Location = new Point(3, 3);
			box.Name = "pictureBox1";
			box.Size = new Size(100, 50);
			box.SizeMode = PictureBoxSizeMode.Zoom;
			box.TabIndex = 0;
			box.TabStop = false;
			*/
			((ISupportInitialize)box).EndInit();
		}

		private void OnClickIcon(object? sender, EventArgs e)
		{
			// launch github link?
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

		private void OnClickRefresh(object? sender, EventArgs e)
		{
			RefreshProjects();
		}

		private void OnClickCreate(object? sender, EventArgs e)
		{
			if (String.IsNullOrWhiteSpace(Toolbar_Input.Text))
			{
				var oldColor = Toolbar_Input.BackColor;
				var newColor = Color.FromArgb(63, Color.OrangeRed);

				_ = TaskTimer.DelayCall(TimeSpan.Zero, TimeSpan.FromMilliseconds(200), 6, Invoke, () =>
				{
					if (Toolbar_Input.BackColor == oldColor)
					{
						Toolbar_Input.BackColor = Color.Red;
					}
					else
					{
						Toolbar_Input.BackColor = oldColor;
					}
				});
			}
			else
			{
				var projectFilePath = Project.GetFilePath(Toolbar_Input.Text);

				var project = Project.OpenOrCreate(projectFilePath);

				var index = Toolbar_Select.Items.IndexOf(project);

				if (index >= 0)
				{
					Toolbar_Select.SelectedIndex = index;
				}
				else
				{
					Toolbar_Select.SelectedIndex = Toolbar_Select.Items.Add(project);
				}

				OnClickOpen(sender, e);
			}
		}

		private void OnClickOpen(object? sender, EventArgs e)
		{
			if (Toolbar_Select.SelectedItem is not Project project)
			{
				return;
			}

			Hide();

			var facetBuilderForm = new FacetBuilder(project);

			facetBuilderForm.Show();
		}

		private void OnClickDelete(object? sender, EventArgs e)
		{
			if (Toolbar_Select.SelectedItem is not Project project)
			{
				return;
			}

			if (project.DeleteFiles())
			{
				Toolbar_Select.Items.Remove(project);

				Toolbar_Select.SelectedIndex = 0;
			}
		}

		private void OnProjectSelection(object? sender, EventArgs e)
		{
			RefreshToolbar();
		}

		private void RefreshToolbar()
		{
			if (Toolbar_Select.SelectedItem is Project)
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

		private void RefreshProjects()
		{
			Project.RefreshProjects();

			var selected = Toolbar_Select.SelectedItem;

			Toolbar_Select.SuspendLayout();
			Toolbar_Select.BeginUpdate();

			var index = Toolbar_Select.Items.Count;

			while (--index >= 0)
			{
				if (Toolbar_Select.Items[index] is Project)
				{
					Toolbar_Select.Items.RemoveAt(index);
				}
			}

			foreach (var project in Project.Projects)
			{
				Toolbar_Select.Items.Add(project);
			}

			var selectIndex = Toolbar_Select.Items.IndexOf(selected);

			if (selectIndex >= 0)
			{
				Toolbar_Select.SelectedIndex = selectIndex;
			}
			else
			{
				Toolbar_Select.SelectedIndex = 0;
			}

			Toolbar_Select.EndUpdate();
			Toolbar_Select.ResumeLayout();

			RefreshToolbar();
		}
	}
}