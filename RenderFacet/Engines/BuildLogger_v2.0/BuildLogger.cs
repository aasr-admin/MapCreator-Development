using Altitude;

using BuildLogger;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

using Terrain;
using Transition;

using UltimaSDK_v432;

namespace BuildLogger
{
    public partial class buildLogger : Form
    {
        private string m_LogName;
        private DateTime m_Task_Start;
        private DateTime m_Task_End;

        PrintDocument document = new PrintDocument();
        PrintDialog dialog = new PrintDialog();

        public buildLogger()
        {
            buildLogger buildLogger = this;
            base.Load += new EventHandler(buildLogger.buildLogger_Load);
            document.PrintPage += new PrintPageEventHandler(document_PrintPage);

            InitializeComponent();
        }

        private void buildLogger_Load(object sender, EventArgs e)
        {
            TextBox logMessageTarget = this.buildLogger_textBox_logOutput;

            logMessageTarget.Text = string.Concat("Ultima Online™ Map Creator Log  |  \r", DateTime.Now.ToString("dd/MM/yyyy, hh:mm:ss tt\r\n"));
            logMessageTarget.Text = string.Concat(logMessageTarget.Text, "=================================================\r\n");

            #region This Lists All Assembies Used By MapCreator On The Log

            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            for (int i = 0; i < (int)assemblies.Length; i++)
            {
                Assembly assembly = assemblies[i];

                if (assembly.EntryPoint != null)
                {
                    this.m_LogName = assembly.EntryPoint.DeclaringType.Name;
                    AssemblyName name = assembly.GetName();

                    logMessageTarget = this.buildLogger_textBox_logOutput;
                    logMessageTarget.Text = string.Concat(logMessageTarget.Text, string.Format("{0} version:{1}\r\n", name.Name, name.Version.ToString()));
                    name = null;

                    AssemblyName[] referencedAssemblies = assembly.GetReferencedAssemblies();

                    for (int j = 0; j < (int)referencedAssemblies.Length; j++)
                    {
                        AssemblyName assemblyName = referencedAssemblies[j];

                        logMessageTarget = this.buildLogger_textBox_logOutput;
                        logMessageTarget.Text = string.Concat(logMessageTarget.Text, string.Format("{0} version:{1}\r\n", assemblyName.Name, assemblyName.Version.ToString()));
                        assemblyName = null;
                    }
                }
            }

            logMessageTarget = this.buildLogger_textBox_logOutput;
            logMessageTarget.Text = string.Concat(logMessageTarget.Text, "\r\n");

            #endregion
        }

        /// LoggerForm Output
        public void StartTask()
        {
            this.m_Task_Start = DateTime.UtcNow;
        }

        public void LogMessage(string Message)
        {
            TextBox textLog = this.buildLogger_textBox_logOutput;
            textLog.Text = string.Concat(textLog.Text, Message, "\r\n");
            this.Refresh();
        }

        public void LogTimeStamp()
        {
            TextBox textLog = this.buildLogger_textBox_logOutput;
            textLog.Text = string.Concat(textLog.Text, string.Format("  Task:{0:dd/MMM/yyyy hh:mm:ss}", this.m_Task_Start));
            textLog = this.buildLogger_textBox_logOutput;
            textLog.Text = string.Concat(textLog.Text, " === > ");
            textLog = this.buildLogger_textBox_logOutput;
            textLog.Text = string.Concat(textLog.Text, string.Format("{0:hh:mm:ss}", this.m_Task_End));
            textLog = this.buildLogger_textBox_logOutput;
            textLog.Text = string.Concat(textLog.Text, string.Format("  Total:{0} seconds\r\n", DateAndTime.DateDiff(DateInterval.Second, this.m_Task_Start, this.m_Task_End, FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1)));
            this.Refresh();
        }

        public void EndTask()
        {
            this.m_Task_End = DateTime.UtcNow;
        }

        /// LoggerForm Saves
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog Log = new SaveFileDialog();

            Log.FileName = "DefaultLog.txt";
            Log.Filter = "ProgressLog | (*.txt)";

            if (Log.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(Log.FileName, true))
                {
                    sw.Write(this.buildLogger_textBox_logOutput.Text);
                    sw.Flush();
                }
            }
        }

        /// LoggerForm Prints
        void document_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawString(buildLogger_textBox_logOutput.Text, new Font("Arial", 20, FontStyle.Regular), Brushes.Black, 20, 20);
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dialog.Document = document;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                document.Print();
            }
        }
    }
}