using System;
using UnityEngine;

namespace GPUHash.Sample
{
    public interface IGPUHashInputType
    {
        string InputTypeString { get; }
        IInputShiftAndScale InputShiftAndScale { get; }
        IInputTransformation InputTransformation { get; }
        Action OnChangedInputType { set; }
        void CheckInputType();
    }

    public abstract class GPUHashInputTypeBase : IGPUHashInputType
    {
        protected Action _onChangedInputType;

        public abstract string InputTypeString { get; }
        public abstract IInputShiftAndScale InputShiftAndScale { get; }
        public abstract IInputTransformation InputTransformation { get; }
        public Action OnChangedInputType { set => _onChangedInputType = value; }
        public abstract void CheckInputType();
    }

    public class GPUHashInput1 : GPUHashInputTypeBase
    {
        [SerializeField] private InputType _inputType;
        [SerializeReference] private IInputShiftAndScale _inputShiftAndScale = new InputShiftAndScale1();
        [SerializeReference] private IInputTransformation _inputTransformation = new InputTransformationOther();

        private InputType _inputTypePre;
        private IInputShiftAndScale _inputShiftAndScalePre = new InputShiftAndScale1();
        private IInputTransformation _inputTransformationPre = new InputTransformationOther();

        public override string InputTypeString { get => _inputType.ToString(); }
        public override IInputShiftAndScale InputShiftAndScale { get => _inputShiftAndScale; }
        public override IInputTransformation InputTransformation { get => _inputTransformation; }


        public override void CheckInputType()
        {
            if (_inputShiftAndScale.InputScale == _inputShiftAndScalePre.InputScale &&
                _inputShiftAndScale.InputShift == _inputShiftAndScalePre.InputShift &&
                _inputTransformation.InputCoefficient == _inputTransformationPre.InputCoefficient &&
                _inputType == _inputTypePre)
                return;
            _onChangedInputType();
            _inputShiftAndScalePre.InputScale = _inputShiftAndScale.InputScale;
            _inputShiftAndScalePre.InputShift = _inputShiftAndScale.InputShift;
            _inputTransformationPre.InputCoefficient = _inputTransformation.InputCoefficient;
            if (_inputType == _inputTypePre)
                return;
            _inputShiftAndScale = GetInputShiftAndScale();
            _inputShiftAndScalePre = GetInputShiftAndScale();
            _inputTransformation = GetInputTransformation();
            _inputTransformationPre = GetInputTransformation();
            _inputTypePre = _inputType;
        }

        public enum InputType
        {
            FloatOrUint1,
            FloatOrUint1Linear,
            FloatOrUint2Linear,
            FloatOrUint2XOR,
            FloatOrUint2Nest,
            FloatOrUint3Linear,
            FloatOrUint3XOR,
            FloatOrUint3Nest,
            FloatOrUint4XOR,
            FloatOrUint4Nest
        }

        private IInputShiftAndScale GetInputShiftAndScale()
        {
            return _inputType switch
            {
                InputType.FloatOrUint1 => new InputShiftAndScale1(),
                InputType.FloatOrUint1Linear => new InputShiftAndScale1(),
                InputType.FloatOrUint2Linear => new InputShiftAndScale2(),
                InputType.FloatOrUint2XOR => new InputShiftAndScale2(),
                InputType.FloatOrUint2Nest => new InputShiftAndScale2(),
                InputType.FloatOrUint3Linear => new InputShiftAndScale3(),
                InputType.FloatOrUint3XOR => new InputShiftAndScale3(),
                InputType.FloatOrUint3Nest => new InputShiftAndScale3(),
                InputType.FloatOrUint4XOR => new InputShiftAndScale4(),
                InputType.FloatOrUint4Nest => new InputShiftAndScale4()
            };
        }

        private IInputTransformation GetInputTransformation()
        {
            return _inputType switch
            {
                InputType.FloatOrUint1 => new InputTransformationOther(),
                InputType.FloatOrUint1Linear => new InputTransformationLinear1XOR2(),
                InputType.FloatOrUint2Linear => new InputTransformationLinear2XOR3(),
                InputType.FloatOrUint2XOR => new InputTransformationLinear1XOR2(),
                InputType.FloatOrUint2Nest => new InputTransformationOther(),
                InputType.FloatOrUint3Linear => new InputTransformationLinear3XOR4(),
                InputType.FloatOrUint3XOR => new InputTransformationLinear2XOR3(),
                InputType.FloatOrUint3Nest => new InputTransformationOther(),
                InputType.FloatOrUint4XOR => new InputTransformationLinear3XOR4(),
                InputType.FloatOrUint4Nest => new InputTransformationOther()
            };
        }
    }

    public class GPUHashInput2 : GPUHashInputTypeBase
    {
        [SerializeField] private InputType _inputType;
        [SerializeReference] private IInputShiftAndScale _inputShiftAndScale = new InputShiftAndScale2();
        [SerializeReference] private IInputTransformation _inputTransformation = new InputTransformationOther();

        private InputType _inputTypePre;
        private IInputShiftAndScale _inputShiftAndScalePre = new InputShiftAndScale2();
        private IInputTransformation _inputTransformationPre = new InputTransformationOther();

        public override string InputTypeString { get => _inputType.ToString(); }
        public override IInputShiftAndScale InputShiftAndScale { get => _inputShiftAndScale; }
        public override IInputTransformation InputTransformation { get => _inputTransformation; }

        public override void CheckInputType()
        {
            if (_inputShiftAndScale.InputScale == _inputShiftAndScalePre.InputScale &&
                _inputShiftAndScale.InputShift == _inputShiftAndScalePre.InputShift &&
                _inputTransformation.InputCoefficient == _inputTransformationPre.InputCoefficient &&
                _inputType == _inputTypePre)
                return;
            _onChangedInputType();
            _inputShiftAndScalePre.InputScale = _inputShiftAndScale.InputScale;
            _inputShiftAndScalePre.InputShift = _inputShiftAndScale.InputShift;
            _inputTransformationPre.InputCoefficient = _inputTransformation.InputCoefficient;
            if (_inputType == _inputTypePre)
                return;
            _inputShiftAndScale = GetInputShiftAndScale();
            _inputShiftAndScalePre = GetInputShiftAndScale();
            _inputTransformation = GetInputTransformation();
            _inputTransformationPre = GetInputTransformation();
            _inputTypePre = _inputType;
        }

        public enum InputType
        {
            FloatOrUint2,
            FloatOrUint2Linear,
            FloatOrUint3Linear,
            FloatOrUint3XOR,
            FloatOrUint4XOR,
            FloatOrUint4Nest
        }

        private IInputShiftAndScale GetInputShiftAndScale()
        {
            return _inputType switch
            {
                InputType.FloatOrUint2 => new InputShiftAndScale2(),
                InputType.FloatOrUint2Linear => new InputShiftAndScale2(),
                InputType.FloatOrUint3Linear => new InputShiftAndScale3(),
                InputType.FloatOrUint3XOR => new InputShiftAndScale3(),
                InputType.FloatOrUint4XOR => new InputShiftAndScale4(),
                InputType.FloatOrUint4Nest => new InputShiftAndScale4()
            };
        }

        private IInputTransformation GetInputTransformation()
        {
            return _inputType switch
            {
                InputType.FloatOrUint2 => new InputTransformationOther(),
                InputType.FloatOrUint2Linear => new InputTransformationLinear2XOR3(),
                InputType.FloatOrUint3Linear => new InputTransformationLinear3XOR4(),
                InputType.FloatOrUint3XOR => new InputTransformationLinear2XOR3(),
                InputType.FloatOrUint4XOR => new InputTransformationLinear3XOR4(),
                InputType.FloatOrUint4Nest => new InputTransformationOther()
            };
        }
    }

    public class GPUHashInput3 : GPUHashInputTypeBase
    {
        [SerializeField] private InputType _inputType;
        [SerializeReference] private IInputShiftAndScale _inputShiftAndScale = new InputShiftAndScale3();
        [SerializeReference] private IInputTransformation _inputTransformation = new InputTransformationOther();

        private InputType _inputTypePre;
        private IInputShiftAndScale _inputShiftAndScalePre = new InputShiftAndScale3();
        private IInputTransformation _inputTransformationPre = new InputTransformationOther();

        public override string InputTypeString { get => _inputType.ToString(); }
        public override IInputShiftAndScale InputShiftAndScale { get => _inputShiftAndScale; }
        public override IInputTransformation InputTransformation { get => _inputTransformation; }

        public override void CheckInputType()
        {
            if (_inputShiftAndScale.InputScale == _inputShiftAndScalePre.InputScale &&
                _inputShiftAndScale.InputShift == _inputShiftAndScalePre.InputShift &&
                _inputTransformation.InputCoefficient == _inputTransformationPre.InputCoefficient &&
                _inputType == _inputTypePre)
                return;
            _onChangedInputType();
            _inputShiftAndScalePre.InputScale = _inputShiftAndScale.InputScale;
            _inputShiftAndScalePre.InputShift = _inputShiftAndScale.InputShift;
            _inputTransformationPre.InputCoefficient = _inputTransformation.InputCoefficient;
            if (_inputType == _inputTypePre)
                return;
            _inputShiftAndScale = GetInputShiftAndScale();
            _inputShiftAndScalePre = GetInputShiftAndScale();
            _inputTransformation = GetInputTransformation();
            _inputTransformationPre = GetInputTransformation();
            _inputTypePre = _inputType;
        }

        public enum InputType
        {
            FloatOrUint3,
            FloatOrUint3Linear,
            FloatOrUint4XOR,
        }

        private IInputShiftAndScale GetInputShiftAndScale()
        {
            return _inputType switch
            {
                InputType.FloatOrUint3 => new InputShiftAndScale3(),
                InputType.FloatOrUint3Linear => new InputShiftAndScale3(),
                InputType.FloatOrUint4XOR => new InputShiftAndScale4(),
            };
        }

        private IInputTransformation GetInputTransformation()
        {
            return _inputType switch
            {
                InputType.FloatOrUint3 => new InputTransformationOther(),
                InputType.FloatOrUint3Linear => new InputTransformationLinear3XOR4(),
                InputType.FloatOrUint4XOR => new InputTransformationLinear3XOR4(),
            };
        }
    }

    public class GPUHashInput4 : GPUHashInputTypeBase
    {
        [SerializeField] private InputType _inputType;
        [SerializeReference] private IInputShiftAndScale _inputShiftAndScale = new InputShiftAndScale4();
        [SerializeReference] private IInputTransformation _inputTransformation = new InputTransformationOther();

        private IInputShiftAndScale _inputShiftAndScalePre = new InputShiftAndScale4();

        public override string InputTypeString { get => _inputType.ToString(); }
        public override IInputShiftAndScale InputShiftAndScale { get => _inputShiftAndScale; }
        public override IInputTransformation InputTransformation { get => _inputTransformation; }

        public override void CheckInputType()
        {
            if (_inputShiftAndScale.InputScale == _inputShiftAndScalePre.InputScale &&
                _inputShiftAndScale.InputShift == _inputShiftAndScalePre.InputShift)
                return;
            _onChangedInputType();
            _inputShiftAndScalePre.InputScale = _inputShiftAndScale.InputScale;
            _inputShiftAndScalePre.InputShift = _inputShiftAndScale.InputShift;
        }

        public enum InputType
        {
            FloatOrUint4
        }
    }

    public interface IInputShiftAndScale
    {
        Vector4 InputShift { get; set; }
        Vector4 InputScale { get; set; }
    }

    [Serializable]
    public class InputShiftAndScale1 : IInputShiftAndScale
    {
        [SerializeField] private float _inputShift = 0;
        [SerializeField] private float _inputScale = 1;

        public Vector4 InputShift { get => new Vector4(_inputShift, 0, 0, 0); set => _inputShift = value.x; }
        public Vector4 InputScale { get => new Vector4(_inputScale, 0, 0, 0); set => _inputScale = value.x; }
    }

    [Serializable]
    public class InputShiftAndScale2 : IInputShiftAndScale
    {
        [SerializeField] private Vector2 _inputShift = Vector2.zero;
        [SerializeField] private Vector2 _inputScale = Vector2.one;

        public Vector4 InputShift { get => new Vector4(_inputShift.x, _inputShift.y, 0, 0); set => _inputShift = value; }
        public Vector4 InputScale { get => new Vector4(_inputScale.x, _inputScale.y, 0, 0); set => _inputScale = value; }
    }

    [Serializable]
    public class InputShiftAndScale3 : IInputShiftAndScale
    {
        [SerializeField] private Vector3 _inputShift = Vector3.zero;
        [SerializeField] private Vector3 _inputScale = Vector3.one;

        public Vector4 InputShift { get => new Vector4(_inputShift.x, _inputShift.y, _inputShift.z, 0); set => _inputShift = value; }
        public Vector4 InputScale { get => new Vector4(_inputScale.x, _inputScale.y, _inputScale.z, 0); set => _inputScale = value; }
    }

    [Serializable]
    public class InputShiftAndScale4 : IInputShiftAndScale
    {
        [SerializeField] private Vector4 _inputShift = Vector4.zero;
        [SerializeField] private Vector4 _inputScale = Vector4.one;

        public Vector4 InputShift { get => _inputShift; set => _inputShift = value; }
        public Vector4 InputScale { get => _inputScale; set => _inputScale = value; }
    }

    public interface IInputTransformation
    {
        Vector4 InputCoefficient { get; set; }
    }

    [Serializable]
    public class InputTransformationXOR1 : IInputTransformation
    {
        [SerializeField] private float _a = 1;

        public Vector4 InputCoefficient { get => new Vector4(_a, 0, 0, 0); set => _a = value.x; }
    }

    [Serializable]
    public class InputTransformationLinear1XOR2 : IInputTransformation
    {
        [SerializeField] private float _a = 1;
        [SerializeField] private float _b = 1;

        public Vector4 InputCoefficient
        {
            get => new Vector4(_a, _b, 0, 0);
            set
            {
                _a = value.x;
                _b = value.y;
            }
        }
    }

    [Serializable]
    public class InputTransformationLinear2XOR3 : IInputTransformation
    {
        [SerializeField] private float _a = 1;
        [SerializeField] private float _b = 1;
        [SerializeField] private float _c = 1;

        public Vector4 InputCoefficient
        {
            get => new Vector4(_a, _b, _c, 0);
            set
            {
                _a = value.x;
                _b = value.y;
                _c = value.z;
            }
        }
    }

    [Serializable]
    public class InputTransformationLinear3XOR4 : IInputTransformation
    {
        [SerializeField] private float _a = 1;
        [SerializeField] private float _b = 1;
        [SerializeField] private float _c = 1;
        [SerializeField] private float _d = 1;

        public Vector4 InputCoefficient
        {
            get => new Vector4(_a, _b, _c, _d);
            set
            {
                _a = value.x;
                _b = value.y;
                _c = value.z;
                _d = value.w;
            }
        }
    }

    [Serializable]
    public class InputTransformationOther : IInputTransformation
    {
        public Vector4 InputCoefficient { get => Vector4.one; set { } }
    }
}