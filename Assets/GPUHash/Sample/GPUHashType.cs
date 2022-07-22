using System.Security.AccessControl;
using System;
using UnityEngine;

namespace GPUHash.Sample
{
    enum GPUHashType { Float1To1, Uint1To1, Float1To2, Float1To3, Float1To4, Float2To1, Uint2To1, Float2To2, Uint2To2, Float2To3, Float2To4, Float3To1, Uint3To1, Float3To2, Float3To3, Uint3To3, Float3To4, Uint4To1, Float4To4, Uint4To4 }

    public interface IGPUHashType
    {
        string ShaderPath { get; }
        string HashType { get; }
        string HashTypePre { get; }
        string OutputType { get; }
        Vector2 InputShift { get; }
        Vector2 InputScale { get; }
        Action OnChangedHashType { set; }
        void CheckHashType();
        Action OnChangedInput { set; }
        void CheckInput();
        Action OnChangedOutputType { set; }
        void CheckOutputType();
    }

    public class GPUHashTypeBase : IGPUHashType
    {
        [SerializeField] private Vector2 _inputShift = new Vector2(0, 0);
        [SerializeField] private Vector2 _inputScale = new Vector2(1, 1);
        private Vector2 _preShift = new Vector2(0, 0);
        private Vector2 _preScale = new Vector2(1, 1);
        private Action _onChangedHashType;
        private Action _onChangedInput;
        private Action _onChangedOutputType;
        public virtual string ShaderPath => "";
        public virtual string HashType => "";
        public virtual string HashTypePre { get => ""; set => _ = ""; }
        public virtual string OutputType => "";
        public virtual string OutputTypePre { get => ""; set => _ = ""; }
        public Vector2 InputShift => _inputShift;
        public Vector2 InputScale => _inputScale;
        public Action OnChangedHashType { set => _onChangedHashType = value; }
        public void CheckHashType(){ if (HashType != HashTypePre) { _onChangedHashType(); HashTypePre = HashType; } }
        public Action OnChangedInput { set => _onChangedInput = value; }
        public void CheckInput(){ if (InputShift != _preShift || InputScale != _preScale) { _onChangedInput(); _preShift = InputShift; _preScale = InputScale; } }
        public Action OnChangedOutputType { set => _onChangedOutputType = value; }
        public void CheckOutputType(){ if (OutputType != OutputTypePre) { _onChangedOutputType(); OutputTypePre = OutputType; } }
    }

    [System.Serializable]
    public class GPUHashFloat1To1 : GPUHashTypeBase
    {
        private string _shaderPath => "Assets/GPUHash/Sample/Shaders/GPUHash1To1FloatVisualizer.shader";
        [SerializeField, Strings("hashwithoutsine11")]
        private string _hashType = "hashwithoutsine11";
        private string _hashTypePre = "hashwithoutsine11";
        [SerializeField, Strings("Float1")]
        private string _outputType = "Float1";
        private string _outputTypePre = "Float1";
        public override string ShaderPath => _shaderPath;
        public override string HashType => _hashType;
        public override string HashTypePre { get => _hashTypePre; set => _hashTypePre = value; }
        public override string OutputType => _outputType;
        public override string OutputTypePre { get => _outputTypePre; set => _outputTypePre = value; }
    }

    [System.Serializable]
    public class GPUHashUint1To1 : GPUHashTypeBase
    {
        private string _shaderPath => "Assets/GPUHash/Sample/Shaders/GPUHash1To1UIntVisualizer.shader";
        [SerializeField, Strings("bbs", "city", "esgtsa", "iqint1", "lcg", "murmur3", "pcg", "ranlim32", "superfast", "wang", "xorshift32", "xxhash32")]
        private string _hashType = "bbs";
        private string _hashTypePre = "bbs";
        [SerializeField, Strings("Float1")]
        private string _outputType = "Float1";
        private string _outputTypePre = "Float1";
        public override string ShaderPath => _shaderPath;
        public override string HashType => _hashType;
        public override string HashTypePre { get => _hashTypePre; set => _hashTypePre = value; }
        public override string OutputType => _outputType;
        public override string OutputTypePre { get => _outputTypePre; set => _outputTypePre = value; }
    }

    [System.Serializable]
    public class GPUHashFloat1To2 : GPUHashTypeBase
    {
        private string _shaderPath = "Assets/GPUHash/Sample/Shaders/GPUHash1To2FloatVisualizer.shader";
        [SerializeField, Strings("hashwithoutsine21")]
        private string _hashType = "hashwithoutsine21";
        private string _hashTypePre = "hashwithoutsine21";
        [SerializeField, Strings("Float1", "Float2")]
        private string _outputType = "Float2";
        private string _outputTypePre = "Float2";
        public override string ShaderPath => _shaderPath;
        public override string HashType => _hashType;
        public override string HashTypePre { get => _hashTypePre; set => _hashTypePre = value; }
        public override string OutputType => _outputType;
        public override string OutputTypePre { get => _outputTypePre; set => _outputTypePre = value; }
    }

    [System.Serializable]
    public class GPUHashFloat1To3 : GPUHashTypeBase
    {
        private string _shaderPath = "Assets/GPUHash/Sample/Shaders/GPUHash1To3FloatVisualizer.shader";
        [SerializeField, Strings("hashwithoutsine31")]
        private string _hashType = "hashwithoutsine31";
        private string _hashTypePre = "hashwithoutsine31";
        [SerializeField, Strings("Float1", "Float2", "Float3")]
        private string _outputType = "Float3";
        private string _outputTypePre = "Float3";
        public override string ShaderPath => _shaderPath;
        public override string HashType => _hashType;
        public override string HashTypePre { get => _hashTypePre; set => _hashTypePre = value; }
        public override string OutputType => _outputType;
        public override string OutputTypePre { get => _outputTypePre; set => _outputTypePre = value; }
    }

    [System.Serializable]
    public class GPUHashFloat1To4 : GPUHashTypeBase
    {
        private string _shaderPath = "Assets/GPUHash/Sample/Shaders/GPUHash1To4FloatVisualizer.shader";
        [SerializeField, Strings("hashwithoutsine41")]
        private string _hashType = "hashwithoutsine41";
        private string _hashTypePre = "hashwithoutsine41";
        [SerializeField, Strings("Float1", "Float2", "Float3", "Float4")]
        private string _outputType = "Float4";
        private string _outputTypePre = "Float4";
        public override string ShaderPath => _shaderPath;
        public override string HashType => _hashType;
        public override string HashTypePre { get => _hashTypePre; set => _hashTypePre = value; }
        public override string OutputType => _outputType;
        public override string OutputTypePre { get => _outputTypePre; set => _outputTypePre = value; }
    }

    [System.Serializable]
    public class GPUHashFloat2To1 : GPUHashTypeBase
    {
        private string _shaderPath = "Assets/GPUHash/Sample/Shaders/GPUHash2To1FloatVisualizer.shader";
        [SerializeField, Strings("fast", "hashwithoutsine12", "ign", "pseudo", "trig")]
        private string _hashType = "fast";
        private string _hashTypePre = "fast";
        [SerializeField, Strings("Float1")]
        private string _outputType = "Float1";
        private string _outputTypePre = "Float1";
        public override string ShaderPath => _shaderPath;
        public override string HashType => _hashType;
        public override string HashTypePre { get => _hashTypePre; set => _hashTypePre = value; }
        public override string OutputType => _outputType;
        public override string OutputTypePre { get => _outputTypePre; set => _outputTypePre = value; }
    }

    [System.Serializable]
    public class GPUHashUint2To1 : GPUHashTypeBase
    {
        private string _shaderPath = "Assets/GPUHash/Sample/Shaders/GPUHash2To1UIntVisualizer.shader";
        [SerializeField, Strings("city", "iqint3", "jkiss32", "murmur3", "superfast", "xxhash32")]
        private string _hashType = "city";
        private string _hashTypePre = "city";
        [SerializeField, Strings("Float1")]
        private string _outputType = "Float1";
        private string _outputTypePre = "Float1";
        public override string ShaderPath =>_shaderPath;
        public override string HashType => _hashType;
        public override string HashTypePre { get => _hashTypePre; set => _hashTypePre = value; }
        public override string OutputType => _outputType;
        public override string OutputTypePre { get => _outputTypePre; set => _outputTypePre = value; }
    }

    [System.Serializable]
    public class GPUHashFloat2To2 : GPUHashTypeBase
    {
        private string _shaderPath = "Assets/GPUHash/Sample/Shaders/GPUHash2To2FloatVisualizer.shader";
        [SerializeField, Strings("hashwithoutsine22")]
        private string _hashType = "hashwithoutsine22";
        private string _hashTypePre = "hashwithoutsine22";
        [SerializeField, Strings("Float1", "Float2")]
        private string _outputType = "Float2";
        private string _outputTypePre = "Float2";
        public override string ShaderPath => _shaderPath;
        public override string HashType => _hashType;
        public override string HashTypePre { get => _hashTypePre; set => _hashTypePre = value; }
        public override string OutputType => _outputType;
        public override string OutputTypePre { get => _outputTypePre; set => _outputTypePre = value; }
    }

    [System.Serializable]
    public class GPUHashUint2To2 : GPUHashTypeBase
    {
        private string _shaderPath = "Assets/GPUHash/Sample/Shaders/GPUHash2To2UintVisualizer.shader";
        [SerializeField, Strings("pcg2d", "tea")]
        private string _hashType = "pcg2d";
        private string _hashTypePre = "pcg2d";
        [SerializeField, Strings("Float1", "Float2")]
        private string _outputType = "Float2";
        private string _outputTypePre = "Float2";
        public override string ShaderPath =>_shaderPath;
        public override string HashType => _hashType;
        public override string HashTypePre { get => _hashTypePre; set => _hashTypePre = value; }
        public override string OutputType => _outputType;
        public override string OutputTypePre { get => _outputTypePre; set => _outputTypePre = value; }
    }

    [System.Serializable]
    public class GPUHashFloat2To3 : GPUHashTypeBase
    {
        private string _shaderPath = "Assets/GPUHash/Sample/Shaders/GPUHash2To3FloatVisualizer.shader";
        [SerializeField, Strings("hashwithoutsine32")]
        private string _hashType = "hashwithoutsine32";
        private string _hashTypePre = "hashwithoutsine32";
        [SerializeField, Strings("Float1", "Float2", "Float3")]
        private string _outputType = "Float3";
        private string _outputTypePre = "Float3";
        public override string ShaderPath => _shaderPath;
        public override string HashType => _hashType;
        public override string HashTypePre { get => _hashTypePre; set => _hashTypePre = value; }
        public override string OutputType => _outputType;
        public override string OutputTypePre { get => _outputTypePre; set => _outputTypePre = value; }
    }

    [System.Serializable]
    public class GPUHashFloat2To4 : GPUHashTypeBase
    {
        private string _shaderPath = "Assets/GPUHash/Sample/Shaders/GPUHash2To4FloatVisualizer.shader";
        [SerializeField, Strings("hashwithoutsine32")]
        private string _hashType = "hashwithoutsine32";
        private string _hashTypePre = "hashwithoutsine32";
        [SerializeField, Strings("Float1", "Float2", "Float3", "Float4")]
        private string _outputType = "Float4";
        private string _outputTypePre = "Float4";
        public override string ShaderPath => _shaderPath;
        public override string HashType => _hashType;
        public override string HashTypePre { get => _hashTypePre; set => _hashTypePre = value; }
        public override string OutputType => _outputType;
        public override string OutputTypePre { get => _outputTypePre; set => _outputTypePre = value; }
    }

    [System.Serializable]
    public class GPUHashFloat3To1 : GPUHashTypeBase
    {
        private string _shaderPath = "Assets/GPUHash/Sample/Shaders/GPUHash3To1FloatVisualizer.shader";
        [SerializeField, Strings("hashwithoutsine13")]
        private string _hashType = "hashwithoutsine13";
        private string _hashTypePre = "hashwithoutsine13";
        [SerializeField, Strings("Float1")]
        private string _outputType = "Float1";
        private string _outputTypePre = "Float1";
        public override string ShaderPath => _shaderPath;
        public override string HashType => _hashType;
        public override string HashTypePre { get => _hashTypePre; set => _hashTypePre = value; }
        public override string OutputType => _outputType;
        public override string OutputTypePre { get => _outputTypePre; set => _outputTypePre = value; }
    }

    [System.Serializable]
    public class GPUHashUint3To1 : GPUHashTypeBase
    {
        private string _shaderPath = "Assets/GPUHash/Sample/Shaders/GPUHash3To1UIntVisualizer.shader";
        [SerializeField, Strings("city", "superfast", "xxhash32", "murmur3")]
        private string _hashType = "city";
        private string _hashTypePre = "city";
        [SerializeField, Strings("Float1")]
        private string _outputType = "Float1";
        private string _outputTypePre = "Float1";
        public override string ShaderPath =>_shaderPath;
        public override string HashType => _hashType;
        public override string HashTypePre { get => _hashTypePre; set => _hashTypePre = value; }
        public override string OutputType => _outputType;
        public override string OutputTypePre { get => _outputTypePre; set => _outputTypePre = value; }
    }

    [System.Serializable]
    public class GPUHashFloat3To2 : GPUHashTypeBase
    {
        private string _shaderPath = "Assets/GPUHash/Sample/Shaders/GPUHash3To2FloatVisualizer.shader";
        [SerializeField, Strings("hashwithoutsine23")]
        private string _hashType = "hashwithoutsine23";
        private string _hashTypePre = "hashwithoutsine23";
        [SerializeField, Strings("Float1", "Float2")]
        private string _outputType = "Float2";
        private string _outputTypePre = "Float2";
        public override string ShaderPath => _shaderPath;
        public override string HashType => _hashType;
        public override string HashTypePre { get => _hashTypePre; set => _hashTypePre = value; }
        public override string OutputType => _outputType;
        public override string OutputTypePre { get => _outputTypePre; set => _outputTypePre = value; }
    }

    [System.Serializable]
    public class GPUHashFloat3To3 : GPUHashTypeBase
    {
        private string _shaderPath = "Assets/GPUHash/Sample/Shaders/GPUHash3To3FloatVisualizer.shader";
        [SerializeField, Strings("hashwithoutsine33")]
        private string _hashType = "hashwithoutsine33";
        private string _hashTypePre = "hashwithoutsine33";
        [SerializeField, Strings("Float1", "Float2", "Float3")]
        private string _outputType = "Float3";
        private string _outputTypePre = "Float3";
        public override string ShaderPath => _shaderPath;
        public override string HashType => _hashType;
        public override string HashTypePre { get => _hashTypePre; set => _hashTypePre = value; }
        public override string OutputType => _outputType;
        public override string OutputTypePre { get => _outputTypePre; set => _outputTypePre = value; }
    }

    [System.Serializable]
    public class GPUHashUint3To3 : GPUHashTypeBase
    {
        private string _shaderPath = "Assets/GPUHash/Sample/Shaders/GPUHash3To3UintVisualizer.shader";
        [SerializeField, Strings("iqint2", "pcg3d", "pcg3d16")]
        private string _hashType = "iqint2";
        private string _hashTypePre = "iqint2";
        [SerializeField, Strings("Float1", "Float2", "Float3")]
        private string _outputType = "Float3";
        private string _outputTypePre = "Float3";
        public override string ShaderPath =>_shaderPath;
        public override string HashType => _hashType;
        public override string HashTypePre { get => _hashTypePre; set => _hashTypePre = value; }
        public override string OutputType => _outputType;
        public override string OutputTypePre { get => _outputTypePre; set => _outputTypePre = value; }
    }

    [System.Serializable]
    public class GPUHashFloat3To4 : GPUHashTypeBase
    {
        private string _shaderPath = "Assets/GPUHash/Sample/Shaders/GPUHash3To4FloatVisualizer.shader";
        [SerializeField, Strings("hashwithoutsine43")]
        private string _hashType = "hashwithoutsine43";
        private string _hashTypePre = "hashwithoutsine43";
        [SerializeField, Strings("Float1", "Float2", "Float3", "Float4")]
        private string _outputType = "Float4";
        private string _outputTypePre = "Float4";
        public override string ShaderPath => _shaderPath;
        public override string HashType => _hashType;
        public override string HashTypePre { get => _hashTypePre; set => _hashTypePre = value; }
        public override string OutputType => _outputType;
        public override string OutputTypePre { get => _outputTypePre; set => _outputTypePre = value; }
    }

    [System.Serializable]
    public class GPUHashUint4To1 : GPUHashTypeBase
    {
        private string _shaderPath = "Assets/GPUHash/Sample/Shaders/GPUHash4To1UIntVisualizer.shader";
        [SerializeField, Strings("city", "hybridtaus", "murmur3", "superfast", "xorshift128", "xxhash32")]
        private string _hashType = "city";
        private string _hashTypePre = "city";
        [SerializeField, Strings("Float1")]
        private string _outputType = "Float1";
        private string _outputTypePre = "Float1";
        public override string ShaderPath =>_shaderPath;
        public override string HashType => _hashType;
        public override string HashTypePre { get => _hashTypePre; set => _hashTypePre = value; }
        public override string OutputType => _outputType;
        public override string OutputTypePre { get => _outputTypePre; set => _outputTypePre = value; }
    }

    [System.Serializable]
    public class GPUHashFloat4To4 : GPUHashTypeBase
    {
        private string _shaderPath = "Assets/GPUHash/Sample/Shaders/GPUHash4To4FloatVisualizer.shader";
        [SerializeField, Strings("hashwithoutsine44")]
        private string _hashType = "hashwithoutsine44";
        private string _hashTypePre = "hashwithoutsine44";
        [SerializeField, Strings("Float1", "Float2", "Float3", "Float4")]
        private string _outputType = "Float4";
        private string _outputTypePre = "Float4";
        public override string ShaderPath => _shaderPath;
        public override string HashType => _hashType;
        public override string HashTypePre { get => _hashTypePre; set => _hashTypePre = value; }
        public override string OutputType => _outputType;
        public override string OutputTypePre { get => _outputTypePre; set => _outputTypePre = value; }
    }

    [System.Serializable]
    public class GPUHashUint4to4 : GPUHashTypeBase
    {
        private string _shaderPath = "Assets/GPUHash/Sample/Shaders/GPUHash4To4UintVisualizer.shader";
        [SerializeField, Strings("md5", "pcg4d")]
        private string _hashType = "md5";
        private string _hashTypePre = "md5";
        [SerializeField, Strings("Float1", "Float2", "Float3", "Float4")]
        private string _outputType = "Float4";
        private string _outputTypePre = "Float4";
        public override string ShaderPath =>_shaderPath;
        public override string HashType => _hashType;
        public override string HashTypePre { get => _hashTypePre; set => _hashTypePre = value; }
        public override string OutputType => _outputType;
        public override string OutputTypePre { get => _outputTypePre; set => _outputTypePre = value; }
    }
}