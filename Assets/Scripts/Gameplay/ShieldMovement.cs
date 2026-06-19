using Eclipse.Audio;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace Eclipse
{
    public class ShieldMovement : MonoBehaviour
    {
        PlayerInput m_playerInput;
        [SerializeField] float m_rotationSpeed = 50f;
        [SerializeField] GameObject m_shield;
        bool m_audioActive = false;

        private PlayerMovement m_playerMovement;


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            m_playerInput = GetComponentInParent<PlayerInput>();
            m_playerMovement = GetComponentInParent<PlayerMovement>();
            m_shield.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (m_playerInput.actions == null || m_shield == null)
            {
                Debug.LogError("PlayerInput.actions / shield not found");
                return;
            }
            ToggleShield();
            Aim();

        }

        private void Aim()
        {
            Vector2 joystickInput = m_playerInput.actions["Look"].ReadValue<Vector2>();

            if (joystickInput.magnitude < 0.1f) return; // deadzone = no input

            // Calculate angle to aim at & interpolate rotation towards it
            float aimAngle = Mathf.Atan2(joystickInput.y, joystickInput.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, aimAngle);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * m_rotationSpeed);
        }

        void ToggleShield()
        {

            if (!m_playerMovement.abilityIsActive) return;

            if (m_playerInput.actions["SpecialMove"].IsPressed())
            {
                m_shield.SetActive(true);
                //SND Start shield
                if (!m_audioActive)
                {
                    AudioEvents.ShieldEquip();
                    m_audioActive = true;
                }
            }
            if (m_playerInput.actions["SpecialMove"].WasReleasedThisFrame())
            {
                m_shield.SetActive(false);
                //SND End shield
                AudioEvents.ShieldStow();
                m_audioActive = false;

            }

        }
    }

}
