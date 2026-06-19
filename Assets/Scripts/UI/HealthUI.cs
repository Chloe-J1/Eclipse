using System.Collections.Generic;
using UnityEngine;

namespace Eclipse
{
    public class HealthUI : MonoBehaviour
    {

        public GameObject m_heartPrefab;

        [SerializeField] private List<GameObject> m_hearts = new List<GameObject>();

        private void OnEnable()
        {
            HealthManager.OnHealthChanged += UpdateHealthUI;
        }

        private void OnDisable()
        {
            HealthManager.OnHealthChanged -= UpdateHealthUI;
        }

        private void Start()
        {
            // Build the initial heart icons based on current health
            SpawnHearts(HealthManager.Instance.Health);
            UpdateHealthUI(HealthManager.Instance.Health);
        }

        private void SpawnHearts(int maxHealth)
        {
            foreach (var heart in m_hearts)
                Destroy(heart);

            m_hearts.Clear();

            for (int i = 0; i < maxHealth; i++)
            {
                GameObject heart = Instantiate(m_heartPrefab, transform);
                m_hearts.Add(heart);
            }
        }

        private void UpdateHealthUI(int currentHealth)
        {
            for (int i = 0; i < m_hearts.Count; i++)
            {
                // Hearts at or below current health are active, the rest are hidden
                m_hearts[i].SetActive(i < currentHealth);
            }
        }

    }

}
