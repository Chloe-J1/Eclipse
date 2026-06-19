using UnityEngine;
using UnityEngine.InputSystem;

namespace Eclipse
{
    public class FollowPlayer : MonoBehaviour
    {
        [SerializeField] float _idleOffset;
        [SerializeField] float _lookOffset;
        PlayerInput _playerInput;
        PlayerMovement _playerMovement;
        public float _offset = 2.5f;

        public GameObject playerVisual; // Reference to the player GameObject
        private float _lookLerpSpeed = 2f;
        private float _idleLerpSpeed = 1f;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _playerInput = GetComponentInParent<PlayerInput>();
            _playerMovement = GetComponentInParent<PlayerMovement>();
        }

        // Update is called once per frame
        void Update()
        {
            MoveCamera();
        }

        void MoveCamera()
        {
            Vector2 input = _playerInput.actions["Look"].ReadValue<Vector2>();
            float yInput = Mathf.Clamp(input.y, -0.1f, 1f);

            float facingSign = Mathf.Sign(Vector3.Dot(_playerMovement.playerVisual.transform.forward, Vector3.right));
            if (input != Vector2.zero && _playerInput.actions["SpecialMove"].IsPressed() == false)
            {
                Vector3 destination = new Vector3(
                    facingSign * _idleOffset + input.x * _lookOffset, // Move relative to the idle offset you already had
                    _offset + _lookOffset * input.y,
                    transform.localPosition.z
                );
                transform.localPosition = Vector3.Lerp(transform.localPosition, destination, Time.deltaTime * _lookLerpSpeed);
            }
            else
            {
                Vector3 idlePosition = new Vector3(
                    facingSign * _idleOffset,
                    _offset,
                    transform.localPosition.z
                );
                transform.localPosition = Vector3.Lerp(transform.localPosition, idlePosition, Time.deltaTime * _idleLerpSpeed);
            }
        }


    }
}

