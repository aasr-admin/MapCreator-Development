using System;
using System.Linq;
using System.Numerics;
using Assimp;
using Vortice.Mathematics;

namespace PlanetViewer
{
    public sealed class ModelFactory : IModelFactory
    {
        #if NET6_0
        private readonly AssimpContext _context;

        public ModelFactory()
        {
            _context = new AssimpContext();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
#else
        private readonly AssimpContext _context;

        public ModelFactory()
        {
            _context = new AssimpContext();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
#endif

        public void CreateModelFromFile(
            string fileName,
            bool flipUvs,
            out Span<VertexPositionColorUv> vertices,
            out Span<int> indices)
        {
            const PostProcessSteps postProcessFlags = PostProcessSteps.None |
                                                      PostProcessSteps.Triangulate |
                                                      PostProcessSteps.CalculateTangentSpace |
                                                      PostProcessSteps.FindDegenerates |
                                                      PostProcessSteps.FindInvalidData |
                                                      PostProcessSteps.FlipWindingOrder |
                                                      PostProcessSteps.MakeLeftHanded |
                                                      PostProcessSteps.JoinIdenticalVertices |
                                                      PostProcessSteps.OptimizeMeshes;
            var scene = _context.ImportFile(fileName, postProcessFlags);
            var mesh = scene.Meshes.First();

            indices = mesh.GetIndices();
            vertices = mesh.Vertices
                .Select((v, index) => new VertexPositionColorUv(
                    new Vector3(v.X, v.Y, v.Z),
                    new Color4(Color3.Blue, 1.0f),
                    new Vector2(mesh.TextureCoordinateChannels[0][index].X, flipUvs ? 1.0f - mesh.TextureCoordinateChannels[0][index].Y : mesh.TextureCoordinateChannels[0][index].Y)))
                .ToArray();
        }
    }
}