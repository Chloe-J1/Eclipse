using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Audio;

namespace Eclipse
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_mainMenu;
        [SerializeField]
        private GameObject m_settingsMenu;
        [SerializeField]
        private GameObject m_gamOverMenu;

        [SerializeField]
        private GameObject m_defaultMainMenu;
        [SerializeField]
        private GameObject m_defaultSettings;
        [SerializeField]
        private GameObject m_defaultGameOver;

        [SerializeField]
        private GameObject m_indicator;
        [SerializeField]
        private Vector2 m_offset;
        [SerializeField]
        private float m_sliderOffset;

        [Header("Audio")]
        [SerializeField] AudioMixer m_mixer;

        private void Start()
        {
            if (SceneManager.GetActiveScene().name != "MainMenu")
            {
                PlaceIndicator(m_defaultMainMenu);
            }
        }

        public void OnPause()
        {
            Time.timeScale = .001f;
        }

        public void OnClick()
        {
        }

        public void OnBack()
        {
            if (SceneManager.GetActiveScene().name == "MainMenu")
            {
                m_mainMenu.SetActive(true);
                m_settingsMenu.SetActive(false);

                PlaceIndicator(m_defaultMainMenu);
            }
        }

        public void OnScroll(InputAction.CallbackContext obj)
        {
            PlaceIndicator(null);
        }

        public void OnStart()
        {
            SceneManager.LoadScene("CharacterSelectionMenu");
        }

        public void OnQuit()
        {
            Application.Quit();
        }

        public void OnSettingsOpen()
        {
            m_mainMenu.SetActive(false);
            m_settingsMenu.SetActive(true);

            //sets default button when openening settings
            PlaceIndicator(m_defaultSettings);
        }

        public void OnRestart()
        {
            SceneManager.LoadScene("LevelTutorial");
        }

        public void OnMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void OnGameOver()
        {
            //OnPause();
            m_gamOverMenu.SetActive(true);
            m_indicator.SetActive(true);

            //sets default button when openening settings
            PlaceIndicator(m_defaultGameOver);
        }

        private void PlaceIndicator(GameObject button)
        {
            if (button != null)
            {
                EventSystem.current.SetSelectedGameObject(button);
            }

            GameObject currentGo = EventSystem.current.currentSelectedGameObject;
            //float offset = currentGo.CompareTag("Slider") ? currentGo.transform.parent.position.x + m_offset.x : m_offset.x;
        
            //m_indicator.transform.position = currentGo.transform.position.y*Vector3.up + new Vector3(offset, m_offset.y, 0);
            //m_indicator.transform.position = currentGo.transform.position + new Vector3(m_offset.x, m_offset.y, 0);
            m_indicator.transform.position = new Vector3(m_indicator.transform.position.x, currentGo.transform.position.y+m_offset.y, 0);
        }

        public void SetMasterVolume(float sliderValue)
        {
            m_mixer.SetFloat("Master Volume", Mathf.Log10(sliderValue) * 20);
        }

        public void SetAmbienceVolume(float sliderValue)
        {
            m_mixer.SetFloat("Ambience Volume", Mathf.Log10(sliderValue) * 20);
        }

        public void SetEnvSFXVolume(float sliderValue)
        {
            m_mixer.SetFloat("Environment SFX", Mathf.Log10(sliderValue) * 20);
        }

        public void SetPlayerVolume(float sliderValue)
        {
            m_mixer.SetFloat("Player Volume", Mathf.Log10(sliderValue) * 20);
        }

        public void SetMusicVolume(float sliderValue)
        {
            m_mixer.SetFloat("Music Volume", Mathf.Log10(sliderValue) * 20);
        }
    }
}

