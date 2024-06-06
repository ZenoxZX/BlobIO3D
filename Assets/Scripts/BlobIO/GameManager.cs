using System;
using System.Collections.Generic;
using UnityEngine;

namespace BlobIO
{
    public class GameManager : MonoBehaviour
    {
        #region Factories
    
        [SerializeField] private BlobFactory m_BlobFactory;
        [SerializeField] private ObstacleFactory m_ObstacleFactory;
        [SerializeField] private EnemyFactory m_EnemyFactory;
        
        private LevelData m_LevelData;
    
        #endregion

        public Action LevelCompleted;
        public Action<LevelFailArgs> LevelFailed;

        private void Awake()
        {
            m_LevelData = LevelData.GetInstance();
        }
        
        private void Start()
        {
            CreateBlobs();
            CreateObstacles();
            CreateEnemy();
        }

        private void CreateBlobs()
        {
            int blobLevel = 1;
            
            for (int i = 0; i < m_LevelData.MaxBlobCount; i++)
            {
                Vector3 randomPosition = WorldManager.Instance.GetRandomPosition();
                m_BlobFactory.Create(blobLevel, randomPosition);
            }
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
    }
}