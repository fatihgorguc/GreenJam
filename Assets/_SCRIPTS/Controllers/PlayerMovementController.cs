using System;
using System.Collections;
using _SCRIPTS.Signals;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace _SCRIPTS.Controllers
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 0.5f;
        [SerializeField] private UnityEngine.Camera mainCamera;
        [SerializeField] private int speed;
        [SerializeField] private int dashSpeed;
        [SerializeField] private MMFeedbacks dashFb;
        [SerializeField] private Joystick joystick;
        
        private float _horizontalMovement;
        private float _verticalMovement;
        private Vector3 _movementDirection = Vector3.zero;
        private const int _rotationAngle = 45;
        private bool _canMove = true;
        private bool _canDash = true;
        private Rigidbody _rigidbody;
        public Vector3 lookAtPos;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _canMove = true;
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.OnGetMovementDirection += OnGetMovementDirection;
            CoreGameSignals.Instance.OnDash += OnDash;
        }

        private void Update()
        {
            Movement();
            
        }

        private void Movement()
        {
            if (_canMove)
            {
                _horizontalMovement = joystick.Horizontal;
                _verticalMovement = joystick.Vertical;
        
                // Joystick girişlerini kontrol edin
                Debug.Log($"Joystick Horizontal: {_horizontalMovement}, Vertical: {_verticalMovement}");
            }

            _movementDirection = new Vector3(_horizontalMovement, 0f, _verticalMovement).normalized;
            Quaternion rotation = Quaternion.Euler(0f, _rotationAngle, 0f);
            _movementDirection = rotation * _movementDirection;

            Debug.Log($"Movement Direction: {_movementDirection}");

            if (_movementDirection != Vector3.zero)
            {
                if (_movementDirection.magnitude >= 0.1f)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(_movementDirection, Vector3.up);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
                }
            }

            Vector3 velocity = _movementDirection * speed;
            transform.position += velocity * Time.deltaTime;

        }

        /*private void LookRotation()
        {
            
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float rayDistance;

            if (groundPlane.Raycast(ray, out rayDistance))
            {
                lookAtPos = ray.GetPoint(rayDistance);
                Quaternion targetRotation = Quaternion.LookRotation(lookAtPos - transform.position);

                // Yalnızca y eksenindeki rotasyonu al
                targetRotation = Quaternion.Euler(0f, targetRotation.eulerAngles.y, 0f);

                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }*/

        private void OnDash()
        {
            if (_canDash)
            {
                _canDash = false;
                _canMove = false;
                _rigidbody.AddForce(_movementDirection.normalized*dashSpeed,ForceMode.Impulse);
                dashFb.PlayFeedbacks();
                StartCoroutine(DashCooldown());
            }
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
        
    }


}
