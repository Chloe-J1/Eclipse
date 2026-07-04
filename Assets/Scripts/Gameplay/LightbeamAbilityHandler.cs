using Eclipse;
using Eclipse.Audio;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Eclipse
{
    public class LightbeamAbilityHandler : MonoBehaviour
    {
        [SerializeField] private PlayerInput m_playerInput;
        private PlayerMovement m_playerMovement;
        [SerializeField] GameObject m_lightOrb;
        Lightbeam m_lightbeam;

        void Start()
        {
            m_playerMovement = GetComponentInParent<PlayerMovement>();
            m_playerInput = GetComponentInParent<PlayerInput>();
            m_lightbeam = GetComponentInChildren<Lightbeam>();
            m_lightOrb.SetActive(false);

            // Subscribe to pickup event of every orb so you know when the ability can be used
            OrbHandler[] orbs = FindObjectsByType<OrbHandler>(FindObjectsSortMode.None);
            foreach (var orb in orbs)
            {
                orb.m_pickedUp += OnOrbPickup;
            }
        }

        private void OnOrbPickup(object sender, EventArgs e)
        {
            m_lightOrb.SetActive(true);
            m_lightbeam.DisableBeam();
        }

        void Update()
        {
            ToggleLightbeam();
            if (m_lightbeam.IsBeamActive)
            {
                m_lightbeam.AimDirection = GetAimDirection();
            }
        }

        Vector3 GetAimDirection()
        {
            Vector2 joystickDirection = m_playerInput.actions["Look"].ReadValue<Vector2>();

            // Aim upwards if there is no joystick input
            if (joystickDirection == Vector2.zero)
                joystickDirection = new Vector2(0, 1);

            //---------------------------------------------------------------------------------------
            //reflectionHitFX.transform.position = new Vector3(-100, -100, -100);

            return joystickDirection;
        }

        private void ToggleLightbeam()
        {

            if (!m_playerMovement.abilityIsActive) return;

            if (m_playerMovement == null)
            {
                Debug.LogError("Player movement not found");
                return;
            }
            if (m_playerInput.actions["SpecialMove"].WasPressedThisFrame())
            {
                m_lightbeam.EnableBeam();
                m_playerMovement.CanMove = false;
            }
            if (m_playerInput.actions["SpecialMove"].WasReleasedThisFrame())
            {
                m_lightbeam.DisableBeam();
                m_playerMovement.CanMove = true;
            }
        }
    }
}