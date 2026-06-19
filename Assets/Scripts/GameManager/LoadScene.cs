using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Eclipse
{
    public class LoadScene : MonoBehaviour
    {

        List<GameObject> m_players = new List<GameObject>();

        [SerializeField] Camera m_LightCamera, m_DarkCamera;

        public List<GameObject> m_UIElements;

        public GameObject m_Flash,m_ending;
        public float m_timer = 1f;



        public string m_sceneToLoad;



        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                m_players.Add(other.gameObject);

                if (other.GetComponent<PlayerMovement>().m_playerType == Eclipse.Audio.PlayerType.Light)
                {
                    m_LightCamera = other.GetComponentInChildren<Camera>();
                }
                else if (other.GetComponent<PlayerMovement>().m_playerType == Eclipse.Audio.PlayerType.Dark)
                {
                    m_DarkCamera = other.GetComponentInChildren<Camera>();
                }

            }

            if (m_players.Count == 2)
            {

                StartCoroutine(EndSequence());


                /*HealthManager.Instance.ResetHealth();
                foreach (var p in m_players)
                {
                    p.GetComponentInParent<PersistentPlayer>().DestroyInstance();

                }
                SceneManager.LoadScene(m_sceneToLoad);*/
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                m_players.Remove(other.gameObject);
            }
        }


        IEnumerator EndSequence()
        {
            Debug.Log("EndSequence started");
            //m_Flash.SetActive(true);
            Vector3 originalScale = m_Flash.transform.localScale;
            float elapsed = 0f;

            m_DarkCamera.GetComponentInParent<PlayerMovement>().enabled = false;
            m_LightCamera.GetComponentInParent<PlayerMovement>().enabled = false;

            while (elapsed < m_timer)
            {
                elapsed += Time.deltaTime;
                m_Flash.transform.localScale = Vector3.Lerp(m_Flash.transform.localScale, Vector3.one * 40, elapsed / m_timer);
                yield return null;
            }

            yield return new WaitForSeconds(.5f);

            Debug.Log("Changed cameras");

            //m_LightCamera.rect = new Rect(0, 0.5f, 1, .5f);
            m_LightCamera.cullingMask |= LayerMask.GetMask("Player Dark");
            m_LightCamera.cullingMask &= ~LayerMask.GetMask("Player Light");

            //m_DarkCamera.rect = new Rect(0, 0, 1, .5f);
            m_DarkCamera.cullingMask |= LayerMask.GetMask("Player Light");
            m_DarkCamera.cullingMask &= ~LayerMask.GetMask("Player Dark");

            foreach(GameObject ui in m_UIElements)
            {
                ui.SetActive(false);
            }
            

            yield return new WaitForSeconds(1f);
            Debug.Log("Flashing out");
            m_ending.SetActive(true);

            HealthManager.Instance.ResetHealth();
            foreach (var p in m_players)
            {
                p.GetComponentInParent<PersistentPlayer>().DestroyInstance();

            }

            elapsed = 0f;

            while (elapsed < m_timer)
            {
                elapsed += Time.deltaTime;
                m_Flash.transform.localScale = Vector3.Lerp(Vector3.one * 40, originalScale, elapsed / m_timer);
                yield return null;
            }

            
            

            yield return new WaitForSeconds(30f);

            
            SceneManager.LoadScene(m_sceneToLoad);
        }
    }
}

