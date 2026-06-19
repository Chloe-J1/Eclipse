using UnityEngine;

public class AutoKill : MonoBehaviour
{
    [SerializeField] float m_lifeTime = 5.0f;

    private void Awake()
    {
        Invoke(nameof(Kill), m_lifeTime);
    }

    void Kill()
    {
        Destroy(gameObject);
    }
}
