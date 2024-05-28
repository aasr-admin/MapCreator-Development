using System.Drawing.Imaging;
using System.Runtime.InteropServices;

using Microsoft.VisualBasic.CompilerServices;

namespace MapCreator
{
    public static class EncodeAltitudeBitmapHelper
    {
        public static void MakeAltitudeImage(string projectPath, string terrainFile, string altitudeFile, ClsAltitudeTable iAltitude, ClsTerrainTable iTerrain, IProgress<int> Progress, IProgress<string> Logger)
        {
            Bitmap bitmap = null;
            Bitmap bitmap1 = null;
            try
            {
                Logger.Report("Load Terrain Image Map.");
                bitmap1 = new Bitmap(string.Format("{0}\\{1}", projectPath, terrainFile));
                bitmap = new Bitmap(bitmap1.Width, bitmap1.Height, PixelFormat.Format8bppIndexed);
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                var exception = exception1;
                Logger.Report("Error in loading image maps.\r\n");
                Logger.Report(exception.Message);
                ProjectData.ClearProjectError();
            }

            bitmap.Palette = iAltitude.GetAltPalette();
            var width = bitmap1.Width;
            var rectangle = new Rectangle(0, 0, width, bitmap1.Height);
            var bitmapDatum = bitmap1.LockBits(rectangle, ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);
            var scan0 = bitmapDatum.Scan0;
            var num = checked(bitmapDatum.Width * bitmapDatum.Height);
            var numArray = new byte[checked(checked(num - 1) + 1)];
            Marshal.Copy(scan0, numArray, 0, num);
            var bitmapDatum1 = bitmap.LockBits(rectangle, ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);
            var intPtr = bitmapDatum1.Scan0;
            var width1 = checked(bitmapDatum1.Width * bitmapDatum1.Height);
            var numArray1 = new byte[checked(checked(width1 - 1) + 1)];
            Marshal.Copy(intPtr, numArray1, 0, width1);
            Logger.Report("Creating Altitude Image Map.");
            var num1 = checked(num - 1);
            for (var i = 0; i <= num1; i++)
            {
                if (i % 1000 == 0)
                {
                    Progress.Report(i * 100 / width1);
                }

                var altID = (byte)iTerrain.TerrianGroup(numArray[i]).AltID;
                numArray1[i] = altID;
            }

            Marshal.Copy(numArray1, 0, intPtr, width1);
            bitmap.UnlockBits(bitmapDatum1);
            try
            {
                var str = string.Format("{0}\\{1}", projectPath, altitudeFile);
                Logger.Report("Saving Altitude Image Map.\r\n");
                Logger.Report(str);
                bitmap.Save(str, ImageFormat.Bmp);
            }
            catch (Exception exception3)
            {
                ProjectData.SetProjectError(exception3);
                var exception2 = exception3;
                Logger.Report("Error in saving image.\r\n");
                Logger.Report(exception2.Message);
                ProjectData.ClearProjectError();
            }

            bitmap.Dispose();
            bitmap1.Dispose();
            Logger.Report("Done.");
        }
    }
}