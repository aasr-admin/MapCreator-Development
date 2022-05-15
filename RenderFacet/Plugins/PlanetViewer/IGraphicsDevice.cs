using System;
using System.Numerics;
using SixLabors.ImageSharp.Processing;

namespace PlanetViewer
{
    public interface IGraphicsDevice : IDisposable
    {
        bool Initialize(
            IntPtr windowHandle,
            int width,
            int height,
            Camera? camera);

        string CaptureWindow();

        void SetSolid(bool isSolid);

        void Zoom(float zoom);

        void SetTextureFromFile(
            string fileName,
            FlipMode flipMode);

        void SetBackgroundColor(Vector4 color);

        void SetBackgroundGradientColor(
            Vector4 topColor,
            Vector4 bottomColor);

        void SetBackgroundTextureFromFile(
            string fileName,
            FlipMode flipMode);

        void SetGlobeRotation(
            int xAxisRotatin,
            int zAxisRotation);

        void Draw(float speed, bool flipUvs);

        void Resize(int width, int height);
    }
}