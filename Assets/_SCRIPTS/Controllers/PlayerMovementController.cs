using System;
using System.Threading.Tasks;
using _SCRIPTS.Enums;
using _SCRIPTS.Signals;
using MoreMountains.Feedbacks;
using Unity.VisualScripting;
using UnityEngine;
using FixedUpdate = UnityEngine.PlayerLoop.FixedUpdate;

namespace _SCRIPTS.Controllers
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 0.5f;
        [SerializeField] private UnityEngine.Camera mainCamera;
        [SerializeField] private int speed;
        [SerializeField] private int dashSpeed;
        [SerializeField] private MMFeedbacks dashFb;
        
        private float _horizontalMovement;
        private float _verticalMovement;
        private Vector3 _movementDirection = Vector3.zero;
        private const int _rotationAngle = 45;
        private bool _canMove = true;
        private bool _canDash = true;
        private Rigidbody _rigidbody;
        private Vector3 lookAtPos;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.OnGetMovementDirection += OnGetMovementDirection;
        }

        private void Update()
        {
            Movement();
            LookRotation();
            if (Input.GetKeyDown(KeyCode.Space) && _canDash)
            {
                Dash();
            }
        }

        private void Movement()
        {
            if (_canMove)
            {
                _horizontalMovement = Input.GetAxis("Horizontal"); 
                _verticalMovement = Input.GetAxis("Vertical"); 
            }
                _movementDirection = new Vector3(_horizontalMovement, 0f, _verticalMovement).normalized;
                Quaternion rotation = Quaternion.Euler(0f, _rotationAngle, 0f);
                _movementDirection = rotation * _movementDirection;
                Vector3 velocity = _movementDirection;
        
                transform.position += velocity * speed * Time.deltaTime;
            
        }

        private void LookRotation()
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
        }

        private void Dash()
        {
            _canDash = false;
            _canMove = false;
            _rigidbody.AddForce(_movementDirection.normalized*dashSpeed,ForceMode.Impulse);
            dashFb.PlayFeedbacks();
            CanDashAsync();
            
        }
        
        private async Task CanDashAsync()
        {
            await Task.Delay(400);
            _canMove = true;
            _canDash = true;
            Debug.Log(_canDash);
        }

        private Vector3 OnGetMovementDirection()
        {
            return lookAtPos;
        }
        
    }


}
