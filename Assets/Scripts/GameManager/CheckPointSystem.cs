using System.Collections;
using UnityEngine;

namespace Eclipse
{
    public class CheckPointSystem : MonoBehaviour
    {
        [SerializeField] Transform m_firstSpawnPos;
        [SerializeField] Transform m_secondSpawnPos;
        private Vector3[] m_checkPoints = new Vector3[2];
        private GameObject[] m_players;
        bool m_canResetLevel = true;

        void Start()
        {
            if (m_firstSpawnPos == null || m_secondSpawnPos == null) Debug.LogError("No spawn points set!");
            m_players = GameObject.FindGameObjectsWithTag("Player");

            m_checkPoints[0] = m_firstSpawnPos.position;
            m_checkPoints[1] = m_secondSpawnPos.position;
        }

        public void RespawnPlayers()
        {
            if (m_canResetLevel == false) return; // Prevent calling this function twice bc players die at same time
            for (int index = 0; index < m_checkPoints.Length; index++)
            {
                m_players[index].transform.position = m_checkPoints[index];

                foreach (Transform child in m_players[index].transform)
                {
                    child.localPosition = Vector3.zero;
                    child.localRotation = Quaternion.identity;
                }
            }
            StartCoroutine(AllowRespawnTimer());
        }

        IEnumerator AllowRespawnTimer()
        {
            const int allowRespawnTime = 1;
            m_canResetLevel = false;
            yield return new WaitForSeconds(allowRespawnTime);
            m_canResetLevel = true;
        }

        public void SetCheckpointPos(Vector3 checkpointPos)
        {
            const float offset = 0.5f;
            m_checkPoints[0] = checkpointPos;
            m_checkPoints[0] += new Vector3(offset, 0, 0);
            m_checkPoints[1] = checkpointPos;
            m_checkPoints[1] += new Vector3(-offset, 0, 0);
        }
    }
}

