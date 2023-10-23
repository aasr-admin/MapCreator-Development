namespace MapCreator.Interface
{
	partial class ProjectBrowser
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			ProjectToolbar = new FlowLayoutPanel();
			ProjectRefreshButton = new Button();
			ProjectSelect = new ComboBox();
			ProjectOpenButton = new Button();
			ProjectDeleteButton = new Button();
			ProjectNameInput = new TextBox();
			ProjectCreateButton = new Button();
			splitContainer1 = new SplitContainer();
			ProjectToolbar.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
			splitContainer1.SuspendLayout();
			SuspendLayout();
			// 
			// ProjectToolbar
			// 
			ProjectToolbar.AutoSize = true;
			ProjectToolbar.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			ProjectToolbar.BackColor = Color.FromArgb(63, 0, 0, 0);
			ProjectToolbar.Controls.Add(ProjectRefreshButton);
			ProjectToolbar.Controls.Add(ProjectSelect);
			ProjectToolbar.Controls.Add(ProjectOpenButton);
			ProjectToolbar.Controls.Add(ProjectDeleteButton);
			ProjectToolbar.Controls.Add(ProjectNameInput);
			ProjectToolbar.Controls.Add(ProjectCreateButton);
			ProjectToolbar.Dock = DockStyle.Bottom;
			ProjectToolbar.Location = new Point(0, 157);
			ProjectToolbar.Name = "ProjectToolbar";
			ProjectToolbar.Padding = new Padding(3);
			ProjectToolbar.Size = new Size(548, 29);
			ProjectToolbar.TabIndex = 1;
			ProjectToolbar.TabStop = true;
			// 
			// ProjectRefreshButton
			// 
			ProjectRefreshButton.BackColor = Color.Transparent;
			ProjectRefreshButton.BackgroundImage = Properties.Buttons.refresh;
			ProjectRefreshButton.BackgroundImageLayout = ImageLayout.Zoom;
			ProjectRefreshButton.Cursor = Cursors.Hand;
			ProjectRefreshButton.FlatAppearance.BorderSize = 0;
			ProjectRefreshButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(79, 255, 255, 255);
			ProjectRefreshButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(63, 255, 255, 255);
			ProjectRefreshButton.FlatStyle = FlatStyle.Flat;
			ProjectRefreshButton.ForeColor = Color.Transparent;
			ProjectRefreshButton.Location = new Point(3, 3);
			ProjectRefreshButton.Margin = new Padding(0);
			ProjectRefreshButton.Name = "ProjectRefreshButton";
			ProjectRefreshButton.Size = new Size(23, 23);
			ProjectRefreshButton.TabIndex = 1;
			ProjectRefreshButton.UseVisualStyleBackColor = false;
			// 
			// ProjectSelect
			// 
			ProjectSelect.DropDownStyle = ComboBoxStyle.DropDownList;
			ProjectSelect.FormattingEnabled = true;
			ProjectSelect.Location = new Point(26, 3);
			ProjectSelect.Margin = new Padding(0);
			ProjectSelect.Name = "ProjectSelect";
			ProjectSelect.Size = new Size(150, 23);
			ProjectSelect.TabIndex = 2;
			// 
			// ProjectOpenButton
			// 
			ProjectOpenButton.BackColor = Color.Transparent;
			ProjectOpenButton.BackgroundImage = Properties.Buttons.valid2;
			ProjectOpenButton.BackgroundImageLayout = ImageLayout.Zoom;
			ProjectOpenButton.Cursor = Cursors.Hand;
			ProjectOpenButton.FlatAppearance.BorderSize = 0;
			ProjectOpenButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(79, 255, 255, 255);
			ProjectOpenButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(63, 255, 255, 255);
			ProjectOpenButton.FlatStyle = FlatStyle.Flat;
			ProjectOpenButton.ForeColor = Color.Transparent;
			ProjectOpenButton.Location = new Point(176, 3);
			ProjectOpenButton.Margin = new Padding(0);
			ProjectOpenButton.Name = "ProjectOpenButton";
			ProjectOpenButton.Size = new Size(23, 23);
			ProjectOpenButton.TabIndex = 3;
			ProjectOpenButton.UseVisualStyleBackColor = false;
			// 
			// ProjectDeleteButton
			// 
			ProjectDeleteButton.BackColor = Color.Transparent;
			ProjectDeleteButton.BackgroundImage = Properties.Buttons.remove1;
			ProjectDeleteButton.BackgroundImageLayout = ImageLayout.Zoom;
			ProjectDeleteButton.Cursor = Cursors.Hand;
			ProjectDeleteButton.FlatAppearance.BorderSize = 0;
			ProjectDeleteButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(79, 255, 255, 255);
			ProjectDeleteButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(63, 255, 255, 255);
			ProjectDeleteButton.FlatStyle = FlatStyle.Flat;
			ProjectDeleteButton.ForeColor = Color.Transparent;
			ProjectDeleteButton.Location = new Point(199, 3);
			ProjectDeleteButton.Margin = new Padding(0);
			ProjectDeleteButton.Name = "ProjectDeleteButton";
			ProjectDeleteButton.Size = new Size(23, 23);
			ProjectDeleteButton.TabIndex = 4;
			ProjectDeleteButton.UseVisualStyleBackColor = false;
			// 
			// ProjectNameInput
			// 
			ProjectNameInput.Location = new Point(222, 3);
			ProjectNameInput.Margin = new Padding(0);
			ProjectNameInput.MaxLength = 30;
			ProjectNameInput.Name = "ProjectNameInput";
			ProjectNameInput.PlaceholderText = "New Project Name";
			ProjectNameInput.Size = new Size(150, 23);
			ProjectNameInput.TabIndex = 5;
			// 
			// ProjectCreateButton
			// 
			ProjectCreateButton.BackColor = Color.Transparent;
			ProjectCreateButton.BackgroundImage = Properties.Buttons.add;
			ProjectCreateButton.BackgroundImageLayout = ImageLayout.Zoom;
			ProjectCreateButton.Cursor = Cursors.Hand;
			ProjectCreateButton.FlatAppearance.BorderSize = 0;
			ProjectCreateButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(79, 255, 255, 255);
			ProjectCreateButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(63, 255, 255, 255);
			ProjectCreateButton.FlatStyle = FlatStyle.Flat;
			ProjectCreateButton.ForeColor = Color.Transparent;
			ProjectCreateButton.Location = new Point(372, 3);
			ProjectCreateButton.Margin = new Padding(0);
			ProjectCreateButton.Name = "ProjectCreateButton";
			ProjectCreateButton.Size = new Size(23, 23);
			ProjectCreateButton.TabIndex = 6;
			ProjectCreateButton.UseVisualStyleBackColor = false;
			// 
			// splitContainer1
			// 
			splitContainer1.Dock = DockStyle.Fill;
			splitContainer1.Location = new Point(0, 0);
			splitContainer1.Name = "splitContainer1";
			splitContainer1.Size = new Size(548, 157);
			splitContainer1.SplitterDistance = 182;
			splitContainer1.TabIndex = 2;
			// 
			// ProjectBrowser
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(splitContainer1);
			Controls.Add(ProjectToolbar);
			Name = "ProjectBrowser";
			ProjectToolbar.ResumeLayout(false);
			ProjectToolbar.PerformLayout();
			((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
			splitContainer1.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private FlowLayoutPanel ProjectToolbar;
		private Button ProjectRefreshButton;
		private ComboBox ProjectSelect;
		private Button ProjectOpenButton;
		private Button ProjectDeleteButton;
		private TextBox ProjectNameInput;
		private Button ProjectCreateButton;
		private SplitContainer splitContainer1;
	}
}
