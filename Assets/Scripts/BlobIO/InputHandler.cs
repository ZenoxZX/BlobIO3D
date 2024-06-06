using UnityEngine;

namespace BlobIO
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
            float verticalAxis = Input.GetAxisRaw("Vertical");
            float horizontalAxis = Input.GetAxisRaw("Horizontal");
            return new Vector3(horizontalAxis, 0, verticalAxis);
        }
    }
}
