using System;
using DG.Tweening;
using UnityEngine;

namespace BlobIO.Game
{
    public class CameraFollower : MonoBehaviour
    {
        [SerializeField] private Transform m_Target;
        [SerializeField] private Vector3 m_Offset = new Vector3(0, 0, 0);
        [SerializeField] private float m_Speed = 5f;
        [SerializeField] private MovementType m_MovementType = MovementType.SmoothDamp;

        private GameConfig m_GameConfig;
        private Tween m_FovTween;
        private Camera m_Camera;
        private float m_InitialFov;
        private Vector3 m_Velocity = Vector3.zero;

        private void Awake()
        {
            m_Camera = GetComponent<Camera>();
            m_InitialFov = m_Camera.fieldOfView;
        }

        private void Start()
        {
            m_GameConfig = GameConfig.GetInstance();
            Player.Instance.LevelChanged += OnLevelChanged;
        }

        private void OnLevelChanged(int level)
        {
            float targetFov = m_InitialFov + (level - 1) * m_GameConfig.CameraFovMultiplier;
            float duration = 1f;

            m_FovTween?.Kill();
            m_FovTween = m_Camera.DOFieldOfView(targetFov, duration);
        }

        private void Update()
        {
            if (m_Target == null)
                return;
        
            Vector3 targetPosition = m_Target.position + m_Offset;
            transform.position = m_MovementType switch
            {
                MovementType.Lerp => MoveWithLerp(targetPosition),
                MovementType.Slerp => MoveWithSlerp(targetPosition),
                MovementType.MoveTowards => MoveWithMoveTowards(targetPosition),
                MovementType.SmoothDamp => MoveWithSmoothDamp(targetPosition),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        #region Movement Types
    
        private Vector3 MoveWithLerp(Vector3 targetPosition) 
            => Vector3.Lerp(transform.position, targetPosition, m_Speed * Time.deltaTime);

        private Vector3 MoveWithSlerp(Vector3 targetPosition) 
            => Vector3.Slerp(transform.position, targetPosition, m_Speed * Time.deltaTime);

        private Vector3 MoveWithMoveTowards(Vector3 targetPosition) 
            => Vector3.MoveTowards(transform.position, targetPosition, m_Speed * Time.deltaTime);

        private Vector3 MoveWithSmoothDamp(Vector3 targetPosition) 
            => Vector3.SmoothDamp(transform.position, targetPosition, ref m_Velocity, 1 / m_Speed);
    
        #endregion

        public enum MovementType
        {
            Lerp,
            Slerp,
            MoveTowards,
            SmoothDamp
        }
    }
}