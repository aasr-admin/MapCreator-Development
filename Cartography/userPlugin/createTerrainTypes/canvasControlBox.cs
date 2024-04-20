namespace MapCreator.userPlugin
{
    public partial class canvasControlBox : Form
    {
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        public event EventHandler ActionNorth;
        public event EventHandler ActionNorthEast;
        public event EventHandler ActionEast;
        public event EventHandler ActionSouthEast;
        public event EventHandler ActionSouth;
        public event EventHandler ActionSouthWest;
        public event EventHandler ActionWest;
        public event EventHandler ActionNorthWest;

        public event EventHandler ActionChangeX;
        public event EventHandler ActionChangeY;
        public event EventHandler ActionChangeZ;

        public canvasControlBox()
        {
            InitializeComponent();
        }

        private void canvasControlBox_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = Location;
        }

        private void canvasControlBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                var dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void canvasControlBox_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        /// NumericUpDown: ValueChanged
        private void xAxis_label_numUpDown_ValueChanged(object sender, EventArgs e)
        {
            ActionChangeX?.Invoke(sender, e);
        }

        private void yAxis_label_numUpDown_ValueChanged(object sender, EventArgs e)
        {
            ActionChangeY?.Invoke(sender, e);
        }

        private void zAxis_label_numUpDown_ValueChanged(object sender, EventArgs e)
        {
            ActionChangeZ?.Invoke(sender, e);
        }

        /// CanvasControlBox Buttons
        private void NorthWestButton_Click(object sender, EventArgs e)
        {
            if (NorthWestButton == null)
            {
                return;
            }

            ActionNorthWest?.Invoke(sender, e);
        }

        private void NorthButton_Click(object sender, EventArgs e)
        {
            if (NorthButton == null)
            {
                return;
            }

            ActionNorth?.Invoke(sender, e);
        }

        private void NorthEastButton_Click(object sender, EventArgs e)
        {
            if (NorthEastButton == null)
            {
                return;
            }

            ActionNorthEast?.Invoke(sender, e);
        }

        private void WestButton_Click(object sender, EventArgs e)
        {
            if (WestButton == null)
            {
                return;
            }

            ActionWest?.Invoke(sender, e);
        }

        private void NavIcon_Click(object sender, EventArgs e)
        {
            /// ToDo: Idea To Bring Up A Compass
        }

        private void EastButton_Click(object sender, EventArgs e)
        {
            if (EastButton == null)
            {
                return;
            }

            ActionEast?.Invoke(sender, e);
        }

        private void SouthWestButton_Click(object sender, EventArgs e)
        {
            if (SouthWestButton == null)
            {
                return;
            }

            ActionSouthWest?.Invoke(sender, e);
        }

        private void SouthButton_Click(object sender, EventArgs e)
        {
            if (SouthButton == null)
            {
                return;
            }

            ActionSouth?.Invoke(sender, e);
        }

        private void SouthEastButton_Click(object sender, EventArgs e)
        {
            if (SouthEastButton == null)
            {
                return;
            }

            ActionSouthEast?.Invoke(sender, e);
        }
    }
}
