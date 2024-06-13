using UnityEngine;
using Random = UnityEngine.Random;

namespace BlobIO.Game
{
    public class SFXManager : MonoBehaviour
    {
        private AudioSource m_HitAudioSource;
        private AudioSource m_CollectAudioSource;
        
        [SerializeField] private AudioClip m_HitClip;
        [SerializeField] private AudioClip m_CollectClip;

        private void Awake()
        {
            m_HitAudioSource = gameObject.AddComponent<AudioSource>();
            m_CollectAudioSource = gameObject.AddComponent<AudioSource>();
            
            m_HitAudioSource.playOnAwake = false;
            m_CollectAudioSource.playOnAwake = false;
            
            m_HitAudioSource.clip = m_HitClip;
            m_CollectAudioSource.clip = m_CollectClip;
        }

        private void Start()
        {
            Player.Instance.LevelChanged += OnLevelChanged;
            GameManager.Instance.PlayerHitEnemy += OnEnemyDied;
            GameManager.Instance.LevelFailed += args => OnEnemyDied(null);
        }

        private void OnEnemyDied(Enemy obj)
        {
            m_HitAudioSource.pitch = Random.Range(0.95f, 1.05f);
            m_HitAudioSource.Play();
        }

        private void OnLevelChanged(int level)
        {
            m_CollectAudioSource.pitch = Random.Range(0.95f, 1.05f);
            m_CollectAudioSource.Play();
        }
    }
}