
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace System.Windows.Forms
{
    public class ResponsivePanel : Panel
    {
        public ResponsivePanel()
        {
            DoubleBuffered = true;

            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }
    }

    public class ResponsiveProgressBar : Panel
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

        public ResponsiveProgressBar()
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

    public class ResponsiveTileView : ListView
    {
        private readonly SolidBrush _transparentBackground = new(Color.FromArgb(0x50, Color.Black));

        private bool _scrolling;

        public bool Scrolling => _scrolling;

        public event EventHandler ScrollStart, ScrollEnd;

        protected override bool ShowFocusCues { get; } = false;

        public ListViewGroup DefaultGroup { get; }
        public ListViewGroup InvalidGroup { get; }

        public ResponsiveTileView()
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

    public class ResponsiveGridView : DataGridView
    {
        private readonly DataGridViewButtonColumn _ActionColumn;

        public object SelectedItem
        {
            get
            {
                foreach (DataGridViewRow row in SelectedRows)
                {
                    if (!row.IsNewRow && row.DataBoundItem != null)
                    {
                        return row.DataBoundItem;
                    }
                }

                return null;
            }
            set
            {
                ClearSelection();

                if (value == null)
                {
                    return;
                }

                foreach (DataGridViewRow row in Rows)
                {
                    if (!row.IsNewRow && row.DataBoundItem == value)
                    {
                        row.Selected = true;

                        return;
                    }
                }
            }
        }

        public IEnumerable<object> SelectedItems
        {
            get
            {
                foreach (DataGridViewRow row in SelectedRows)
                {
                    if (!row.IsNewRow && row.DataBoundItem != null)
                    {
                        yield return row.DataBoundItem;
                    }
                }
            }
        }

        public IEnumerable<object> Items
        {
            get
            {
                foreach (DataGridViewRow row in Rows)
                {
                    if (!row.IsNewRow && row.DataBoundItem != null)
                    {
                        yield return row.DataBoundItem;
                    }
                }
            }
        }

        public ResponsiveGridView()
        {
            _ActionColumn = new DataGridViewButtonColumn
            {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader,
                DefaultCellStyle =
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    BackColor = SystemColors.Control,
                    ForeColor = Color.Red,
                    SelectionBackColor = SystemColors.Control,
                    SelectionForeColor = Color.Red,
                    NullValue = "[+]",
                },
                FlatStyle = FlatStyle.Popup,
                ReadOnly = true,
                Resizable = DataGridViewTriState.False,
                SortMode = DataGridViewColumnSortMode.NotSortable,
                Text = "[x]",
                UseColumnTextForButtonValue = true,
            };

            AllowUserToResizeColumns = false;
            AllowUserToResizeRows = false;
            AutoGenerateColumns = false;
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            BorderStyle = BorderStyle.None;
            ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DoubleBuffered = true;
            MultiSelect = false;
            RowHeadersVisible = false;
            RowHeadersWidth = 20;
            RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            ShowCellToolTips = false;
                        
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        protected override void SetSelectedRowCore(int rowIndex, bool selected)
        {
            base.SetSelectedRowCore(rowIndex, selected);

            if (!MultiSelect)
            {
                OnSelectionChanged(EventArgs.Empty);
            }
        }

        protected override void InitLayout()
        {
            base.InitLayout();

            if (_ActionColumn.DataGridView != this && !DesignMode)
            {
                Columns.Add(_ActionColumn);
            }
        }

        protected void ArrangeColumns()
        {
            if (DesignMode)
            {
                return;
            }

            SuspendLayout();

            _ActionColumn.DisplayIndex = ColumnCount - 1;

            var index = -1;

            foreach (DataGridViewColumn col in Columns)
            {
                if (col != _ActionColumn)
                {
                    col.DisplayIndex = ++index;
                }
            }

            ResumeLayout(false);
        }

        protected override void OnColumnAdded(DataGridViewColumnEventArgs e)
        {
            base.OnColumnAdded(e);

            ArrangeColumns();
        }

        protected override void OnColumnRemoved(DataGridViewColumnEventArgs e)
        {
            base.OnColumnRemoved(e);

            ArrangeColumns();
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            var hit = HitTest(e.X, e.Y);

            if (hit.Type == DataGridViewHitTestType.None)
            {
                var cell = CurrentCell;

                if (cell?.IsInEditMode == true && !cell.ReadOnly)
                {
                    NotifyCurrentCellDirty(true);
                    EndEdit();
                    NotifyCurrentCellDirty(false);
                }
            }

            base.OnMouseClick(e);
        }

        protected override void OnCellEnter(DataGridViewCellEventArgs e)
        {
            base.OnCellEnter(e);

            if (e.RowIndex >= 0 && !MultiSelect)
            {
                Rows[e.RowIndex].Selected = true;
            }
        }

        protected override void OnCellClick(DataGridViewCellEventArgs e)
        {
            base.OnCellClick(e);

            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var row = Rows[e.RowIndex];

                if (row.IsNewRow)
                {
                    var cell = row.Cells[e.ColumnIndex];

                    if (cell.ReadOnly)
                    {
                        cell = CurrentRow.Cells.Cast<DataGridViewCell>().FirstOrDefault(o => !o.ReadOnly);

                        if (cell != null)
                        {
                            CurrentCell = cell;
                        }
                    }

                    NotifyCurrentCellDirty(true);
                    EndEdit();
                    NotifyCurrentCellDirty(false);

                    if (!cell.IsInEditMode)
                    {
                        BeginEdit(true);
                    }
                }
            }
        }

        protected override void OnCellContentClick(DataGridViewCellEventArgs e)
        {
            base.OnCellContentClick(e);

            if (e.RowIndex >= 0 && e.ColumnIndex == _ActionColumn.Index)
            {
                var row = Rows[e.RowIndex];

                if (!row.IsNewRow)
                {
                    Rows.Remove(row);
                }
            }
        }

        protected override void OnCellLeave(DataGridViewCellEventArgs e)
        {
            var cell = CurrentCell;

            if (cell?.IsInEditMode == true && !cell.ReadOnly && cell.ColumnIndex == e.ColumnIndex && cell.RowIndex == e.RowIndex)
            {
                NotifyCurrentCellDirty(true);
                EndEdit();
                NotifyCurrentCellDirty(false);
            }
            
            base.OnCellLeave(e);
        }
    }
}