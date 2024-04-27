using UltimaSDK;

namespace MapCreator.userPlugin
{
    public partial class staticSelector : Form
    {
        private static readonly Lazy<Bitmap> _emptyTile = new(() =>
        {
            var image = new Bitmap(64, 64);

            using var g = Graphics.FromImage(image);

            var x = 10;
            var y = 10;

            g.FillPolygon(Brushes.Black, new[]
            {
                new Point(x + 22, y + 00), new Point(x + 43, y + 21),
                new Point(x + 43, y + 22), new Point(x + 22, y + 43),
                new Point(x + 21, y + 43), new Point(x + 00, y + 22),
                new Point(x + 00, y + 21), new Point(x + 21, y + 00),
            });

            return image;
        });

        private event Action loadedCallback;

        private bool dragging, loading = true;
        private Point dragCursorPoint, dragFormPoint;

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

        public event EventHandler ValueChanged
        {
            add => valueSelector.ValueChanged += value;
            remove => valueSelector.ValueChanged -= value;
        }

        public staticSelector()
        {
            InitializeComponent();
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            valueSelector.Enabled = false;
            searchBox.Enabled = false;
            searchButton.Enabled = false;

            listView.Enabled = false;
            listView.UseWaitCursor = true;

            var length = (int)valueSelector.Maximum + 1;

            var images = new Image[length];
            var items = new ListViewItem[length];

            await Task.Run(() =>
            {
                var max = Art.GetMaxItemID();

                var empty = _emptyTile.Value;
                var bounds = new Rectangle(Point.Empty, empty.Size);

                var blocks = length / 1024;

                Parallel.For(0, blocks, block =>
                {
                    var start = block * 1024;
                    var end = start + 1024;

                    for (var i = start; i < end; i++)
                    {
                        items[i] = new ListViewItem($"{i}", i);

                        if (i <= max)
                        {
                            var image = Art.GetStatic(i, true, false);

                            images[i] = image?.GetThumbnailImage(empty.Width, empty.Height, null, 0);

                            /*
                            if (image != null)
                            {
                                var icon = new Bitmap(empty.Width, empty.Height, image.PixelFormat);

                                using var g = Graphics.FromImage(icon);

                                g.DrawImage(image, bounds, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);

                                images[i] = icon;
                            }
                            */

                            ref var data = ref TileData.ItemTable[i];

                            if (!string.IsNullOrWhiteSpace(data.Name))
                            {
                                items[i].ToolTipText = data.Name;
                            }
                        }

                        images[i] ??= empty;

                        progressBar.BeginInvoke(progressBar.PerformStep);
                    }
                });
            }).ConfigureAwait(true);

            listView.SuspendLayout();

            imageList.Images.AddRange(images);
            listView.Items.AddRange(items);

            listView.ResumeLayout();

            listView.UseWaitCursor = false;
            listView.Enabled = true;

            progressBar.Visible = false;
            progressBar.Value = 0;

            valueSelector.Enabled = true;
            searchBox.Enabled = true;
            searchButton.Enabled = true;

            loading = false;

            loadedCallback?.Invoke();
            loadedCallback = null;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = Location;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (dragging)
            {
                var dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));

                Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            
            dragging = false;
        }

        private void OnValueSelectionChanged(object sender, EventArgs e)
        {
            listView.EnsureVisible(Value);

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
