using System.Collections.Generic;
using UnityEngine;

namespace BlobIO.Game
{
    public class BlobFactory : MonoBehaviour
    {
        [SerializeField] private Blob m_Prefab;
        [SerializeField] private Transform m_Parent;
        [SerializeField] private Material[] m_Materials;
        
        private readonly List<Blob> m_BlobList = new List<Blob>();

        public int Count => m_BlobList.Count;
        
        public Blob Create(int level, Vector3 position = default)
        {
            Blob blob = Instantiate(m_Prefab, position, Quaternion.identity, m_Parent);
            blob.SetLevel(level);
            Material randomMaterial = GetRandomMaterial();
            blob.SetMaterial(randomMaterial);
            m_BlobList.Add(blob);
            return blob;
        }

        public void Destroy(Blob blob)
        {
            m_BlobList.Remove(blob);
            Destroy(blob.gameObject);
        }
    
        public void DestroyAll()
        {
            foreach (Blob blob in m_BlobList)
                Destroy(blob.gameObject);
        
            m_BlobList.Clear();
        }
        
        private Material GetRandomMaterial() => m_Materials[Random.Range(0, m_Materials.Length)];
    }
}