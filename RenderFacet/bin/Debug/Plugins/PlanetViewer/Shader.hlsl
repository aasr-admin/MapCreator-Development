struct VSInput
{
    float3 Position : POSITION;
    float4 Color : COLOR;
    float2 Uv : UV;
};

struct PSInput
{
    float4 Position : SV_POSITION;
    float4 Color : COLOR;
    float2 Uv : UV;
};

cbuffer PerApplication
{
    matrix ProjectionMatrix;
};

cbuffer PerFrame
{
    matrix ViewMatrix;
};

cbuffer PerObject
{
    matrix WorldMatrix;
};

cbuffer BackgroundSettings
{
    int4 BackgroundMode;
    float4 BackgroundColorTop;
    float4 BackgroundColorBottom;
    float4 Padding;
}

PSInput VSMain(VSInput input)
{
    PSInput result = (PSInput)0;

    const matrix modelViewProjection = mul(ProjectionMatrix, mul(ViewMatrix, WorldMatrix));

    result.Position = mul(modelViewProjection, float4(input.Position, 1.0f));
    result.Color = input.Color;
    result.Uv = input.Uv;
    return result;
}

float3 BackgroundGradient(float2 uv, float3 topColor, float3 bottomColor)
{
    return lerp(bottomColor, topColor, uv.y);
}

sampler LinearSampler : register(s0);

Texture2D Texture : register(t0);

float4 PSMain(PSInput input) : SV_TARGET
{
    float3 texel = Texture.Sample(LinearSampler, input.Uv).rgb;
    return float4(texel, 1.0f);
}

struct PSInputFst
{
    float4 PositionClipSpace : SV_POSITION;
    float2 Uv : TEXCOORD0;
    float Depth: TEXCOORD1;
};

PSInputFst VSMainFst(uint id: SV_VertexID)
{
    PSInputFst output;

    output.Uv = float2((id << 1) & 2, id & 2);
    output.PositionClipSpace = float4(output.Uv * float2(2, -2) + float2(-1, 1), 0, 1);
    output.Depth = output.PositionClipSpace.z / output.PositionClipSpace.w;

    return output;
}

Texture2D BackgroundTexture: register(t0);

float4 PSMainFst(PSInputFst input) : SV_TARGET
{
    float3 texel;

    if (BackgroundMode.x == 0)
    {
        texel = BackgroundColorTop.rgb;
    }
    else if (BackgroundMode.x == 1)
    {
        texel = BackgroundGradient(input.Uv, BackgroundColorTop.rgb, BackgroundColorBottom.rgb);
    }
    else if (BackgroundMode.x == 2)
    {
        texel = BackgroundTexture.Sample(LinearSampler, input.Uv).rgb;
    }
    else if (BackgroundMode.x == 3)
    {
        texel = lerp(
            BackgroundGradient(input.Uv, BackgroundColorTop.rgb, BackgroundColorBottom.rgb),
            BackgroundTexture.Sample(LinearSampler, input.Uv).rgb,
            0.5f);
    }

    return float4(texel, 1.0f);
}