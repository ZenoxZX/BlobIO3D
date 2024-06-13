using TMPro;
using UnityEngine;

namespace BlobIO.Game
{
    public class ScoreGUI : MonoBehaviour
    {
        private TextMeshProUGUI m_ScoreText;

        private void Awake()
        {
            m_ScoreText = GetComponent<TextMeshProUGUI>();
            m_ScoreText.SetText("Score : 0");
        }

        private void Start()
        {
            Player.Instance.LevelChanged += UpdateText;
        }

        private void UpdateText(int score) => m_ScoreText.SetText($"Score : {score - 1}");
    }
}