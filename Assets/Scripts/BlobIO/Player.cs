using System;
using UnityEngine;

namespace BlobIO
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private GameManager m_GameManager;
        [SerializeField] private float m_Speed = 5f;
        [SerializeField] private Transform m_Pivot;
        [SerializeField] private SphereCollider m_Collider;
        [SerializeField] private int m_Level = 1;

        private static Player s_Instance;
        
        private Transform m_Transform;
        public event Action<int> LevelChanged;
        
        private const float k_RadiusPerLevel = 0.02f;
        private const float k_StartRadius = 0.5f;

        public static Player Instance => s_Instance;
        
        #region Lifecycle

        private void Awake()
        {
            s_Instance = this;
            m_Transform = transform;
        }
    
        private void Update()
        {
            Vector3 direction = InputHandler.GetDirection();
            Move(direction);
        }
    
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Blob blob))
                Collect(blob);
        }

        #endregion

        #region Private Methods

        private void Die()
        {
            LevelFailArgs args = new LevelFailArgs();
            args.Level = m_Level;
            args.PlayerName = "Player";
            args.IsGameOver = true;
        
            m_GameManager.LevelFailed?.Invoke(args);
        }
    
        private void Move(Vector3 direction)
        {
            if (direction == Vector3.zero)
                return;

            m_Transform.position += m_Speed * Time.deltaTime * direction.normalized;
            m_Transform.position = WorldManager.Instance.GetClampPosition(m_Transform);
        }
    
        private void SetLevel(int level)
        {
            m_Level = level;
            LevelChanged?.Invoke(level);
            SetScale(m_Level);
        }
    
        private void IncreaseLevel(int level) => SetLevel(m_Level + level);

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
    
        private void Collect(Blob blob)
        {
            IncreaseLevel(blob.Level);
            Destroy(blob.gameObject);
        }
    
        #endregion
    }
}
