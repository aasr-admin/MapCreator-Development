namespace MapCreator.Interface
{
	public partial class MapCreatorUI
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
			var resources = new System.ComponentModel.ComponentResourceManager(typeof(MapCreatorUI));
			Menu = new Panel();
			Menu_Icon = new Button();
			Menu_Minimize = new Button();
			Menu_Exit = new Button();
			Content = new ContentContainer();
			Menu.SuspendLayout();
			SuspendLayout();
			// 
			// Menu
			// 
			Menu.BackColor = Color.FromArgb(63, 0, 0, 0);
			Menu.Controls.Add(Menu_Icon);
			Menu.Controls.Add(Menu_Minimize);
			Menu.Controls.Add(Menu_Exit);
			Menu.Cursor = Cursors.SizeAll;
			Menu.Dock = DockStyle.Top;
			Menu.ForeColor = Color.Transparent;
			Menu.Location = new Point(0, 0);
			Menu.Name = "Menu";
			Menu.Padding = new Padding(3);
			Menu.Size = new Size(548, 38);
			Menu.TabIndex = 1;
			Menu.TabStop = true;
			// 
			// Menu_Icon
			// 
			Menu_Icon.BackColor = Color.Transparent;
			Menu_Icon.BackgroundImage = Properties.Common.appicon;
			Menu_Icon.BackgroundImageLayout = ImageLayout.Zoom;
			Menu_Icon.Cursor = Cursors.Hand;
			Menu_Icon.Dock = DockStyle.Left;
			Menu_Icon.FlatAppearance.BorderSize = 0;
			Menu_Icon.FlatAppearance.MouseDownBackColor = Color.FromArgb(79, 255, 255, 255);
			Menu_Icon.FlatAppearance.MouseOverBackColor = Color.FromArgb(63, 255, 255, 255);
			Menu_Icon.FlatStyle = FlatStyle.Flat;
			Menu_Icon.ForeColor = Color.Transparent;
			Menu_Icon.Location = new Point(3, 3);
			Menu_Icon.Name = "Menu_Icon";
			Menu_Icon.Size = new Size(32, 32);
			Menu_Icon.TabIndex = 1;
			Menu_Icon.UseVisualStyleBackColor = false;
			// 
			// Menu_Minimize
			// 
			Menu_Minimize.BackColor = Color.Transparent;
			Menu_Minimize.BackgroundImage = Properties.Buttons.minimize;
			Menu_Minimize.BackgroundImageLayout = ImageLayout.Zoom;
			Menu_Minimize.Cursor = Cursors.Hand;
			Menu_Minimize.Dock = DockStyle.Right;
			Menu_Minimize.FlatAppearance.BorderSize = 0;
			Menu_Minimize.FlatAppearance.MouseDownBackColor = Color.FromArgb(79, 255, 255, 255);
			Menu_Minimize.FlatAppearance.MouseOverBackColor = Color.FromArgb(63, 255, 255, 255);
			Menu_Minimize.FlatStyle = FlatStyle.Flat;
			Menu_Minimize.ForeColor = Color.Transparent;
			Menu_Minimize.Location = new Point(481, 3);
			Menu_Minimize.Name = "Menu_Minimize";
			Menu_Minimize.Size = new Size(32, 32);
			Menu_Minimize.TabIndex = 2;
			Menu_Minimize.UseVisualStyleBackColor = false;
			// 
			// Menu_Exit
			// 
			Menu_Exit.BackColor = Color.Transparent;
			Menu_Exit.BackgroundImage = Properties.Buttons.cancel;
			Menu_Exit.BackgroundImageLayout = ImageLayout.Zoom;
			Menu_Exit.Cursor = Cursors.Hand;
			Menu_Exit.DialogResult = DialogResult.Cancel;
			Menu_Exit.Dock = DockStyle.Right;
			Menu_Exit.FlatAppearance.BorderSize = 0;
			Menu_Exit.FlatAppearance.MouseDownBackColor = Color.FromArgb(79, 255, 255, 255);
			Menu_Exit.FlatAppearance.MouseOverBackColor = Color.FromArgb(63, 255, 255, 255);
			Menu_Exit.FlatStyle = FlatStyle.Flat;
			Menu_Exit.ForeColor = Color.Transparent;
			Menu_Exit.Location = new Point(513, 3);
			Menu_Exit.Name = "Menu_Exit";
			Menu_Exit.Size = new Size(32, 32);
			Menu_Exit.TabIndex = 3;
			Menu_Exit.UseVisualStyleBackColor = false;
			// 
			// Content
			// 
			Content.BackColor = Color.Transparent;
			Content.ContentIndex = -1;
			Content.ContentValue = null;
			Content.Dock = DockStyle.Fill;
			Content.Location = new Point(0, 38);
			Content.Margin = new Padding(0);
			Content.Name = "Content";
			Content.Size = new Size(548, 186);
			Content.TabIndex = 2;
			// 
			// MapCreatorUI
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			BackgroundImage = Properties.Common.splash;
			BackgroundImageLayout = ImageLayout.Tile;
			ClientSize = new Size(548, 224);
			ControlBox = false;
			Controls.Add(Content);
			Controls.Add(Menu);
			DoubleBuffered = true;
			FormBorderStyle = FormBorderStyle.None;
			Icon = (Icon)resources.GetObject("$this.Icon");
			MaximizeBox = false;
			MinimumSize = new Size(548, 224);
			Name = "MapCreatorUI";
			SizeGripStyle = SizeGripStyle.Hide;
			StartPosition = FormStartPosition.WindowsDefaultLocation;
			Text = "Map Creator";
			Menu.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion

		private Panel Menu;
		private Button Menu_Icon;
		private Button Menu_Minimize;
		private Button Menu_Exit;
		private ContentContainer Content;
	}
}