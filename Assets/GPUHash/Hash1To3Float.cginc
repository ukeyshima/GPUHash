// Original source code: https://www.shadertoy.com/view/XlGcRh


float3 hashwithoutsine31(float p)
{
   float3 p3 = frac(float3(p,p,p) * float3(.1031, .1030, .0973));
   p3 += dot(p3, p3.yzx+33.33);
   return frac((p3.xxy+p3.yzz)*p3.zyx);
}