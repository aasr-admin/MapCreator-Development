namespace MapCreator
{
	public partial class SplashScreen
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Disables The Close [X] Button In The Titlebar
		/// </summary>
		private const int CP_DISABLE_CLOSE_BUTTON = 0x200;
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams cp = base.CreateParams;
				cp.ClassStyle = cp.ClassStyle | CP_DISABLE_CLOSE_BUTTON;
				return cp;
			}
		}

		/// <summary>
		///  Clean up any resources being used.
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

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			var resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashScreen));
			Toolbar_Select = new ComboBox();
			Toolbar = new FlowLayoutPanel();
			Toolbar_Open = new Button();
			Toolbar_Delete = new Button();
			Toolbar_Input = new TextBox();
			Toolbar_Create = new Button();
			Menu_Exit = new Button();
			Menu = new FlowLayoutPanel();
			Menu_Minimize = new Button();
			Form_Tooltip = new ToolTip(components);
			Toolbar.SuspendLayout();
			Menu.SuspendLayout();
			SuspendLayout();
			// 
			// Toolbar_Select
			// 
			Toolbar_Select.DropDownStyle = ComboBoxStyle.DropDownList;
			Toolbar_Select.FormattingEnabled = true;
			Toolbar_Select.Location = new Point(3, 3);
			Toolbar_Select.Margin = new Padding(0);
			Toolbar_Select.Name = "Toolbar_Select";
			Toolbar_Select.Size = new Size(200, 23);
			Toolbar_Select.TabIndex = 0;
			// 
			// Toolbar
			// 
			Toolbar.AutoSize = true;
			Toolbar.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			Toolbar.BackColor = Color.FromArgb(63, 0, 0, 0);
			Toolbar.Controls.Add(Toolbar_Select);
			Toolbar.Controls.Add(Toolbar_Open);
			Toolbar.Controls.Add(Toolbar_Delete);
			Toolbar.Controls.Add(Toolbar_Input);
			Toolbar.Controls.Add(Toolbar_Create);
			Toolbar.Dock = DockStyle.Bottom;
			Toolbar.Location = new Point(0, 195);
			Toolbar.Name = "Toolbar";
			Toolbar.Padding = new Padding(3);
			Toolbar.Size = new Size(548, 29);
			Toolbar.TabIndex = 1;
			// 
			// Toolbar_Open
			// 
			Toolbar_Open.BackColor = Color.Transparent;
			Toolbar_Open.BackgroundImage = (Image)resources.GetObject("Toolbar_Open.BackgroundImage");
			Toolbar_Open.BackgroundImageLayout = ImageLayout.Zoom;
			Toolbar_Open.Cursor = Cursors.Hand;
			Toolbar_Open.FlatAppearance.BorderSize = 0;
			Toolbar_Open.FlatAppearance.CheckedBackColor = Color.FromArgb(0, 0, 0, 0);
			Toolbar_Open.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 0, 0, 0);
			Toolbar_Open.FlatAppearance.MouseOverBackColor = Color.FromArgb(63, 255, 255, 255);
			Toolbar_Open.FlatStyle = FlatStyle.Flat;
			Toolbar_Open.ForeColor = Color.Transparent;
			Toolbar_Open.Location = new Point(203, 3);
			Toolbar_Open.Margin = new Padding(0);
			Toolbar_Open.Name = "Toolbar_Open";
			Toolbar_Open.Size = new Size(23, 23);
			Toolbar_Open.TabIndex = 1;
			Toolbar_Open.UseVisualStyleBackColor = false;
			// 
			// Toolbar_Delete
			// 
			Toolbar_Delete.BackColor = Color.Transparent;
			Toolbar_Delete.BackgroundImage = (Image)resources.GetObject("Toolbar_Delete.BackgroundImage");
			Toolbar_Delete.BackgroundImageLayout = ImageLayout.Zoom;
			Toolbar_Delete.Cursor = Cursors.Hand;
			Toolbar_Delete.FlatAppearance.BorderSize = 0;
			Toolbar_Delete.FlatAppearance.CheckedBackColor = Color.FromArgb(0, 0, 0, 0);
			Toolbar_Delete.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 0, 0, 0);
			Toolbar_Delete.FlatAppearance.MouseOverBackColor = Color.FromArgb(63, 255, 255, 255);
			Toolbar_Delete.FlatStyle = FlatStyle.Flat;
			Toolbar_Delete.ForeColor = Color.Transparent;
			Toolbar_Delete.Location = new Point(226, 3);
			Toolbar_Delete.Margin = new Padding(0);
			Toolbar_Delete.Name = "Toolbar_Delete";
			Toolbar_Delete.Size = new Size(23, 23);
			Toolbar_Delete.TabIndex = 2;
			Toolbar_Delete.UseVisualStyleBackColor = false;
			// 
			// Toolbar_Input
			// 
			Toolbar_Input.Location = new Point(249, 3);
			Toolbar_Input.Margin = new Padding(0);
			Toolbar_Input.MaxLength = 30;
			Toolbar_Input.Name = "Toolbar_Input";
			Toolbar_Input.PlaceholderText = "New Project Name";
			Toolbar_Input.Size = new Size(200, 23);
			Toolbar_Input.TabIndex = 4;
			// 
			// Toolbar_Create
			// 
			Toolbar_Create.BackColor = Color.Transparent;
			Toolbar_Create.BackgroundImage = (Image)resources.GetObject("Toolbar_Create.BackgroundImage");
			Toolbar_Create.BackgroundImageLayout = ImageLayout.Zoom;
			Toolbar_Create.Cursor = Cursors.Hand;
			Toolbar_Create.FlatAppearance.BorderSize = 0;
			Toolbar_Create.FlatAppearance.CheckedBackColor = Color.FromArgb(0, 0, 0, 0);
			Toolbar_Create.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 0, 0, 0);
			Toolbar_Create.FlatAppearance.MouseOverBackColor = Color.FromArgb(63, 255, 255, 255);
			Toolbar_Create.FlatStyle = FlatStyle.Flat;
			Toolbar_Create.ForeColor = Color.Transparent;
			Toolbar_Create.Location = new Point(449, 3);
			Toolbar_Create.Margin = new Padding(0);
			Toolbar_Create.Name = "Toolbar_Create";
			Toolbar_Create.Size = new Size(23, 23);
			Toolbar_Create.TabIndex = 3;
			Toolbar_Create.UseVisualStyleBackColor = false;
			// 
			// Menu_Exit
			// 
			Menu_Exit.BackColor = Color.Transparent;
			Menu_Exit.BackgroundImage = (Image)resources.GetObject("Menu_Exit.BackgroundImage");
			Menu_Exit.BackgroundImageLayout = ImageLayout.Zoom;
			Menu_Exit.Cursor = Cursors.Hand;
			Menu_Exit.DialogResult = DialogResult.Cancel;
			Menu_Exit.FlatAppearance.BorderSize = 0;
			Menu_Exit.FlatAppearance.CheckedBackColor = Color.FromArgb(0, 0, 0, 0);
			Menu_Exit.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 0, 0, 0);
			Menu_Exit.FlatAppearance.MouseOverBackColor = Color.FromArgb(63, 255, 255, 255);
			Menu_Exit.FlatStyle = FlatStyle.Flat;
			Menu_Exit.ForeColor = Color.Transparent;
			Menu_Exit.Location = new Point(510, 3);
			Menu_Exit.Margin = new Padding(0);
			Menu_Exit.Name = "Menu_Exit";
			Menu_Exit.Size = new Size(32, 32);
			Menu_Exit.TabIndex = 2;
			Menu_Exit.UseVisualStyleBackColor = false;
			// 
			// Menu
			// 
			Menu.AutoSize = true;
			Menu.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			Menu.BackColor = Color.FromArgb(63, 0, 0, 0);
			Menu.Controls.Add(Menu_Exit);
			Menu.Controls.Add(Menu_Minimize);
			Menu.Cursor = Cursors.SizeAll;
			Menu.Dock = DockStyle.Top;
			Menu.FlowDirection = FlowDirection.RightToLeft;
			Menu.Location = new Point(0, 0);
			Menu.Name = "Menu";
			Menu.Padding = new Padding(3);
			Menu.Size = new Size(548, 38);
			Menu.TabIndex = 3;
			// 
			// Menu_Minimize
			// 
			Menu_Minimize.BackColor = Color.Transparent;
			Menu_Minimize.BackgroundImage = (Image)resources.GetObject("Menu_Minimize.BackgroundImage");
			Menu_Minimize.BackgroundImageLayout = ImageLayout.Zoom;
			Menu_Minimize.Cursor = Cursors.Hand;
			Menu_Minimize.FlatAppearance.BorderSize = 0;
			Menu_Minimize.FlatAppearance.CheckedBackColor = Color.FromArgb(0, 0, 0, 0);
			Menu_Minimize.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 0, 0, 0);
			Menu_Minimize.FlatAppearance.MouseOverBackColor = Color.FromArgb(63, 255, 255, 255);
			Menu_Minimize.FlatStyle = FlatStyle.Flat;
			Menu_Minimize.ForeColor = Color.Transparent;
			Menu_Minimize.Location = new Point(478, 3);
			Menu_Minimize.Margin = new Padding(0);
			Menu_Minimize.Name = "Menu_Minimize";
			Menu_Minimize.Size = new Size(32, 32);
			Menu_Minimize.TabIndex = 3;
			Menu_Minimize.UseVisualStyleBackColor = false;
			// 
			// Form_Tooltip
			// 
			Form_Tooltip.ToolTipIcon = ToolTipIcon.Info;
			// 
			// SplashScreen
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
			BackgroundImageLayout = ImageLayout.Zoom;
			ClientSize = new Size(548, 224);
			ControlBox = false;
			Controls.Add(Menu);
			Controls.Add(Toolbar);
			DoubleBuffered = true;
			FormBorderStyle = FormBorderStyle.None;
			Icon = (Icon)resources.GetObject("$this.Icon");
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "SplashScreen";
			SizeGripStyle = SizeGripStyle.Hide;
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Map Creator";
			Toolbar.ResumeLayout(false);
			Toolbar.PerformLayout();
			Menu.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private ComboBox Toolbar_Select;
		private FlowLayoutPanel Toolbar;
		private Button Toolbar_Open;
		private Button Toolbar_Delete;
		private Button Menu_Exit;
		private FlowLayoutPanel Menu;
		private Button Toolbar_Create;
		private TextBox Toolbar_Input;
		private Button Menu_Minimize;
		private ToolTip Form_Tooltip;
	}
}