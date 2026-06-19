using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Eclipse
{
    public class CharacterSelectionMenu : MonoBehaviour
    {
        int m_nrConnectedDevices = 0;
        const int m_maxNrPlayers = 2;
        [SerializeField] GameObject m_greyOverlayDark;
        [SerializeField] GameObject m_greyOverlayLight;
        [SerializeField] GameObject m_startText;
        [SerializeField] GameObject m_joinDark;
        [SerializeField] GameObject m_joinLight;
        bool m_canStart = false;
        private void Start()
        {
            PlayerInputManager.instance.onPlayerJoined += OnPlayerJoined;
            m_startText.SetActive(false);
        }

        private void OnDestroy()
        {
            if(PlayerInputManager.instance)
                PlayerInputManager.instance.onPlayerJoined -= OnPlayerJoined;
        }

        private void OnPlayerJoined(PlayerInput input)
        {
            ++m_nrConnectedDevices;
            if (m_nrConnectedDevices == 1)
            {
                m_joinDark.SetActive(false);
                m_greyOverlayDark.SetActive(false);
            }
            else
            {
                m_joinLight.SetActive(false);
                m_greyOverlayLight.SetActive(false);
                StartCoroutine(EnableCanStart());
            }
        }

        IEnumerator EnableCanStart()
        {
            const float waitTime = 0.5f;
            yield return new WaitForSeconds(waitTime);
            m_joinLight.SetActive(false);
            m_startText.SetActive(true);
            m_canStart = true;
        }

        void Update()
        {
            CheckStartGame();
        }

        void CheckStartGame()
        {
            if (m_canStart == false) return;
            if (Gamepad.current.buttonSouth.wasReleasedThisFrame && m_nrConnectedDevices == m_maxNrPlayers)
            {
                SceneManager.LoadScene("LevelTutorial");
            }
        }
    }
}


