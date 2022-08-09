using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase.Weapons
{
    public class RectangleFieldOfView : FieldOfView
    {
        [Button]
        protected override void DrawFieldOfView()
        {
            var meshOriginPosition = Vector3.zero;
            meshOriginPosition.x -= FieldOfViewAngle / 2;
            
            var position00 = meshOriginPosition;
            
            var position01 = meshOriginPosition;
            position01.x += FieldOfViewAngle;
            
            var position10 = meshOriginPosition;
            position10.z += FieldOfViewDistance;
            
            var position11 = meshOriginPosition;
            position11.x += FieldOfViewAngle;
            position11.z += FieldOfViewDistance;

            var vertices = new Vector3[]
            {
                position00,
                position01,
                position10,
                position11
            };

            var triangles = new int[]
            {
                0, 1, 2,
                1, 3, 2
            };
            
            var uv = new Vector2[vertices.Length];
            
            Mesh.vertices = vertices;
            Mesh.uv = uv;
            Mesh.triangles = triangles;

            MeshFilter.mesh = Mesh;
        }
    }
}