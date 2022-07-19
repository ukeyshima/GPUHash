using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GPUHash.Sample
{
    public class QuadRenderer : MonoBehaviour
    {
        [SerializeField] private Mesh _quad;
        [SerializeField] private Shader _shader;

        private Material _mat;
        private Matrix4x4 _quadMatrix = Matrix4x4.identity;
        private Vector2Int _screenResolution = new Vector2Int(0, 0);

        public Material Mat { get => _mat; set => _mat = value; }

        private void Awake()
        {
            _mat = new Material(_shader);
        }

        private void Update()
        {
            Graphics.DrawMesh(_quad, _quadMatrix, _mat, 0);

            if (_screenResolution.x == Screen.width && _screenResolution.y == Screen.height) return;
            OnChangedResolution();
        }

        private void OnChangedResolution()
        {
            _screenResolution = new Vector2Int(Screen.width, Screen.height);
            float aspectRatio = (float)_screenResolution.x / (float)_screenResolution.y;
            float height = Camera.main.orthographicSize * 2;
            float width = height * aspectRatio;
            _quadMatrix.SetTRS(Vector3.zero, Quaternion.identity, new Vector3(width, Camera.main.orthographicSize * 2, 0));
        }

        private void OnDestroy()
        {
            Destroy(_mat);
        }
    }

}
