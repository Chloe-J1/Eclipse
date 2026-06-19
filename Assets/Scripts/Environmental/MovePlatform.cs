using UnityEngine;

namespace Eclipse
{
    public class MovePlatform : MonoBehaviour
    {
        public Transform[] m_positions;
        [SerializeField]
        float m_moveSpeed;
        int m_currPointIndex = 0;

        private void Start()
        {
            transform.position = m_positions[0].position;
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            const float epsilon = 0.1f;
            if (Vector2.Distance(transform.position, m_positions[m_currPointIndex].position) < epsilon)
            {
                m_currPointIndex++;
                if (m_currPointIndex >= m_positions.Length)
                    m_currPointIndex = 0;
            }

            transform.position = Vector2.MoveTowards(transform.position, m_positions[m_currPointIndex].position, Time.deltaTime * m_moveSpeed);
        }


        // Make objects on the platform move along with the platform
        private void OnCollisionEnter(Collision other)
        {
            other.gameObject.transform.SetParent(this.transform);
        }

        private void OnCollisionExit(Collision other)
        {
            other.gameObject.transform.SetParent(null);
        }
    }
}

