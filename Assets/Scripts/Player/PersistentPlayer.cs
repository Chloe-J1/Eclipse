using UnityEngine;


namespace Eclipse
{
    public class PersistentPlayer : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void DestroyInstance()
        {
            Destroy(gameObject);
        }
    }
}
