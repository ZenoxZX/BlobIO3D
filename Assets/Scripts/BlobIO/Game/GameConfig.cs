using Sirenix.OdinInspector;
using UnityEngine;

namespace BlobIO.Game
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Game Config")]
    public class GameConfig : ScriptableInstance<GameConfig>
    {
        [Title("Speed Settings")]
        [SerializeField] private float m_Speed = 4f;
        [SerializeField] private float m_SpeedDecreasePerLevel = 0.025f;
        [SerializeField] private float m_PlayerAdvanceSpeed = 1.5f;
        
        [Title("Radius Settings")]
        [SerializeField] private float m_RadiusPerLevel = 0.03f;
        [SerializeField] private float m_StartRadius = 0.5f;
        [SerializeField] private float m_EnemyCheckRadius = 2.5f;
        [SerializeField] private float m_CameraFovMultiplier = 1.5f;
        
        [Title("Hit Settings")]
        [SerializeField] private float m_DifferenceMultiplier = 1.5f;
        
        public float RadiusPerLevel => m_RadiusPerLevel;
        public float StartRadius => m_StartRadius;
        public float EnemyCheckRadius => m_EnemyCheckRadius;
        public float CameraFovMultiplier => m_CameraFovMultiplier;
        
        public float GetSpeed(int level, bool isPlayer = false)
        {
            if (isPlayer)
                return (m_Speed - m_SpeedDecreasePerLevel * (level - 1)) * m_PlayerAdvanceSpeed;
            
            return m_Speed - m_SpeedDecreasePerLevel * (level - 1);
        }
        
        public bool CanEat(int myLevel, int otherLevel)
        {
            if (myLevel == 1)
                return false;
            
            if (myLevel == otherLevel)
                return false;
            
            return myLevel >= (otherLevel - 1) * m_DifferenceMultiplier;
        }

        public static GameConfig GetInstance() => GetInstance("GameConfig");
    }
}