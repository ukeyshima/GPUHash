using System.Diagnostics;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;

namespace GPUHash.Sample
{
    public class RuntimeShaderCreator : IDisposable
    {
        private string _baseShaderString;
        private string _fileName;
        private bool _enableWrite = true;

        public bool EnableWrite { get => _enableWrite; }

        public RuntimeShaderCreator(string baseShaderString, string fileName)
        {
            _baseShaderString = baseShaderString;
            _fileName = fileName;
        }

        public async Task Create(string regex, string replaceText, Action<Shader> callBack)
        {
            if(!_enableWrite) return;
            string shaderString = Regex.Replace(_baseShaderString, regex, replaceText);
            _enableWrite = false;
            await WriteAsync(Application.dataPath + "/Resources/" + _fileName + ".shader", shaderString);
            _enableWrite = true;

#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
#endif

            Shader shader = Resources.Load(_fileName) as Shader;
            callBack(shader);
        }

        public async Task Update(string regex, string replaceText, Action<Shader> callBack)
        {
            if(!_enableWrite) return;
            string baseShaderString = await ReadAsync(Application.dataPath + "/Resources/" + _fileName + ".shader");
            string shaderString = Regex.Replace(baseShaderString, regex, replaceText);
            _enableWrite = false;
            await WriteAsync(Application.dataPath + "/Resources/" + _fileName + ".shader", shaderString);
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