// Original source code: https://www.shadertoy.com/view/XlGcRh

uint2 pcg2d(uint2 v)
{
    v = v * 1664525u + 1013904223u;

    v.x += v.y * 1664525u;
    v.y += v.x * 1664525u;

    v = v ^ (v>>16u);

    v.x += v.y * 1664525u;
    v.y += v.x * 1664525u;

    v = v ^ (v>>16u);

    return v;
}

// Tiny Encryption Algorithm
//  - Zafar et al., GPU random numbers via the tiny encryption algorithm, HPG 2010
uint2 tea(int tea, uint2 p) {
    uint s = 0u;

    for( int i = 0; i < tea; i++) {
        s += 0x9E3779B9u;
        p.x += (p.y<<4u)^(p.y+s)^(p.y>>5u);
        p.y += (p.x<<4u)^(p.x+s)^(p.x>>5u);
    }
    return p.xy;
}