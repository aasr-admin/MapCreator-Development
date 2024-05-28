
namespace MapCreator
{
    public partial class CanvasControlBox : Form
    {
        private bool _initialized, _suppressEvent;

        private bool _dragging = false;
        private Point _dragStart, _dragCursor;

        public sbyte XAxisValue
        {
            get => (sbyte)xAxis_label_numUpDown.Value;
            protected set => xAxis_label_numUpDown.Value = Math.Clamp(value, xAxis_label_numUpDown.Minimum, xAxis_label_numUpDown.Maximum);
        }

        public sbyte XAxisMinimum
        {
            get => (sbyte)xAxis_label_numUpDown.Minimum;
            set => xAxis_label_numUpDown.Minimum = Math.Min(value, xAxis_label_numUpDown.Maximum);
        }

        public sbyte XAxisMaximum
        {
            get => (sbyte)xAxis_label_numUpDown.Maximum;
            set => xAxis_label_numUpDown.Maximum = Math.Max(value, xAxis_label_numUpDown.Minimum);
        }

        public sbyte YAxisValue
        {
            get => (sbyte)yAxis_label_numUpDown.Value;
            protected set => yAxis_label_numUpDown.Value = Math.Clamp(value, yAxis_label_numUpDown.Minimum, yAxis_label_numUpDown.Maximum);
        }

        public sbyte YAxisMinimum
        {
            get => (sbyte)yAxis_label_numUpDown.Minimum;
            set => yAxis_label_numUpDown.Minimum = Math.Min(value, yAxis_label_numUpDown.Maximum);
        }

        public sbyte YAxisMaximum
        {
            get => (sbyte)yAxis_label_numUpDown.Maximum;
            set => yAxis_label_numUpDown.Maximum = Math.Max(value, yAxis_label_numUpDown.Minimum);
        }

        public sbyte ZAxisValue
        {
            get => (sbyte)zAxis_label_numUpDown.Value;
            protected set => zAxis_label_numUpDown.Value = Math.Clamp(value, zAxis_label_numUpDown.Minimum, zAxis_label_numUpDown.Maximum);
        }

        public sbyte ZAxisMinimum
        {
            get => (sbyte)zAxis_label_numUpDown.Minimum;
            set => zAxis_label_numUpDown.Minimum = Math.Min(value, zAxis_label_numUpDown.Maximum);
        }

        public sbyte ZAxisMaximum
        {
            get => (sbyte)zAxis_label_numUpDown.Maximum;
            set => zAxis_label_numUpDown.Maximum = Math.Max(value, zAxis_label_numUpDown.Minimum);
        }

        public event EventHandler XAxisValueChanged
        {
            add => xAxis_label_numUpDown.ValueChanged += value;
            remove => xAxis_label_numUpDown.ValueChanged -= value;
        }

        public event EventHandler YAxisValueChanged
        {
            add => yAxis_label_numUpDown.ValueChanged += value;
            remove => yAxis_label_numUpDown.ValueChanged -= value;
        }

        public event EventHandler ZAxisValueChanged
        {
            add => zAxis_label_numUpDown.ValueChanged += value;
            remove => zAxis_label_numUpDown.ValueChanged -= value;
        }

        public event EventHandler AxisValueChanged;

        public CanvasControlBox()
        {
            InitializeComponent();
        }

        private void OnSingleValueChanged(object sender, EventArgs e)
        {
            if (!_suppressEvent && sender is NumericUpDown num)
            {
                AxisValueChanged?.Invoke(num, EventArgs.Empty);
            }
        }

        public bool OffsetAxis(sbyte? x, sbyte? y, sbyte? z)
        {
            if (x != null)
            {
                x += XAxisValue;
            }

            if (y != null)
            {
                y += YAxisValue;
            }

            if (z != null)
            {
                z += ZAxisValue;
            }

            return UpdateAxis(x, y, z);
        }

        public bool UpdateAxis(sbyte? x, sbyte? y, sbyte? z)
        {
            var updated = !_initialized;

            _suppressEvent = true;

            try
            {
                if (x != null)
                {
                    XAxisValue = x.Value;
                    updated = true;
                }

                if (y != null)
                {
                    YAxisValue = y.Value;
                    updated = true;
                }

                if (z != null)
                {
                    ZAxisValue = z.Value;
                    updated = true;
                }

                return updated;
            }
            finally
            {
                _initialized = true;
                _suppressEvent = false;

                if (updated)
                {
                    AxisValueChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private void OnAxisButtonClick(object sender, EventArgs e)
        {
            if (sender is ToolStripButton button && button.Tag is int index)
            {
                switch (index)
                {
                    case 1: // W
                    OffsetAxis(-1, 0, 0);
                    break;

                    case 2: // NW
                    OffsetAxis(-1, -1, 0);
                    break;

                    case 3: // N
                    OffsetAxis(0, -1, 0);
                    break;

                    case 4: // SW
                    OffsetAxis(-1, 1, 0);
                    break;

                    case 5: // Center
                    UpdateAxis(0, 0, 0);
                    break;

                    case 6: // NE
                    OffsetAxis(1, -1, 0);
                    break;

                    case 7: // S
                    OffsetAxis(0, 1, 0);
                    break;

                    case 8: // SE
                    OffsetAxis(1, 1, 0);
                    break;

                    case 9: // E
                    OffsetAxis(1, 0, 0);
                    break;
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _dragging = true;
                _dragStart = Location;
                _dragCursor = Cursor.Position;
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (_dragging)
            {
                var loc = Cursor.Position;

                loc.Offset(-_dragCursor.X, -_dragCursor.Y);
                loc.Offset(_dragStart.X, _dragStart.Y);

                Location = loc;
            }

            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            _dragging = false;

            base.OnMouseUp(e);
        }
    }
}
