using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using Vortice.Direct3D11;

namespace PlanetViewer
{
    public sealed class GdiImagingFactory : IImagingFactory
    {
        public void Dispose()
        {
        }

        public void ExportTextureToFile(
            ID3D11DeviceContext? deviceContext,
            ID3D11Texture2D stagingTexture,
            string filePath)
        {
            var mappedSubresource = deviceContext!.Map(stagingTexture, 0);

            using var bitmap = new Bitmap(
                stagingTexture.Description.Width,
                stagingTexture.Description.Height,
                PixelFormat.Format32bppArgb);
            var boundsRect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

            var bitmapData = bitmap.LockBits(
                boundsRect,
                ImageLockMode.WriteOnly,
                bitmap.PixelFormat);

            var sourcePtr = mappedSubresource.DataPointer;
            var destPtr = bitmapData.Scan0;
            for (var y = 0; y < bitmap.Height; y++)
            {
                CopyMemory(destPtr, sourcePtr, new UIntPtr((uint)bitmap.Width * 4));

                sourcePtr = IntPtr.Add(sourcePtr, mappedSubresource.RowPitch);
                destPtr = IntPtr.Add(destPtr, bitmapData.Stride);
            }

            bitmap.UnlockBits(bitmapData);
            deviceContext.Unmap(stagingTexture, 0);
        }

        [DllImport("msvcrt.dll", EntryPoint = "memcpy", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        private static extern IntPtr CopyMemory(IntPtr dest, IntPtr src, UIntPtr count);
    }
}