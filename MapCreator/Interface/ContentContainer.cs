using System.ComponentModel;
using System.Windows.Forms.Animation;

namespace MapCreator.Interface
{
	[ToolboxItem(false)]
	public partial class ContentContainer : UserControl
	{
		public event EventHandler<ContentChangingEventArgs>? ContentChanging;
		public event EventHandler<ContentChangedEventArgs>? ContentChanged;
		public event EventHandler<ContentEventArgs>? ContentAdded;
		public event EventHandler<ContentEventArgs>? ContentRemoved;

		protected override Size DefaultSize { get; } = new Size(548, 186);
		protected override Size DefaultMinimumSize { get; } = new Size(548, 186);

		[Browsable(false)]
		public int ContentCount => Controls.Count;

		private int _ContentIndex = -1;

		[Browsable(false)]
		public int ContentIndex
		{
			get => _ContentIndex;
			set
			{
				Control? oldContent = null;

				if (ContentIndex >= 0 && ContentIndex < ContentCount)
				{
					oldContent = Controls[ContentIndex];
				}

				Control? newContent = null;

				if (value >= 0 && value < ContentCount)
				{
					newContent = Controls[value];
				}

				if (oldContent == newContent)
				{
					return;
				}

				var changingArgs = new ContentChangingEventArgs(oldContent, newContent);

				OnContentChanging(changingArgs);

				if (changingArgs.PreventChange)
				{
					return;
				}

				_ContentIndex = Controls.IndexOf(newContent);

				oldContent?.Hide();
				newContent?.Show();

				OnContentChanged(new ContentChangedEventArgs(oldContent, newContent));
			}
		}

		[Browsable(false)]
		public Control? ContentValue
		{
			get
			{
				if (ContentIndex >= 0 && ContentIndex < ContentCount)
				{
					return Controls[ContentIndex];
				}

				return null;
			}
			set => ContentIndex = Controls.IndexOf(value);
		}

		private Size _InitialMinimumSize, _InitialSize;

		public ContentContainer()
		{
			InitializeComponent();
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			_InitialMinimumSize = MinimumSize;
			_InitialSize = Size;

			this.PreventFocusOutline<Button>();
		}

		public void AddContent(Control content)
		{
			Controls.Add(content);
		}

		public void RemoveContent(Control content)
		{
			Controls.Remove(content);
		}

		protected sealed override void OnControlAdded(ControlEventArgs e)
		{
			base.OnControlAdded(e);

			if (e.Control != null && Controls.Contains(e.Control))
			{
				OnContentAdded(new ContentEventArgs(e.Control));

				if (!Controls.Contains(e.Control))
				{
					return;
				}

				e.Control.Location = Point.Empty;
				e.Control.TabIndex = ContentCount;
				e.Control.Dock = DockStyle.Fill;

				if (ContentCount > 1)
				{
					e.Control.Hide();
				}

				if (ContentCount == 1)
				{
					ContentValue = e.Control;
				}
			}
		}

		protected sealed override void OnControlRemoved(ControlEventArgs e)
		{
			base.OnControlRemoved(e);

			if (e.Control != null)
			{
				if (!Controls.Contains(e.Control))
				{
					if (ContentValue == e.Control)
					{
						ContentValue = null;
					}

					OnContentRemoved(new ContentEventArgs(e.Control));
				}
			}
		}

		protected virtual void OnContentChanging(ContentChangingEventArgs e)
		{
			ContentChanging?.Invoke(this, e);

			if (!e.PreventChange)
			{
				MinimumSize = e.Content?.MinimumSize ?? _InitialMinimumSize;
				Size = e.Content?.Size ?? _InitialSize;
			}
		}

		protected virtual void OnContentChanged(ContentChangedEventArgs e)
		{
			ContentChanged?.Invoke(this, e);
		}

		protected virtual void OnContentAdded(ContentEventArgs e)
		{
			ContentAdded?.Invoke(this, e);
		}

		protected virtual void OnContentRemoved(ContentEventArgs e)
		{
			ContentRemoved?.Invoke(this, e);
		}
	}

	public class ContentEventArgs : EventArgs
	{
		public Control? Content { get; }

		public ContentEventArgs(Control? content)
		{
			Content = content;
		}
	}

	public class ContentChangingEventArgs : ContentEventArgs
	{
		public Control? OldContent { get; }

		public bool PreventChange { get; set; }

		public ContentChangingEventArgs(Control? oldContent, Control? newContent)
			: base(newContent)
		{
			OldContent = oldContent;
		}
	}

	public class ContentChangedEventArgs : ContentEventArgs
	{
		public Control? OldContent { get; }

		public ContentChangedEventArgs(Control? oldContent, Control? newContent)
			: base(newContent)
		{
			OldContent = oldContent;
		}
	}
}
