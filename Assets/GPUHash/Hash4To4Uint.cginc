// Original source code: https://www.shadertoy.com/view/XlGcRh

uint4 md5(uint4 u)
{
    uint4 digest = uint4(A0, B0, C0, D0);
    uint4 r, v = digest;
    uint i = 0u;

	uint M[16];
	M[0] = u.x; M[1] = u.y;	M[2] = u.z;	M[3] = u.w;
	M[4] = 0u; M[5] = 0u; M[6] = 0u; M[7] = 0u; M[8] = 0u;
	M[9] = 0u; M[10] = 0u; M[11] = 0u; M[12] = 0u; M[13] = 0u;
	M[14] = 0u; M[15] = 0u;

    r = uint4(7, 12, 17, 22);
    FF(v, r, M[0], K(i++));
    FF(v, r, M[1], K(i++));
    FF(v, r, M[2], K(i++));
    FF(v, r, M[3], K(i++));
    FF(v, r, M[4], K(i++));
    FF(v, r, M[5], K(i++));
    FF(v, r, M[6], K(i++));
    FF(v, r, M[7], K(i++));
    FF(v, r, M[8], K(i++));
    FF(v, r, M[9], K(i++));
    FF(v, r, M[10], K(i++));
    FF(v, r, M[11], K(i++));
    FF(v, r, M[12], K(i++));
    FF(v, r, M[13], K(i++));
    FF(v, r, M[14], K(i++));
    FF(v, r, M[15], K(i++));

    r = uint4(5, 9, 14, 20);
    GG(v, r, M[1], K(i++));
    GG(v, r, M[6], K(i++));
    GG(v, r, M[11], K(i++));
    GG(v, r, M[0], K(i++));
    GG(v, r, M[5], K(i++));
    GG(v, r, M[10], K(i++));
    GG(v, r, M[15], K(i++));
    GG(v, r, M[4], K(i++));
    GG(v, r, M[9], K(i++));
    GG(v, r, M[14], K(i++));
    GG(v, r, M[3], K(i++));
    GG(v, r, M[8], K(i++));
    GG(v, r, M[13], K(i++));
    GG(v, r, M[2], K(i++));
    GG(v, r, M[7], K(i++));
    GG(v, r, M[12], K(i++));

    r = uint4(4, 11, 16, 23);
    HH(v, r, M[5], K(i++));
    HH(v, r, M[8], K(i++));
    HH(v, r, M[11], K(i++));
    HH(v, r, M[14], K(i++));
    HH(v, r, M[1], K(i++));
    HH(v, r, M[4], K(i++));
    HH(v, r, M[7], K(i++));
    HH(v, r, M[10], K(i++));
    HH(v, r, M[13], K(i++));
    HH(v, r, M[0], K(i++));
    HH(v, r, M[3], K(i++));
    HH(v, r, M[6], K(i++));
    HH(v, r, M[9], K(i++));
    HH(v, r, M[12], K(i++));
    HH(v, r, M[15], K(i++));
    HH(v, r, M[2], K(i++));

    r = uint4(6, 10, 15, 21);
    II(v, r, M[0], K(i++));
    II(v, r, M[7], K(i++));
    II(v, r, M[14], K(i++));
    II(v, r, M[5], K(i++));
    II(v, r, M[12], K(i++));
    II(v, r, M[3], K(i++));
    II(v, r, M[10], K(i++));
    II(v, r, M[1], K(i++));
    II(v, r, M[8], K(i++));
    II(v, r, M[15], K(i++));
    II(v, r, M[6], K(i++));
    II(v, r, M[13], K(i++));
    II(v, r, M[4], K(i++));
    II(v, r, M[11], K(i++));
    II(v, r, M[2], K(i++));
    II(v, r, M[9], K(i++));

    return digest + v;
}

// http://www.jcgt.org/published/0009/03/02/
uint4 pcg4d(uint4 v)
{
    v = v * 1664525u + 1013904223u;

    v.x += v.y*v.w;
    v.y += v.z*v.x;
    v.z += v.x*v.y;
    v.w += v.y*v.z;

    v ^= v >> 16u;

    v.x += v.y*v.w;
    v.y += v.z*v.x;
    v.z += v.x*v.y;
    v.w += v.y*v.z;

    return v;
}