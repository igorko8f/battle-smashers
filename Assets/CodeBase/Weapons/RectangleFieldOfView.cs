using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase.Weapons
{
    public class RectangleFieldOfView : FieldOfView
    {
        [SerializeField] private Transform minZonePosition;
        [SerializeField] private Transform maxZonePosition;
        
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

            minZonePosition.localPosition = position00;
            maxZonePosition.localPosition = position11;
            
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

        public override bool IsPositionInTheFieldOfView(Vector3 targetPosition)
        {
            if (Vector3.Distance(transform.position, targetPosition) <= FieldOfViewDistance)
            {
                if (targetPosition.x >= minZonePosition.position.x
                    && targetPosition.x <= maxZonePosition.position.x
                    && targetPosition.z >= minZonePosition.position.z
                    && targetPosition.z <= maxZonePosition.position.z)
                {
                    return true;
                }
            }

            return false;
        }
    }
}