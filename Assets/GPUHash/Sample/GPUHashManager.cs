using System.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using UnityEngine;

namespace GPUHash.Sample
{
    public class GPUHashManager : MonoBehaviour
    {
        [SerializeField] private GPUHashType _gpuHashType;
        [SerializeField] private QuadRenderer _quadRenderer;
        [SerializeReference] private IGPUHashType _iGPUHashType;

        private GPUHashType _preGPUHashType;
        private RuntimeShaderCreator _runTimeShaderCreator;

        private void Start()
        {
            InitIGPUHashType();
            _preGPUHashType = _gpuHashType;
        }

        private void Update()
        {
            if (_preGPUHashType != _gpuHashType)
            {
                InitIGPUHashType();
                _preGPUHashType = _gpuHashType;
            }
            _iGPUHashType.CheckHashType();
            _iGPUHashType.CheckInput();
            _iGPUHashType.CheckOutputType();
        }

        private void OnDestroy()
        {
            _runTimeShaderCreator.Dispose();
        }

        private void InitIGPUHashType()
        {
            _iGPUHashType = CreateIGPUHashTypeInstance(_gpuHashType);
            _runTimeShaderCreator = new RuntimeShaderCreator(File.ReadAllText(_iGPUHashType.ShaderPath), "GPUHashVisualizer");
            _runTimeShaderCreator.Create(_iGPUHashType.HashTypeDefault, _iGPUHashType.HashType, shader => _quadRenderer.Mat = new Material(shader));
            _iGPUHashType.OnChangedHashType = () => _runTimeShaderCreator.Update(_iGPUHashType.HashTypePre, _iGPUHashType.HashType, shader => _quadRenderer.Mat = new Material(shader));
            _iGPUHashType.OnChangedOutputType = () => _runTimeShaderCreator.Update(@"return float4\(.*\);", GetFragmentShaderLastCode(_gpuHashType, _iGPUHashType.OutputType), shader => _quadRenderer.Mat = new Material(shader));

            if(_gpuHashType.ToString().Contains("Float"))
            {
                _iGPUHashType.OnChangedInput = () => _runTimeShaderCreator.Update(@"i\.uv.*;(\n|\r)", $"i.uv * float2({_iGPUHashType.InputScale.x}, {_iGPUHashType.InputScale.y}) + float2({_iGPUHashType.InputShift.x}, {_iGPUHashType.InputShift.y});\n", shader => _quadRenderer.Mat = new Material(shader));
            }
            else if(_gpuHashType.ToString().Contains("Uint"))
            {
                _iGPUHashType.OnChangedInput = () => _runTimeShaderCreator.Update(@"i\.vertex\.xy.*;(\n|\r)", $"i.vertex.xy * uint2({_iGPUHashType.InputScale.x}, {_iGPUHashType.InputScale.y}) + uint2({_iGPUHashType.InputShift.x}, {_iGPUHashType.InputShift.y});\n", shader => _quadRenderer.Mat = new Material(shader));
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

        private string GetFragmentShaderLastCode(GPUHashType gpuHashType, string outputType)
        {
            int outputTypeDefault = Int32.Parse(Regex.Match(gpuHashType.ToString(), @"\dTo\d").Value[^1].ToString());

            return outputTypeDefault switch{
                1 => outputType switch
                {
                    "Float1" => "return float4(c, c, c, 1.0);",
                },
                2 => outputType switch
                {
                    "Float1" => "return float4(c.xxx, 1.0);",
                    "Float2" => "return float4(c, 1.0, 1.0);",
                },
                3 => outputType switch
                {
                    "Float1" => "return float4(c.xxx, 1.0);",
                    "Float2" => "return float4(c.xy, 1.0, 1.0);",
                    "Float3" => "return float4(c, 1.0);",
                },
                4 => outputType switch
                {
                    "Float1" => "return float4(c.xxx, 1.0);",
                    "Float2" => "return float4(c.xy, 1.0, 1.0);",
                    "Float3" => "return float4(c.xyz, 1.0);",
                    "Float4" => "return float4(c);",
                },
            };
        }
    }
}