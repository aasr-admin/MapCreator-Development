using Microsoft.VisualBasic;

using System.Drawing.Printing;

namespace Compiler
{
	public partial class BuildLogger : Form
	{
		private DateTime _taskStart;
		private DateTime _taskEnd;

		private readonly PrintDocument _document = new();
		private readonly PrintDialog _dialog = new();

		public BuildLogger()
		{
			Load += BuildLogger_Load;

			_document.PrintPage += Document_PrintPage;

			InitializeComponent();
		}

		private void BuildLogger_Load(object sender, EventArgs e)
		{
			var logMessageTarget = buildLogger_textBox_logDisplay;

			logMessageTarget.Text = String.Concat("MapCreator: Logger                      |  \r", DateTime.UtcNow.ToString("dd.MM.yyyy - hh:mm:ss tt\r\n"));
			logMessageTarget.Text = String.Concat(logMessageTarget.Text, "========================================\r\n");

			#region This Lists All Assembies Used By MapCreator On The Log

			var assemblies = AppDomain.CurrentDomain.GetAssemblies();

			for (var i = 0; i < assemblies.Length; i++)
			{
				var assembly = assemblies[i];

				if (assembly.EntryPoint != null)
				{
					var name = assembly.GetName();

					logMessageTarget = buildLogger_textBox_logDisplay;
					logMessageTarget.Text = String.Concat(logMessageTarget.Text, String.Format("{0} version:{1}\r\n", name.Name, name.Version.ToString()));

					var referencedAssemblies = assembly.GetReferencedAssemblies();

					for (var j = 0; j < referencedAssemblies.Length; j++)
					{
						var assemblyName = referencedAssemblies[j];

						logMessageTarget = buildLogger_textBox_logDisplay;
						logMessageTarget.Text = String.Concat(logMessageTarget.Text, String.Format("{0} version:{1}\r\n", assemblyName.Name, assemblyName.Version.ToString()));
					}
				}
			}

			logMessageTarget = buildLogger_textBox_logDisplay;
			logMessageTarget.Text = String.Concat(logMessageTarget.Text, "\r\n");

			#endregion
		}

		/// LoggerForm Output
		public void StartTask()
		{
			_taskStart = DateTime.UtcNow;
		}

		public void LogMessage(string Message)
		{
			var textLog = buildLogger_textBox_logDisplay;
			textLog.Text = String.Concat(textLog.Text, Message, "\r\n");
			Refresh();
		}

		public void LogTimeStamp()
		{
			var textLog = buildLogger_textBox_logDisplay;
			textLog.Text = String.Concat(textLog.Text, String.Format("  Task:{0:dd/MMM/yyyy hh:mm:ss}", _taskStart));
			textLog = buildLogger_textBox_logDisplay;
			textLog.Text = String.Concat(textLog.Text, " === > ");
			textLog = buildLogger_textBox_logDisplay;
			textLog.Text = String.Concat(textLog.Text, String.Format("{0:hh:mm:ss}", _taskEnd));
			textLog = buildLogger_textBox_logDisplay;
			textLog.Text = String.Concat(textLog.Text, String.Format("  Total:{0} seconds\r\n", DateAndTime.DateDiff(DateInterval.Second, _taskStart, _taskEnd, FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1)));
			Refresh();
		}

		public void EndTask()
		{
			_taskEnd = DateTime.UtcNow;
		}

		/// LoggerForm Saves
		private void BuildLogger_menuStrip_button_saveLog_Click(object sender, EventArgs e)
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
		private void Document_PrintPage(object sender, PrintPageEventArgs e)
		{
			e.Graphics.DrawString(buildLogger_textBox_logDisplay.Text, new Font("Arial", 20, FontStyle.Regular), Brushes.Black, 20, 20);
		}

		private void BuildLogger_menuStrip_button_printLog_Click(object sender, EventArgs e)
		{
			_dialog.Document = _document;

			if (_dialog.ShowDialog() == DialogResult.OK)
			{
				_document.Print();
			}
		}
	}
}
