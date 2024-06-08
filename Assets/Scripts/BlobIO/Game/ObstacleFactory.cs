using System.Collections.Generic;
using UnityEngine;

namespace BlobIO.Game
{
    public class ObstacleFactory : MonoBehaviour
    {
        [SerializeField] private Obstacle m_Prefab;
        [SerializeField] private Transform m_Parent;
        [SerializeField] private List<Obstacle> m_ObstacleList = new List<Obstacle>();

        public Obstacle Create(Vector3 position = default)
        {
            Obstacle obstacle = Instantiate(m_Prefab, position, Quaternion.identity, m_Parent);
            m_ObstacleList.Add(obstacle);
            return obstacle;
        }
        
        public void DestroyAll()
        {
            foreach (Obstacle obstacle in m_ObstacleList)
                Destroy(obstacle.gameObject);
        
            m_ObstacleList.Clear();
        }
    }
}