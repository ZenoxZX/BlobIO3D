using System.Collections.Generic;
using UnityEngine;

namespace BlobIO.Game
{
    public class EnemyFactory : MonoBehaviour
    {
        [SerializeField] private Enemy m_Prefab;
        [SerializeField] private Transform m_Parent;
        [SerializeField] private bool m_ShowGizmos = true;
        
        private readonly List<Enemy> m_EnemyList = new List<Enemy>();
        
        public bool ShowGizmos => m_ShowGizmos;

        public Enemy Create(Vector3 position = default, Quaternion rotation = default)
        {
            Enemy enemy = Instantiate(m_Prefab, position, rotation, m_Parent);
            enemy.Initialize(this);
            m_EnemyList.Add(enemy);
            return enemy;
        }
        
        public void Destroy(Enemy enemy)
        {
            m_EnemyList.Remove(enemy);
            Destroy(enemy.gameObject);
        }
    
        public void DestroyAll()
        {
            foreach (Enemy enemy in m_EnemyList)
                Destroy(enemy.gameObject);
        
            m_EnemyList.Clear();
        }
    }
}