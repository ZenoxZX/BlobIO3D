using UnityEngine;

namespace BlobIO
{
    [CreateAssetMenu(fileName = "LevelData 001", menuName = "Level Data")]
    public class LevelData : ScriptableObject
    {
        [Header("World Settings")]
        [SerializeField] private float m_WorldSizeX = 10f;
        [SerializeField] private float m_WorldSizeZ = 10f;
        [Header("Blob Settings")]
        [SerializeField] private int m_MaxBlobCount = 50;
        [SerializeField] private float m_BlobSpawnInterval = 1f;
        [Header("Enemy Settings")]
        [SerializeField] private int m_MaxEnemyCount = 5;
        [SerializeField] private float m_EnemyDamage = 10;
    }
}