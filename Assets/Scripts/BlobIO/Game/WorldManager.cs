using UnityEngine;
using Random = UnityEngine.Random;

namespace BlobIO.Game
{
    public class WorldManager : MonoBehaviour
    {
        private static WorldManager s_Instance;
        
        [SerializeField] private GameObject m_WallPrefab;
        
        private LevelData m_LevelData;
        
        public static WorldManager Instance => s_Instance;
        private Vector3 WorldSizeMax => new Vector3(m_LevelData.WorldSizeX, 0, m_LevelData.WorldSizeZ);
        private Vector3 WorldSizeMin => new Vector3(-m_LevelData.WorldSizeX, 0, -m_LevelData.WorldSizeZ);
        
        private void Awake()
        {
            s_Instance = this;
            m_LevelData = LevelData.GetInstance();
            
            Vector3 positiveZ = new Vector3(0, 0, m_LevelData.WorldSizeZ);
            Vector3 negativeZ = new Vector3(0, 0, -m_LevelData.WorldSizeZ);
            Vector3 positiveX = new Vector3(m_LevelData.WorldSizeX, 0, 0);
            Vector3 negativeX = new Vector3(-m_LevelData.WorldSizeX, 0, 0);
            
            GameObject wallPositiveZ = Instantiate(m_WallPrefab, positiveZ, Quaternion.identity);
            GameObject wallNegativeZ = Instantiate(m_WallPrefab, negativeZ, Quaternion.identity);
            GameObject wallPositiveX = Instantiate(m_WallPrefab, positiveX, Quaternion.Euler(0, 90, 0));
            GameObject wallNegativeX = Instantiate(m_WallPrefab, negativeX, Quaternion.Euler(0, 90, 0));
            
            wallPositiveZ.transform.localScale = new Vector3(m_LevelData.WorldSizeX * 2, 1, 1);
            wallNegativeZ.transform.localScale = new Vector3(m_LevelData.WorldSizeX * 2, 1, 1);
            wallPositiveX.transform.localScale = new Vector3(m_LevelData.WorldSizeZ * 2, 1, 1);
            wallNegativeX.transform.localScale = new Vector3(m_LevelData.WorldSizeZ * 2, 1, 1);
        }

        public Vector3 GetClampPosition(Transform t)
        {
            Vector3 clampedPosition = t.position;
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, WorldSizeMin.x, WorldSizeMax.x);
            clampedPosition.z = Mathf.Clamp(clampedPosition.z, WorldSizeMin.z, WorldSizeMax.z);
            return clampedPosition;
        }
        
        public Vector3 GetRandomPosition()
        {
            float x = Random.Range(WorldSizeMin.x, WorldSizeMax.x);
            float z = Random.Range(WorldSizeMin.z, WorldSizeMax.z);
            return new Vector3(x, 0, z);
        }

        private void OnDrawGizmosSelected()
        {
            if (!Application.isPlaying)
                return;
            
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(Vector3.zero, WorldSizeMax * 2);
        }
    }
}