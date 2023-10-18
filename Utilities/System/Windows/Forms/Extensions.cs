using System.Collections;
using System.Timers;

namespace System.Windows.Forms
{
	public static class Extensions
	{
		public static IEnumerable<T> FindChildren<T>(this Control root) where T : Control
		{
			return FindChildren<T>(root, true);
		}

		public static IEnumerable<T> FindChildren<T>(this Control root, bool recursive) where T : Control
		{
			if (!root.HasChildren)
			{
				yield break;
			}

			foreach (Control child in root.Controls)
			{
				if (child is T found)
				{
					yield return found;
				}

				if (!child.HasChildren)
				{
					continue;
				}

				foreach (var nested in FindChildren<T>(child))
				{
					yield return nested;
				}
			}
		}

		public static void PreventFocusOutline<T>(this ContainerControl container) where T : Control
		{
			PreventFocusOutline<T>(container, true);
		}

		public static void PreventFocusOutline<T>(this ContainerControl container, bool recursive) where T : Control
		{
			foreach (var c in FindChildren<T>(container, recursive))
			{
				PreventFocusOutline(c);
			}
		}

		public static void PreventFocusOutline(this Control control)
		{
			control.GotFocus -= InternalPreventFocusOutline;
			control.GotFocus += InternalPreventFocusOutline;
		}

		private static void InternalPreventFocusOutline(object? sender, EventArgs e)
		{
			if (sender is Control control)
			{
				var form = control.FindForm();

				if (form?.ActiveControl == control)
				{
					form.ActiveControl = control.Parent;
				}
			}
		}

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

		public static void SetWindowDragHandle(this Control control)
		{
			control.MouseDown -= Native.BeginMoveWindow;
			control.MouseDown += Native.BeginMoveWindow;
		}

		#region Global ToolTip

		private static readonly ToolTip _ToolTip = new();

		private static TaskTimer? _ToolTipTimer;

		public static void SetToolTip(this Control control, Exception error)
		{
			SetToolTip(control, ToolTipIcon.Error, error.Message);
		}

		public static void SetToolTip(this Control control, string? text)
		{
			SetToolTip(control, ToolTipIcon.None, text);
		}

		public static void SetToolTip(this Control control, ToolTipIcon icon, string? text)
		{
			SetToolTip(control, icon, null, text);
		}

		public static void SetToolTip(this Control control, string? title, string? text)
		{
			SetToolTip(control, ToolTipIcon.None, title, text);
		}

		public static void SetToolTip(this Control control, ToolTipIcon icon, string? title, string? text)
		{
			control.MouseEnter += (s, e) =>
			{
				_ToolTip.RemoveAll();

				_ToolTip.ToolTipIcon = ToolTipIcon.None;
				_ToolTip.ToolTipTitle = null;

				_ToolTipTimer = TaskTimer.DelayCall(TimeSpan.FromMilliseconds(100), control.Invoke, () =>
				{
					_ToolTip.ToolTipIcon = icon;
					_ToolTip.ToolTipTitle = title;

					_ToolTip.SetToolTip(control, text);
				});
			};

			control.MouseLeave += (s, e) =>
			{
				_ToolTipTimer?.Stop();
				_ToolTipTimer = null;

				_ToolTip.ToolTipIcon = ToolTipIcon.None;
				_ToolTip.ToolTipTitle = null;

				_ToolTip.RemoveAll();
			};
		}

		#endregion
	}
}