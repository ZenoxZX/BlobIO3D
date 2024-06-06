using System;
using UnityEngine;

namespace BlobIO
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Transform m_Pivot;
        [SerializeField] private SphereCollider m_Collider;
        [SerializeField] private int m_Level = 1;
        [SerializeField] private float m_CheckPlayerRadius = 10f;

        private EnemyFactory m_Factory;
        private Transform m_Transform;
        private Player m_Player;
        private Transform m_PlayerTransform;
        private Vector3 m_FreeFormPosition;
        
        private const float k_RadiusPerLevel = 0.15f;
        private const float k_StartRadius = 0.5f;

        public void SetFactory(EnemyFactory factory)
        {
            m_Factory = factory;
        }
        
        
        #region Lifecycle
        
        private void Awake()
        {
            m_Transform = transform;
            SetScale(m_Level);
        }

        private void Start()
        {
            m_Player = Player.Instance;
            m_PlayerTransform = m_Player.transform;
        }
        
        private void Update()
        {
            Think();
        }
        
        #endregion


        private void Think()
        {
            bool isPlayerInRange = IsPlayerInRange();

            if (isPlayerInRange)
            {
                bool isMyLevelBiggerThanPlayer = IsMyLevelBiggerThanPlayer();
                
                if (isMyLevelBiggerThanPlayer)
                {
                    CatchPlayer();
                }
                
                else
                {
                    RunAway();
                }
            }
            
            else
            {
                FreeRoam();
            }
        }

        private void CatchPlayer()
        {
            Vector3 direction = m_PlayerTransform.position - m_Transform.position;
            MoveTo(direction.normalized);
        }
        
        private void RunAway()
        {
            Vector3 direction = m_Transform.position - m_PlayerTransform.position;
            MoveTo(direction.normalized);
        }
        
        private void FreeRoam()
        {
            float distance = Vector3.Distance(m_Transform.position, m_FreeFormPosition);
            
            if (distance < 0.1f)
            {
                m_FreeFormPosition = WorldManager.Instance.GetRandomPosition();
            }

            else
            {
                Vector3 direction = m_FreeFormPosition - m_Transform.position;
                MoveTo(direction.normalized);
            }
        }

        private void MoveTo(Vector3 direction)
        {
            m_Transform.position += direction * Time.deltaTime;
        }

        private bool IsPlayerInRange() => Vector3.Distance(m_Transform.position, m_PlayerTransform.position) < m_CheckPlayerRadius;
        private bool IsMyLevelBiggerThanPlayer() => m_Level > m_Player.Level;

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

        private void OnDrawGizmos()
        {
            if (m_Factory == null)
                return;
            
            if (!m_Factory.ShowGizmos)
                return;
            
            bool isPlayerInRange = IsPlayerInRange();

            Gizmos.color = isPlayerInRange ? Color.green : Color.yellow;
            Gizmos.DrawWireSphere(transform.position, m_CheckPlayerRadius);
        }
    }
}