// Original source code: https://www.shadertoy.com/view/XlGcRh

// BBS-inspired hash
//  - Olano, Modified Noise for Evaluation on Graphics Hardware, GH 2005
uint bbs(uint v) {
    v = v % 65521u;
    v = (v * v) % 65521u;
    v = (v * v) % 65521u;
    return v;
}

// CityHash32, adapted from Hash32Len0to4 in https://github.com/google/cityhash
uint city(uint s)
{
    uint len = 4u;
	uint b = 0u;
    uint c = 9u;

    for (uint i = 0u; i < len; i++) {
    	uint v = (s >> (i * 8u)) & 0xffu;
        b = b * c1 + v;
        c ^= b;
    }

    return fmix(mur(b, mur(len, c)));
}

// Schechter and Bridson hash
// https://www.cs.ubc.ca/~rbridson/docs/schechter-sca08-turbulence.pdf
uint esgtsa(uint s)
{
    s = (s ^ 2747636419u) * 2654435769u;// % 4294967296u;
    s = (s ^ (s >> 16u)) * 2654435769u;// % 4294967296u;
    s = (s ^ (s >> 16u)) * 2654435769u;// % 4294967296u;
    return s;
}



// Integer Hash - I
// - Inigo Quilez, Integer Hash - I, 2017
//   https://www.shadertoy.com/view/llGSzw
uint iqint1(uint n)
{
    // integer hash copied from Hugo Elias
	n = (n << 13U) ^ n;
    n = n * (n * n * 15731U + 789221U) + 1376312589U;

    return n;
}

// linear congruential generator
uint lcg(uint p)
{
    return p * 1664525u + 1013904223u;
}

// Adapted from MurmurHash3_x86_32 from https://github.com/aappleby/smhasher
uint murmur3(uint seed)
{
    uint h = 0u;
    uint k = seed;

    k *= c1;
    k = rotl(k,15u);
    k *= c2;

    h ^= k;
    h = rotl(h,13u);
    h = h*5u+0xe6546b64u;

    h ^= 4u;

    return fmix(h);
}

// https://www.pcg-random.org/
uint pcg(uint v)
{
	uint state = v * 747796405u + 2891336453u;
	uint word = ((state >> ((state >> 28u) + 4u)) ^ state) * 277803737u;
	return (word >> 22u) ^ word;
}

// Numerical Recipies 3rd Edition
uint ranlim32(uint j){
    uint u, v, w1, w2, x, y;

    v = 2244614371U;
    w1 = 521288629U;
    w2 = 362436069U;

    u = j ^ v;

    u = u * 2891336453U + 1640531513U;
    v ^= v >> 13; v ^= v << 17; v ^= v >> 5;
    w1 = 33378u * (w1 & 0xffffu) + (w1 >> 16);
    w2 = 57225u * (w2 & 0xffffu) + (w2 >> 16);

    v = u;

    u = u * 2891336453U + 1640531513U;
    v ^= v >> 13; v ^= v << 17; v ^= v >> 5;
    w1 = 33378u * (w1 & 0xffffu) + (w1 >> 16);
    w2 = 57225u * (w2 & 0xffffu) + (w2 >> 16);

    x = u ^ (u << 9); x ^= x >> 17; x ^= x << 6;
    y = w1 ^ (w1 << 17); y ^= y >> 15; y ^= y << 5;

    return (x + v) ^ (y + w2);
}

// SuperFastHash, adapated from http://www.azillionmonkeys.com/qed/hash.html
uint superfast(uint data)
{
	uint hash = 4u, tmp;

    hash += data & 0xffffu;
    tmp = (((data >> 16) & 0xffffu) << 11) ^ hash;
    hash = (hash << 16) ^ tmp;
    hash += hash >> 11;

    /* Force "avalanching" of final 127 bits */
    hash ^= hash << 3;
    hash += hash >> 5;
    hash ^= hash << 4;
    hash += hash >> 17;
    hash ^= hash << 25;
    hash += hash >> 6;

    return hash;
}

// Wang hash, described on http://burtleburtle.net/bob/hash/integer.html
// original page by Thomas Wang 404
uint wang(uint v)
{
    v = (v ^ 61u) ^ (v >> 16u);
    v *= 9u;
    v ^= v >> 4u;
    v *= 0x27d4eb2du;
    v ^= v >> 15u;
    return v;
}

// 32-bit xorshift
//  - Marsaglia, Xorshift RNGs, Journal of Statistical Software, v8n14, 2003
uint xorshift32(uint v)
{
    v ^= v << 13u;
    v ^= v >> 17u;
    v ^= v << 5u;
    return v;
}

// xxhash (https://github.com/Cyan4973/xxHash)
//   From https://www.shadertoy.com/view/Xt3cDn
uint xxhash32(uint p)
{
	const uint PRIME32_2 = 2246822519U, PRIME32_3 = 3266489917U;
	const uint PRIME32_4 = 668265263U, PRIME32_5 = 374761393U;
	uint h32 = p + PRIME32_5;
	h32 = PRIME32_4*((h32 << 17) | (h32 >> (32 - 17)));
    h32 = PRIME32_2*(h32^(h32 >> 15));
    h32 = PRIME32_3*(h32^(h32 >> 13));
    return h32^(h32 >> 16);
}