using Runtime.Extensions;
using UniRx;
using UnityEngine;

namespace Runtime.Logic.Gameplay.Enemy
{
    // Original from https://www.youtube.com/watch?v=luLrhoTZYD8
    public class VisionCone : MonoBehaviour
    {
        public MeshFilter m_ConeMeshFilter;
        public MeshCollider m_ConeMeshCollider;
        
        public float m_VisionRange;
        public float m_VisionAngle;
        // Layer with objects that obstruct the enemy view, like walls, for example.
        public LayerMask m_VisionObstructingLayer;
        // The vision cone will be made up of triangles, the higher this value is the prettier the vision cone will be.
        public int m_VisionConeResolution = 120;
        
        /// <summary>
        /// Invokes every frame and transfer hero transform or null if detected or not detected hero.
        /// </summary>
        public ReactiveCommand<Transform> OnDetectedHero { get; } = new();

        private float RadianAngle => m_VisionAngle * Mathf.Deg2Rad;
        private Mesh _visionConeMesh;

        //Create all of these variables, most of them are self explanatory, but for the ones that aren't i've added a comment to clue you in on what they do
        //for the ones that you dont understand dont worry, just follow along.

        private void Start() => _visionConeMesh = new();

        public void Update()
        {
            // Calling the vision cone function every frame just so the cone is updated every frame.
            DrawVisionCone(); 
        }

        // This method creates the vision cone mesh.
        private void DrawVisionCone()
        {
            var heroDetectedOnFrame = false;
            var heroTransform = transform;
            
            var triangles = new int[(m_VisionConeResolution - 1) * 3];
            var vertices = new Vector3[m_VisionConeResolution + 1];
            vertices[0] = Vector3.zero;
            
            float currentAngle = -RadianAngle / 2;
            float angleIncrement = RadianAngle / (m_VisionConeResolution - 1);

            for (int i = 0; i < m_VisionConeResolution; i++)
            {
                var sine = Mathf.Sin(currentAngle);
                var cosine = Mathf.Cos(currentAngle);
                
                Vector3 raycastDirection = transform.forward * cosine + transform.right * sine;
                Vector3 vertForward = Vector3.forward * cosine + Vector3.right * sine;
                
                if (Physics.Raycast(transform.position, raycastDirection, out RaycastHit hit, m_VisionRange, m_VisionObstructingLayer))
                {
                    vertices[i + 1] = vertForward * hit.distance;

                    if (hit.transform.IsPlayer())
                    {
                        heroDetectedOnFrame = true;
                        heroTransform = hit.transform;
                    }
                }
                else
                {
                    vertices[i + 1] = vertForward * m_VisionRange;
                }

                currentAngle += angleIncrement;
            }

            OnDetectedHero?.Execute(heroDetectedOnFrame ? heroTransform : null);


            for (int i = 0, j = 0; i < triangles.Length; i += 3, j++)
            {
                triangles[i] = 0;
                triangles[i + 1] = j + 1;
                triangles[i + 2] = j + 2;
            }
            
            _visionConeMesh.Clear();
            _visionConeMesh.vertices = vertices;
            _visionConeMesh.triangles = triangles;

    //         m_ConeMeshCollider.sharedMesh = _visionConeMesh;
            m_ConeMeshFilter.mesh = _visionConeMesh;
        }
    }
}