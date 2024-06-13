using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BlobIO.Game
{
    public class LevelFailGUI : MonoBehaviour
    {
        [SerializeField] private GameManager m_GameManager;
        [SerializeField] private GameObject m_Panel;
        [SerializeField] private CanvasGroup m_CanvasGroup;
        [SerializeField] private TextMeshProUGUI m_LevelText;
        [SerializeField] private Button m_RestartButton;

        private void Awake()
        {
            m_RestartButton.onClick.AddListener(OnRestartButtonClicked);
        }

        private void OnEnable()
        {
            m_GameManager.LevelFailed += Show;
        }

        private void Show(LevelFailArgs args)
        {
            m_LevelText.text = $"Score : {args.Level - 1}";
            StartCoroutine(ShowCoroutine());
        }
        
        private void OnRestartButtonClicked()
        {
            m_RestartButton.interactable = false;
            Scene activeScene = SceneManager.GetActiveScene();
            int buildIndex = activeScene.buildIndex;
            SceneManager.LoadScene(buildIndex);
        }
        
        private IEnumerator ShowCoroutine()
        {
            m_Panel.SetActive(true);
            m_CanvasGroup.alpha = 0;
            float time = 0;
            while (time < 1)
            {
                time += Time.deltaTime;
                m_CanvasGroup.alpha = time;
                yield return null;
            }
        }
    }
}