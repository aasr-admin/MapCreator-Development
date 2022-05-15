using System;
using System.IO;
using Vortice.Direct3D11;
using Vortice.DXGI;
using Vortice.Mathematics;
using Vortice.WIC;
using Color = Vortice.Mathematics.Color;

namespace PlanetViewer
{
    public sealed class WicImagingFactory : IImagingFactory
    {
        private readonly IWICImagingFactory2 _imagingFactory;

        public WicImagingFactory()
        {
            _imagingFactory = new IWICImagingFactory2();
        }

        public void Dispose()
        {
            _imagingFactory.Dispose();
        }

        public void ExportTextureToFile(
            ID3D11DeviceContext? deviceContext,
            ID3D11Texture2D stagingTexture,
            string filePath)
        {
            var stagingTextureDescriptor = stagingTexture!.Description;

            var sourcePixelFormatGuid = GetPixelFormatGuid(stagingTextureDescriptor.Format, out var sRgb);
            var targetPixelFormatGuid = GetPixelFormatWithoutAlpha(stagingTextureDescriptor.Format);

            using Stream stream = File.OpenWrite(filePath);
            using var wicStream = _imagingFactory.CreateStream(stream);
            using var encoder = _imagingFactory.CreateEncoder(ContainerFormat.Png, wicStream);

            using var frame = encoder.CreateNewFrame(out var props);
            frame.Initialize(props);
            frame.SetSize(stagingTextureDescriptor.Width, stagingTextureDescriptor.Height);
            frame.SetResolution(96, 96);
            frame.SetPixelFormat(targetPixelFormatGuid);

            //const bool isNative = true;
            //if (isNative)
            {
                ExportTextureToFileNativeInternal(
                    deviceContext,
                    stagingTexture,
                    frame,
                    sourcePixelFormatGuid,
                    targetPixelFormatGuid);
            }
            /*
            else
            {
                ExportTextureToFileInternal(
                    deviceContext,
                    stagingTexture,
                    frame,
                    sourcePixelFormatGuid,
                    targetPixelFormatGuid);
            }
            */

            frame.Commit();
            encoder.Commit();
        }

        private static Guid GetPixelFormatWithoutAlpha(Format format)
        {
            switch (format)
            {
                case Format.R32G32B32A32_Float:
                case Format.R16G16B16A16_Float:
                {
                    return PixelFormat.Format96bppRGBFloat;
                }
                case Format.R16G16B16A16_UNorm:
                    return PixelFormat.Format48bppBGR;
                case Format.B5G5R5A1_UNorm:
                    return PixelFormat.Format16bppBGR555;
                case Format.B5G6R5_UNorm:
                    return PixelFormat.Format16bppBGR565;
                case Format.R32_Float:
                case Format.R16_Float:
                case Format.R16_UNorm:
                case Format.R8_UNorm:
                case Format.A8_UNorm:
                    return PixelFormat.Format8bppGray;
                default:
                    return PixelFormat.Format24bppBGR;
            }
        }

        private static Guid GetPixelFormatGuid(Format format, out bool sRgb)
        {
            sRgb = false;
            switch (format)
            {
                case Format.R32G32B32A32_Float:
                    return PixelFormat.Format128bppRGBAFloat;
                case Format.R8G8B8A8_UNorm:
                    return PixelFormat.Format32bppRGBA;
                case Format.R8G8B8A8_UNorm_SRgb:
                    sRgb = true;
                    return PixelFormat.Format32bppRGBA;
                case Format.B8G8R8A8_UNorm: // DXGI 1.1
                    return PixelFormat.Format32bppBGRA;
                case Format.B8G8R8A8_UNorm_SRgb: // DXGI 1.1
                    sRgb = true;
                    return PixelFormat.Format32bppBGRA;
                case Format.B8G8R8X8_UNorm: // DXGI 1.1
                    return PixelFormat.Format32bppBGR;
                case Format.B8G8R8X8_UNorm_SRgb: // DXGI 1.1
                    sRgb = true;
                    return PixelFormat.Format32bppBGR;
                default:
                    throw new ArgumentOutOfRangeException(nameof(format));
            }
        }

        private void ExportTextureToFileNativeInternal(
            ID3D11DeviceContext? deviceContext,
            ID3D11Texture2D stagingTexture,
            IWICBitmapFrameEncode frame,
            Guid sourcePixelFormatGuid,
            Guid targetPixelFormatGuid)
        {
            var mappedSubresource = deviceContext!.Map(stagingTexture, 0);
            var imageSizeBytes = mappedSubresource.RowPitch * stagingTexture.Description.Height;
            if (targetPixelFormatGuid != sourcePixelFormatGuid)
            {
                // Conversion required to write
                using var bitmapSource = _imagingFactory.CreateBitmapFromMemory(
                    stagingTexture.Description.Width,
                    stagingTexture.Description.Height,
                    sourcePixelFormatGuid,
                    mappedSubresource.RowPitch,
                    imageSizeBytes,
                    mappedSubresource.DataPointer);

                using var formatConverter = _imagingFactory.CreateFormatConverter();
                if (!formatConverter.CanConvert(sourcePixelFormatGuid, targetPixelFormatGuid))
                {
                    deviceContext.Unmap(stagingTexture, 0);
                    return;
                }

                formatConverter.Initialize(bitmapSource, targetPixelFormatGuid, BitmapDitherType.None, null, 0, BitmapPaletteType.MedianCut);
                frame.WriteSource(formatConverter, new RectI(stagingTexture.Description.Width, stagingTexture.Description.Height));
            }
            else
            {
                // No conversion required
                frame.WritePixels(
                    stagingTexture.Description.Height,
                    mappedSubresource.RowPitch,
                    imageSizeBytes,
                    mappedSubresource.DataPointer);
            }

            deviceContext.Unmap(stagingTexture, 0);
        }

        private void ExportTextureToFileInternal(
            ID3D11DeviceContext deviceContext,
            ID3D11Texture2D stagingTexture,
            IWICBitmapFrameEncode frame,
            Guid sourcePixelFormatGuid,
            Guid targetPixelFormatGuid)
        {
            var stride = PixelFormat.GetStride(sourcePixelFormatGuid, stagingTexture.Description.Width);
            var colors = deviceContext.MapReadOnly<Color>(stagingTexture);

            if (targetPixelFormatGuid != sourcePixelFormatGuid)
            {
                // Conversion required to write
                using var bitmapSource = _imagingFactory.CreateBitmapFromMemory(
                    stagingTexture.Description.Width,
                    stagingTexture.Description.Height,
                    sourcePixelFormatGuid,
                    colors,
                    stride);
                using var formatConverter = _imagingFactory.CreateFormatConverter();
                if (!formatConverter.CanConvert(sourcePixelFormatGuid, targetPixelFormatGuid))
                {
                    deviceContext.Unmap(stagingTexture, 0);
                    return;
                }

                formatConverter.Initialize(
                    bitmapSource,
                    targetPixelFormatGuid,
                    BitmapDitherType.None,
                    null,
                    0,
                    BitmapPaletteType.MedianCut);

                frame.WriteSource(formatConverter,
                    new RectI(
                        stagingTexture.Description.Width,
                        stagingTexture.Description.Height));
            }
            else
            {
                // No conversion required
                frame.WritePixels(stagingTexture.Description.Height, stride, colors);
            }

            deviceContext.Unmap(stagingTexture, 0);
        }
    }
}