// Original source code: https://www.shadertoy.com/view/XlGcRh

// Hash without Sine
// https://www.shadertoy.com/view/4djSRW
float hashwithoutsine11(float p)
{
    p = frac(p * .1031);
    p *= p + 33.33;
    p *= p + p;
    return frac(p);
}