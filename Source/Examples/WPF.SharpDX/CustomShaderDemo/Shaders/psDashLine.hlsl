#ifndef PSLINE_HLSL
#define PSLINE_HLSL
#define POINTLINE
#include"DataStructs.hlsl"
#include"CommonBuffers.hlsl"
#include"Common.hlsl"

float4 main(GSInputDASH input) : SV_Target
{
    int eyeDis = (int)length(vEyePos - input.wp.xyz) / 20u;
    if (eyeDis == 0)
        eyeDis = 1;
    float dashDis = eyeDis * 2;
    float stepDis = eyeDis * 3;
    float distance = input.c.a * 10000;
    float lineUMod = fmod(distance, stepDis);
    float dash = step(lineUMod, dashDis);
    return float4(pColor.rgb, dash);
}
#endif