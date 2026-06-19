using UnityEngine;

namespace Eclipse
{
    public class CameraClipping : MonoBehaviour
    {
        BoxCollider m_levelBoundaries;

        private void Start()
        {
            m_levelBoundaries = GameObject.Find("LevelBoundaries").GetComponent<BoxCollider>();
        }

        void Update()
        {
            if (m_levelBoundaries == null)
            {
                Debug.LogError("level boundaries not found");
            }
            // Clamp the camera within the level bounds
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, m_levelBoundaries.bounds.min.x, m_levelBoundaries.bounds.max.x),
                Mathf.Clamp(transform.position.y, m_levelBoundaries.bounds.min.y, m_levelBoundaries.bounds.max.y),
                transform.position.z); // Don't clip the depth
        }
    }
}

