namespace MapCreator.Interface
{
	public partial class ContentContainer : Panel
	{
		public event EventHandler<ContentEventArgs>? ContentChanging;
		public event EventHandler<ContentEventArgs>? ContentChanged;
		public event EventHandler<ContentEventArgs>? ContentAdded;
		public event EventHandler<ContentEventArgs>? ContentRemoved;

		public int ContentCount => Controls.Count;

		private int _ContentIndex;

		public int ContentIndex
		{
			get => _ContentIndex;
			set
			{
				OnContentChanging(new ContentEventArgs(ContentValue));

				_ContentIndex = value;

				SuspendLayout();

				for (var i = 0; i < ContentCount; i++)
				{
					if (i != _ContentIndex)
					{
						Controls[i].Hide();
					}
				}

				ContentValue?.Show();

				OnContentChanged(new ContentEventArgs(ContentValue));

				ResumeLayout(false);
			}
		}

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

		public ContentContainer()
		{
			InitializeComponent();
		}

		public void AddContent(Control content)
		{
			SuspendLayout();

			Controls.Add(content);

			ResumeLayout(false);
		}

		public void RemoveContent(Control content)
		{
			SuspendLayout();

			Controls.Remove(content);

			ResumeLayout(false);
		}

		protected sealed override void OnControlAdded(ControlEventArgs e)
		{
			base.OnControlAdded(e);

			if (e.Control != null)
			{
				e.Control.Dock = DockStyle.Fill;
				e.Control.Location = Point.Empty;
				e.Control.Size = Size;
				e.Control.TabIndex = ContentCount;

				if (ContentCount > 1)
				{
					e.Control.Visible = false;
				}

				OnContentAdded(new ContentEventArgs(e.Control));
			}
		}

		protected sealed override void OnControlRemoved(ControlEventArgs e)
		{
			base.OnControlRemoved(e);

			if (e.Control != null)
			{
				OnContentRemoved(new ContentEventArgs(e.Control));
			}
		}

		protected virtual void OnContentChanging(ContentEventArgs e)
		{
			ContentChanging?.Invoke(this, e);
		}

		protected virtual void OnContentChanged(ContentEventArgs e)
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
}
