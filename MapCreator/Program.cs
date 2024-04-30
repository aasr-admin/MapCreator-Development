using System.IO;

using UltimaSDK;

namespace MapCreator
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                var clientPath = File.ReadAllText("ClientPath.cfg");

                if (Directory.Exists(clientPath))
                {
                    Files.SetMulPath(clientPath);
                }
            }
            catch { }

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new splashScreen());
        }
    }
}