using Assets;

using System.Collections.Concurrent;

namespace MapCreator
{
	public partial class StaticSelector : Form
	{
		private Action _loadedCallback;

		private bool loading = true;

		private string searchText;
		private int searchIndex = -1;

		public int Value
		{
			get => (int)valueSelector.Value;
			set
			{
				if (loading)
				{
					_loadedCallback += () => valueSelector.Value = value;
				}
				else
				{
					valueSelector.Value = value;
				}
			}
		}

		public int Minimum => (int)valueSelector.Minimum;
		public int Maximum => (int)valueSelector.Maximum;

		public int Capacity => Maximum + 1;

		public event EventHandler ValueChanged
		{
			add => valueSelector.ValueChanged += value;
			remove => valueSelector.ValueChanged -= value;
		}

		public StaticSelector()
		{
			InitializeComponent();

			progressBar.Limit = Capacity;
		}

		protected override async void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			closeButton.Select();

			UseWaitCursor = true;

			valueSelector.Enabled = false;
			searchBox.Enabled = false;
			searchButton.Enabled = false;

			tileView.Enabled = false;

			var staticGroup = tileView.DefaultGroup;

			staticGroup.Header = "Statics";

			var emptyGroup = tileView.InvalidGroup;

			emptyGroup.Name = "Empty";

			var items = new ListViewItem[Capacity];

			var complete = new ConcurrentBag<string>();

			var tasks = new Task[32];

			var chunk = (int)Math.Ceiling(items.Length / (double)tasks.Length);

			for (var i = 0; i < tasks.Length; i++)
			{
				var start = i * chunk;
				var end = Math.Min(start + chunk, items.Length);

				tasks[i] = Task.Factory.StartNew(() =>
				{
					for (var index = start; index < end; index++)
					{
						var image = AssetData.Art.GetStatic(index);

						image?.MakeTransparent(Color.White);

						ListViewItem item;

						if (image != null)
						{
							item = new ListViewItem($"{index}", staticGroup)
							{
								Tag = image
							};

							ref var data = ref AssetData.Tiles.ItemTable[index];

							if (!String.IsNullOrWhiteSpace(data.Name))
							{
								item.ToolTipText = item.Name = data.Name;

								complete.Add(data.Name.Trim().ToLowerInvariant());
							}
						}
						else
						{
							item = new ListViewItem($"{index}", emptyGroup);
						}

						items[index] = item;

						_ = BeginInvoke(progressBar.Step);
					}
				}, TaskCreationOptions.LongRunning);
			}

			await Task.WhenAll(tasks);

			await Task.Run(() =>
			{
				while (progressBar.Value < progressBar.Limit)
				{
					_ = Thread.Yield();
				}
			});

			_ = BeginInvoke(() =>
			{
				_ = tileView.Groups.Add(staticGroup);
				_ = tileView.Groups.Add(emptyGroup);

				if (!complete.IsEmpty)
				{
					var pruned = new HashSet<string>(complete.Count);

					pruned.UnionWith(complete);

					complete.Clear();

					var auto = new AutoCompleteStringCollection();

					foreach (var match in pruned)
					{
						_ = auto.Add(match);
					}

					pruned.Clear();
					pruned.TrimExcess();

					searchBox.AutoCompleteCustomSource = auto;
					searchBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
				}

				tileView.BeginUpdate();
				tileView.Items.AddRange(items);
				tileView.EndUpdate();

				progressBar.Visible = false;

				tileView.Enabled = true;

				valueSelector.Enabled = true;
				searchBox.Enabled = true;
				searchButton.Enabled = true;

				UseWaitCursor = false;

				loading = false;

				_loadedCallback?.Invoke();
				_loadedCallback = null;
			});
		}

		private void OnValueSelectionChanged(object sender, EventArgs e)
		{
			if (searchIndex != Value)
			{
				searchIndex = -1;
			}

			tileView.SelectedIndices.Clear();

			if (Value >= 0 && Value < tileView.Items.Count)
			{
				_ = tileView.SelectedIndices.Add(Value);

				tileView.EnsureVisible(Value);

				previewImage.Image = tileView.Items[Value].Tag as Image;
			}
		}

		private void OnItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			if (e.IsSelected)
			{
				Value = (int)Math.Clamp(e.ItemIndex, valueSelector.Minimum, valueSelector.Maximum);
			}
		}

		private void OnClearClick(object sender, EventArgs e)
		{
			searchIndex = -1;
			searchText = searchBox.Text = null;
		}

		private void OnSearchClick(object sender, EventArgs e)
		{
			if (searchText != searchBox.Text)
			{
				searchIndex = -1;
				searchText = searchBox.Text;
			}

			if (String.IsNullOrWhiteSpace(searchText))
			{
				return;
			}

			var count = tileView.Items.Count;

			while (--count >= 0)
			{
				searchIndex = ++searchIndex % tileView.Items.Count;

				var item = tileView.Items[searchIndex];

				if (String.IsNullOrWhiteSpace(item.ToolTipText))
				{
					continue;
				}

				if (item.ToolTipText.Contains(searchText, StringComparison.OrdinalIgnoreCase))
				{
					Value = searchIndex;
					return;
				}
			}

			searchIndex = -1;
		}

		private void OnCloseClick(object sender, EventArgs e)
		{
			Hide();
		}
	}
}
