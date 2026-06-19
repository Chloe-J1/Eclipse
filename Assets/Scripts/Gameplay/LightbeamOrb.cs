using Eclipse.Audio;
using System;
using System.Collections;
using UnityEngine;

namespace Eclipse
{
    public class LightbeamOrb : MonoBehaviour
    {
        private LineRenderer m_lineRenderer;
        private int m_maxPoints;
        private int m_maxDistance = 50;
        private bool m_gotPickedUp = false;
        public LayerMask m_lightLayer;

        public EventHandler<ButtonArgs> m_buttonHit;

        private bool m_isShieldHitThisFrame = false;
        private bool m_isShieldHitPreviousFrame = false;

        //---------------------------------------------------------
        [SerializeField] ParticleSystem reflectionHitFX;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            m_lineRenderer = GetComponent<LineRenderer>();
            m_maxPoints = m_lineRenderer.positionCount;

            reflectionHitFX.transform.position = new Vector3(-100, -100, -100);

        }

        // Update is called once per frame
        void Update()
        {
            CastLightBeam();
            reflectionHitFX.Play();
        }

        private void CastLightBeam()
        {
            Vector3 origin = transform.position;
            Vector3 direction = Vector3.down;

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

                    if (Physics.Raycast(ray, out hit, m_maxDistance))
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
                        m_lineRenderer.SetPosition(index, ray.GetPoint(m_maxDistance));
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
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player") && 1 << collision.gameObject.layer == m_lightLayer.value)
            {
                collision.gameObject.GetComponent<PlayerMovement>().abilityIsActive = true;

                AudioEvents.PickupOrb();

                //enable in playerscript the ability to cast lightbeam
                //Destroy(this.gameObject);
                StartCoroutine(DestroyOnDelay());
            }
        }

        IEnumerator DestroyOnDelay()
        {

            yield return new WaitForSeconds(.01f);
            Destroy(this.gameObject);
        }
    }
}

