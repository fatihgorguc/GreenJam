using System;
using System.Collections;
using _SCRIPTS.Signals;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace _SCRIPTS.Controllers
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Serialize Field

        [SerializeField] private float rotationSpeed = 0.5f;
        [SerializeField] private UnityEngine.Camera mainCamera;
        [SerializeField] private int speed;
        [SerializeField] private int dashSpeed;
        [SerializeField] private MMFeedbacks dashFb;
        [SerializeField] private Joystick joystick;

        #endregion

        #region Private Field

        private float _horizontalMovement;
        private float _verticalMovement;
        private Vector3 _movementDirection = Vector3.zero;
        private const int _rotationAngle = 45;
        private bool _canMove = true;
        private bool _canDash = true;
        private Rigidbody _rigidbody;
        public Vector3 lookAtPos;
        
        #endregion

        #region Awake, OnEnable
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _canMove = true;
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }
        
        #endregion

        #region Functions
        
        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.OnGetMovementDirection += OnGetMovementDirection;
            CoreGameSignals.Instance.OnDash += OnDash;
        }

        private void Update()
        {
            MovementInput();
        }

        private void MovementInput()
        {
            if (_canMove)
            {
                _horizontalMovement = joystick.Horizontal;
                _verticalMovement = joystick.Vertical;
            }
        }

        private void FixedUpdate()
        {
            Movement();
            
        }

        private void Movement()
        {
            _movementDirection = new Vector3(_horizontalMovement, 0f, _verticalMovement);
            Quaternion rotation = Quaternion.Euler(0f, _rotationAngle, 0f);
            _movementDirection = rotation * _movementDirection;

            if (_movementDirection != Vector3.zero)
            {
                if (_movementDirection.magnitude >= 0.1f)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(_movementDirection, Vector3.up);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * rotationSpeed);
                }
            }

            Vector3 velocity = _movementDirection * speed;
            transform.position += velocity * Time.fixedDeltaTime;

        }
        private void OnDash()
        {
            if (!_canDash) return;
            _canDash = false;
            _canMove = false;
            _rigidbody.AddForce(_movementDirection.normalized*dashSpeed,ForceMode.Impulse);
            dashFb.PlayFeedbacks();
            StartCoroutine(DashCooldown());
        }
        
        IEnumerator DashCooldown()
        {
            yield return new WaitForSeconds(0.2f);
            _canMove = true;
            yield return new WaitForSeconds(0.2f);
            _canDash = true;
        }

        private Vector3 OnGetMovementDirection()
        {
            return lookAtPos;
        }

        #endregion
    }


}
