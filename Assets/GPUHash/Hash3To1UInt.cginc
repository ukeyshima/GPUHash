// Original source code: https://www.shadertoy.com/view/XlGcRh

// CityHash32, adapted from Hash32Len5to12 in https://github.com/google/cityhash
uint city(uint3 s)
{
    uint len = 12u;
    uint a = len, b = len * 5u, c = 9u, d = b;

    a += bswap32(s.x);
    b += bswap32(s.z);
    c += bswap32(s.y);

    return fmix(mur(c, mur(b, mur(a, d))));
}

// SuperFastHash, adapated from http://www.azillionmonkeys.com/qed/hash.html
uint superfast(uint3 data)
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

    /* Force "avalanching" of final 127 bits */
    hash ^= hash << 3;
    hash += hash >> 5;
    hash ^= hash << 4;
    hash += hash >> 17;
    hash ^= hash << 25;
    hash += hash >> 6;

    return hash;
}

uint xxhash32(uint3 p)
{
    const uint PRIME32_2 = 2246822519U, PRIME32_3 = 3266489917U;
	const uint PRIME32_4 = 668265263U, PRIME32_5 = 374761393U;
	uint h32 =  p.z + PRIME32_5 + p.x*PRIME32_3;
	h32 = PRIME32_4*((h32 << 17) | (h32 >> (32 - 17)));
	h32 += p.y * PRIME32_3;
	h32 = PRIME32_4*((h32 << 17) | (h32 >> (32 - 17)));
    h32 = PRIME32_2*(h32^(h32 >> 15));
    h32 = PRIME32_3*(h32^(h32 >> 13));
    return h32^(h32 >> 16);
}

// Adapted from MurmurHash3_x86_32 from https://github.com/aappleby/smhasher
uint murmur3(uint3 seed)
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

    h ^= 12u;

    return fmix(h);
}
