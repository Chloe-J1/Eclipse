using Eclipse.Audio;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Eclipse
{
    public class PlayerHealth : MonoBehaviour
    {
        private bool m_isInvincible = false;
        private const float m_invincibleTime = 1f;

        [SerializeField] ParticleSystem fx_Hurt;
        private Material playerMat;

        [SerializeField] private PlayerMovement m_movement;
        private Rigidbody m_rigidbody;

        [SerializeField] float m_knockbackForce;
        [SerializeField] PlayerType m_playerType;
        private void Start()
        {
            Renderer renderer = GetComponentInChildren<Renderer>();
            playerMat = renderer.sharedMaterial;
            playerMat.SetFloat("_isHurt", 0f);
            fx_Hurt.Stop();


            m_movement = GetComponent<PlayerMovement>();
            if (m_movement == null) Debug.LogError("No movement found");
            m_rigidbody = GetComponent<Rigidbody>();
            if (m_rigidbody == null) Debug.LogError("No Rigidbody found");
        }

        private void OnEnable()
        {
            HealthManager.OnPlayerDied += PlayerDied;

        }

        private void OnDisable()
        {
            HealthManager.OnPlayerDied -= PlayerDied;
        }

        private void PlayerDied()
        {
            m_movement._playerAnimator.SetTrigger("dead");
            m_movement._playerAnimator.SetBool("hasDied", true);
            AudioEvents.DeathVoice(m_playerType);

        }

        IEnumerator InvincibleTimer()
        {
            playerMat.SetFloat("_isHurt", 1f);

            m_isInvincible = true;
            yield return new WaitForSeconds(m_invincibleTime);
            m_isInvincible = false;

            playerMat.SetFloat("_isHurt", 0f);
        }


        private void OnTriggerStay(Collider other)
        {
            DamagePlayer(other);
        }

        private void OnTriggerExit(Collider other)
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            if (damageable is Thorns thorns)
            {
                thorns.StopAttack();
            }
        }

        void DamagePlayer(Collider other)
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            if (damageable == null || HealthManager.Instance.Health <= 0) return;

            if (!m_isInvincible)
            {
                // Change health
                int nrOfDamage = damageable.Attack();
                HealthManager.Instance.TakeDamage(nrOfDamage);

                AudioEvents.PainVoice(m_playerType);

                Vector3 knockbackDir = new Vector3(0, 1f, 0);
                m_rigidbody.AddForce(knockbackDir * m_knockbackForce);

                fx_Hurt.Play();

                // Check if screenshake is possible
                IScreenShake screenShake = other.GetComponent<IScreenShake>();
                if (screenShake != null)
                {
                    Camera cam = gameObject.GetComponentInChildren<Camera>();
                    Gamepad gamepad = GetComponentInParent<PlayerInput>().GetDevice<Gamepad>();
                    screenShake.ShakeCamera(cam, gamepad);
                }

                StartCoroutine(InvincibleTimer());
            }
        }


    }
}

