using UnityEngine;

namespace BlobIO.Game
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Transform m_Pivot;
        [SerializeField] private SphereCollider m_Collider;
        [SerializeField] private int m_Level = 1;

        private GameManager m_GameManager;
        private GameConfig m_GameConfig;
        private EnemyFactory m_Factory;
        private Player m_Player;
        
        private Transform m_Transform;
        private Transform m_PlayerTransform;
        private Vector3 m_FreeFormPosition;
        private bool m_IsDead;
        
        public int Level => m_Level;

        public void Initialize(EnemyFactory factory)
        {
            m_Factory = factory;
            m_FreeFormPosition = WorldManager.Instance.GetRandomPosition();
        }
        
        #region Lifecycle
        
        private void Awake() => m_Transform = transform;

        private void Start()
        {
            m_GameManager = GameManager.Instance;
            m_Player = Player.Instance;
            m_PlayerTransform = m_Player.transform;
            m_GameConfig = GameConfig.GetInstance();
            SetScale(m_Level);
        }
        
        private void Update() => Think();
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Blob blob))
                Collect(blob);
            
            if (other.TryGetComponent(out Obstacle obstacle))
                Die();
        }

        private void OnDrawGizmos()
        {
            if (m_Factory == null)
                return;
            
            if (!m_Factory.ShowGizmos)
                return;
            
            bool isPlayerInRange = IsPlayerInRange();

            Gizmos.color = isPlayerInRange ? Color.green : Color.yellow;
            Gizmos.DrawWireSphere(transform.position, m_GameConfig.EnemyCheckRadius);
        }
        
        #endregion

        #region Think Methods

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

        private bool IsMyLevelBiggerThanPlayer() => m_GameConfig.CanEat(m_Level, m_Player.Level);
        private bool IsPlayerInRange() => Vector3.Distance(m_Transform.position, m_PlayerTransform.position) < m_GameConfig.EnemyCheckRadius;

        #endregion

        private void Die()
        {
            if (m_IsDead)
                return;
            
            m_IsDead = true;

            m_GameManager.EnemyDied?.Invoke(this);
        }
        
        private void MoveTo(Vector3 direction)
        {
            m_Transform.position  += m_GameConfig.GetSpeed(m_Level) * Time.deltaTime * direction;
        }
        
        private void SetLevel(int level)
        {
            m_Level = level;
            SetScale(m_Level);
        }
        
        private void IncreaseLevel(int level) => SetLevel(m_Level + level);

        private void SetScale(int level)
        {
            float radius = (level - 1) * m_GameConfig.RadiusPerLevel + m_GameConfig.StartRadius;
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
            m_GameManager.BlobCollected?.Invoke(blob);
        }
    }
}