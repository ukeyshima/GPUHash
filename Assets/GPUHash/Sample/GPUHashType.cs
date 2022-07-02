using System;
using UnityEngine;

namespace GPUHash.Sample
{
    enum GPUHashType { Float1To1, Uint1To1, Float1To2, Float1To3, Float1To4, Float2To1, Uint2To1, Float2To2, Uint2To2, Float2To3, Float2To4, Float3To1, Uint3To1, Float3To2, Float3To3, Uint3To3, Float3To4, Uint4To1, Float4To4, Uint4To4 }

    public interface IGPUHashType
    {
        string ShaderPath { get; }
        string HashType { get; }
        string HashTypeDefault { get; }
        void CheckHashType();
        Action OnChangedHashType { set; }
    }

    [System.Serializable]
    class GPUHashFloat1To1 : IGPUHashType
    {
        public Hash _hash;
        private Hash _preHash;
        public enum Hash { hashwithoutsine11 }
        private Action _onChangedHashType;
        public string ShaderPath => "Assets/GPUHash/Sample/Shaders/GPUHash1To1FloatVisualizer.shader";
        public string HashType => _hash.ToString();
        public string HashTypeDefault => ((Hash)0).ToString();
        public void CheckHashType() { if (_hash != _preHash) { _onChangedHashType(); _preHash = _hash; } }
        public Action OnChangedHashType { set => _onChangedHashType = value; }
    }

    [System.Serializable]
    class GPUHashUint1To1 : IGPUHashType
    {
        public Hash _hash;
        private Hash _preHash;
        public enum Hash { bbs, city, esgtsa, iqint1, lcg, murmur3, pcg, ranlim32, superfast, wang, xorshift32, xxhash32 }
        private Action _onChangedHashType;
        public string ShaderPath => "Assets/GPUHash/Sample/Shaders/GPUHash1To1UIntVisualizer.shader";
        public string HashType => _hash.ToString();
        public string HashTypeDefault => ((Hash)0).ToString();
        public void CheckHashType() { if (_hash != _preHash) { _onChangedHashType(); _preHash = _hash; } }
        public Action OnChangedHashType { set => _onChangedHashType = value; }
    }

    [System.Serializable]
    class GPUHashFloat1To2 : IGPUHashType
    {
        public Hash _hash;
        private Hash _preHash;
        public enum Hash { hashwithoutsine21 }
        private Action _onChangedHashType;
        public string ShaderPath => "Assets/GPUHash/Sample/Shaders/GPUHash1To2FloatVisualizer.shader";
        public string HashType => _hash.ToString();
        public string HashTypeDefault => ((Hash)0).ToString();
        public void CheckHashType() { if (_hash != _preHash) { _onChangedHashType(); _preHash = _hash; } }
        public Action OnChangedHashType { set => _onChangedHashType = value; }
    }

    [System.Serializable]
    class GPUHashFloat1To3 : IGPUHashType
    {
        public Hash _hash;
        private Hash _preHash;
        public enum Hash { hashwithoutsine31 }
        private Action _onChangedHashType;
        public string ShaderPath => "Assets/GPUHash/Sample/Shaders/GPUHash1To3FloatVisualizer.shader";
        public string HashType => _hash.ToString();
        public string HashTypeDefault => ((Hash)0).ToString();
        public void CheckHashType() { if (_hash != _preHash) { _onChangedHashType(); _preHash = _hash; } }
        public Action OnChangedHashType { set => _onChangedHashType = value; }
    }

    [System.Serializable]
    class GPUHashFloat1To4 : IGPUHashType
    {
        public Hash _hash;
        private Hash _preHash;
        public enum Hash { hashwithoutsine41 }
        private Action _onChangedHashType;
        public string ShaderPath => "Assets/GPUHash/Sample/Shaders/GPUHash1To4FloatVisualizer.shader";
        public string HashType => _hash.ToString();
        public string HashTypeDefault => ((Hash)0).ToString();
        public void CheckHashType() { if (_hash != _preHash) { _onChangedHashType(); _preHash = _hash; } }
        public Action OnChangedHashType { set => _onChangedHashType = value; }
    }

    [System.Serializable]
    class GPUHashFloat2To1 : IGPUHashType
    {
        public Hash _hash;
        private Hash _preHash;
        public enum Hash { fast, hashwithoutsine12, ign, pseudo, trig }
        private Action _onChangedHashType;
        public string ShaderPath => "Assets/GPUHash/Sample/Shaders/GPUHash2To1FloatVisualizer.shader";
        public string HashType => _hash.ToString();
        public string HashTypeDefault => ((Hash)0).ToString();
        public void CheckHashType() { if (_hash != _preHash) { _onChangedHashType(); _preHash = _hash; } }
        public Action OnChangedHashType { set => _onChangedHashType = value; }
    }

    [System.Serializable]
    class GPUHashUint2To1 : IGPUHashType
    {
        public Hash _hash;
        private Hash _preHash;
        public enum Hash { city, iqint3, jkiss32, murmur3, superfast, xxhash32 }
        private Action _onChangedHashType;
        public string ShaderPath => "Assets/GPUHash/Sample/Shaders/GPUHash2To1UIntVisualizer.shader";
        public string HashType => _hash.ToString();
        public string HashTypeDefault => ((Hash)0).ToString();
        public void CheckHashType() { if (_hash != _preHash) { _onChangedHashType(); _preHash = _hash; } }
        public Action OnChangedHashType { set => _onChangedHashType = value; }
    }

    [System.Serializable]
    class GPUHashFloat2To2 : IGPUHashType
    {
        public Hash _hash;
        private Hash _preHash;
        public enum Hash { hashwithoutsine22 }
        private Action _onChangedHashType;
        public string ShaderPath => "Assets/GPUHash/Sample/Shaders/GPUHash2To2FloatVisualizer.shader";
        public string HashType => _hash.ToString();
        public string HashTypeDefault => ((Hash)0).ToString();
        public void CheckHashType() { if (_hash != _preHash) { _onChangedHashType(); _preHash = _hash; } }
        public Action OnChangedHashType { set => _onChangedHashType = value; }
    }

    [System.Serializable]
    class GPUHashUint2To2 : IGPUHashType
    {
        public Hash _hash;
        private Hash _preHash;
        public enum Hash { pcg2d, tea }
        private Action _onChangedHashType;
        public string ShaderPath => "Assets/GPUHash/Sample/Shaders/GPUHash2To2UintVisualizer.shader";
        public string HashType => _hash.ToString();
        public string HashTypeDefault => ((Hash)0).ToString();
        public void CheckHashType() { if (_hash != _preHash) { _onChangedHashType(); _preHash = _hash; } }
        public Action OnChangedHashType { set => _onChangedHashType = value; }
    }

    [System.Serializable]
    class GPUHashFloat2To3 : IGPUHashType
    {
        public Hash _hash;
        private Hash _preHash;
        public enum Hash { hashwithoutsine32 }
        private Action _onChangedHashType;
        public string ShaderPath => "Assets/GPUHash/Sample/Shaders/GPUHash2To3FloatVisualizer.shader";
        public string HashType => _hash.ToString();
        public string HashTypeDefault => ((Hash)0).ToString();
        public void CheckHashType() { if (_hash != _preHash) { _onChangedHashType(); _preHash = _hash; } }
        public Action OnChangedHashType { set => _onChangedHashType = value; }
    }

    [System.Serializable]
    class GPUHashFloat2To4 : IGPUHashType
    {
        public Hash _hash;
        private Hash _preHash;
        public enum Hash { hashwithoutsine42 }
        private Action _onChangedHashType;
        public string ShaderPath => "Assets/GPUHash/Sample/Shaders/GPUHash2To4FloatVisualizer.shader";
        public string HashType => _hash.ToString();
        public string HashTypeDefault => ((Hash)0).ToString();
        public void CheckHashType() { if (_hash != _preHash) { _onChangedHashType(); _preHash = _hash; } }
        public Action OnChangedHashType { set => _onChangedHashType = value; }
    }

    [System.Serializable]
    class GPUHashFloat3To1 : IGPUHashType
    {
        public Hash _hash;
        private Hash _preHash;
        public enum Hash { hashwithoutsine13 }
        private Action _onChangedHashType;
        public string ShaderPath => "Assets/GPUHash/Sample/Shaders/GPUHash3To1FloatVisualizer.shader";
        public string HashType => _hash.ToString();
        public string HashTypeDefault => ((Hash)0).ToString();
        public void CheckHashType() { if (_hash != _preHash) { _onChangedHashType(); _preHash = _hash; } }
        public Action OnChangedHashType { set => _onChangedHashType = value; }
    }

    [System.Serializable]
    class GPUHashUint3To1 : IGPUHashType
    {
        public Hash _hash;
        private Hash _preHash;
        public enum Hash { city, superfast, xxhash32, murmur3 }
        private Action _onChangedHashType;
        public string ShaderPath => "Assets/GPUHash/Sample/Shaders/GPUHash3To1UIntVisualizer.shader";
        public string HashType => _hash.ToString();
        public string HashTypeDefault => ((Hash)0).ToString();
        public void CheckHashType() { if (_hash != _preHash) { _onChangedHashType(); _preHash = _hash; } }
        public Action OnChangedHashType { set => _onChangedHashType = value; }
    }

    [System.Serializable]
    class GPUHashFloat3To2 : IGPUHashType
    {
        public Hash _hash;
        private Hash _preHash;
        public enum Hash { hashwithoutsine23 }
        private Action _onChangedHashType;
        public string ShaderPath => "Assets/GPUHash/Sample/Shaders/GPUHash3To2FloatVisualizer.shader";
        public string HashType => _hash.ToString();
        public string HashTypeDefault => ((Hash)0).ToString();
        public void CheckHashType() { if (_hash != _preHash) { _onChangedHashType(); _preHash = _hash; } }
        public Action OnChangedHashType { set => _onChangedHashType = value; }
    }

    [System.Serializable]
    class GPUHashFloat3To3 : IGPUHashType
    {
        public Hash _hash;
        private Hash _preHash;
        public enum Hash { hashwithoutsine33 }
        private Action _onChangedHashType;
        public string ShaderPath => "Assets/GPUHash/Sample/Shaders/GPUHash3To3FloatVisualizer.shader";
        public string HashType => _hash.ToString();
        public string HashTypeDefault => ((Hash)0).ToString();
        public void CheckHashType() { if (_hash != _preHash) { _onChangedHashType(); _preHash = _hash; } }
        public Action OnChangedHashType { set => _onChangedHashType = value; }
    }

    [System.Serializable]
    class GPUHashUint3To3 : IGPUHashType
    {
        public Hash _hash;
        private Hash _preHash;
        public enum Hash { iqint2, pcg3d, pcg3d16 }
        private Action _onChangedHashType;
        public string ShaderPath => "Assets/GPUHash/Sample/Shaders/GPUHash3To3UintVisualizer.shader";
        public string HashType => _hash.ToString();
        public string HashTypeDefault => ((Hash)0).ToString();
        public void CheckHashType() { if (_hash != _preHash) { _onChangedHashType(); _preHash = _hash; } }
        public Action OnChangedHashType { set => _onChangedHashType = value; }
    }

    [System.Serializable]
    class GPUHashFloat3To4 : IGPUHashType
    {
        public Hash _hash;
        private Hash _preHash;
        public enum Hash { hashwithoutsine43 }
        private Action _onChangedHashType;
        public string ShaderPath => "Assets/GPUHash/Sample/Shaders/GPUHash3To4FloatVisualizer.shader";
        public string HashType => _hash.ToString();
        public string HashTypeDefault => ((Hash)0).ToString();
        public void CheckHashType() { if (_hash != _preHash) { _onChangedHashType(); _preHash = _hash; } }
        public Action OnChangedHashType { set => _onChangedHashType = value; }
    }

    [System.Serializable]
    class GPUHashUint4To1 : IGPUHashType
    {
        public Hash _hash;
        private Hash _preHash;
        public enum Hash { city, hybridtaus, murmur3, superfast, xorshift128, xxhash32 }
        private Action _onChangedHashType;
        public string ShaderPath => "Assets/GPUHash/Sample/Shaders/GPUHash4To1UIntVisualizer.shader";
        public string HashType => _hash.ToString();
        public string HashTypeDefault => ((Hash)0).ToString();
        public void CheckHashType() { if (_hash != _preHash) { _onChangedHashType(); _preHash = _hash; } }
        public Action OnChangedHashType { set => _onChangedHashType = value; }
    }

    [System.Serializable]
    class GPUHashFloat4To4 : IGPUHashType
    {
        public Hash _hash;
        private Hash _preHash;
        public enum Hash { hashwithoutsine44 }
        private Action _onChangedHashType;
        public string ShaderPath => "Assets/GPUHash/Sample/Shaders/GPUHash4To4FloatVisualizer.shader";
        public string HashType => _hash.ToString();
        public string HashTypeDefault => ((Hash)0).ToString();
        public void CheckHashType() { if (_hash != _preHash) { _onChangedHashType(); _preHash = _hash; } }
        public Action OnChangedHashType { set => _onChangedHashType = value; }
    }

    [System.Serializable]
    class GPUHashUint4to4 : IGPUHashType
    {
        public Hash _hash;
        private Hash _preHash;
        public enum Hash { md5, pcg4d }
        private Action _onChangedHashType;
        public string ShaderPath => "Assets/GPUHash/Sample/Shaders/GPUHash4To4UintVisualizer.shader";
        public string HashType => _hash.ToString();
        public string HashTypeDefault => ((Hash)0).ToString();
        public void CheckHashType() { if (_hash != _preHash) { _onChangedHashType(); _preHash = _hash; } }
        public Action OnChangedHashType { set => _onChangedHashType = value; }
    }
}