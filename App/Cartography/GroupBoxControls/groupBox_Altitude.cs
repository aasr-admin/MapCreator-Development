using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace MapCreator
{
	public static class EncodeAltitudeBitmapHelper
	{
		public static void MakeAltitudeImage(string projectPath, string terrainFile, string altitudeFile, ClsAltitudeTable altitude, ClsTerrainTable terrain, IProgress<int> progress, IProgress<string> logger)
		{
			Bitmap loadedImage = null;
			Bitmap memoryImage = null;

			var terrainPath = Utility.FindDataFile(projectPath, terrainFile);

			try
			{
				try
				{
					logger.Report("Load Terrain Image Map.\r\n");
					logger.Report(terrainPath);

					loadedImage = new Bitmap(terrainPath);
					memoryImage = new Bitmap(loadedImage.Width, loadedImage.Height, PixelFormat.Format8bppIndexed);
				}
				catch (Exception exception)
				{
					logger.Report("Error in loading image maps.\r\n");
					logger.Report(exception.Message);

					return;
				}

				memoryImage.Palette = altitude.GetAltPalette();

				var loadedBound = new Rectangle(0, 0, loadedImage.Width, loadedImage.Height);

				var loadedBD = loadedImage.LockBits(loadedBound, ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);

				var loadedScan0 = loadedBD.Scan0;
				var loadedArea = loadedBD.Width * loadedBD.Height;

				var loadedData = new byte[loadedArea];

				Marshal.Copy(loadedScan0, loadedData, 0, loadedArea);

				var memoryBound = new Rectangle(0, 0, memoryImage.Width, memoryImage.Height);

				var memoryBD = memoryImage.LockBits(memoryBound, ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);

				var memoryScan0 = memoryBD.Scan0;
				var memoryArea = memoryBD.Width * memoryBD.Height;

				var memoryData = new byte[memoryArea];

				Marshal.Copy(memoryScan0, memoryData, 0, memoryArea);

				logger.Report("Creating Altitude Image Map.");

				for (var i = 0; i < loadedArea; i++)
				{
					if (i % 1000 == 0)
					{
						progress.Report(i * 100 / memoryArea);
					}

					memoryData[i] = terrain[loadedData[i]].AltID;
				}

				Marshal.Copy(memoryData, 0, memoryScan0, memoryArea);

				memoryImage.UnlockBits(memoryBD);

				var altitudePath = Path.Combine(Path.GetDirectoryName(terrainPath), altitudeFile);

				try
				{
					logger.Report("Saving Altitude Image Map.\r\n");
					logger.Report(altitudePath);

					memoryImage.Save(altitudePath, ImageFormat.Bmp);
				}
				catch (Exception exception)
				{
					logger.Report("Error in saving image.\r\n");
					logger.Report(exception.Message);

					return;
				}
			}
			finally
			{
				memoryImage?.Dispose();
				loadedImage?.Dispose();

				logger.Report("Done.");
			}
		}
	}
}