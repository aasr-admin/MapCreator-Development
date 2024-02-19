namespace MapCreator.Interface.Content
{
	partial class ProjectBrowserPage
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
			Preview = new TableLayoutPanel();
			TerrainPreview = new PictureBox();
			AltitudePreview = new PictureBox();
			ProjectCreateButton = new Button();
			ProjectNameInput = new TextBox();
			ProjectDeleteButton = new Button();
			ProjectOpenButton = new Button();
			ProjectSelect = new ComboBox();
			ProjectRefreshButton = new Button();
			ProjectToolbar = new FlowLayoutPanel();
			Preview.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)TerrainPreview).BeginInit();
			((System.ComponentModel.ISupportInitialize)AltitudePreview).BeginInit();
			ProjectToolbar.SuspendLayout();
			SuspendLayout();
			// 
			// Preview
			// 
			Preview.ColumnCount = 2;
			Preview.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
			Preview.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
			Preview.Controls.Add(TerrainPreview, 1, 0);
			Preview.Controls.Add(AltitudePreview, 0, 0);
			Preview.Dock = DockStyle.Fill;
			Preview.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
			Preview.Location = new Point(0, 0);
			Preview.Name = "Preview";
			Preview.Padding = new Padding(10);
			Preview.RowCount = 1;
			Preview.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
			Preview.Size = new Size(548, 157);
			Preview.TabIndex = 2;
			// 
			// TerrainPreview
			// 
			TerrainPreview.BackgroundImageLayout = ImageLayout.Zoom;
			TerrainPreview.BorderStyle = BorderStyle.FixedSingle;
			TerrainPreview.Dock = DockStyle.Fill;
			TerrainPreview.ErrorImage = Properties.Common.image;
			TerrainPreview.InitialImage = null;
			TerrainPreview.Location = new Point(277, 13);
			TerrainPreview.Name = "TerrainPreview";
			TerrainPreview.Size = new Size(258, 131);
			TerrainPreview.TabIndex = 1;
			TerrainPreview.TabStop = false;
			// 
			// AltitudePreview
			// 
			AltitudePreview.BackgroundImageLayout = ImageLayout.Zoom;
			AltitudePreview.BorderStyle = BorderStyle.FixedSingle;
			AltitudePreview.Dock = DockStyle.Fill;
			AltitudePreview.ErrorImage = Properties.Common.image;
			AltitudePreview.InitialImage = null;
			AltitudePreview.Location = new Point(13, 13);
			AltitudePreview.Name = "AltitudePreview";
			AltitudePreview.Size = new Size(258, 131);
			AltitudePreview.TabIndex = 0;
			AltitudePreview.TabStop = false;
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
			// ProjectBrowserPage
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(Preview);
			Controls.Add(ProjectToolbar);
			Name = "ProjectBrowserPage";
			Preview.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)TerrainPreview).EndInit();
			((System.ComponentModel.ISupportInitialize)AltitudePreview).EndInit();
			ProjectToolbar.ResumeLayout(false);
			ProjectToolbar.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion
		private TableLayoutPanel Preview;
		private PictureBox AltitudePreview;
		private PictureBox TerrainPreview;
		private Button ProjectCreateButton;
		private TextBox ProjectNameInput;
		private Button ProjectDeleteButton;
		private Button ProjectOpenButton;
		private ComboBox ProjectSelect;
		private Button ProjectRefreshButton;
		private FlowLayoutPanel ProjectToolbar;
	}
}
