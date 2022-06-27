using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GPUHash.Sample
{
    public class GPUHashManager : MonoBehaviour
    {
        [SerializeField] private GPUHashType _gpuHashType;
        [SerializeField] private Material _mat;
        [SerializeReference] private IGPUHashType _iGPUHashType;

        private GPUHashType _preGPUHashType;

        private void Start()
        {
            _iGPUHashType = CreateIGPUHashTypeInstance(_gpuHashType);
            _preGPUHashType = _gpuHashType;
        }

        private void Update()
        {
            if (_preGPUHashType != _gpuHashType)
            {
                _iGPUHashType = CreateIGPUHashTypeInstance(_gpuHashType);
                _preGPUHashType = _gpuHashType;
            }
        }

        private IGPUHashType CreateIGPUHashTypeInstance(GPUHashType gpuHashType)
        {
            return gpuHashType switch
            {
                GPUHashType.Float1To1 => new GPUHashFloat1To1(),
                GPUHashType.Uint1To1 => new GPUHashUint1To1(),
                GPUHashType.Float1To2 => new GPUHashFloat1To2(),
                GPUHashType.Float1To3 => new GPUHashFloat1To3(),
                GPUHashType.Float1To4 => new GPUHashFloat1To4(),
                GPUHashType.Float2To1 => new GPUHashFloat2To1(),
                GPUHashType.Uint2To1 => new GPUHashUint2To1(),
                GPUHashType.Float2To2 => new GPUHashFloat2To2(),
                GPUHashType.Uint2To2 => new GPUHashUint2To2(),
                GPUHashType.Float2To3 => new GPUHashFloat2To3(),
                GPUHashType.Float2To4 => new GPUHashFloat2To4(),
                GPUHashType.Float3To1 => new GPUHashFloat3To1(),
                GPUHashType.Uint3To1 => new GPUHashUint3To1(),
                GPUHashType.Float3To2 => new GPUHashFloat3To2(),
                GPUHashType.Float3To3 => new GPUHashFloat3To3(),
                GPUHashType.Uint3To3 => new GPUHashUint3To3(),
                GPUHashType.Float3To4 => new GPUHashFloat3To4(),
                GPUHashType.Uint4To1 => new GPUHashUint4To1(),
                GPUHashType.Float4To4 => new GPUHashFloat4To4(),
                GPUHashType.Uint4To4 => new GPUHashUint4to4()
            };
        }
    }
}