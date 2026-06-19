using UnityEngine;

namespace Eclipse
{
    public class CheckPoint : MonoBehaviour
    {
        private CheckPointSystem m_checkPointSystem;
        [SerializeField] private Material m_greenMat;
        private Renderer m_renderer;

        void Start()
        {
            m_checkPointSystem = FindFirstObjectByType<CheckPointSystem>();
            if (m_checkPointSystem == null) Debug.LogError("No checkpoint system found!");
            m_renderer = GetComponentInChildren<Renderer>();
            if (m_renderer == null) Debug.LogError("No renderer found!");
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                m_checkPointSystem.SetCheckpointPos(gameObject.transform.position);
                GetComponent<Collider>().enabled = false;
                m_renderer.material = m_greenMat;
            }
        }
    }
}

