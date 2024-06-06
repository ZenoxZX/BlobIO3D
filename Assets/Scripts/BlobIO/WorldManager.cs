using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BlobIO
{
    public class WorldManager : MonoBehaviour
    {
        private static WorldManager s_Instance;
        private LevelData m_LevelData;
        
        public static WorldManager Instance => s_Instance;
        private Vector3 WorldSizeMax => new Vector3(m_LevelData.WorldSizeX, 0, m_LevelData.WorldSizeZ);
        private Vector3 WorldSizeMin => new Vector3(-m_LevelData.WorldSizeX, 0, -m_LevelData.WorldSizeZ);
        
        private void Awake()
        {
            s_Instance = this;
            m_LevelData = LevelData.GetInstance();
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