using System;
using System.Text.RegularExpressions;
using System.IO;
using JetBrains.Annotations;
using UnityEngine;

namespace GPUHash.Sample
{
    public class GPUHashManager : MonoBehaviour
    {
        private const int SHADER_INPUT_COLUMN = 39;
        private const int SHADER_HASH_FUNCTION_COLUMN = 40;
        private const int SHADER_LAST_COLUMN = 41;
        private const string BASE_SHADERPATH = "Assets/GPUHash/Sample/Shaders/GPUHashVisualizer.shader";

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

            string baseShaderString = GetBaseShaderString();
            
            _runTimeShaderCreator = new RuntimeShaderCreator(baseShaderString,
                "GPUHashVisualizer",
                shader =>
                {
                    Destroy(_quadRenderer.Mat);
                    _quadRenderer.Mat = new Material(shader);
                });

            _iGPUHashType.OnChangedHashType = () => _runTimeShaderCreator.Update(SHADER_HASH_FUNCTION_COLUMN,
                GetShaderHashFunctionColumn(),
                shader =>
                {
                    Destroy(_quadRenderer.Mat);
                    _quadRenderer.Mat = new Material(shader);
                });
            _iGPUHashType.OnChangedOutputType = () => _runTimeShaderCreator.Update(SHADER_LAST_COLUMN,
                GetShaderLastColumn(_iGPUHashType.OutputTypeString),
                shader =>
                {
                    Destroy(_quadRenderer.Mat);
                    _quadRenderer.Mat = new Material(shader);
                });
            _iGPUHashType.OnChangedInput = () => _runTimeShaderCreator.Update(SHADER_INPUT_COLUMN,
                GetShaderInputColumn(),
                shader =>
                {
                    Destroy(_quadRenderer.Mat);
                    _quadRenderer.Mat = new Material(shader);
                });
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
                GPUHashType.Uint4To4 => new GPUHashUint4to4(),
                _ => throw new ArgumentOutOfRangeException(nameof(gpuHashType), gpuHashType, null)
            };
        }

        private string GetBaseShaderString()
        {
            string baseShaderString = File.ReadAllText(BASE_SHADERPATH);
            string[] shaderColumns = baseShaderString.Split("\n");
            shaderColumns[SHADER_INPUT_COLUMN] = GetShaderInputColumn();
            baseShaderString = string.Join("\n", shaderColumns);
            shaderColumns = baseShaderString.Split("\n");
            shaderColumns[SHADER_HASH_FUNCTION_COLUMN] = GetShaderHashFunctionColumn();
            baseShaderString = string.Join("\n", shaderColumns);
            shaderColumns = baseShaderString.Split("\n");
            shaderColumns[SHADER_LAST_COLUMN] = GetShaderLastColumn(_iGPUHashType.OutputTypeString);
            baseShaderString = string.Join("\n", shaderColumns);
            return baseShaderString;
        }
        
        private string GetShaderInputColumn()
        {
            (string type, string uvOrCoord) = _gpuHashType.ToString().Contains("Float") ? ("float", "i.uv") : ("uint", "i.vertex.xy");

            return
                $"                {type}{2} input = {uvOrCoord} * {type}{2}({_iGPUHashType.InputScale.x}, {_iGPUHashType.InputScale.y}) + {type}{2}({_iGPUHashType.InputShift.x}, {_iGPUHashType.InputShift.y});";
        }

        private string GetShaderHashFunctionColumn()
        {
            int inputTypeNumDefault = Int32.Parse(Regex.Match(_gpuHashType.ToString(), @"\dTo\d").Value[0].ToString());
            int outputTypeNumDefault = Int32.Parse(Regex.Match(_gpuHashType.ToString(), @"\dTo\d").Value[^1].ToString());
            (string type, string toFloat) = _gpuHashType.ToString().Contains("Float") ? ("float", "") : ("uint", " / float(0xffffffffu)");

            string argument = inputTypeNumDefault switch
            {
                1 => "input.x",
                2 => "input.xy",
                3 => "input.xy, 1",
                4 => "input.xy, 1, 1",
                _ => throw new ArgumentOutOfRangeException()
            };

            return
                $"                float{(outputTypeNumDefault == 1 ? "" : outputTypeNumDefault)} c = {_iGPUHashType.HashTypeString}({type}{(inputTypeNumDefault == 1 ? "" : inputTypeNumDefault)}({argument})){toFloat};";
        }

        private string GetShaderLastColumn(string outputType)
        {
            int outputTypeNumDefault = Int32.Parse(Regex.Match(_gpuHashType.ToString(), @"\dTo\d").Value[^1].ToString());

            return outputTypeNumDefault switch
            {
                1 => outputType switch
                {
                    "Float1" => "                return float4(c, c, c, 1.0);",
                },
                2 => outputType switch
                {
                    "Float1" => "                return float4(c.xxx, 1.0);",
                    "Float2" => "                return float4(c, 1.0, 1.0);",
                },
                3 => outputType switch
                {
                    "Float1" => "                return float4(c.xxx, 1.0);",
                    "Float2" => "                return float4(c.xy, 1.0, 1.0);",
                    "Float3" => "                return float4(c, 1.0);",
                },
                4 => outputType switch
                {
                    "Float1" => "                return float4(c.xxx, 1.0);",
                    "Float2" => "                return float4(c.xy, 1.0, 1.0);",
                    "Float3" => "                return float4(c.xyz, 1.0);",
                    "Float4" => "                return float4(c);",
                },
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}