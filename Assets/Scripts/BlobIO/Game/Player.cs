using System;
using DG.Tweening;
using UnityEngine;

namespace BlobIO.Game
{
    public class Player : MonoBehaviour
    {
        private static Player s_Instance;
        public event Action<int> LevelChanged;
        
        [SerializeField] private GameManager m_GameManager;
        [SerializeField] private Transform m_Pivot;
        [SerializeField] private SphereCollider m_Collider;
        [SerializeField] private int m_Level = 1;

        private GameConfig m_GameConfig;
        private bool m_IsDead;
        private Transform m_Transform;
        private Tween m_ScaleTween;

        public static Player Instance => s_Instance;
        
        public int Level => m_Level;
        
        #region Lifecycle

        private void Awake()
        {
            s_Instance = this;
            m_Transform = transform;
        }

        private void Start()
        {
            m_GameConfig = GameConfig.GetInstance();
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
            
            if (other.TryGetComponent(out Obstacle obstacle))
                Die();
            
            if (other.TryGetComponent(out Enemy enemy))
                HitEnemy(enemy);
        }

        #endregion

        #region Private Methods

        private void Die()
        {
            if (m_IsDead)
                return;
            
            m_IsDead = true;
            
            LevelFailArgs args = new LevelFailArgs
            {
                Level = m_Level,
                PlayerName = "Player",
                IsGameOver = true
            };

            m_GameManager.LevelFailed?.Invoke(args);
        }
    
        private void Move(Vector3 direction)
        {
            if (m_IsDead)
                return;
            
            if (direction == Vector3.zero)
                return;
            
            if (direction.sqrMagnitude > 1)
                direction.Normalize();

            m_Transform.position += m_GameConfig.GetSpeed(m_Level, isPlayer: true) * Time.deltaTime * direction;
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
            float radius = (level - 1) * m_GameConfig.RadiusPerLevel + m_GameConfig.StartRadius;
            float scale = radius * 2;
            
            m_ScaleTween?.Kill();
            
            m_ScaleTween = m_Pivot.DOScale(scale * Vector3.one, 0.5f);
            m_Collider.radius = radius;
            Vector3 center = m_Collider.center;
            center.y = radius;
            m_Collider.center = center;
        }
    
        private void Collect(Blob blob)
        {
            IncreaseLevel(blob.Level);
            m_GameManager.BlobCollected?.Invoke(blob);
        }
        
        private void HitEnemy(Enemy enemy)
        {
            if (m_GameConfig.CanEat(m_Level, enemy.Level))
            {
                enemy.Die();
                m_GameManager.PlayerHitEnemy?.Invoke(enemy);
            }
            
            else if (m_GameConfig.CanEat(enemy.Level, m_Level))
            {
                Die();
            }
        }
    
        #endregion
    }
}
