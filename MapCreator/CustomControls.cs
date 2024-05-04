
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace System.Windows.Forms
{
    public class FastPanel : Panel
    {
        public FastPanel()
        {
            DoubleBuffered = true;

            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }
    }

    public class FastProgressBar : Panel
    {
        protected override Size DefaultSize { get; } = new Size(100, 30);

        protected override bool ShowFocusCues { get; } = false;

        private float _value;

        [DefaultValue(0f), Range(0f, float.MaxValue)]
        public virtual float Value
        {
            get => _value;
            set
            {
                value = Math.Clamp(value, 0f, _limit);

                if (_value == value)
                {
                    return;
                }

                _value = value;

                _percentage = _value / _limit;

                Invalidate();
            }
        }

        private float _limit = 100f;

        [DefaultValue(100f), Range(0f, float.MaxValue)]
        public virtual float Limit
        {
            get => _limit;
            set
            {
                value = Math.Max(value, 0f);

                if (_limit == value)
                {
                    return;
                }

                _limit = value;

                if (_value > _limit)
                {
                    _value = _limit;
                }

                _percentage = _value / _limit;

                Invalidate();
            }
        }

        private float _percentage;

        [DefaultValue(0f), Range(0f, 1f)]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public virtual float Percentage
        {
            get => _percentage;
            set
            {
                if (_percentage == value)
                {
                    return;
                }

                _percentage = Math.Clamp(value, 0f, 1f);

                _value = _limit * _percentage;

                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the text associated with this control.
        /// </summary>
        [DefaultValue(null)]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Always)]
        public override string Text { get => base.Text; set => base.Text = value; }

        private Color _barColor = SystemColors.Highlight;

        /// <summary>
        /// Gets or sets the color of the bar within the control.
        /// </summary>
        [DefaultValue(typeof(SystemColors), "Highlight")]
        public virtual Color BarColor
        {
            get => _barColor;
            set
            {
                if (_barColor == value)
                {
                    return;
                }

                _barColor = value;

                Invalidate();
            }
        }

        public FastProgressBar()
        {
            DoubleBuffered = true;

            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
        }

        public void Step()
        {
            Step(1);
        }

        public void Step(int delta)
        {
            Value += delta;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            
            var clip = new RectangleF(e.ClipRectangle.X, e.ClipRectangle.Y, e.ClipRectangle.Width, e.ClipRectangle.Height);

            e.Graphics.FillRectangle(SystemBrushes.Highlight, clip.X, clip.Y, clip.Width * _percentage, clip.Height);

            if (!string.IsNullOrWhiteSpace(Text))
            {
                using var brush = new SolidBrush(ForeColor);

                var size = e.Graphics.MeasureString(Text, Font, clip.Size);

                var delta = SizeF.Subtract(clip.Size, size);

                if (delta.Width > 0)
                {
                    delta.Width /= 2f;

                    clip.X += delta.Width;
                    clip.Width -= delta.Width;
                }

                if (delta.Height > 0)
                {
                    delta.Height /= 2f;

                    clip.Y += delta.Height;
                    clip.Height -= delta.Height;
                }

                e.Graphics.DrawString(Text, Font, brush, clip);
            }
        }
    }

    public class FastTileView : ListView
    {
        private readonly SolidBrush _transparentBackground = new SolidBrush(Color.FromArgb(0x50, Color.Black));

        private bool _scrolling;

        public bool Scrolling => _scrolling;

        public event EventHandler ScrollStart, ScrollEnd;

        protected override bool ShowFocusCues { get; } = false;

        public ListViewGroup DefaultGroup { get; }
        public ListViewGroup InvalidGroup { get; }

        public FastTileView()
        {
            Alignment = ListViewAlignment.SnapToGrid;
            DoubleBuffered = true;
            HeaderStyle = ColumnHeaderStyle.None;
            LabelWrap = false;
            MultiSelect = false;
            OwnerDraw = true;
            ResizeRedraw = false;
            ShowGroups = true;
            ShowItemToolTips = true;
            TileSize = new Size(44, 44);
            View = View.Tile;

            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            Groups.Add(DefaultGroup = new ListViewGroup("Default", "Entries")
            {
                CollapsedState = ListViewGroupCollapsedState.Expanded
            });

            Groups.Add(InvalidGroup = new ListViewGroup("Invalid", "Invalid")
            {
                CollapsedState = ListViewGroupCollapsedState.Collapsed
            });
        }

        protected override void OnDrawItem(DrawListViewItemEventArgs e)
        {
            base.OnDrawItem(e);

            if (e.DrawDefault)
            {
                return;
            }
            
            var bounds = new RectangleF(e.Bounds.Location, e.Bounds.Size);
            
            using var back = new SolidBrush(e.Item.BackColor);

            e.Graphics.FillRectangle(back, bounds);
            
            if (e.Item.Selected)
            {
                e.Graphics.FillRectangle(Brushes.LightSkyBlue, bounds);
            }

            if (e.Item.Tag is Image image)
            {
                var unit = GraphicsUnit.Pixel;
                var imageBounds = image.GetBounds(ref unit);

                if (imageBounds.Width > bounds.Width)
                {
                    imageBounds.Width = bounds.Width;
                }

                if (imageBounds.Height > bounds.Height)
                {
                    imageBounds.Height = bounds.Height;
                }

                e.Graphics.DrawImage(image, bounds, imageBounds, unit);
            }

            if (!string.IsNullOrWhiteSpace(e.Item.Text))
            {
                var size = e.Graphics.MeasureString(e.Item.Text, e.Item.Font);

                e.Graphics.FillRectangle(_transparentBackground, bounds.X, bounds.Bottom - size.Height, bounds.Width, size.Height);

                e.Graphics.DrawString(e.Item.Text, e.Item.Font, Brushes.WhiteSmoke, bounds.X, bounds.Bottom - size.Height);
            }

            if (e.Item.Selected)
            {
                e.Graphics.DrawRectangle(Pens.DeepSkyBlue, bounds);
            }
            else
            {
                e.Graphics.DrawRectangle(Pens.SkyBlue, bounds);
            }
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == 0x115) //WM_VSCROLL
            {
                switch ((ScrollEventType)(m.WParam.ToInt32() & 0xFFFF))
                {
                    default:
                    case ScrollEventType.SmallDecrement:
                    case ScrollEventType.SmallIncrement:
                    case ScrollEventType.LargeDecrement:
                    case ScrollEventType.LargeIncrement:
                    case ScrollEventType.ThumbPosition:
                    case ScrollEventType.ThumbTrack:
                    case ScrollEventType.First:
                    case ScrollEventType.Last:
                    {
                        if (!_scrolling)
                        {
                            _scrolling = true;

                            OnScrollStart(EventArgs.Empty);
                        }
                    }
                    break;

                    case ScrollEventType.EndScroll:
                    {
                        if (_scrolling)
                        {
                            _scrolling = false;

                            OnScrollEnd(EventArgs.Empty);
                        }
                    }
                    break;
                }
            }
        }

        protected virtual void OnScrollStart(EventArgs e)
        {
            //BeginUpdate();

            ScrollStart?.Invoke(this, e);
        }

        protected virtual void OnScrollEnd(EventArgs e)
        {
            //EndUpdate();

            ScrollEnd?.Invoke(this, e);
        }
    }
}