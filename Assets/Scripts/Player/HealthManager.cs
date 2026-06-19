using System;

namespace Eclipse
{
    public class HealthManager : MonobehaviourSingleton<HealthManager>
    {
        int m_health;
        const int m_maxHealth = 3;
        public static event Action<int> OnHealthChanged;
        
public static event Action OnPlayerDied;
        public int Health
        {
            get { return m_health; }
        }
        protected override void Awake()
        {
            base.Awake(); // Call singleton awake implementation
            m_health = m_maxHealth;
        }

        public void ResetHealth()
        {
            m_health = 3;
            OnHealthChanged?.Invoke(m_health);
        }

        public void TakeDamage(int nrOfDamage)
        {
            if (m_health <= 0) return;
            m_health -= nrOfDamage;
            OnHealthChanged?.Invoke(m_health);

            // Handle player death
            if (m_health <= 0)
            {
                OnPlayerDied?.Invoke();
            }
        }
    }
}

