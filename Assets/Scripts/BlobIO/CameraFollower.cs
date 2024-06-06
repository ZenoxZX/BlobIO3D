using System;
using UnityEngine;

namespace BlobIO
{
    public class CameraFollower : MonoBehaviour
    {
        [SerializeField] private Transform m_Target;
        [SerializeField] private Vector3 m_Offset = new Vector3(0, 0, 0);
        [SerializeField] private float m_Speed = 5f;
        [SerializeField] private MovementType m_MovementType = MovementType.SmoothDamp;
    
        private Vector3 m_Velocity = Vector3.zero;
    
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