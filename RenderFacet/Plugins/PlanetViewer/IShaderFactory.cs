using System;

namespace PlanetViewer
{
    public interface IShaderFactory
    {
        Span<byte> CompileBytecodeFromFile(
            string filePath,
            string entryPoint,
            string profile);

        Span<byte> CompileBytecodeFromSource(
            string shaderSource,
            string entryPoint,
            string profile);
    }
}