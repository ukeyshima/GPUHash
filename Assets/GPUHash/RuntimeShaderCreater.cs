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

        public RuntimeShaderCreator(string baseShaderString, string fileName)
        {
            _baseShaderString = baseShaderString;
            _fileName = fileName;
        }

        public async Task Create(string regex, string replaceText, Action<Shader> callBack)
        {
            string shaderString = Regex.Replace(_baseShaderString, regex, replaceText);
            await WriteAsync(Application.dataPath + "/Resources/" + _fileName + ".shader", shaderString);

#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
#endif

            Shader shader = Resources.Load(_fileName) as Shader;
            callBack(shader);
        }

        public async Task Update(string regex, string replaceText, Action<Shader> callBack)
        {
            string baseShaderString = await ReadAsync(Application.dataPath + "/Resources/" + _fileName + ".shader");
            string shaderString = Regex.Replace(baseShaderString, regex, replaceText);
            await WriteAsync(Application.dataPath + "/Resources/" + _fileName + ".shader", shaderString);

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