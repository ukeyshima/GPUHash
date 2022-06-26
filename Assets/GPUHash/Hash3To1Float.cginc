// Original source code: https://www.shadertoy.com/view/XlGcRh

float hashwithoutsine13(float3 p3)
{
    p3  = frac(p3 * .1031);
    p3 += dot(p3, p3.yzx + 33.33);
    return frac((p3.x + p3.y) * p3.z);
}