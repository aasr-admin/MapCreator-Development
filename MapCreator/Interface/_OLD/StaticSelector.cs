using Assets;

namespace MapCreator.userPlugin
{
	public partial class StaticSelector : Form
	{
		private bool dragging = false;
		private Point dragCursorPoint;
		private Point dragFormPoint;

		public StaticSelector()
		{
			MaximizeBox = false;
			MinimizeBox = false;

			InitializeComponent();
		}

		private void staticSelector_MouseDown(object sender, MouseEventArgs e)
		{
			dragging = true;
			dragCursorPoint = Cursor.Position;
			dragFormPoint = Location;
		}

		private void staticSelector_MouseMove(object sender, MouseEventArgs e)
		{
			if (dragging)
			{
				var dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));

				Location = Point.Add(dragFormPoint, new Size(dif));
			}
		}

		private void staticSelector_MouseUp(object sender, MouseEventArgs e)
		{
			dragging = false;
		}

		private void staticSelector_closeButton_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
		{
			Refresh();
		}

		private void staticSelector_staticPreview_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				Tag = vScrollBar1.Value + (6 * (e.X % 50)) + (e.X / 50);
			}
		}

		private void staticSelector_staticPreview_Paint(object sender, PaintEventArgs e)
		{
			using var font = new Font("Arial", 8f);
			using var brush = new SolidBrush(Color.Black);
			using var pen = new Pen(Color.Black);

			var graphics = e.Graphics;

			graphics.Clear(Color.LightGray);

			var value = vScrollBar1.Value;

			for (var y = 0; y < 8; y++)
			{
				for (var x = 0; x < 6; x++)
				{
					graphics.DrawRectangle(pen, x * 50, y * 60, 48, 58);

					var image = AssetData.Art.GetStatic(value);

					if (image != null)
					{
						graphics.DrawString($"{value}", font, brush, (x * 50) + 1, (y * 60) + 1);
						graphics.DrawImage(image, new Rectangle((x * 50) + 2, (y * 60) + 12, 44, 44), new Rectangle(1, 1, 44, 44), GraphicsUnit.Pixel);
					}

					++value;
				}
			}
		}
	}
}
