using System.Drawing.Printing;

using Microsoft.VisualBasic;

namespace Compiler
{
    public partial class buildLogger : Form
    {
        private string m_LogName;
        private DateTime m_Task_Start;
        private DateTime m_Task_End;
        private readonly PrintDocument document = new();
        private readonly PrintDialog dialog = new();

        public buildLogger()
        {
            var buildLogger = this;
            base.Load += new EventHandler(buildLogger.buildLogger_Load);
            document.PrintPage += new PrintPageEventHandler(document_PrintPage);

            InitializeComponent();
        }

        private void buildLogger_Load(object? sender, EventArgs e)
        {
            var logMessageTarget = buildLogger_textBox_logDisplay;

            logMessageTarget.Text = string.Concat("MapCreator: Logger                      |  \r", DateTime.UtcNow.ToString("dd.MM.yyyy - hh:mm:ss tt\r\n"));
            logMessageTarget.Text = string.Concat(logMessageTarget.Text, "========================================\r\n");

            #region This Lists All Assembies Used By MapCreator On The Log

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            for (var i = 0; i < assemblies.Length; i++)
            {
                var assembly = assemblies[i];

                if (assembly.EntryPoint != null)
                {
                    m_LogName = assembly.EntryPoint.DeclaringType.Name;
                    var name = assembly.GetName();

                    logMessageTarget = buildLogger_textBox_logDisplay;
                    logMessageTarget.Text = string.Concat(logMessageTarget.Text, string.Format("{0} version:{1}\r\n", name.Name, name.Version.ToString()));

                    var referencedAssemblies = assembly.GetReferencedAssemblies();

                    for (var j = 0; j < referencedAssemblies.Length; j++)
                    {
                        var assemblyName = referencedAssemblies[j];

                        logMessageTarget = buildLogger_textBox_logDisplay;
                        logMessageTarget.Text = string.Concat(logMessageTarget.Text, string.Format("{0} version:{1}\r\n", assemblyName.Name, assemblyName.Version.ToString()));
                    }
                }
            }

            logMessageTarget = buildLogger_textBox_logDisplay;
            logMessageTarget.Text = string.Concat(logMessageTarget.Text, "\r\n");

            #endregion
        }

        /// LoggerForm Output
        public void StartTask()
        {
            m_Task_Start = DateTime.UtcNow;
        }

        public void LogMessage(string Message)
        {
            var textLog = buildLogger_textBox_logDisplay;
            textLog.Text = string.Concat(textLog.Text, Message, "\r\n");
            Refresh();
        }

        public void LogTimeStamp()
        {
            var textLog = buildLogger_textBox_logDisplay;
            textLog.Text = string.Concat(textLog.Text, string.Format("  Task:{0:dd/MMM/yyyy hh:mm:ss}", m_Task_Start));
            textLog = buildLogger_textBox_logDisplay;
            textLog.Text = string.Concat(textLog.Text, " === > ");
            textLog = buildLogger_textBox_logDisplay;
            textLog.Text = string.Concat(textLog.Text, string.Format("{0:hh:mm:ss}", m_Task_End));
            textLog = buildLogger_textBox_logDisplay;
            textLog.Text = string.Concat(textLog.Text, string.Format("  Total:{0} seconds\r\n", DateAndTime.DateDiff(DateInterval.Second, m_Task_Start, m_Task_End, FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1)));
            Refresh();
        }

        public void EndTask()
        {
            m_Task_End = DateTime.UtcNow;
        }

        /// LoggerForm Saves
        private void buildLogger_menuStrip_button_saveLog_Click(object sender, EventArgs e)
        {
            var Log = new SaveFileDialog
            {
                FileName = "DefaultLog.txt",
                Filter = "ProgressLog | (*.txt)"
            };

            if (Log.ShowDialog() == DialogResult.OK)
            {
                using var sw = new StreamWriter(Log.FileName, true);
                sw.Write(buildLogger_textBox_logDisplay.Text);
                sw.Flush();
            }
        }

        /// LoggerForm Prints
        private void document_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawString(buildLogger_textBox_logDisplay.Text, new Font("Arial", 20, FontStyle.Regular), Brushes.Black, 20, 20);
        }

        private void buildLogger_menuStrip_button_printLog_Click(object sender, EventArgs e)
        {
            dialog.Document = document;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                document.Print();
            }
        }
    }
}
