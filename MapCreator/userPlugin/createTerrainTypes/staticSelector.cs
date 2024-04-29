using UltimaSDK;

namespace MapCreator
{
    public partial class staticSelector : Form
    {
        private event Action loadedCallback;

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
                    loadedCallback += () => valueSelector.Value = value;
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

        public staticSelector()
        {
            InitializeComponent();

            progressLayout.Visible = false;
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            valueSelector.Enabled = false;
            searchBox.Enabled = false;
            searchButton.Enabled = false;

            listView.Enabled = false;
            listView.UseWaitCursor = true;

            var max = progressBar.Maximum = Capacity;

            progressBar.Value = 0;
            progressLabel.Text = $"{progressBar.Value} / {progressBar.Maximum}";
            progressLayout.Visible = true;

            var size = listView.TileSize;

            var items = new ListViewItem[max];

            var complete = new HashSet<string>();

            await Task.Factory.StartNew(() =>
            {
                for (var index = 0; index < max; index++)
                {
                    var image = Art.GetStatic(index, false);

                    image?.MakeTransparent(Color.White);

                    var item = new ListViewItem($"{index}")
                    {
                        Tag = image
                    };

                    ref var data = ref TileData.ItemTable[index];

                    if (!string.IsNullOrWhiteSpace(data.Name))
                    {
                        item.ToolTipText = data.Name;

                        complete.Add(data.Name.ToLowerInvariant());
                    }

                    items[index] = item;

                    Invoke(PushProgress);
                }
            }, TaskCreationOptions.LongRunning).ConfigureAwait(true);

            if (complete.Count > 0)
            {
                var auto = new AutoCompleteStringCollection();

                foreach (var match in complete)
                {
                    auto.Add(match);
                }

                complete.Clear();
                complete.TrimExcess();

                searchBox.AutoCompleteCustomSource = auto;
                searchBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                searchBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }

            listView.BeginUpdate();
            listView.Items.AddRange(items);
            listView.EndUpdate();

            listView.UseWaitCursor = false;
            listView.Enabled = true;

            progressLayout.Visible = false;

            valueSelector.Enabled = true;
            searchBox.Enabled = true;
            searchButton.Enabled = true;

            loading = false;

            loadedCallback?.Invoke();
            loadedCallback = null;
        }

        private void PushProgress()
        {
            progressBar.PerformStep();
            progressLabel.Text = $"{progressBar.Value} / {progressBar.Maximum}";
        }

        private void OnDrawItem(object sender, DrawListViewItemEventArgs e)
        {
            if (e.Item.Selected)
            {
                e.Graphics.FillRectangle(Brushes.LightSkyBlue, e.Bounds);
                e.Graphics.DrawRectangle(Pens.DeepSkyBlue, e.Bounds);
            }
            else
            {
                e.Graphics.DrawRectangle(Pens.LightSkyBlue, e.Bounds);
            }

            if (e.Item.Tag is Image image)
            {
                var srcWidth = Math.Min(e.Bounds.Width, image.Width);
                var srcHeight = Math.Min(e.Bounds.Height, image.Height);

                e.Graphics.DrawImage(image, e.Bounds, 0, 0, srcWidth, srcHeight, GraphicsUnit.Pixel);
            }

            using var trans = new SolidBrush(Color.FromArgb(96, Color.Black));

            var size = e.Graphics.MeasureString(e.Item.Text, e.Item.Font);

            var height = size.Height;

            var rect = new RectangleF(e.Bounds.Location, e.Bounds.Size);

            rect.Y = rect.Bottom - height;
            rect.Height = height;

            e.Graphics.FillRectangle(trans, rect);

            e.Graphics.DrawString(e.Item.Text, e.Item.Font, Brushes.WhiteSmoke, rect);
        }

        private void OnValueSelectionChanged(object sender, EventArgs e)
        {
            listView.SelectedIndices.Clear();

            if (Value >= 0 && Value < listView.Items.Count)
            {
                listView.SelectedIndices.Add(Value);

                listView.EnsureVisible(Value);

                previewImage.Image = listView.Items[Value].Tag as Image;
            }

            if (searchIndex != Value)
            {
                searchIndex = -1;
            }
        }

        private void OnItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                Value = (int)Math.Clamp(e.ItemIndex, valueSelector.Minimum, valueSelector.Maximum);
            }
        }

        private void OnSearchClick(object sender, EventArgs e)
        {
            if (searchText != searchBox.Text)
            {
                searchIndex = -1;
                searchText = searchBox.Text;
            }

            if (string.IsNullOrWhiteSpace(searchText))
            {
                return;
            }

            var count = listView.Items.Count;

            while (--count >= 0)
            {
                searchIndex = ++searchIndex % listView.Items.Count;

                var item = listView.Items[searchIndex];

                if (string.IsNullOrWhiteSpace(item.ToolTipText))
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
