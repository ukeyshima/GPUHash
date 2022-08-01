using System;
using System.Text.RegularExpressions;
using System.IO;
using JetBrains.Annotations;
using UnityEngine;

namespace GPUHash.Sample
{
    public class GPUHashManager : MonoBehaviour
    {
        private const int SHADER_SEED_COLUMN = 39;
        private const int SHADER_INPUT_COLUMN = 40;
        private const int SHADER_INPUT_TRANSFORMATION_COLUMN = 41;
        private const int SHADER_HASH_FUNCTION_COLUMN = 42;
        private const int SHADER_LAST_COLUMN = 43;
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
            _iGPUHashType.InputType.CheckInputType();
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
            _iGPUHashType.InputType.OnChangedInputType = () => _runTimeShaderCreator.Update(
                new int[] { SHADER_INPUT_COLUMN, SHADER_INPUT_TRANSFORMATION_COLUMN },
                new string[] { GetShaderInputColumn(), GetShaderInputTransformationColumn() },
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
            shaderColumns[SHADER_SEED_COLUMN] = GetShaderSeedColumn();
            baseShaderString = string.Join("\n", shaderColumns);
            shaderColumns = baseShaderString.Split("\n");
            shaderColumns[SHADER_INPUT_COLUMN] = GetShaderInputColumn();
            baseShaderString = string.Join("\n", shaderColumns);
            shaderColumns = baseShaderString.Split("\n");
            shaderColumns[SHADER_INPUT_TRANSFORMATION_COLUMN] = GetShaderInputTransformationColumn();
            baseShaderString = string.Join("\n", shaderColumns);
            shaderColumns = baseShaderString.Split("\n");
            shaderColumns[SHADER_HASH_FUNCTION_COLUMN] = GetShaderHashFunctionColumn();
            baseShaderString = string.Join("\n", shaderColumns);
            shaderColumns = baseShaderString.Split("\n");
            shaderColumns[SHADER_LAST_COLUMN] = GetShaderLastColumn(_iGPUHashType.OutputTypeString);
            baseShaderString = string.Join("\n", shaderColumns);
            return baseShaderString;
        }

        private string GetShaderSeedColumn()
        {
            return _gpuHashType.ToString().Contains("Float")
                ? "                float4 seed = float4(i.uv.x, i.uv.y, uint(i.uv.x) ^ uint(i.uv.y), i.uv.x + i.uv.y);"
                : "                uint4 seed = uint4(i.vertex.x, i.vertex.y, uint(i.vertex.x) ^ uint(i.vertex.y), i.vertex.x + i.vertex.y);";
        }

        private string GetShaderInputColumn()
        {
            string type = _gpuHashType.ToString().Contains("Float") ? "float" : "uint";
            Vector4 inputScale = _iGPUHashType.InputType.InputShiftAndScale.InputScale;
            Vector4 inputShift = _iGPUHashType.InputType.InputShiftAndScale.InputShift;

            string scale =
                $"{type}4({inputScale.x}, {inputScale.y}, {inputScale.z}, {inputScale.w})";

            string shift =
                $"{type}4({inputShift.x}, {inputShift.y}, {inputShift.z}, {inputShift.w})";

            return $"                {type}4 input = (seed * {scale} + {shift});";
        }

        private string GetShaderInputTransformationColumn()
        {
            string inputTypeString = _iGPUHashType.InputType.InputTypeString;
            int inputTypeNumDefault = Int32.Parse(Regex.Match(_gpuHashType.ToString(), @"\dTo\d").Value[0].ToString());
            int inputTypeNum = Int32.Parse(Regex.Match(inputTypeString, @"FloatOrUint\d").Value[^1].ToString());
            string hashType = _iGPUHashType.HashTypeString;
            string type = _gpuHashType.ToString().Contains("Float") ? "float" : "uint";
            int linearOrXOROrNestOrOther = inputTypeString.Contains("Linear") ? 0 :
                inputTypeString.Contains("XOR") ? 1 :
                inputTypeString.Contains("Nest") ? 2 : 3;

            Vector4 inputCoefficient = _iGPUHashType.InputType.InputTransformation.InputCoefficient;

            return linearOrXOROrNestOrOther switch
            {
                0 => inputTypeNumDefault switch
                {
                    1 => inputTypeNum switch
                    {
                        1 => $"                input = {type}4({inputCoefficient.x} * input.x + {inputCoefficient.y}, 0, 0, 0);",
                        2 =>
                            $"                input = {type}4({inputCoefficient.x} * input.x + {inputCoefficient.y} * input.y + {inputCoefficient.z}, 0, 0, 0);",
                        3 =>
                            $"                input = {type}4({inputCoefficient.x} * input.x + {inputCoefficient.y} * input.y + {inputCoefficient.z} * input.z + {inputCoefficient.w}, 0, 0, 0);",
                    },
                    2 => inputTypeNum switch
                    {
                        2 =>
                            $"                input = {type}4({inputCoefficient.x} * input.x + {inputCoefficient.y} * input.y + {inputCoefficient.z}, input.y, 0, 0);",
                        3 =>
                            $"                input = {type}4({inputCoefficient.x} * input.x + {inputCoefficient.y} * input.y + {inputCoefficient.z} * input.z + {inputCoefficient.w}, input.y, 0, 0);",
                    },
                    3 => inputTypeNum switch
                    {
                        3 =>
                            $"                input = {type}4({inputCoefficient.x} * input.x + {inputCoefficient.y} * input.y + {inputCoefficient.z} * input.z + {inputCoefficient.w}, input.y, input.z, 0);",
                    },
                },
                1 => inputTypeNumDefault switch
                {
                    1 => inputTypeNum switch
                    {
                        2 => $"                input = {type}4(uint({inputCoefficient.x} * input.x) ^ uint({inputCoefficient.y} * input.y), 0, 0, 0);",
                        3 =>
                            $"                input = {type}4(uint({inputCoefficient.x} * input.x) ^ uint({inputCoefficient.y} * input.y) ^ uint({inputCoefficient.z} * input.z), 0, 0, 0);",
                        4 =>
                            $"                input = {type}4(uint({inputCoefficient.x} * input.x) ^ uint({inputCoefficient.y} * input.y) ^ uint({inputCoefficient.z} * input.z) ^ uint({inputCoefficient.w} * input.w), 0, 0, 0);",
                    },
                    2 => inputTypeNum switch
                    {
                        3 =>
                            $"                input = {type}4(uint({inputCoefficient.x} * input.x) ^ uint({inputCoefficient.y} * input.y) ^ uint({inputCoefficient.z} * input.z), input.y, 0, 0);",
                        4 =>
                            $"                input = {type}4(uint({inputCoefficient.x} * input.x) ^ uint({inputCoefficient.y} * input.y) ^ uint({inputCoefficient.z} * input.z) ^ uint({inputCoefficient.w} * input.w), input.y, 0, 0);",
                    },
                    3 => inputTypeNum switch
                    {
                        4 =>
                            $"                input = {type}4(uint({inputCoefficient.x} * input.x) ^ uint({inputCoefficient.y} * input.y) ^ uint({inputCoefficient.z} * input.z) ^ uint({inputCoefficient.w} * input.w), input.y, 0, 0);",
                    },
                },
                2 => inputTypeNumDefault switch
                {
                    1 => inputTypeNum switch
                    {
                        2 => $"                input = {type}4(input.x + {hashType}(input.y), 0, 0, 0);",
                        3 => $"                input = {type}4(input.x + {hashType}(input.y + {hashType}(input.z)), 0, 0, 0);",
                        4 => $"                input = {type}4(input.x + {hashType}(input.y + {hashType}(input.z + {hashType}(input.w))), 0, 0, 0);",
                    },
                    2 => inputTypeNum switch
                    {
                        4 => $"                input = {type}4(input.xy + {hashType}(input.zw), 0, 0);",
                    },
                },
                3 => $"                input = {type}4(input.x, input.y, input.z, input.w);"
            };
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
                3 => "input.xyz",
                4 => "input",
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
                    "Float1" => "                return float4(c.xxx, 1.0);",
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