using System.Numerics;
using System.Runtime.InteropServices;
using Vortice.Mathematics;

namespace PlanetViewer
{
    [StructLayout(LayoutKind.Sequential)]
    public struct VertexPositionColorUv
    {
        public Vector3 Position;

        public Color4 Color;

        public Vector2 Uv;

        public VertexPositionColorUv(
            Vector3 position,
            Color4 color,
            Vector2 uv)
        {
            Position = position;
            Color = color;
            Uv = uv;
        }
    }
}