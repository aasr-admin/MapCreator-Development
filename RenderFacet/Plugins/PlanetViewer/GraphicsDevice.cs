using System;
using System.IO;
using System.Numerics;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using Vortice.Direct3D;
using Vortice.Direct3D11;
using Vortice.DXGI;
using Vortice.Mathematics;
using Image = SixLabors.ImageSharp.Image;

namespace PlanetViewer
{
    public sealed unsafe class GraphicsDevice : IGraphicsDevice
    {
        private static readonly FeatureLevel[] _featureLevels;
        private readonly IShaderFactory _shaderFactory;
        private readonly IModelFactory _modelFactory;
        private readonly IImagingFactory _imagingFactory;

        private IntPtr _windowHandle;
        private IDXGIFactory2? _factory;
        private ID3D11Device1? _device;
        private ID3D11DeviceContext1? _deviceContext;

        private ID3D11RasterizerState? _wireFrameRasterizerState;
        private ID3D11RasterizerState? _solidRasterizerState;
        private ID3D11RasterizerState? _fullscreenRasterizerState;
        private ID3D11Buffer[]? _constantBuffers;
        private ID3D11Buffer? _backgroundSettingsBuffer;
        private GpuBackgroundSettings _backgroundSettings;
        private ID3D11InputLayout? _inputLayout;
        private ID3D11Buffer? _vertexBufferWithNormalUvs;
        private ID3D11Buffer? _vertexBufferWithFlippedUvs;
        private ID3D11Buffer? _indexBuffer;
        private ID3D11VertexShader? _fullscreenVertexShader;
        private ID3D11VertexShader? _defaultVertexShader;
        private ID3D11PixelShader? _defaultPixelShader;
        private ID3D11PixelShader? _fullscreenPixelShader;
        private Viewport _viewport;
        private Int2 _scissorRectDimensions;
        private int _vertexCount;
        private int _indexCount;
        private ID3D11Texture2D? _texture;
        private ID3D11Texture2D? _backgroundTexture;
        private ID3D11ShaderResourceView? _backgroundTextureSrv;
        private ID3D11ShaderResourceView? _textureSrv;
        private ID3D11SamplerState? _textureLinearSamplerState;

        private Matrix4x4 _projectionMatrix;
        private Matrix4x4 _viewMatrix;
        private Matrix4x4 _worldMatrix;

        private readonly Color4 _clearColor;
        private IDXGISwapChain1? _swapchain;
        private ID3D11Texture2D? _backBuffer;
        private ID3D11RenderTargetView? _backBufferRtv;
        private ID3D11Texture2D? _depthStencilBuffer;
        private ID3D11DepthStencilView? _depthStencilView;

        private ID3D11DepthStencilState _fullscreenDepthStencilState;

        private bool _isSolid;
        private Camera? _camera;

        private int _xAxisRotation;
        private int _zAxisRotation;

        static GraphicsDevice()
        {
            _featureLevels = new[]
            {
                FeatureLevel.Level_11_0
            };
        }

        public GraphicsDevice(
            IShaderFactory shaderFactory,
            IModelFactory modelFactory,
            IImagingFactory imagingFactory)
        {
            _shaderFactory = shaderFactory;
            _modelFactory = modelFactory;
            _imagingFactory = imagingFactory;
            _clearColor = new Color4(0.05f, 0.05f, 0.05f, 1.0f);
            _backgroundSettings = new GpuBackgroundSettings();
        }

        public string CaptureWindow()
        {
            _deviceContext?.Flush();
            using var staging = CaptureTexture(_backBuffer)!;

            var filePath = Path.ChangeExtension(Path.GetTempFileName(), ".png");
            _imagingFactory.ExportTextureToFile(_deviceContext, staging, filePath);
            return filePath;
        }

        public void Dispose()
        {
            DestroySwapchain();

            _backgroundTexture?.Dispose();
            _backgroundTextureSrv?.Dispose();
            _fullscreenRasterizerState?.Dispose();
            _fullscreenVertexShader?.Dispose();
            _fullscreenPixelShader?.Dispose();
            _fullscreenDepthStencilState?.Dispose();
            _defaultVertexShader?.Dispose();
            _defaultPixelShader?.Dispose();
            _inputLayout?.Dispose();
            _vertexBufferWithNormalUvs?.Dispose();
            _vertexBufferWithFlippedUvs?.Dispose();

            if (_constantBuffers != null)
            {
                foreach (var constantBuffer in _constantBuffers)
                {
                    constantBuffer?.Dispose();
                }
            }
            _backgroundSettingsBuffer?.Dispose();

            _textureLinearSamplerState?.Dispose();
            _textureSrv?.Dispose();
            _texture?.Dispose();
            _wireFrameRasterizerState?.Dispose();
            _solidRasterizerState?.Dispose();

            _deviceContext?.Dispose();
            _device?.Dispose();
            _factory?.Dispose();

            _imagingFactory?.Dispose();
        }

        public bool Initialize(IntPtr windowHandle, int width, int height, Camera? camera)
        {
            _windowHandle = windowHandle;
            _camera = camera;
            _factory = DXGI.CreateDXGIFactory1<IDXGIFactory2>();

            using var adapter = GetHardwareAdapter();
            var deviceCreationFlags = DeviceCreationFlags.BgraSupport | DeviceCreationFlags.Debug;
            var result = D3D11.D3D11CreateDevice(
                adapter,
                DriverType.Unknown,
                deviceCreationFlags,
                _featureLevels,
                out var tempDevice,
                out _,
                out var tempDeviceContext);
            if (result.Failure)
            {
                D3D11.D3D11CreateDevice(
                    IntPtr.Zero,
                    DriverType.Warp,
                    deviceCreationFlags,
                    _featureLevels,
                    out tempDevice,
                    out _,
                    out tempDeviceContext);
            }

            _device = tempDevice.QueryInterface<ID3D11Device1>();
            _deviceContext = tempDeviceContext.QueryInterface<ID3D11DeviceContext1>();

            tempDeviceContext.Dispose();
            tempDevice.Dispose();

            _constantBuffers = new ID3D11Buffer[3];
            var initialData = new[] { Matrix4x4.Identity }.AsSpan();
            _constantBuffers[0] = _device.CreateBuffer(BindFlags.ConstantBuffer, initialData, initialData.Length * sizeof(Matrix4x4));
            _constantBuffers[1] = _device.CreateBuffer(BindFlags.ConstantBuffer, initialData, initialData.Length * sizeof(Matrix4x4));
            _constantBuffers[2] = _device.CreateBuffer(BindFlags.ConstantBuffer, initialData, initialData.Length * sizeof(Matrix4x4));

            _backgroundSettings.BackgroundMode = new Int4(1, 0, 0, 0);
            _backgroundSettings.BackgroundColorTop = new Vector4(Color3.Orange.ToVector3(), 1.0f);
            _backgroundSettings.BackgroundColorBottom = new Vector4(Color3.Teal.ToVector3(), 1.0f);
            var backgroundSettingsBufferInitialData = new[] { _backgroundSettings }.AsSpan();
            _backgroundSettingsBuffer = _device.CreateBuffer(
                BindFlags.ConstantBuffer,
                backgroundSettingsBufferInitialData,
                backgroundSettingsBufferInitialData.Length * sizeof(GpuBackgroundSettings));

            CreateSwapchain(_windowHandle, width, height);

            var vertexShaderBytes = _shaderFactory.CompileBytecodeFromFile("MapCompiler\\Plugins\\PlanetViewer\\Shader.hlsl", "VSMain", "vs_5_0");
            var pixelShaderBytes = _shaderFactory.CompileBytecodeFromFile("MapCompiler\\Plugins\\PlanetViewer\\Shader.hlsl", "PSMain", "ps_5_0");

            _defaultVertexShader = _device.CreateVertexShader(vertexShaderBytes);
            _defaultPixelShader = _device.CreatePixelShader(pixelShaderBytes);

            var inputLayoutDescriptor = new[]
            {
                new InputElementDescription("POSITION", 0, Format.R32G32B32_Float, 0, 0),
                new InputElementDescription("COLOR", 0, Format.R32G32B32A32_Float, 12, 0),
                new InputElementDescription("UV", 0, Format.R32G32_Float, 28, 0)
            };
            _inputLayout = _device.CreateInputLayout(inputLayoutDescriptor, vertexShaderBytes);

            vertexShaderBytes = _shaderFactory.CompileBytecodeFromFile("MapCompiler\\Plugins\\PlanetViewer\\Shader.hlsl", "VSMainFst", "vs_5_0");
            pixelShaderBytes = _shaderFactory.CompileBytecodeFromFile("MapCompiler\\Plugins\\PlanetViewer\\Shader.hlsl", "PSMainFst", "ps_5_0");
            _fullscreenVertexShader = _device.CreateVertexShader(vertexShaderBytes);
            _fullscreenPixelShader = _device.CreatePixelShader(pixelShaderBytes);

            _modelFactory.CreateModelFromFile(
                "MapCompiler\\Plugins\\PlanetViewer\\SM_Planet.dae",
                false,
                out var vertices,
                out var indices);
            _vertexBufferWithNormalUvs = _device.CreateBuffer(
                BindFlags.VertexBuffer,
                vertices,
                vertices.Length * sizeof(VertexPositionColorUv));

            _modelFactory.CreateModelFromFile(
                "MapCompiler\\Plugins\\PlanetViewer\\SM_Planet.dae",
                true,
                out vertices,
                out indices);
            _vertexBufferWithFlippedUvs = _device.CreateBuffer(
                BindFlags.VertexBuffer,
                vertices,
                vertices.Length * sizeof(VertexPositionColorUv));

            _vertexCount = vertices.Length;

            _indexBuffer = _device.CreateBuffer(BindFlags.IndexBuffer, indices, indices.Length * sizeof(int));
            _indexCount = indices.Length;

            _viewMatrix = Matrix4x4.CreateLookAt(new Vector3(0, 0, 10), Vector3.Zero, Vector3.UnitY);
            _deviceContext.UpdateSubresource(_viewMatrix, _constantBuffers[1]);

            _wireFrameRasterizerState = _device.CreateRasterizerState(RasterizerDescription.Wireframe);
            _solidRasterizerState = _device.CreateRasterizerState(RasterizerDescription.CullFront);
            _fullscreenRasterizerState = _device.CreateRasterizerState(RasterizerDescription.CullBack);

            _textureLinearSamplerState = _device.CreateSamplerState(SamplerDescription.LinearWrap);

            var fullscreenDepthStencilDescriptor = new DepthStencilDescription(
                true,
                DepthWriteMask.All, ComparisonFunction.Always);
            _fullscreenDepthStencilState = _device.CreateDepthStencilState(fullscreenDepthStencilDescriptor);

            SetTextureFromFile("MapCompiler\\Plugins\\PlanetViewer\\T_Planet_D.jpg", FlipMode.None);
            SetBackgroundTextureFromFile("MapCompiler\\Plugins\\PlanetViewer\\T_Background.png", FlipMode.None);

            return true;
        }

        private float _counter;

        public void SetSolid(bool isSolid)
        {
            _isSolid = isSolid;
        }

        public void SetGlobeRotation(
            int xAxisRotation,
            int zAxisRotation)
        {
            _xAxisRotation = xAxisRotation;
            _zAxisRotation = zAxisRotation;
        }

        public void Zoom(float zoom)
        {
            //_viewMatrix = Matrix4x4.CreateLookAt(new Vector3(0, 0, 10 - zoom), Vector3.Zero, Vector3.UnitY);
            //_deviceContext.UpdateSubresource(_viewMatrix, _constantBuffers[1]);
        }

        public void SetTextureFromFile(string fileName, FlipMode flipMode)
        {
            if (!File.Exists(fileName))
            {
                return;
            }

            var configuration = Configuration.Default;
            configuration.PreferContiguousImageBuffers = true;

            using var imageStream = File.Open(fileName, FileMode.Open);
            using var rawImage = Image.Load(imageStream);
            if (flipMode != FlipMode.None)
            {
                rawImage.Mutate(i => i.Flip(flipMode));
            }
            using var textureImage = rawImage.CloneAs<Rgba32>(configuration);

            if (textureImage.DangerousTryGetSinglePixelMemory(out var pixels))
            {
                _texture?.Dispose();

                var subResource = new SubresourceData(new IntPtr(pixels.Pin().Pointer), 4 * rawImage.Width, 0);

                var textureDescriptor = new Texture2DDescription(
                    rawImage.Width,
                    rawImage.Height,
                    Format.R8G8B8A8_UNorm,
                    1,
                    1);
                _texture = _device!.CreateTexture2D(
                    textureDescriptor,
                    new[] { subResource });

                _textureSrv?.Dispose();
                _textureSrv = _device.CreateShaderResourceView(_texture);
            }
        }

        public void SetBackgroundColor(Vector4 color)
        {
            _backgroundSettings.BackgroundMode = new Int4(0, 0, 0, 0);
            _backgroundSettings.BackgroundColorTop = color;
            _deviceContext!.UpdateSubresource(_backgroundSettings, _backgroundSettingsBuffer!);
        }

        public void SetBackgroundGradientColor(Vector4 topColor, Vector4 bottomColor)
        {
            _backgroundSettings.BackgroundMode = new Int4(1, 0, 0, 0);
            _backgroundSettings.BackgroundColorTop = topColor;
            _backgroundSettings.BackgroundColorBottom = bottomColor;
            _deviceContext!.UpdateSubresource(_backgroundSettings, _backgroundSettingsBuffer!);
        }

        public void SetBackgroundTextureFromFile(string fileName, FlipMode flipMode)
        {
            if (!File.Exists(fileName))
            {
                return;
            }

            var configuration = Configuration.Default;
            configuration.PreferContiguousImageBuffers = true;

            using var imageStream = File.Open(fileName, FileMode.Open);
            using var rawImage = Image.Load(imageStream);
            if (flipMode != FlipMode.None)
            {
                rawImage.Mutate(i => i.Flip(flipMode));
            }
            using var textureImage = rawImage.CloneAs<Rgba32>(configuration);

            if (textureImage.DangerousTryGetSinglePixelMemory(out var pixels))
            {
                _backgroundTexture?.Dispose();

                var subResource = new SubresourceData(new IntPtr(pixels.Pin().Pointer), 4 * rawImage.Width, 0);

                var textureDescriptor = new Texture2DDescription(
                    rawImage.Width,
                    rawImage.Height,
                    Format.R8G8B8A8_UNorm,
                    1,
                    1);
                _backgroundTexture = _device!.CreateTexture2D(
                    textureDescriptor,
                    new[] { subResource });

                _backgroundTextureSrv?.Dispose();
                _backgroundTextureSrv = _device.CreateShaderResourceView(_backgroundTexture);
            }

            _backgroundSettings.BackgroundMode = new Int4(2, 0, 0, 0);
            _deviceContext!.UpdateSubresource(_backgroundSettings, _backgroundSettingsBuffer!);
        }

        public void Draw(float speed, bool flipUvs)
        {
            _counter += speed;
            _worldMatrix = Matrix4x4.Identity *
                Matrix4x4.CreateRotationZ(MathHelper.ToRadians(_zAxisRotation)) *
                Matrix4x4.CreateRotationX(MathHelper.ToRadians(_xAxisRotation)) *
                Matrix4x4.CreateRotationY(MathHelper.ToRadians(_counter), Vector3.UnitY);
            _viewMatrix = _camera!.GetViewMatrix();
            _deviceContext!.UpdateSubresource(_viewMatrix, _constantBuffers![1]);
            _deviceContext.UpdateSubresource(_worldMatrix, _constantBuffers[2]);

            /////

            _deviceContext.ClearRenderTargetView(_backBufferRtv, _clearColor);
            _deviceContext.ClearDepthStencilView(_depthStencilView, DepthStencilClearFlags.Depth, 1.0f, 0);

            _deviceContext.IASetPrimitiveTopology(PrimitiveTopology.TriangleList);

            // draw background

            _deviceContext.VSSetShader(_fullscreenVertexShader);
            _deviceContext.PSSetConstantBuffer(0, _backgroundSettingsBuffer);
            _deviceContext.PSSetShaderResource(0, _backgroundTextureSrv!);
            _deviceContext.PSSetShader(_fullscreenPixelShader);

            _deviceContext.RSSetViewport(_viewport);
            _deviceContext.RSSetScissorRect(_scissorRectDimensions.X, _scissorRectDimensions.Y);
            _deviceContext.RSSetState(_fullscreenRasterizerState);

            _deviceContext.OMSetDepthStencilState(_fullscreenDepthStencilState);
            _deviceContext.OMSetRenderTargets(_backBufferRtv!, _depthStencilView);
            _deviceContext.Draw(3, 0);

            // draw scene

            _deviceContext.IASetInputLayout(_inputLayout);
            _deviceContext.IASetVertexBuffer(
                0,
                flipUvs
                    ? _vertexBufferWithFlippedUvs!
                    : _vertexBufferWithNormalUvs!,
                sizeof(VertexPositionColorUv), 0);
            _deviceContext.IASetIndexBuffer(_indexBuffer, Format.R32_UInt, 0);

            _deviceContext.VSSetConstantBuffer(0, _constantBuffers[0]);
            _deviceContext.VSSetConstantBuffer(1, _constantBuffers[1]);
            _deviceContext.VSSetConstantBuffer(2, _constantBuffers[2]);
            _deviceContext.VSSetShader(_defaultVertexShader);

            _deviceContext.PSSetSampler(0, _textureLinearSamplerState);
            _deviceContext.PSSetShaderResource(0, _textureSrv!);
            _deviceContext.PSSetShader(_defaultPixelShader);

            _deviceContext.RSSetViewport(_viewport);
            _deviceContext.RSSetScissorRect(_scissorRectDimensions.X, _scissorRectDimensions.Y);
            _deviceContext.RSSetState(_isSolid ? _solidRasterizerState : _wireFrameRasterizerState);

            _deviceContext.OMSetRenderTargets(_backBufferRtv!, _depthStencilView);

            _deviceContext.DrawIndexed(_indexCount, 0, 0);

            _swapchain!.Present(1, PresentFlags.None);
        }

        public void Resize(int width, int height)
        {
            if (width > 10 && height > 10)
            {
                DestroySwapchain();
                CreateSwapchain(_windowHandle, width, height);
            }
        }

#if NET6
        private ID3D11Texture2D? CaptureTexture(ID3D11Texture2D source)
        {
            ID3D11Texture2D? stagingTexture;
#else
        private ID3D11Texture2D? CaptureTexture(ID3D11Texture2D? source)
        {
            ID3D11Texture2D? stagingTexture;
#endif
            var sourceTextureDescriptor = source!.Description;

            if (sourceTextureDescriptor.ArraySize > 1 || sourceTextureDescriptor.MipLevels > 1)
            {
                return null;
            }

            if (sourceTextureDescriptor.SampleDescription.Count > 1)
            {
                // MSAA content must be resolved before being copied to a staging texture
                sourceTextureDescriptor.SampleDescription.Count = 1;
                sourceTextureDescriptor.SampleDescription.Quality = 0;

                var temp = _device!.CreateTexture2D(sourceTextureDescriptor);
                var format = sourceTextureDescriptor.Format;
                var formatSupport = _device.CheckFormatSupport(format);

                if ((formatSupport & FormatSupport.MultisampleResolve) == FormatSupport.None)
                {
                    return null;
                }

                for (var item = 0; item < sourceTextureDescriptor.ArraySize; ++item)
                {
                    for (var level = 0; level < sourceTextureDescriptor.MipLevels; ++level)
                    {
                        var index = ID3D11Resource.CalculateSubResourceIndex(
                            level,
                            item,
                            sourceTextureDescriptor.MipLevels);
                        _deviceContext!.ResolveSubresource(temp, index, source, index, format);
                    }
                }

                sourceTextureDescriptor.BindFlags = BindFlags.None;
                sourceTextureDescriptor.OptionFlags &= ResourceOptionFlags.TextureCube;
                sourceTextureDescriptor.CpuAccessFlags = CpuAccessFlags.Read;
                sourceTextureDescriptor.Usage = ResourceUsage.Staging;

                stagingTexture = _device.CreateTexture2D(sourceTextureDescriptor);

                _deviceContext!.CopyResource(stagingTexture, temp);
            }
            else if (sourceTextureDescriptor.Usage == ResourceUsage.Staging &&
                     (sourceTextureDescriptor.CpuAccessFlags & CpuAccessFlags.Read) != CpuAccessFlags.None)
            {
                // Handle case where the source is already a staging texture we can use directly
                stagingTexture = source;
            }
            else
            {
                // Otherwise, create a staging texture from the non-MSAA source
                sourceTextureDescriptor.BindFlags = 0;
                sourceTextureDescriptor.OptionFlags &= ResourceOptionFlags.TextureCube;
                sourceTextureDescriptor.CpuAccessFlags = CpuAccessFlags.Read;
                sourceTextureDescriptor.Usage = ResourceUsage.Staging;

                stagingTexture = _device!.CreateTexture2D(sourceTextureDescriptor);

                _deviceContext!.CopyResource(stagingTexture, source);
            }

            return stagingTexture;
        }

        private void CreateSwapchain(IntPtr windowHandle, int width, int height)
        {
            var swapChainDescriptor = new SwapChainDescription1
            {
                Width = width,
                Height = height,
                Format = Format.R8G8B8A8_UNorm,
                BufferCount = 2,
                BufferUsage = Usage.RenderTargetOutput,
                SampleDescription = new SampleDescription
                {
                    Count = 1,
                    Quality = 0
                },
                Scaling = Scaling.Stretch,
                SwapEffect = SwapEffect.FlipDiscard,
                AlphaMode = AlphaMode.Ignore
            };
            var swapChainFullscreenDescriptor = new SwapChainFullscreenDescription
            {
                Windowed = true,
                Scaling = ModeScaling.Stretched,
                ScanlineOrdering = ModeScanlineOrder.Unspecified
            };

            _swapchain = _factory!.CreateSwapChainForHwnd(
                _device,
                windowHandle,
                swapChainDescriptor,
                swapChainFullscreenDescriptor);

            _factory.MakeWindowAssociation(windowHandle, WindowAssociationFlags.IgnoreAltEnter);
            _backBuffer = _swapchain.GetBuffer<ID3D11Texture2D>(0);
            _backBufferRtv = _device!.CreateRenderTargetView(_backBuffer);

            var depthStencilTextureDescriptor = new Texture2DDescription
            {
                ArraySize = 1,
                BindFlags = BindFlags.DepthStencil,
                Width = width,
                Height = height,
                Format = Format.D24_UNorm_S8_UInt,
                MipLevels = 1,
                Usage = ResourceUsage.Default,
                CpuAccessFlags = CpuAccessFlags.None,
                SampleDescription = new SampleDescription(1, 0)
            };
            _depthStencilBuffer = _device.CreateTexture2D(depthStencilTextureDescriptor);
            _depthStencilView = _device.CreateDepthStencilView(
                _depthStencilBuffer!,
                new DepthStencilViewDescription(
                    _depthStencilBuffer,
                    DepthStencilViewDimension.Texture2D));

            _viewport = new Viewport(width, height);
            _scissorRectDimensions = new Int2(width, height);

            _projectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(60.0f),
                width / (float)height,
                0.1f,
                512.0f);
            _deviceContext!.UpdateSubresource(_projectionMatrix, _constantBuffers![0]);
        }

        private void DestroySwapchain()
        {
            _depthStencilView?.Dispose();
            _depthStencilBuffer?.Dispose();
            _backBufferRtv?.Dispose();
            _backBuffer?.Dispose();
            _swapchain?.Dispose();
        }

        private IDXGIAdapter GetHardwareAdapter()
        {
            var factory6 = _factory!.QueryInterface<IDXGIFactory6>();
            if (factory6 != null)
            {
                for (var adapterIndex = 0;
                     factory6.EnumAdapterByGpuPreference(
                         adapterIndex,
                         GpuPreference.HighPerformance,
#if NET6
                         out IDXGIAdapter1? adapter).Success;
#else
                         out IDXGIAdapter1? adapter).Success;
#endif
                     adapterIndex++)
                {
                    if (adapter == null)
                    {
                        continue;
                    }

                    var adapterDescription = adapter.Description1;
                    if ((adapterDescription.Flags & AdapterFlags.Software) != AdapterFlags.None)
                    {
                        adapter.Dispose();
                        continue;
                    }

                    factory6.Dispose();
                    return adapter;
                }

                factory6.Dispose();
            }

            for (var adapterIndex = 0;
#if NET6
                 _factory.EnumAdapters1(adapterIndex, out IDXGIAdapter1? adapter).Success;
#else
                 _factory.EnumAdapters1(adapterIndex, out var adapter).Success;
#endif
                 adapterIndex++)
            {
                var adapterDescription = adapter.Description1;
                if ((adapterDescription.Flags & AdapterFlags.Software) != AdapterFlags.None)
                {
                    adapter.Dispose();
                    continue;
                }

                return adapter;
            }

            throw new InvalidOperationException("Unable to find a D3D11 adapter");
        }
    }
}
