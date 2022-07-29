using System.Security.AccessControl;
using System;
using UnityEngine;

namespace GPUHash.Sample
{
    enum GPUHashType
    {
        Float1To1,
        Uint1To1,
        Float1To2,
        Float1To3,
        Float1To4,
        Float2To1,
        Uint2To1,
        Float2To2,
        Uint2To2,
        Float2To3,
        Float2To4,
        Float3To1,
        Uint3To1,
        Float3To2,
        Float3To3,
        Uint3To3,
        Float3To4,
        Uint4To1,
        Float4To4,
        Uint4To4
    }

    public interface IGPUHashType
    {
        string HashTypeString { get; }
        string OutputTypeString { get; }
        Vector2 InputShift { get; }
        Vector2 InputScale { get; }
        Action OnChangedHashType { set; }
        Action OnChangedInput { set; }
        Action OnChangedOutputType { set; }
        void CheckHashType();
        void CheckInput();
        void CheckOutputType();
    }

    public abstract class GPUHashTypeBase : IGPUHashType
    {
        [SerializeField] protected Vector2 _inputShift = new Vector2(0, 0);
        [SerializeField] protected Vector2 _inputScale = new Vector2(1, 1);
        protected Vector2 _preShift = new Vector2(0, 0);
        protected Vector2 _preScale = new Vector2(1, 1);
        protected Action _onChangedHashType;
        protected Action _onChangedInput;
        protected Action _onChangedOutputType;
        public abstract string HashTypeString { get; }
        public abstract string OutputTypeString { get; }
        public Vector2 InputShift { get => _inputShift; }
        public Vector2 InputScale { get => _inputScale; }
        public Action OnChangedHashType { set => _onChangedHashType = value; }
        public Action OnChangedInput { set => _onChangedInput = value; }
        public Action OnChangedOutputType { set => _onChangedOutputType = value; }
        public abstract void CheckHashType();

        public void CheckInput()
        {
            if (InputShift == _preShift && InputScale == _preScale) return;
            _onChangedInput();
            _preShift = InputShift;
            _preScale = InputScale;
        }

        public abstract void CheckOutputType();
    }

    [System.Serializable]
    public class GPUHashFloat1To1 : GPUHashTypeBase
    {
        [SerializeField] private HashType _hashType = HashType.hashwithoutsine11;
        private HashType _hashTypePre = HashType.hashwithoutsine11;
        [SerializeField] private OutputType _outputType = OutputType.Float1;
        private OutputType _outputTypePre = OutputType.Float1;
        public override string HashTypeString { get => _hashType.ToString(); }
        public override string OutputTypeString { get => _outputType.ToString(); }

        public override void CheckOutputType()
        {
            if (_outputType == _outputTypePre) return;
            _onChangedOutputType();
            _outputTypePre = _outputType;
        }

        public override void CheckHashType()
        {
            if (_hashType == _hashTypePre) return;
            _onChangedHashType();
            _hashTypePre = _hashType;
        }

        private enum OutputType
        {
            Float1
        }

        private enum HashType
        {
            hashwithoutsine11
        }
    }

    [System.Serializable]
    public class GPUHashUint1To1 : GPUHashTypeBase
    {
        [SerializeField] private HashType _hashType = HashType.bbs;
        private HashType _hashTypePre = HashType.bbs;
        [SerializeField] private OutputType _outputType = OutputType.Float1;
        private OutputType _outputTypePre = OutputType.Float1;
        public override string HashTypeString { get => _hashType.ToString(); }
        public override string OutputTypeString { get => _outputType.ToString(); }

        public override void CheckOutputType()
        {
            if (_outputType == _outputTypePre) return;
            _onChangedOutputType();
            _outputTypePre = _outputType;
        }

        public override void CheckHashType()
        {
            if (_hashType == _hashTypePre) return;
            _onChangedHashType();
            _hashTypePre = _hashType;
        }

        private enum OutputType
        {
            Float1
        }

        private enum HashType
        {
            bbs,
            city,
            esgtsa,
            iqint1,
            lcg,
            murmur3,
            pcg,
            ranlim32,
            superfast,
            wang,
            xorshift32,
            xxhash32
        }
    }

    [System.Serializable]
    public class GPUHashFloat1To2 : GPUHashTypeBase
    {
        [SerializeField] private HashType _hashType = HashType.hashwithoutsine21;
        private HashType _hashTypePre = HashType.hashwithoutsine21;
        [SerializeField] private OutputType _outputType = OutputType.Float1;
        private OutputType _outputTypePre = OutputType.Float1;
        public override string HashTypeString { get => _hashType.ToString(); }
        public override string OutputTypeString { get => _outputType.ToString(); }

        public override void CheckOutputType()
        {
            if (_outputType == _outputTypePre) return;
            _onChangedOutputType();
            _outputTypePre = _outputType;
        }

        public override void CheckHashType()
        {
            if (_hashType == _hashTypePre) return;
            _onChangedHashType();
            _hashTypePre = _hashType;
        }

        private enum OutputType
        {
            Float1,
            Float2
        }

        private enum HashType
        {
            hashwithoutsine21
        }
    }

    [System.Serializable]
    public class GPUHashFloat1To3 : GPUHashTypeBase
    {
        [SerializeField] private HashType _hashType = HashType.hashwithoutsine31;
        private HashType _hashTypePre = HashType.hashwithoutsine31;
        [SerializeField] private OutputType _outputType = OutputType.Float1;
        private OutputType _outputTypePre = OutputType.Float1;
        public override string HashTypeString { get => _hashType.ToString(); }
        public override string OutputTypeString { get => _outputType.ToString(); }

        public override void CheckOutputType()
        {
            if (_outputType == _outputTypePre) return;
            _onChangedOutputType();
            _outputTypePre = _outputType;
        }

        public override void CheckHashType()
        {
            if (_hashType == _hashTypePre) return;
            _onChangedHashType();
            _hashTypePre = _hashType;
        }

        private enum OutputType
        {
            Float1,
            Float2,
            Float3
        }

        private enum HashType
        {
            hashwithoutsine31
        }

    }

    [System.Serializable]
    public class GPUHashFloat1To4 : GPUHashTypeBase
    {
        [SerializeField] private HashType _hashType = HashType.hashwithoutsine41;
        private HashType _hashTypePre = HashType.hashwithoutsine41;
        [SerializeField] private OutputType _outputType = OutputType.Float1;
        private OutputType _outputTypePre = OutputType.Float1;
        public override string HashTypeString { get => _hashType.ToString(); }
        public override string OutputTypeString { get => _outputType.ToString(); }

        public override void CheckOutputType()
        {
            if (_outputType == _outputTypePre) return;
            _onChangedOutputType();
            _outputTypePre = _outputType;
        }

        public override void CheckHashType()
        {
            if (_hashType == _hashTypePre) return;
            _onChangedHashType();
            _hashTypePre = _hashType;
        }

        private enum OutputType
        {
            Float1,
            Float2,
            Float3,
            Float4
        }

        private enum HashType
        {
            hashwithoutsine41
        }

    }

    [System.Serializable]
    public class GPUHashFloat2To1 : GPUHashTypeBase
    {
        [SerializeField] private HashType _hashType = HashType.fast;
        private HashType _hashTypePre = HashType.fast;
        [SerializeField] private OutputType _outputType = OutputType.Float1;
        private OutputType _outputTypePre = OutputType.Float1;
        public override string HashTypeString { get => _hashType.ToString(); }
        public override string OutputTypeString { get => _outputType.ToString(); }

        public override void CheckOutputType()
        {
            if (_outputType == _outputTypePre) return;
            _onChangedOutputType();
            _outputTypePre = _outputType;
        }

        public override void CheckHashType()
        {
            if (_hashType == _hashTypePre) return;
            _onChangedHashType();
            _hashTypePre = _hashType;
        }

        private enum OutputType
        {
            Float1
        }

        private enum HashType
        {
            fast,
            hashwithoutsine12,
            ign,
            pseudo,
            trig
        }

    }

    [System.Serializable]
    public class GPUHashUint2To1 : GPUHashTypeBase
    {
        [SerializeField] private HashType _hashType = HashType.city;
        private HashType _hashTypePre = HashType.city;
        [SerializeField] private OutputType _outputType = OutputType.Float1;

        private OutputType _outputTypePre = OutputType.Float1;
        public override string HashTypeString { get => _hashType.ToString(); }
        public override string OutputTypeString { get => _outputType.ToString(); }

        public override void CheckOutputType()
        {
            if (_outputType == _outputTypePre) return;
            _onChangedOutputType();
            _outputTypePre = _outputType;
        }

        public override void CheckHashType()
        {
            if (_hashType == _hashTypePre) return;
            _onChangedHashType();
            _hashTypePre = _hashType;
        }

        private enum OutputType
        {
            Float1
        }

        private enum HashType
        {
            city,
            iqint3,
            jkiss32,
            murmur3,
            superfast,
            xxhash32
        }

    }

    [System.Serializable]
    public class GPUHashFloat2To2 : GPUHashTypeBase
    {
        [SerializeField] private HashType _hashType = HashType.hashwithoutsine22;
        private HashType _hashTypePre = HashType.hashwithoutsine22;

        [SerializeField] private OutputType _outputType = OutputType.Float1;

        private OutputType _outputTypePre = OutputType.Float1;
        public override string HashTypeString { get => _hashType.ToString(); }
        public override string OutputTypeString { get => _outputType.ToString(); }

        public override void CheckOutputType()
        {
            if (_outputType == _outputTypePre) return;
            _onChangedOutputType();
            _outputTypePre = _outputType;
        }

        public override void CheckHashType()
        {
            if (_hashType == _hashTypePre) return;
            _onChangedHashType();
            _hashTypePre = _hashType;
        }

        private enum OutputType
        {
            Float1,
            Float2
        }

        private enum HashType
        {
            hashwithoutsine22
        }

    }

    [System.Serializable]
    public class GPUHashUint2To2 : GPUHashTypeBase
    {
        [SerializeField] private HashType _hashType = HashType.pcg2d;
        private HashType _hashTypePre = HashType.pcg2d;

        [SerializeField] private OutputType _outputType = OutputType.Float1;

        private OutputType _outputTypePre = OutputType.Float1;
        public override string HashTypeString { get => _hashType.ToString(); }
        public override string OutputTypeString { get => _outputType.ToString(); }

        public override void CheckOutputType()
        {
            if (_outputType == _outputTypePre) return;
            _onChangedOutputType();
            _outputTypePre = _outputType;
        }

        public override void CheckHashType()
        {
            if (_hashType == _hashTypePre) return;
            _onChangedHashType();
            _hashTypePre = _hashType;
        }

        private enum OutputType
        {
            Float1,
            Float2
        }

        private enum HashType
        {
            pcg2d,
            tea
        }

    }

    [System.Serializable]
    public class GPUHashFloat2To3 : GPUHashTypeBase
    {
        [SerializeField] private HashType _hashType = HashType.hashwithoutsine32;
        private HashType _hashTypePre = HashType.hashwithoutsine32;

        [SerializeField] private OutputType _outputType = OutputType.Float1;

        private OutputType _outputTypePre = OutputType.Float1;
        public override string HashTypeString { get => _hashType.ToString(); }
        public override string OutputTypeString { get => _outputType.ToString(); }

        public override void CheckOutputType()
        {
            if (_outputType == _outputTypePre) return;
            _onChangedOutputType();
            _outputTypePre = _outputType;
        }

        public override void CheckHashType()
        {
            if (_hashType == _hashTypePre) return;
            _onChangedHashType();
            _hashTypePre = _hashType;
        }

        private enum OutputType
        {
            Float1,
            Float2,
            Float3
        }

        private enum HashType
        {
            hashwithoutsine32
        }

    }

    [System.Serializable]
    public class GPUHashFloat2To4 : GPUHashTypeBase
    {
        [SerializeField] private HashType _hashType = HashType.hashwithoutsine42;
        private HashType _hashTypePre = HashType.hashwithoutsine42;

        [SerializeField] private OutputType _outputType = OutputType.Float1;

        private OutputType _outputTypePre = OutputType.Float1;
        public override string HashTypeString { get => _hashType.ToString(); }
        public override string OutputTypeString { get => _outputType.ToString(); }

        public override void CheckOutputType()
        {
            if (_outputType == _outputTypePre) return;
            _onChangedOutputType();
            _outputTypePre = _outputType;
        }

        public override void CheckHashType()
        {
            if (_hashType == _hashTypePre) return;
            _onChangedHashType();
            _hashTypePre = _hashType;
        }

        private enum OutputType
        {
            Float1,
            Float2,
            Float3,
            Float4
        }

        private enum HashType
        {
            hashwithoutsine42
        }

    }

    [System.Serializable]
    public class GPUHashFloat3To1 : GPUHashTypeBase
    {
        [SerializeField] private HashType _hashType = HashType.hashwithoutsine13;
        private HashType _hashTypePre = HashType.hashwithoutsine13;
        [SerializeField] private OutputType _outputType = OutputType.Float1;

        private OutputType _outputTypePre = OutputType.Float1;
        public override string HashTypeString { get => _hashType.ToString(); }
        public override string OutputTypeString { get => _outputType.ToString(); }

        public override void CheckOutputType()
        {
            if (_outputType == _outputTypePre) return;
            _onChangedOutputType();
            _outputTypePre = _outputType;
        }

        public override void CheckHashType()
        {
            if (_hashType == _hashTypePre) return;
            _onChangedHashType();
            _hashTypePre = _hashType;
        }

        private enum OutputType
        {
            Float1
        }

        private enum HashType
        {
            hashwithoutsine13
        }

    }

    [System.Serializable]
    public class GPUHashUint3To1 : GPUHashTypeBase
    {
        [SerializeField] private HashType _hashType = HashType.city;
        private HashType _hashTypePre = HashType.city;
        [SerializeField] private OutputType _outputType = OutputType.Float1;

        private OutputType _outputTypePre = OutputType.Float1;
        public override string HashTypeString { get => _hashType.ToString(); }
        public override string OutputTypeString { get => _outputType.ToString(); }

        public override void CheckOutputType()
        {
            if (_outputType == _outputTypePre) return;
            _onChangedOutputType();
            _outputTypePre = _outputType;
        }

        public override void CheckHashType()
        {
            if (_hashType == _hashTypePre) return;
            _onChangedHashType();
            _hashTypePre = _hashType;
        }

        private enum OutputType
        {
            Float1
        }

        private enum HashType
        {
            city,
            superfast,
            xxhash32,
            murmur3
        }

    }

    [System.Serializable]
    public class GPUHashFloat3To2 : GPUHashTypeBase
    {
        [SerializeField] private HashType _hashType = HashType.hashwithoutsine23;
        private HashType _hashTypePre = HashType.hashwithoutsine23;

        [SerializeField] private OutputType _outputType = OutputType.Float1;

        private OutputType _outputTypePre = OutputType.Float1;
        public override string HashTypeString { get => _hashType.ToString(); }
        public override string OutputTypeString { get => _outputType.ToString(); }

        public override void CheckOutputType()
        {
            if (_outputType == _outputTypePre) return;
            _onChangedOutputType();
            _outputTypePre = _outputType;
        }

        public override void CheckHashType()
        {
            if (_hashType == _hashTypePre) return;
            _onChangedHashType();
            _hashTypePre = _hashType;
        }

        private enum OutputType
        {
            Float1,
            Float2
        }

        private enum HashType
        {
            hashwithoutsine23
        }

    }

    [System.Serializable]
    public class GPUHashFloat3To3 : GPUHashTypeBase
    {
        [SerializeField] private HashType _hashType = HashType.hashwithoutsine33;
        private HashType _hashTypePre = HashType.hashwithoutsine33;

        [SerializeField] private OutputType _outputType = OutputType.Float1;

        private OutputType _outputTypePre = OutputType.Float1;
        public override string HashTypeString { get => _hashType.ToString(); }
        public override string OutputTypeString { get => _outputType.ToString(); }

        public override void CheckOutputType()
        {
            if (_outputType == _outputTypePre) return;
            _onChangedOutputType();
            _outputTypePre = _outputType;
        }

        public override void CheckHashType()
        {
            if (_hashType == _hashTypePre) return;
            _onChangedHashType();
            _hashTypePre = _hashType;
        }

        private enum OutputType
        {
            Float1,
            Float2,
            Float3
        }

        private enum HashType
        {
            hashwithoutsine33
        }

    }

    [System.Serializable]
    public class GPUHashUint3To3 : GPUHashTypeBase
    {
        [SerializeField] private HashType _hashType = HashType.iqint2;
        private HashType _hashTypePre = HashType.iqint2;

        [SerializeField] private OutputType _outputType = OutputType.Float1;

        private OutputType _outputTypePre = OutputType.Float1;
        public override string HashTypeString { get => _hashType.ToString(); }
        public override string OutputTypeString { get => _outputType.ToString(); }

        public override void CheckOutputType()
        {
            if (_outputType == _outputTypePre) return;
            _onChangedOutputType();
            _outputTypePre = _outputType;
        }

        public override void CheckHashType()
        {
            if (_hashType == _hashTypePre) return;
            _onChangedHashType();
            _hashTypePre = _hashType;
        }

        private enum OutputType
        {
            Float1,
            Float2,
            Float3
        }

        private enum HashType
        {
            iqint2,
            pcg3d,
            pcg3d16
        }

    }

    [System.Serializable]
    public class GPUHashFloat3To4 : GPUHashTypeBase
    {
        [SerializeField] private HashType _hashType = HashType.hashwithoutsine43;
        private HashType _hashTypePre = HashType.hashwithoutsine43;

        [SerializeField] private OutputType _outputType = OutputType.Float1;

        private OutputType _outputTypePre = OutputType.Float1;
        public override string HashTypeString { get => _hashType.ToString(); }
        public override string OutputTypeString { get => _outputType.ToString(); }

        public override void CheckOutputType()
        {
            if (_outputType == _outputTypePre) return;
            _onChangedOutputType();
            _outputTypePre = _outputType;
        }

        public override void CheckHashType()
        {
            if (_hashType == _hashTypePre) return;
            _onChangedHashType();
            _hashTypePre = _hashType;
        }

        private enum OutputType
        {
            Float1,
            Float2,
            Float3,
            Float4
        }

        private enum HashType
        {
            hashwithoutsine43
        }

    }

    [System.Serializable]
    public class GPUHashUint4To1 : GPUHashTypeBase
    {
        [SerializeField] private HashType _hashType = HashType.city;
        private HashType _hashTypePre = HashType.city;

        [SerializeField] private OutputType _outputType = OutputType.Float1;
        private OutputType _outputTypePre = OutputType.Float1;
        public override string HashTypeString { get => _hashType.ToString(); }
        public override string OutputTypeString { get => _outputType.ToString(); }

        public override void CheckOutputType()
        {
            if (_outputType == _outputTypePre) return;
            _onChangedOutputType();
            _outputTypePre = _outputType;
        }

        public override void CheckHashType()
        {
            if (_hashType == _hashTypePre) return;
            _onChangedHashType();
            _hashTypePre = _hashType;
        }

        private enum OutputType
        {
            Float1
        }

        private enum HashType
        {
            city,
            hybridtaus,
            murmur3,
            superfast,
            xorshift128,
            xxhash32
        }
    }

    [System.Serializable]
    public class GPUHashFloat4To4 : GPUHashTypeBase
    {
        [SerializeField] private HashType _hashType = HashType.hashwithoutsine44;
        private HashType _hashTypePre = HashType.hashwithoutsine44;

        [SerializeField] private OutputType _outputType = OutputType.Float1;

        private OutputType _outputTypePre = OutputType.Float1;
        public override string HashTypeString { get => _hashType.ToString(); }
        public override string OutputTypeString { get => _outputType.ToString(); }

        public override void CheckOutputType()
        {
            if (_outputType == _outputTypePre) return;
            _onChangedOutputType();
            _outputTypePre = _outputType;
        }

        public override void CheckHashType()
        {
            if (_hashType == _hashTypePre) return;
            _onChangedHashType();
            _hashTypePre = _hashType;
        }

        private enum OutputType
        {
            Float1,
            Float2,
            Float3,
            Float4
        }

        private enum HashType
        {
            hashwithoutsine44
        }
    }

    [System.Serializable]
    public class GPUHashUint4to4 : GPUHashTypeBase
    {
        [SerializeField] private HashType _hashType = HashType.md5;
        private HashType _hashTypePre = HashType.md5;

        [SerializeField] private OutputType _outputType = OutputType.Float1;

        private OutputType _outputTypePre = OutputType.Float1;
        public override string HashTypeString { get => _hashType.ToString(); }
        public override string OutputTypeString { get => _outputType.ToString(); }

        public override void CheckOutputType()
        {
            if (_outputType == _outputTypePre) return;
            _onChangedOutputType();
            _outputTypePre = _outputType;
        }

        public override void CheckHashType()
        {
            if (_hashType == _hashTypePre) return;
            _onChangedHashType();
            _hashTypePre = _hashType;
        }

        private enum OutputType
        {
            Float1,
            Float2,
            Float3,
            Float4
        }

        private enum HashType
        {
            md5,
            pcg4d
        }
    }

}