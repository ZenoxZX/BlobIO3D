using System;
using System.Collections.Generic;
using UnityEngine;

namespace BlobIO
{
    public class GameManager : MonoBehaviour
    {
        #region Factories
    
        [SerializeField] private EnemyFactory m_EnemyFactory;
        [SerializeField] private BlobFactory m_BlobFactory;
        [SerializeField] private List<LevelData> m_Levels;
    
        #endregion

        public Action LevelCompleted;
        public Action<LevelFailArgs> LevelFailed;

        private void Awake()
        {
            m_Levels = LoadLevels();
        }
    
        private static List<LevelData> LoadLevels()
        {
            LevelData[] datas = Resources.LoadAll<LevelData>("Levels");
            return new List<LevelData>(datas);
        }
    }
}