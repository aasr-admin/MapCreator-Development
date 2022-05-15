using System;

namespace PlanetViewer
{
    public interface IModelFactory : IDisposable
    {
        void CreateModelFromFile(
            string fileName,
            bool flipUvs,
            out Span<VertexPositionColorUv> vertices,
            out Span<int> indices);
    }
}