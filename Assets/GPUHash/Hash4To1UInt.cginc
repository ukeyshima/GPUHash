// Original source code: https://www.shadertoy.com/view/XlGcRh

// CityHash32, adapted from Hash32Len12to24 in https://github.com/google/cityhash
uint city(uint4 s)
{
    uint len = 16u;
    uint a = bswap32(s.w);
    uint b = bswap32(s.y);
    uint c = bswap32(s.z);
    uint d = bswap32(s.z);
    uint e = bswap32(s.x);
    uint f = bswap32(s.w);
    uint h = len;

    return fmix(mur(f, mur(e, mur(d, mur(c, mur(b, mur(a, h)))))));
}

// Hybrid Taus
// https://developer.nvidia.com/gpugems/GPUGems3/gpugems3_ch37.html
uint hybridtaus(uint4 z)
{
    z.x = taus(z.x, 13, 19, 12, 0xfffffffeu);
    z.y = taus(z.y, 2, 25, 4, 0xfffffff8u);
    z.z = taus(z.z, 3, 11, 17, 0xfffffff0u);
    z.w = z.w * 1664525u + 1013904223u;

    return z.x ^ z.y ^ z.z ^ z.w;
}

// Adapted from MurmurHash3_x86_32 from https://github.com/aappleby/smhasher
uint murmur3(uint4 seed)
{
    uint h = 0u;
    uint k = seed.x;

    k *= c1;
    k = rotl(k,15u);
    k *= c2;

    h ^= k;
    h = rotl(h,13u);
    h = h*5u+0xe6546b64u;

    k = seed.y;

    k *= c1;
    k = rotl(k,15u);
    k *= c2;

    h ^= k;
    h = rotl(h,13u);
    h = h*5u+0xe6546b64u;

    k = seed.z;

    k *= c1;
    k = rotl(k,15u);
    k *= c2;

    h ^= k;
    h = rotl(h,13u);
    h = h*5u+0xe6546b64u;

    k = seed.w;

    k *= c1;
    k = rotl(k,15u);
    k *= c2;

    h ^= k;
    h = rotl(h,13u);
    h = h*5u+0xe6546b64u;

    h ^= 16u;

    return fmix(h);
}

// SuperFastHash, adapated from http://www.azillionmonkeys.com/qed/hash.html
uint superfast(uint4 data)
{
    uint hash = 8u, tmp;

    hash += data.x & 0xffffu;
    tmp = (((data.x >> 16) & 0xffffu) << 11) ^ hash;
    hash = (hash << 16) ^ tmp;
    hash += hash >> 11;

    hash += data.y & 0xffffu;
    tmp = (((data.y >> 16) & 0xffffu) << 11) ^ hash;
    hash = (hash << 16) ^ tmp;
    hash += hash >> 11;

    hash += data.z & 0xffffu;
    tmp = (((data.z >> 16) & 0xffffu) << 11) ^ hash;
    hash = (hash << 16) ^ tmp;
    hash += hash >> 11;

    hash += data.w & 0xffffu;
    tmp = (((data.w >> 16) & 0xffffu) << 11) ^ hash;
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

// 128-bit xorshift
//  - Marsaglia, Xorshift RNGs, Journal of Statistical Software, v8n14, 2003
uint xorshift128(uint4 v)
{
    v.w ^= v.w << 11u;
    v.w ^= v.w >> 8u;
    v = v.wxyz;
    v.x ^= v.y;
    v.x ^= v.y >> 19u;
    return v.x;
}

uint xxhash32(uint4 p)
{
    const uint PRIME32_2 = 2246822519U, PRIME32_3 = 3266489917U;
	const uint PRIME32_4 = 668265263U, PRIME32_5 = 374761393U;
	uint h32 =  p.w + PRIME32_5 + p.x*PRIME32_3;
	h32 = PRIME32_4*((h32 << 17) | (h32 >> (32 - 17)));
	h32 += p.y * PRIME32_3;
	h32 = PRIME32_4*((h32 << 17) | (h32 >> (32 - 17)));
	h32 += p.z * PRIME32_3;
	h32 = PRIME32_4*((h32 << 17) | (h32 >> (32 - 17)));
    h32 = PRIME32_2*(h32^(h32 >> 15));
    h32 = PRIME32_3*(h32^(h32 >> 13));
    return h32^(h32 >> 16);
}