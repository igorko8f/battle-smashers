using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(Renderer))]
public class FieldOfView : MonoBehaviour
{
    [SerializeField] private float fieldOfViewDistance;
    [SerializeField] private float fieldOfViewAngle;
    [SerializeField] private int raysCount;
    
    private MeshFilter _meshFilter;
    private Renderer _fieldOfViewRenderer;
    private Mesh _mesh;

    private MaterialPropertyBlock _materialPropertyBlock;
    private Color _originalMaterialColor;

    private void Awake()
    {
        _meshFilter = GetComponent<MeshFilter>();
        _fieldOfViewRenderer = GetComponent<Renderer>();
        _mesh = new Mesh();
        _materialPropertyBlock = new MaterialPropertyBlock();
        
        _originalMaterialColor = _fieldOfViewRenderer.material.color;
    }
    
    private void Start()
    {
        DrawFieldOfView();
        HideFieldOfView();
    }

    public bool IsPositionInTheFieldOfView(Vector3 targetPosition)
    {
        if (Vector3.Distance(transform.position, targetPosition) <= fieldOfViewDistance)
        {
            var direction = (targetPosition - transform.position).normalized;
            if (Vector3.Angle(transform.forward, direction) < fieldOfViewAngle / 2f)
            {
                return true;
            }
        }

        return true;
    }

    [Button]
    public void ShowFieldOfView()
    {
        _originalMaterialColor.a = 0.65f;
        
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
    
    private void DrawFieldOfView()
    {
        var origin = Vector3.zero;
        var currentAngle = -1 * (fieldOfViewAngle / 2);
        var angleIncrease = fieldOfViewAngle / raysCount;

        var vertices = new Vector3[raysCount + 1 + 1];
        var uv = new Vector2[vertices.Length];
        var triangles = new int[raysCount * 3];

        vertices[0] = origin;

        var vertexIndex = 1;
        var triangleIndex = 0;

        for (int i = 0; i <= raysCount; i++)
        {
            var vertex = origin + GetVectorFromAngle(currentAngle) * fieldOfViewDistance;
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

        _mesh.vertices = vertices;
        _mesh.uv = uv;
        _mesh.triangles = triangles;

        _meshFilter.mesh = _mesh;
    }

    private Vector3 GetVectorFromAngle(float angle)
    {
        var angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Sin(angleRad),0, Mathf.Cos(angleRad));
    }
}
