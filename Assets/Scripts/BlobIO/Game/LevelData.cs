using UnityEngine;

namespace BlobIO.Game
{
    [CreateAssetMenu(fileName = "LevelData 001", menuName = "Level Data")]
    public class LevelData : ScriptableInstance<LevelData>
    {
        [Header("World Settings")]
        [SerializeField] private float m_WorldSizeX = 10f;
        [SerializeField] private float m_WorldSizeZ = 10f;
        
        [Header("Blob Settings")]
        [SerializeField] private int m_InitialBlobCount = 10;
        [SerializeField] private int m_MaxBlobCount = 50;
        [SerializeField] private float m_BlobSpawnInterval = 1f;
        
        [Header("Obstacle Settings")]
        [SerializeField] private int m_MaxObstacleCount = 10;
        
        [Header("Enemy Settings")]
        [SerializeField] private int m_MaxEnemyCount = 5;
        
        public float WorldSizeX => m_WorldSizeX;
        public float WorldSizeZ => m_WorldSizeZ;
        public int InitialBlobCount => m_InitialBlobCount;
        public int MaxBlobCount => m_MaxBlobCount;
        public int MaxObstacleCount => m_MaxObstacleCount;
        public float BlobSpawnInterval => m_BlobSpawnInterval;
        public int MaxEnemyCount => m_MaxEnemyCount;
        
        
        public static LevelData GetInstance() => GetInstance("LevelData");
    }
}