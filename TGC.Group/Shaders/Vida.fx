float4x4 matWorld; //Matriz de transformacion World
float4x4 matWorldView; //Matriz World * View
float4x4 matWorldViewProj; //Matriz World * View * Projection
float4x4 matInverseTransposeWorld; //Matriz Transpose(Invert(World))

float time;

texture texDiffuseMap;
sampler2D diffuseMap = sampler_state
{
    Texture = (texDiffuseMap);
    ADDRESSU = WRAP;
    ADDRESSV = WRAP;
    MINFILTER = LINEAR;
    MAGFILTER = LINEAR;
    MIPFILTER = LINEAR;
};

struct VS_INPUT
{
    float4 Position : POSITION0;
    float4 Color : COLOR0;
    float2 Texcoord : TEXCOORD0;
};

struct VS_OUTPUT
{
    float4 Position : POSITION0;
    float2 Texcoord : TEXCOORD0;
    float4 Color : COLOR0;
    float4 RealPos : TEXCOORD1;
};

VS_OUTPUT vs_main(VS_INPUT Input)
{
    VS_OUTPUT Output;

    Output.RealPos = Input.Position;

    Input.Position.y = Input.Position.y + 5 * cos(time);

    Output.Position = mul(Input.Position, matWorldViewProj);
   
    Output.Texcoord = Input.Texcoord;

    Output.Color = Input.Color;

    return (Output);
}


float4 ps_main(VS_OUTPUT Input) : COLOR0
{
    float dist = 1 / sqrt(pow(distance(Input.RealPos.x, 0), 2) + pow(distance(Input.RealPos.y, 0), 2));
    float modifier = frac(time);
    modifier = modifier * 200;
    float4 Base = tex2D(diffuseMap, Input.Texcoord.xy);
    float4 Color = tex2D(diffuseMap, Input.Texcoord.xy) * dist * modifier;
    if (Color.x < Base.x || Color.x > Base.x * 10)
    {
        Color.x = Base.x;
    }
    if (Color.y < Base.y || Color.y > Base.y * 10)
    {
        Color.y = Base.y;
    }
    if (Color.z < Base.z || Color.z > Base.z * 10)
    {
        Color.z = Base.z;
    }
    return Color;
}

technique Normal
{
    pass Pass_0
    {
        VertexShader = compile vs_3_0 vs_main();
        PixelShader = compile ps_3_0 ps_main();
    }
}