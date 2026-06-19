using Eclipse.Audio;
using JetBrains.Annotations;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.UI.Image;

namespace Eclipse
{
    public class Lightbeam : MonoBehaviour
    {
        [SerializeField] private LineRenderer m_lineRenderer;
        [SerializeField] private PlayerInput m_playerInput;
        [SerializeField] GameObject m_lightOrb;

        //Add Particles
        [SerializeField] ParticleSystem lightOrbFX;
        [SerializeField] ParticleSystem reflectionHitFX;


        private int m_maxPoints;

        private int m_maxDisctance = 50;
        private bool m_isBeamActive = false;
        private bool m_startedLightBeamSFX = false;

        private PlayerMovement m_playerMovement;

        private bool m_isShieldHitThisFrame = false;
        private bool m_isShieldHitPreviousFrame = false;

        public EventHandler<ButtonArgs> m_buttonHit;
        void Start()
        {
            // Get components
            m_playerMovement = GetComponentInParent<PlayerMovement>();
            m_lineRenderer = GetComponent<LineRenderer>();
            m_playerInput = GetComponentInParent<PlayerInput>();

            // Initialize linerenderer
            m_lightOrb.SetActive(false);
            m_maxPoints = m_lineRenderer.positionCount;
            m_lineRenderer.enabled = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (m_playerInput == null)
            {
                Debug.LogError("PlayerInput is missing");
                return;
            }



            ToggleLightbeam();
            if (m_isBeamActive)
            {
                Vector3 aimDirection = GetAimDirection();

                CastLightbeam(m_lightOrb.transform.position, aimDirection);
            }




        }

        Vector3 GetAimDirection()
        {
            Vector2 joystickDirection = m_playerInput.actions["Look"].ReadValue<Vector2>();

            // Aim upwards if there is no joystick input
            if (joystickDirection == Vector2.zero)
                joystickDirection = new Vector2(0, 1);

            //---------------------------------------------------------------------------------------
            reflectionHitFX.transform.position = new Vector3(-100, -100, -100);

            return joystickDirection;
        }

        void CastLightbeam(Vector3 origin, Vector3 direction)
        {
            m_isShieldHitThisFrame = false;
            for (int index = 0; index < m_maxPoints; ++index)
            {
                
                if (index == 0)
                {
                    // Make a start point
                    m_lineRenderer.SetPosition(0, origin);
                    m_lineRenderer.positionCount = m_maxPoints;

                }
                else
                {
                    // Check for hits
                    Ray ray = new Ray(origin, direction);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, m_maxDisctance))
                    {
                        //-------------------------------------------------
                        var reflectionmain = reflectionHitFX.main;
                        //reflectionmain.startColor = Color.yellow;

                        
                        reflectionHitFX.GetComponent<ParticleSystemRenderer>().material.SetFloat("_onHit", 0f);


                        reflectionHitFX.transform.position = hit.point;

                        float angle = Mathf.Atan2(hit.normal.y, hit.normal.x) * Mathf.Rad2Deg;


                        reflectionmain.startRotation = (angle - 90f) * Mathf.Deg2Rad;

                        if (hit.collider.CompareTag("Shield"))
                        {
                            m_isShieldHitThisFrame = true;

                            Reflect(ref origin, ref direction, hit, index);
                        }
                        else if (hit.collider.transform.CompareTag("Mirror"))
                        {
                            Reflect(ref origin, ref direction, hit, index);
                        }
                        else
                        {
                            //Raise button hit event
                            if (hit.collider.transform.CompareTag("Button"))
                            {

                                //--------------------------------------------------

                                reflectionHitFX.GetComponent<ParticleSystemRenderer>().material.SetFloat("_onHit", 1f);


                                m_buttonHit?.Invoke(this, new ButtonArgs(hit.transform.gameObject));

                            }
                            ShieldHitCheck();

                            // Stop reflecting
                            m_lineRenderer.SetPosition(index, hit.point);
                            m_lineRenderer.positionCount = index + 1;
                            return;
                        }

                    }
                    else
                    {
                        ShieldHitCheck();

                        // No hit, aim at joystick direction
                        m_lineRenderer.SetPosition(index, ray.GetPoint(m_maxDisctance));
                        m_lineRenderer.positionCount = index + 1;
                        return;
                    }
                }
                
            }
        }

        private void ShieldHitCheck()
        {
            if (m_isShieldHitPreviousFrame && m_isShieldHitThisFrame == false)
            {
                // SND shield NOT hit
                AudioEvents.ChangeLightLoopPitch(false);
            }
            if (m_isShieldHitThisFrame && m_isShieldHitPreviousFrame == false)
            {
                // SND shield hit
                AudioEvents.ChangeLightLoopPitch(true);
            }
            m_isShieldHitPreviousFrame = m_isShieldHitThisFrame;
        }

        private void Reflect(ref Vector3 origin, ref Vector3 direction, RaycastHit hit, int idx)
        {
            origin = hit.point;
            direction = Vector3.Reflect(direction, hit.normal);
            m_lineRenderer.SetPosition(idx, hit.point);
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
                m_isBeamActive = true;
                m_lightOrb.SetActive(true);

                //--------------------------------------------------------------------------
                lightOrbFX.Play();
                reflectionHitFX.Play();

                m_lineRenderer.enabled = true;
                m_playerMovement.CanMove = false;


                //SND Start lightbeam
                AudioEvents.LightBeamStart();
                m_startedLightBeamSFX = true;

            }
            if (m_playerInput.actions["SpecialMove"].WasReleasedThisFrame())
            {
                m_isBeamActive = false;
                m_lightOrb.SetActive(false);

                //---------------------------------------------------------------------------
                lightOrbFX.Stop();
                reflectionHitFX.Stop();

                m_lineRenderer.enabled = false;
                m_playerMovement.CanMove = true;
                //SND End lightbeam
                AudioEvents.LightBeamEnd();
                m_startedLightBeamSFX = false;
            }
        }
    }

    public class ButtonArgs : EventArgs
    {
        public GameObject m_hitButton { get; }
        public ButtonArgs(GameObject hitButton)
        {
            m_hitButton = hitButton;
        }
    }

}
