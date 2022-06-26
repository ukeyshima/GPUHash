// Original source code: https://www.shadertoy.com/view/XlGcRh

// UE4's RandFast function
// https://github.com/EpicGames/UnrealEngine/blob/release/Engine/Shaders/Private/Random.ush
float fast(float2 v)
{
    v = (1./4320.) * v + float2(0.25,0.);
    float state = frac( dot( v * v, float2(3571, 3571)));
    return frac( state * state * (3571. * 2.));
}

float hashwithoutsine12(float2 p)
{
	float3 p3  = frac(float3(p.xyx) * .1031);
    p3 += dot(p3, p3.yzx + 33.33);
    return frac((p3.x + p3.y) * p3.z);
}

// Interleaved Gradient Noise
//  - Jimenez, Next Generation Post Processing in Call of Duty: Advanced Warfare
//    Advances in Real-time Rendering, SIGGRAPH 2014
float ign(float2 v)
{
    float3 magic = float3(0.06711056, 0.00583715, 52.9829189);
    return frac(magic.z * frac(dot(v, magic.xy)));
}

// UE4's PseudoRandom function
// https://github.com/EpicGames/UnrealEngine/blob/release/Engine/Shaders/Private/Random.ush
float pseudo(float2 v) {
    v = frac(v/128.)*128. + float2(-64.340622, -72.465622);
    return frac(dot(v.xyx * v.xyy, float3(20.390625, 60.703125, 2.4281209)));
}

// common GLSL hash
//  - Rey, On generating random numbers, with help of y= [(a+x)sin(bx)] mod 1,
//    22nd European Meeting of Statisticians and the 7th Vilnius Conference on
//    Probability Theory and Mathematical Statistics, August 1998
/*
uint2 trig(uint2 p) {
    return uint2(float(0xffffff)*frac(43757.5453*sin(dot(float2(p),float2(12.9898,78.233)))));
}
*/
float trig(float2 p)
{
    return frac(43757.5453*sin(dot(p, float2(12.9898,78.233))));
}