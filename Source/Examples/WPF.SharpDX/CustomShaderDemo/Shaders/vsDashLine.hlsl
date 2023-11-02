#ifndef VSPOINT_HLSL
#define VSPOINT_HLSL
#define POINTLINE
#include"DataStructs.hlsl"
#include"CommonBuffers.hlsl"
#include"Common.hlsl"

GSInputDASH main(VSInputDASH input)
{
    GSInputDASH output = (GSInputDASH) 0;
    output.p = input.p;

	//set position into clip space	
    output.wp = mul(output.p, mWorld);
    float3 vEye = vEyePos - output.wp.xyz;
    output.vEye = float4(normalize(vEye), length(vEye)); //Use wp for camera->vertex direction
    output.p = mul(output.wp, mViewProjection);
    // Allow to quickly change blending mode and do linear blending
    //output.c = (1 - pEnableBlending) * input.c * pColor + pEnableBlending * (pBlendingFactor * input.c + (1 - pBlendingFactor) * pColor);
    //input.c * pColor;
    output.c = input.c;
    //output.l = input.l;
    return output;
}
#endif