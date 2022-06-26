// Original source code: https://www.shadertoy.com/view/XlGcRh

// commonly used constants
#define c1 0xcc9e2d51u
#define c2 0x1b873593u

// Helper Functions
uint rotl(uint x, uint r)
{
	return (x << r) | (x >> (32u - r));
}

uint rotr(uint x, uint r)
{
	return (x >> r) | (x << (32u - r));
}

uint fmix(uint h)
{
    h ^= h >> 16;
    h *= 0x85ebca6bu;
    h ^= h >> 13;
    h *= 0xc2b2ae35u;
    h ^= h >> 16;
    return h;
}

uint mur(uint a, uint h) {
    // Helper from Murmur3 for combining two 32-bit values.
    a *= c1;
    a = rotr(a, 17u);
    a *= c2;
    h ^= a;
    h = rotr(h, 19u);
    return h * 5u + 0xe6546b64u;
}

uint bswap32(uint x) {
    return (((x & 0x000000ffu) << 24) |
            ((x & 0x0000ff00u) <<  8) |
            ((x & 0x00ff0000u) >>  8) |
            ((x & 0xff000000u) >> 24));
}

uint taus(uint z, int s1, int s2, int s3, uint m)
{
	uint b = (((z << s1) ^ z) >> s2);
    return (((z & m) << s3) ^ b);
}



// convert 2D seed to 1D
// 2 imad
uint seed(uint2 p) {
    return 19u * p.x + 47u * p.y + 101u;
}

// convert 3D seed to 1D
uint seed(uint3 p) {
    return 19u * p.x + 47u * p.y + 101u * p.z + 131u;
}

// convert 4D seed to 1D
uint seed(uint4 p) {
	return 19u * p.x + 47u * p.y + 101u * p.z + 131u * p.w + 173u;
}

// MD5GPU
// https://www.microsoft.com/en-us/research/wp-content/uploads/2007/10/tr-2007-141.pdf
#define A0 0x67452301u
#define B0 0xefcdab89u
#define C0 0x98badcfeu
#define D0 0x10325476u

uint F(uint3 v) { return (v.x & v.y) | (~v.x & v.z); }
uint G(uint3 v) { return (v.x & v.z) | (v.y & ~v.z); }
uint H(uint3 v) { return v.x ^ v.y ^ v.z; }
uint I(uint3 v) { return v.y ^ (v.x | ~v.z); }

void FF(inout uint4 v, inout uint4 rotate, uint x, uint ac)
{
    v.x = v.y + rotl(v.x + F(v.yzw) + x + ac, rotate.x);

    rotate = rotate.yzwx;
    v = v.yzwx;
}

void GG(inout uint4 v, inout uint4 rotate, uint x, uint ac)
{
    v.x = v.y + rotl(v.x + G(v.yzw) + x + ac, rotate.x);

    rotate = rotate.yzwx;
    v = v.yzwx;
}

void HH(inout uint4 v, inout uint4 rotate, uint x, uint ac)
{
    v.x = v.y + rotl(v.x + H(v.yzw) + x + ac, rotate.x);

    rotate = rotate.yzwx;
    v = v.yzwx;
}

void II(inout uint4 v, inout uint4 rotate, uint x, uint ac)
{
    v.x = v.y + rotl(v.x + I(v.yzw) + x + ac, rotate.x);

    rotate = rotate.yzwx;
    v = v.yzwx;
}

uint K(uint i)
{
    return uint(abs(sin(float(i)+1.)) * float(0xffffffffu));
}