using UnityEngine;

namespace BlobIO
{
    [CreateAssetMenu(fileName = "LevelData 001", menuName = "Level Data")]
    public class LevelData : ScriptableObject
    {
        private static LevelData s_Instance;
        
        [Header("World Settings")]
        [SerializeField] private float m_WorldSizeX = 10f;
        [SerializeField] private float m_WorldSizeZ = 10f;
        
        [Header("Blob Settings")]
        [SerializeField] private int m_MaxBlobCount = 50;
        [SerializeField] private float m_BlobSpawnInterval = 1f;
        
        [Header("Obstacle Settings")]
        [SerializeField] private int m_MaxObstacleCount = 10;
        
        [Header("Enemy Settings")]
        [SerializeField] private int m_MaxEnemyCount = 5;
        
        public float WorldSizeX => m_WorldSizeX;
        public float WorldSizeZ => m_WorldSizeZ;
        public int MaxBlobCount => m_MaxBlobCount;
        public int MaxObstacleCount => m_MaxObstacleCount;
        public float BlobSpawnInterval => m_BlobSpawnInterval;
        public int MaxEnemyCount => m_MaxEnemyCount;
        
        
        public static LevelData GetInstance()
        {
            if (s_Instance == null)
            {
                s_Instance = Resources.Load<LevelData>("LevelData");
            }

            return s_Instance;
        }
    }
}