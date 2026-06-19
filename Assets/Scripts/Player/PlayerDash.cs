using DG.Tweening;
using Eclipse.Audio;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Eclipse
{
    public class PlayerDash : MonoBehaviour
    {

        [SerializeField] ParticleSystem fx_dash_light;
        [SerializeField] ParticleSystem fx_dash_dark;
       


        public enum Player
        {
            Dark,
            Light
        }

        public Player _player;
        public float _dashForce = 10f;
        Rigidbody _rigidbody;

        public bool _isDashing = false;
        public float _dashDuration = 1;

        public bool _canDash = true;

        float fx_startSpeed;

        PlayerInput _playerInput;
        Eclipse.Audio.PlayerType m_audioPlayerType;

        void Start()
        {
            _playerInput = GetComponentInParent<PlayerInput>();
            _rigidbody = GetComponent<Rigidbody>();


            fx_startSpeed = fx_dash_light.main.startSpeed.constant;


        }


        void Update()
        {
            if (_playerInput.actions["Dash"].WasPressedThisFrame() && !_isDashing && _canDash)
            {
                Dash();
            }
        }




        void Dash()
        {
            StartCoroutine(DashCoroutine());

        }

        IEnumerator DashCoroutine()
        {
            _isDashing = true;


            Vector2 input = _playerInput.actions["Move"].ReadValue<Vector2>();

            // optionally freeze Y too so gravity doesn't drag mid-dash
            _rigidbody.linearVelocity = new Vector3(_rigidbody.linearVelocity.x, 0f, 0f);
            _rigidbody.useGravity = false;

            PlayerMovement movement = GetComponent<PlayerMovement>();

            //movement._playerAnimator.SetBool("isDashing", _isDashing);

            movement._playerAnimator.SetTrigger("dash");

            switch (_player)
            {
                case Player.Dark:
                    m_audioPlayerType = PlayerType.Dark;

                    _canDash = false;


                    if (!movement._isGrounded)
                    {
                        _rigidbody.linearVelocity = new Vector3(_rigidbody.linearVelocity.x, -_dashForce, _rigidbody.linearVelocity.z);

                    }
                    else
                    {
                        _rigidbody.linearVelocity = new Vector3(_rigidbody.linearVelocity.x, _dashForce / 2, _rigidbody.linearVelocity.z);
                    }

                    // dash animation trigger 
                    //------------------------------------------------------------
                    //movement._playerAnimator.SetTrigger("dash_Dark");

                    fx_dash_dark.Play();

                    AudioEvents.DashVoice(m_audioPlayerType);
                    AudioEvents.Dash(m_audioPlayerType);

                    break;

                case Player.Light:
                    m_audioPlayerType = PlayerType.Light;

                    _canDash = false;

                    float dir = input.x != 0 ? Mathf.Sign(input.x) : movement.playerVisual.transform.forward.x;

                    var fx_direction = fx_dash_light.main;
                    fx_direction.startSpeed = new ParticleSystem.MinMaxCurve(fx_startSpeed * dir);
                    fx_dash_light.Play();

                    if (!movement._isGrounded)
                    {
                        _rigidbody.linearVelocity = new Vector3(dir * _dashForce, 0f, _rigidbody.linearVelocity.z);

                    }
                    else
                    {
                        _rigidbody.linearVelocity = new Vector3(dir * _dashForce / 2, 0f, _rigidbody.linearVelocity.z);

                    }

                    // dash animation trigger
                    //-----------------------------------------------------
                    //movement._playerAnimator.SetTrigger("dash_Light");

                    AudioEvents.DashVoice(m_audioPlayerType);
                    AudioEvents.Dash(m_audioPlayerType);


                    break;
            }

            StartCoroutine(DashScaler(movement.playerVisual));

            yield return new WaitForSeconds(_dashDuration);

            _isDashing = false;
            _rigidbody.useGravity = true;

            //movement._playerAnimator.SetBool("isDashing", _isDashing);

        }

        IEnumerator DashScaler(GameObject playerVisual)
        {
            playerVisual.transform.localScale = new Vector3(transform.localScale.x, Mathf.Lerp(transform.localScale.y, .5f, 0.5f), transform.localScale.z);

            yield return new WaitForSeconds(0.1f);

            playerVisual.transform.localScale = new Vector3(transform.localScale.x, Mathf.Lerp(transform.localScale.y, 1, 0.5f), transform.localScale.z);

        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_isDashing && collision.gameObject.CompareTag("Break"))
            {

                //insert break animation trigger here


                if (_player == Player.Dark)
                {
                    Breakable breakable = collision.gameObject.GetComponentInParent<Breakable>();
                    breakable.Break();
                    //collision.gameObject.SetActive(false);
                }


            }
        }
    }
}

