using Vortice.Direct3D11;

namespace PlanetViewer
{
    public sealed class NullImagingFactory : IImagingFactory
    {
        public void Dispose()
        {
        }

        public void ExportTextureToFile(
            ID3D11DeviceContext? deviceContext,
            ID3D11Texture2D stagingTexture,
            string filePath)
        {
        }
    }
}