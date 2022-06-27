using UnityEngine;

namespace GPUHash.Sample
{
    enum GPUHashType { Float1To1, Uint1To1, Float1To2, Float1To3, Float1To4, Float2To1, Uint2To1, Float2To2, Uint2To2, Float2To3, Float2To4, Float3To1, Uint3To1, Float3To2, Float3To3, Uint3To3, Float3To4, Uint4To1, Float4To4, Uint4To4 }

    public interface IGPUHashType { }

    [System.Serializable]
    class GPUHashFloat1To1 : IGPUHashType
    {
        public Hash _hash;
        public enum Hash { hashwithoutsine11 }
    }

    [System.Serializable]
    class GPUHashUint1To1 : IGPUHashType
    {
        public Hash _hash;
        public enum Hash { bbs, city, esgtsa, iqint1, lcg, murmur3, pcg, ranlim32, superfast, wang, xorshift32, xxhash32 }
    }

    [System.Serializable]
    class GPUHashFloat1To2 : IGPUHashType
    {
        public Hash _hash;
        public enum Hash { hashwithoutsine21 }
    }

    [System.Serializable]
    class GPUHashFloat1To3 : IGPUHashType
    {
        public Hash _hash;
        public enum Hash { hashwithoutsine31 }
    }

    [System.Serializable]
    class GPUHashFloat1To4 : IGPUHashType
    {
        public Hash _hash;
        public enum Hash { hashwithoutsine41 }
    }

    [System.Serializable]
    class GPUHashFloat2To1 : IGPUHashType
    {
        public Hash _hash;
        public enum Hash { fast, hashwithoutsine12, ign, pseudo, trig }
    }

    [System.Serializable]
    class GPUHashUint2To1 : IGPUHashType
    {
        public Hash _hash;
        public enum Hash { city, iqint3, jkiss32, murmur3, superfast, xxhash32 }
    }

    [System.Serializable]
    class GPUHashFloat2To2 : IGPUHashType
    {
        public Hash _hash;
        public enum Hash { hashwithoutsine22 }
    }

    [System.Serializable]
    class GPUHashUint2To2 : IGPUHashType
    {
        public Hash _hash;
        public enum Hash { pcg2d, tea }
    }

    [System.Serializable]
    class GPUHashFloat2To3 : IGPUHashType
    {
        public Hash _hash;
        public enum Hash { hashwithoutsine32 }
    }

    [System.Serializable]
    class GPUHashFloat2To4 : IGPUHashType
    {
        public Hash _hash;
        public enum Hash { hashwithoutsine42 }
    }

    [System.Serializable]
    class GPUHashFloat3To1 : IGPUHashType
    {
        public Hash _hash;
        public enum Hash { hashwithoutsine13 }
    }

    [System.Serializable]
    class GPUHashUint3To1 : IGPUHashType
    {
        public Hash _hash;
        public enum Hash { city, superfast, xxhash32, murmur3 }
    }

    [System.Serializable]
    class GPUHashFloat3To2 : IGPUHashType
    {
        public Hash _hash;
        public enum Hash { hashwithoutsine23 }
    }

    [System.Serializable]
    class GPUHashFloat3To3 : IGPUHashType
    {
        public Hash _hash;
        public enum Hash { hashwithoutsine33 }
    }

    [System.Serializable]
    class GPUHashUint3To3 : IGPUHashType
    {
        public Hash _hash;
        public enum Hash { iqint2, pcg3d, pcg3d16 }
    }

    [System.Serializable]
    class GPUHashFloat3To4 : IGPUHashType
    {
        public Hash _hash;
        public enum Hash { hashwithoutsine43 }
    }

    [System.Serializable]
    class GPUHashUint4To1 : IGPUHashType
    {
        public Hash _hash;
        public enum Hash { city, hybridtaus, murmur3, superfast, xorshift128, xxhash32 }
    }

    [System.Serializable]
    class GPUHashFloat4To4 : IGPUHashType
    {
        public Hash _hash;
        public enum Hash { hashwithoutsine44 }
    }

    [System.Serializable]
    class GPUHashUint4to4 : IGPUHashType
    {
        public Hash _hash;
        public enum Hash { md5, pcg4d }
    }
}