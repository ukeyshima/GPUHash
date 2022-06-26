// Original source code: https://www.shadertoy.com/view/XlGcRh

// CityHash32, adapted from Hash32Len5to12 in https://github.com/google/cityhash
uint city(uint2 s)
{
    uint len = 8u;
    uint a = len, b = len * 5u, c = 9u, d = b;

    a += bswap32(s.x);
    b += bswap32(s.y);
    c += bswap32(s.y);

    return fmix(mur(c, mur(b, mur(a, d))));
}

// Integer Hash - III
// - Inigo Quilez, Integer Hash - III, 2017
//   https://www.shadertoy.com/view/4tXyWN
uint iqint3(uint2 x)
{
    uint2 q = 1103515245U * ( (x>>1U) ^ (x.yx   ) );
    uint  n = 1103515245U * ( (q.x  ) ^ (q.y>>3U) );

    return n;
}

uint jkiss32(uint2 p)
{
    uint x=p.x;//123456789;
    uint y=p.y;//234567891;

    uint z=345678912u,w=456789123u,c=0u;
    int t;
    y ^= (y<<5); y ^= (y>>7); y ^= (y<<22);
    t = int(z+w+c); z = w; c = uint(t < 0); w = uint(t&2147483647);
    x += 1411392427u;
    return x + y + w;
}

// Adapted from MurmurHash3_x86_32 from https://github.com/aappleby/smhasher
uint murmur3(uint2 seed)
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

    h ^= 8u;

    return fmix(h);
}

// SuperFastHash, adapated from http://www.azillionmonkeys.com/qed/hash.html
uint superfast(uint2 data)
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

    /* Force "avalanching" of final 127 bits */
    hash ^= hash << 3;
    hash += hash >> 5;
    hash ^= hash << 4;
    hash += hash >> 17;
    hash ^= hash << 25;
    hash += hash >> 6;

    return hash;
}

uint xxhash32(uint2 p)
{
    const uint PRIME32_2 = 2246822519U, PRIME32_3 = 3266489917U;
	const uint PRIME32_4 = 668265263U, PRIME32_5 = 374761393U;
    uint h32 = p.y + PRIME32_5 + p.x*PRIME32_3;
    h32 = PRIME32_4*((h32 << 17) | (h32 >> (32 - 17)));
    h32 = PRIME32_2*(h32^(h32 >> 15));
    h32 = PRIME32_3*(h32^(h32 >> 13));
    return h32^(h32 >> 16);
}