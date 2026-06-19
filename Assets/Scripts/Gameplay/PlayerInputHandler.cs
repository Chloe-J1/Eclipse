using UnityEngine;

namespace Eclipse
{
    public class PlayerInputHandler : MonoBehaviour
    {
        [SerializeField] private GameObject m_lightPrefab;
        [SerializeField] private GameObject m_darkPrefab;
        public Transform[] m_spawnPoints;

        private void Awake()
        {
            if (m_spawnPoints == null)
            {
                Debug.LogError("No spawn points set");
                return;
            }

            GameObject[] persistentPlayers = GameObject.FindGameObjectsWithTag("Player");

            GameObject go = Instantiate(m_darkPrefab);
            go.transform.SetParent(persistentPlayers[0].transform, false);
            persistentPlayers[0].transform.position = m_spawnPoints[0].position;

            go = Instantiate(m_lightPrefab);
            go.transform.SetParent(persistentPlayers[1].transform, false);
            persistentPlayers[1].transform.position = m_spawnPoints[1].position;
        }
    }
}

