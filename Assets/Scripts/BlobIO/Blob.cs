using UnityEngine;

namespace BlobIO
{
    public class Blob : MonoBehaviour
    {
        [SerializeField] private Transform m_Pivot;
        [SerializeField] private SphereCollider m_Collider;
        [SerializeField] private int m_Level = 1;

        private const float k_RadiusPerLevel = 0.15f;
        private const float k_StartRadius = 0.15f;
    
        public int Level => m_Level;
    
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
                SetScale(m_Level);
        }
    
        public void SetLevel(int level)
        {
            m_Level = level;
            SetScale(m_Level);
        }

        private void SetScale(int level)
        {
            float radius = (level - 1) * k_RadiusPerLevel + k_StartRadius;
            float scale = radius * 2;
            m_Pivot.localScale = scale * Vector3.one;
            m_Collider.radius = radius;
            Vector3 center = m_Collider.center;
            center.y = radius;
            m_Collider.center = center;
        }
    }
}
