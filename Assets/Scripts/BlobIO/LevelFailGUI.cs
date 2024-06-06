using System.Collections;
using TMPro;
using UnityEngine;

namespace BlobIO
{
    public class LevelFailGUI : MonoBehaviour
    {
        [SerializeField] private GameManager m_GameManager;
        [SerializeField] private GameObject m_Panel;
        [SerializeField] private CanvasGroup m_CanvasGroup;
        [SerializeField] private TextMeshProUGUI m_LevelText;
        
        private void OnEnable()
        {
            m_GameManager.LevelFailed += Show;
        }

        private void Show(LevelFailArgs args)
        {
            m_LevelText.text = $"Player Level : {args.Level}, Player Name : {args.PlayerName}, Game Over : {args.IsGameOver}";
            StartCoroutine(ShowCoroutine());
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