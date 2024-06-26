using UnityEngine;

namespace BlobIO.Game
{
    public class InputHandler : MonoBehaviour
    {
        public static InputHandler Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }
    
        public static Vector3 GetDirection()
        {
            float verticalAxis = Input.GetAxis("Vertical");
            float horizontalAxis = Input.GetAxis("Horizontal");
            return new Vector3(horizontalAxis, 0, verticalAxis);
        }
    }
}
