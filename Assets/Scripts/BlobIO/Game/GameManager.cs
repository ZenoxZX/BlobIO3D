using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BlobIO.Game
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager s_Instance;
        [SerializeField] private int m_Seed;
        
        public Action<Enemy> EnemyDied;
        public Action<Enemy> PlayerHitEnemy;
        public Action<Blob> BlobCollected;
        public Action LevelCompleted;
        public Action<LevelFailArgs> LevelFailed;
        
        #region Factories
    
        [SerializeField] private BlobFactory m_BlobFactory;
        [SerializeField] private ObstacleFactory m_ObstacleFactory;
        [SerializeField] private EnemyFactory m_EnemyFactory;
        
        private LevelData m_LevelData;
    
        #endregion

        private float m_Timer;
        
        public static GameManager Instance => s_Instance;

        #region Lifecycle
        
        private void Awake()
        {
            Random.InitState(m_Seed);
            Random.Range(0, 100);
            
            s_Instance = this;
            m_LevelData = LevelData.GetInstance();
        }
        
        private void Start()
        {
            CreateInitialBlobs();
            CreateObstacles();
            CreateEnemy();
            
            EnemyDied += OnEnemyDied;
            BlobCollected += OnBlobCollected;
        }

        

        private void Update()
        {
            CreateSequentialBlobs();
        }

        #endregion

        #region Creation Methods

        private void CreateInitialBlobs()
        {
            int blobLevel = 1;
            
            for (int i = 0; i < m_LevelData.InitialBlobCount; i++)
            {
                Vector3 randomPosition = WorldManager.Instance.GetRandomPosition();
                m_BlobFactory.Create(blobLevel, randomPosition);
            }
        }
        
        private void CreateSequentialBlobs()
        {
            if (m_BlobFactory.Count >= m_LevelData.MaxBlobCount)
                return;

            m_Timer += Time.deltaTime;
            
            if (m_Timer < m_LevelData.BlobSpawnInterval)
                return;
            
            m_Timer = 0f;
            int blobLevel = 1;
            
            Vector3 randomPosition = WorldManager.Instance.GetRandomPosition();
            m_BlobFactory.Create(blobLevel, randomPosition);
        }
        
        private void CreateObstacles()
        {
            for (int i = 0; i < m_LevelData.MaxObstacleCount; i++)
            {
                Vector3 randomPosition = WorldManager.Instance.GetRandomPosition();
                m_ObstacleFactory.Create(randomPosition);
            }
        }
        
        public void CreateEnemy()
        {
            for (int i = 0; i < m_LevelData.MaxEnemyCount; i++)
            {
                Vector3 randomPosition = WorldManager.Instance.GetRandomPosition();
                m_EnemyFactory.Create(randomPosition);
            }
        }
        
        #endregion

        private void OnEnemyDied(Enemy enemy)
        {
            enemy.gameObject.SetActive(false);
            m_EnemyFactory.Destroy(enemy);

            Vector3 randomPosition = WorldManager.Instance.GetRandomPosition();
            m_EnemyFactory.Create(randomPosition);
        }
        
        private void OnBlobCollected(Blob blob)
        {
            blob.gameObject.SetActive(false);
            m_BlobFactory.Destroy(blob);
            
            int blobLevel = 1;
            
            Vector3 randomPosition = WorldManager.Instance.GetRandomPosition();
            m_BlobFactory.Create(blobLevel, randomPosition);
        }
    }
}