using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Eclipse
{
    public class MainMenu : MonoBehaviour
    {
        [FormerlySerializedAs("m_playBtn")]
        [SerializeField] private UnityEngine.UI.Button m_playBtn;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            m_playBtn.onClick.AddListener(PlayBtnClicked);
        }

        private void PlayBtnClicked()
        {
            SceneManager.LoadScene("LevelTutorial");
        }
    }
}

