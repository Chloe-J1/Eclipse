using UnityEngine;
namespace Eclipse
{
    public class PoisonPool : MonoBehaviour, IDamageable
    {
        const int m_nrOfDamage = 1;

        private AudioSource m_audioSource;

        private void Awake()
        {
            m_audioSource = GetComponent<AudioSource>();
        }
        public int Attack()
        {
            m_audioSource.Play();
            return m_nrOfDamage;
        }
    }
}

