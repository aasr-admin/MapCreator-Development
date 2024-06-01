using Assets;

namespace MapCreator
{
	internal static class Program
	{
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main()
		{
			// To customize application configuration such as set high DPI settings or default font,
			// see https://aka.ms/applicationconfiguration.
			ApplicationConfiguration.Initialize();

			var clientPath = String.Empty;

			var cfg = Path.Combine("ClientPath.cfg");

			if (File.Exists(cfg))
			{
				clientPath = File.ReadAllText(cfg);
			}

			FolderBrowserDialog browser = null;

			while (!Directory.Exists(clientPath))
			{
				var result = MessageBox.Show("Ultima Online files could not be found.\r\nClick retry to manually select a path.", "Missing Files", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

				if (result == DialogResult.Retry)
				{
					browser ??= new FolderBrowserDialog()
					{
						Description = "Select an Ultima Online directory",
						ShowNewFolderButton = false,
						AddToRecent = false,
						RootFolder = Environment.SpecialFolder.ProgramFilesX86
					};

					result = browser.ShowDialog();

					if (result == DialogResult.OK)
					{
						clientPath = browser.SelectedPath;
						continue;
					}
				}

				break;
			}

			if (!Directory.Exists(clientPath))
			{
				Application.Exit();
				return;
			}

			File.WriteAllText(cfg, clientPath);

			AssetData.Load(clientPath, "enu", true);

			Application.Run(new SplashScreen());
		}
	}
}