using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Vortice.Direct3D11;
using Image = SixLabors.ImageSharp.Image;

namespace PlanetViewer
{
    public sealed class SixLaborsImagingFactory : IImagingFactory
    {
        public void Dispose()
        {
        }

        public void ExportTextureToFile(
            ID3D11DeviceContext? deviceContext,
            ID3D11Texture2D stagingTexture,
            string filePath)
        {
            var configuration = Configuration.Default;
            //configuration.PreferContiguousImageBuffers = true;
            //configuration.ReadOrigin = ReadOrigin.Begin;

            var pixels = deviceContext!.MapReadOnly<Rgba32>(stagingTexture, 0, 0);
            using var intermediateImage = Image.LoadPixelData<Rgba32>(
                configuration,
                pixels,
                stagingTexture.Description.Width,
                stagingTexture.Description.Height);

            using var outputImage = intermediateImage.CloneAs<Rgb24>();
            intermediateImage.SaveAsPng(@"T:\Temp\YYY.png");
            outputImage.SaveAsPng(@"T:\Temp\YY.png");

            deviceContext.Unmap(stagingTexture, 0, 0);
        }
    }
}