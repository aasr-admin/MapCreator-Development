using System.Collections;

namespace System.Windows.Forms
{
	public static class Extensions
	{
		public static void Fill(this ListControl control, IEnumerable collection)
		{
			control.SuspendLayout();

			if (control is ListBox list)
			{
				list.Items.Clear();

				foreach (var entry in collection)
				{
					list.Items.Add(entry);
				}
			}
			else if (control is ComboBox combo)
			{
				combo.Items.Clear();

				foreach (var entry in collection)
				{
					combo.Items.Add(entry);
				}
			}

			control.ResumeLayout(true);
		}
	}
}