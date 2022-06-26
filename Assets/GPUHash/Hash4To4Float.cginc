// Original source code: https://www.shadertoy.com/view/XlGcRh

float4 hashwithoutsine44(float4 p4)
{
	p4 = frac(p4  * float4(.1031, .1030, .0973, .1099));
    p4 += dot(p4, p4.wzxy+33.33);
    return frac((p4.xxyz+p4.yzzw)*p4.zywx);
}