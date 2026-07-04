using UnityEngine;
using System;
using System.Linq;

namespace Eclipse
{
    public class Button : MonoBehaviour
    {
        private Animator m_animator;
        public event EventHandler m_buttonActivatedEvent;

        //private Lightbeam m_lightBeam;
        private Lightbeam[] m_lightbeams;
        private bool m_isPressed = false;
        private Renderer m_renderer;

        void Start()
        {
            m_lightbeams = FindObjectsByType<Lightbeam>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            foreach (Lightbeam orb in m_lightbeams)
            {
                orb.m_buttonHit += OnButtonHit;
            }

            m_animator = GetComponentInChildren<Animator>();
            if (m_animator == null) Debug.LogError("No animator found");

            m_renderer = GetComponentInChildren<Renderer>();
            if (m_renderer == null) Debug.LogError("No renderer found");
        }

        private void OnButtonHit(object sender, ButtonArgs hitButtonArgs)
        {
            if(hitButtonArgs.m_hitButton == transform.gameObject)
            {
                m_animator.SetBool("isPressed", true);
                m_buttonActivatedEvent?.Invoke(this, EventArgs.Empty);
                m_isPressed = true;
                m_renderer.material.SetInt("_onHit", 1);
            }
        }

        void Update()
        {
            if(m_isPressed == false)
            {
                m_animator.SetBool("isPressed", false);
                m_renderer.material.SetInt("_onHit", 0);
            }
            m_isPressed = false;
        }
    }
}
