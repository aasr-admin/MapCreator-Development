using System;
using System.IO;
using Vortice.D3DCompiler;

namespace PlanetViewer
{
    public sealed class ShaderFactory : IShaderFactory
    {
        public Span<byte> CompileBytecodeFromFile(
            string filePath,
            string entryPoint,
            string profile)
        {
            var shaderSource = File.ReadAllText(filePath);
            return CompileBytecodeFromSource(shaderSource, entryPoint, profile);
        }

        public Span<byte> CompileBytecodeFromSource(
            string shaderSource,
            string entryPoint,
            string profile)
        {
            var result = Compiler.Compile(shaderSource, entryPoint, "Test", profile, out var blob, out var errorBlob);
            if (result.Failure)
            {
                throw new InvalidOperationException("Unable to compile shader\n\n" + errorBlob.AsString());
            }

            return blob.AsBytes();
        }
    }
}
