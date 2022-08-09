using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace CodeBase.Weapons
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(Renderer))]
    public class FieldOfView : MonoBehaviour
    {
        [SerializeField] private int raysCount;
        [Range(0f, 1f)] [SerializeField] private float fadeColor = 0.3f;

        protected Mesh Mesh;
        protected MeshFilter MeshFilter;
        
        protected float FieldOfViewDistance;
        protected float FieldOfViewAngle;
        
        private Renderer _fieldOfViewRenderer;

        private MaterialPropertyBlock _materialPropertyBlock;
        private Color _originalMaterialColor;
        
        public void Initialize(float distance, float angle)
        {
            _fieldOfViewRenderer = GetComponent<Renderer>();
            
            MeshFilter = GetComponent<MeshFilter>();
            Mesh = new Mesh();
            
            _materialPropertyBlock = new MaterialPropertyBlock();
            _originalMaterialColor = _fieldOfViewRenderer.material.color;
            
            FieldOfViewAngle = angle;
            FieldOfViewDistance = distance;
        }

        private void Start()
        {
            DrawFieldOfView();
            HideFieldOfView();
        }

        public bool IsPositionInTheFieldOfView(Vector3 targetPosition)
        {
            if (Vector3.Distance(transform.position, targetPosition) <= FieldOfViewDistance)
            {
                var direction = (targetPosition - transform.position).normalized;
                if (Vector3.Angle(transform.forward, direction) < FieldOfViewAngle / 2f)
                {
                    return true;
                }
            }

            return false;
        }

        [Button]
        public void ShowFieldOfView()
        {
            _originalMaterialColor.a = fadeColor;
        
            _fieldOfViewRenderer.GetPropertyBlock(_materialPropertyBlock);
            _materialPropertyBlock.SetColor("_Color", _originalMaterialColor);
            _fieldOfViewRenderer.SetPropertyBlock(_materialPropertyBlock);

        }

        [Button]
        public void HideFieldOfView()
        {
            _originalMaterialColor.a = 0f;
        
            _fieldOfViewRenderer.GetPropertyBlock(_materialPropertyBlock);
            _materialPropertyBlock.SetColor("_Color", _originalMaterialColor);
            _fieldOfViewRenderer.SetPropertyBlock(_materialPropertyBlock);
        }
    
        [Button]
        protected virtual void DrawFieldOfView()
        {
            var origin = Vector3.zero;
            var currentAngle = -1 * (FieldOfViewAngle / 2);
            var angleIncrease = FieldOfViewAngle / raysCount;

            var vertices = new Vector3[raysCount + 1 + 1];
            var uv = new Vector2[vertices.Length];
            var triangles = new int[raysCount * 3];

            vertices[0] = origin;

            var vertexIndex = 1;
            var triangleIndex = 0;

            for (int i = 0; i <= raysCount; i++)
            {
                var vertex = origin + GetVectorFromAngle(currentAngle) * FieldOfViewDistance;
                vertices[vertexIndex] = vertex;

                if (i > 0)
                {
                    triangles[triangleIndex + 0] = 0;
                    triangles[triangleIndex + 1] = vertexIndex - 1;
                    triangles[triangleIndex + 2] = vertexIndex;

                    triangleIndex += 3;
                }

                vertexIndex += 1;
                currentAngle += angleIncrease;
            }

            Mesh.vertices = vertices;
            Mesh.uv = uv;
            Mesh.triangles = triangles;

            MeshFilter.mesh = Mesh;
        }

        private Vector3 GetVectorFromAngle(float angle)
        {
            var angleRad = angle * (Mathf.PI / 180f);
            return new Vector3(Mathf.Sin(angleRad),0, Mathf.Cos(angleRad));
        }
    }
}
