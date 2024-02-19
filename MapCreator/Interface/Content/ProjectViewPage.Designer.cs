namespace MapCreator.Interface.Content
{
	partial class ProjectViewPage
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
			ProjectUndoButton = new Button();
			SuspendLayout();
			// 
			// ProjectUndoButton
			// 
			ProjectUndoButton.BackColor = Color.Transparent;
			ProjectUndoButton.BackgroundImage = Properties.Buttons.undo;
			ProjectUndoButton.BackgroundImageLayout = ImageLayout.Zoom;
			ProjectUndoButton.Cursor = Cursors.Hand;
			ProjectUndoButton.FlatAppearance.BorderSize = 0;
			ProjectUndoButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(79, 255, 255, 255);
			ProjectUndoButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(63, 255, 255, 255);
			ProjectUndoButton.FlatStyle = FlatStyle.Flat;
			ProjectUndoButton.ForeColor = Color.Transparent;
			ProjectUndoButton.Location = new Point(289, 189);
			ProjectUndoButton.Margin = new Padding(0);
			ProjectUndoButton.Name = "ProjectUndoButton";
			ProjectUndoButton.Size = new Size(23, 23);
			ProjectUndoButton.TabIndex = 7;
			ProjectUndoButton.UseVisualStyleBackColor = false;
			// 
			// ProjectView
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			BackColor = Color.Transparent;
			BackgroundImage = Properties.Common.bg;
			BackgroundImageLayout = ImageLayout.Stretch;
			Controls.Add(ProjectUndoButton);
			MinimumSize = new Size(600, 400);
			Name = "ProjectView";
			Size = new Size(600, 400);
			ResumeLayout(false);
		}

		#endregion

		private Button ProjectUndoButton;
	}
}
