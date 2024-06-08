using UnityEngine;

namespace BlobIO.Game
{
    public abstract class ScriptableInstance<T> : ScriptableObject where T : ScriptableObject
    {
        private static T s_Instance;
        
        protected static T GetInstance(string path)
        {
            if (s_Instance == null)
            {
                s_Instance = Resources.Load<T>(path);
            }

            return s_Instance;
        }
    }
}