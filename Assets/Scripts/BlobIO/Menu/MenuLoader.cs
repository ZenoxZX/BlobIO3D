using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace BlobIO.Menu
{
    public class MenuLoader : MonoBehaviour
    {
        [SerializeField] private Button m_PlayButton;

        private void Awake()
        {
            m_PlayButton.onClick.AddListener(OnPlayButtonClicked);
        }

        private void OnPlayButtonClicked()
        {
            m_PlayButton.interactable = false;
            AsyncOperation operation = SceneManager.LoadSceneAsync(1);
            
            operation.completed += op => Debug.Log("Scene Loaded");
        }
    }
}