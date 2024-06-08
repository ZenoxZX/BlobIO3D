using UnityEngine;

namespace BlobIO.Game
{
    public class VectorExample : MonoBehaviour
    {
        [SerializeField] private Vector3 m_PlayerPosition;
        [SerializeField] private Vector3 m_EnemyPosition;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                Vector3 movement = m_PlayerPosition / 3;
                Debug.Log(Vector3.Angle(m_PlayerPosition, m_EnemyPosition));
            }
        }

        private void OnDrawGizmos()
        {
            float radius = 0.1f;
        
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, m_PlayerPosition);
            Gizmos.DrawWireSphere(m_PlayerPosition, radius);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, m_EnemyPosition);
            Gizmos.DrawWireSphere(m_EnemyPosition, radius);

        }
    }
}