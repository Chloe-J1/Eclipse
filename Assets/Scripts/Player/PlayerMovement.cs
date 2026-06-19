using Eclipse.Audio;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Eclipse
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Movement")]
        Rigidbody _rb;
        PlayerInput _input;

        public GameObject playerVisual;

        public int _moveSpeed = 6;
        public int _jumpStrength = 10;
        [SerializeField] int _acceleration = 20;
        PlayerDash _dash;
        private bool _canMove = true;
        public bool CanMove
        {
            get { return _canMove; }
            set { _canMove = value; }
        }

        public bool abilityIsActive;


        [Header("GroundCheck")]
        public Transform _groundCheck;
        public bool _isGrounded;
        [SerializeField] LayerMask m_excludeGroundcheckLayer;
        [SerializeField] float _groundCheckRadius;

        [Header("Jump")]
        [SerializeField] float _fallMultiplier = 3.5f;
        [SerializeField] float _GravityMultiplier = 2;

        [Header("Audio")]
        public Eclipse.Audio.PlayerType m_playerType;
        [SerializeField] private bool m_isAirborne = false;
        private bool m_playedLandSFX = false;
        private FootstepHandler m_footstepHandler;


        [Header("Fx")]
        [SerializeField] ParticleSystem fxJump;
        [SerializeField] ParticleSystem fxWalk;

        [Header("Animation")]
        public Animator _playerAnimator;






        void Awake()
        {
            _rb = GetComponent<Rigidbody>();

            _dash = GetComponent<PlayerDash>();
            m_footstepHandler = GetComponentInChildren<FootstepHandler>();

            _playerAnimator = GetComponentInChildren<Animator>();

        }

        void Start()
        {
            _input = GetComponentInParent<PlayerInput>();
            if (_input == null) Debug.LogError("no input found!");
            _input.actions.Enable();
        }

        private void OnDestroy()
        {
            _input.actions.Disable();
        }

        void Update()
        {
            Jump();

            GroundCheck();


        }


        void FixedUpdate()
        {
            Move();
            MultiplyGravity();



        }



        void Move()
        {

            if (_dash._isDashing == false)
            {


                if (_canMove)
                {


                    Vector2 inputVector = _input.actions["Move"].ReadValue<Vector2>();

                    //check if player is moving; play Particles 
                    bool isMoving = Mathf.Abs(inputVector.x) > 0.01f;

                    _playerAnimator.SetBool("isMoving", isMoving);

                    if (isMoving && !m_isAirborne)
                    {
                        if (!fxWalk.isPlaying)
                        {
                            fxWalk.Play();

                        }
                    }
                    else
                    {
                        if (fxWalk.isPlaying)
                        {
                            fxWalk.Stop();
                        }


                    }


                    //move player visual to face direction of movement
                    if (inputVector != Vector2.zero)
                    {


                        if (inputVector.x > 0)
                            playerVisual.transform.rotation = Quaternion.Euler(0, 90, 0);
                        else if (inputVector.x < 0)
                            playerVisual.transform.rotation = Quaternion.Euler(0, -90, 0);
                    }


                    // Directly set horizontal velocity — snappy and responsive
                    float targetVelocityX = inputVector.x * _moveSpeed;

                    _rb.linearVelocity = new Vector3(Mathf.Lerp(_rb.linearVelocity.x, targetVelocityX, _acceleration * Time.deltaTime), _rb.linearVelocity.y, _rb.linearVelocity.z);
                }
                else
                {
                    // Descellerate

                    _rb.linearVelocity = new Vector3(Mathf.Lerp(_rb.linearVelocity.x, 0, _acceleration * Time.deltaTime), _rb.linearVelocity.y, _rb.linearVelocity.z);

                }
            }


        }

        public bool GroundCheck()
        {


            _isGrounded = Physics.Raycast(_groundCheck.position + new Vector3(0, .1f, 0), -_groundCheck.transform.up, _groundCheckRadius, ~m_excludeGroundcheckLayer);



            //_playerAnimator.SetTrigger("hitGround");


            if (_isGrounded && m_isAirborne)
            {
                //Debug.Log("Grounded, can dash again");

                _playerAnimator.ResetTrigger("jump");
                _playerAnimator.SetTrigger("hitGround");

                _dash._canDash = true;

                if (!m_playedLandSFX)
                {
                    m_footstepHandler.LandSFX();
                    m_playedLandSFX = true;
                    StartCoroutine(LandSFXDelay(0.2f));
                }



                //m_isAirborne = false;
            }
            if (_isGrounded) _dash._canDash = true;

            m_isAirborne = !_isGrounded;
            _playerAnimator.SetBool("isAirborne", m_isAirborne);




            return _isGrounded;
        }

        void Jump()
        {
            if (_input.actions["Jump"].WasPressedThisFrame() && _isGrounded)
            {


                // Trigger jump animation
                _playerAnimator.ResetTrigger("hitGround");
                _playerAnimator.SetTrigger("jump");



                fxJump.Play();

                // ADD AUDIO TRIGGER HERE
                AudioEvents.Jump(m_playerType);
                int randomCheck = Random.Range(0, 3);

                if(randomCheck == 2)
                {
                    AudioEvents.JumpVoice(m_playerType);

                }


                // Reset Y velocity before jumping so previous fall speed doesn't interfere
                _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, 0f, _rb.linearVelocity.z);
                _rb.AddForce(Vector3.up * _jumpStrength, ForceMode.Impulse);
            }
        }

        void MultiplyGravity()
        {
            if (_rb.linearVelocity.y < 0f)
            {
                // Falling: apply strong downward gravity for a snappy drop
                _rb.linearVelocity += Vector3.up * Physics.gravity.y * _GravityMultiplier * Time.deltaTime;
            }
            else if (_rb.linearVelocity.y > 0f)
            {
                // Rising: apply moderate extra gravity to reduce floatiness on ascent
                _rb.linearVelocity += Vector3.up * Physics.gravity.y * _GravityMultiplier * Time.deltaTime;
            }
        }

        private IEnumerator LandSFXDelay(float delaytime)
        {
            yield return new WaitForSeconds(delaytime);
            m_playedLandSFX = false;
        }

    }
}

