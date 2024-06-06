using System.Collections.Generic;
using UnityEngine;

namespace BlobIO
{
    public class EnemyFactory : MonoBehaviour
    {
        [SerializeField] private Enemy m_Prefab;
        [SerializeField] private Transform m_Parent;
        [SerializeField] private List<Enemy> m_EnemyList = new List<Enemy>();

        public Enemy Create(Vector3 position = default, Quaternion rotation = default)
        {
            Enemy enemy = Instantiate(m_Prefab, position, rotation, m_Parent);
            m_EnemyList.Add(enemy);
            return enemy;
        }
    
        public void DestroyAll()
        {
            foreach (Enemy enemy in m_EnemyList)
                Destroy(enemy.gameObject);
        
            m_EnemyList.Clear();
        }
    }
}