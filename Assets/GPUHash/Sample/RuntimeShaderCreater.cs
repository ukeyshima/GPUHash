using System.Diagnostics;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;

namespace GPUHash.Sample
{
    public class RuntimeShaderCreator : IDisposable
    {
        private string _shaderString;
        private string _fileName;
        private bool _enableWrite = true;

        public RuntimeShaderCreator(string baseShaderString, string fileName, Action<Shader> callBack)
        {
            _shaderString = baseShaderString;
            _fileName = fileName;
            Create(baseShaderString, callBack);
        }

        private async Task Create(string shaderString, Action<Shader> callBack)
        {
            if (!_enableWrite) return;
            _enableWrite = false;
            await WriteAsync(Application.dataPath + "/Resources/" + _fileName + ".shader", shaderString);
            _enableWrite = true;

#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
#endif

            Shader shader = Resources.Load(_fileName) as Shader;
            callBack(shader);
        }

        public async Task Update(int columnNum, string replaceText, Action<Shader> callBack)
        {
            if (!_enableWrite) return;
            _shaderString = await ReadAsync(Application.dataPath + "/Resources/" + _fileName + ".shader");
            string[] shaderColumns = _shaderString.Split("\n");
            shaderColumns[columnNum] = replaceText;
            _shaderString = string.Join("\n", shaderColumns);
            _enableWrite = false;
            await WriteAsync(Application.dataPath + "/Resources/" + _fileName + ".shader", _shaderString);
            _enableWrite = true;

#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
#endif

            Shader shader = Resources.Load(_fileName) as Shader;
            callBack(shader);
        }

        public async Task Update(int[] columnNum, string[] replaceText, Action<Shader> callBack)
        {
            if (!_enableWrite) return;
            _shaderString = await ReadAsync(Application.dataPath + "/Resources/" + _fileName + ".shader");
            string[] shaderColumns = _shaderString.Split("\n");
            for (int i = 0; i < columnNum.Length; i++)
            {
                shaderColumns[columnNum[i]] = replaceText[i];
            }
            _shaderString = string.Join("\n", shaderColumns);
            _enableWrite = false;
            await WriteAsync(Application.dataPath + "/Resources/" + _fileName + ".shader", _shaderString);
            _enableWrite = true;

#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
#endif

            Shader shader = Resources.Load(_fileName) as Shader;
            callBack(shader);
        }

        public async Task Update(string regex, string replaceText, Action<Shader> callBack)
        {
            if (!_enableWrite) return;
            _shaderString = await ReadAsync(Application.dataPath + "/Resources/" + _fileName + ".shader");
            _shaderString = Regex.Replace(_shaderString, regex, replaceText);
            _enableWrite = false;
            await WriteAsync(Application.dataPath + "/Resources/" + _fileName + ".shader", _shaderString);
            _enableWrite = true;

#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
#endif

            Shader shader = Resources.Load(_fileName) as Shader;
            callBack(shader);
        }

        public void Dispose()
        {
            File.Delete(Application.dataPath + "/Resources/" + _fileName + ".shader");
        }

        private async Task<string> ReadAsync(string path)
        {
            using (var fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read))
            using (var sr = new StreamReader(fs))
            {
                var text = await sr.ReadToEndAsync();
                return text;
            }
        }

        private async Task WriteAsync(string path, string text)
        {
            using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            using (var sr = new StreamWriter(fs))
            {
                await sr.WriteLineAsync(text);
            }
        }
    }
}