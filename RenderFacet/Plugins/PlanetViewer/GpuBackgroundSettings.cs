using System.Numerics;
using System.Runtime.InteropServices;
using Vortice.Mathematics;

namespace PlanetViewer
{

    [StructLayout(LayoutKind.Sequential)]
    public struct GpuBackgroundSettings
    {
        public Int4 BackgroundMode;
        public Vector4 BackgroundColorTop;
        public Vector4 BackgroundColorBottom;
        public Vector4 Padding;
    }
}