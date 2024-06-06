using System.Collections.Generic;
using UnityEngine;

namespace BlobIO
{
    public class BlobFactory : MonoBehaviour
    {
        [SerializeField] private Blob m_Prefab;
        [SerializeField] private Transform m_Parent;
        [SerializeField] private List<Blob> m_BlobList = new List<Blob>();

        public Blob Create(int level, Vector3 position = default)
        {
            Blob blob = Instantiate(m_Prefab, position, Quaternion.identity, m_Parent);
            blob.SetLevel(level);
            m_BlobList.Add(blob);
            return blob;
        }
    
        public void DestroyAll()
        {
            foreach (Blob blob in m_BlobList)
                Destroy(blob.gameObject);
        
            m_BlobList.Clear();
        }
    }
}