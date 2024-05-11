using System;
using System.Threading.Tasks;
using _SCRIPTS.Signals;
using UnityEngine;

namespace _SCRIPTS.Ball
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private int ballSpeed;

        private GameObject _model;
        private Rigidbody _rigidbody;
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _model = GetComponentInChildren<MeshRenderer>().gameObject;
        }

        private void Start()
        {
            _rigidbody.AddForce(transform.forward.normalized*ballSpeed,ForceMode.Impulse);
        }

        
        private async Task CanDashAsync()
        {
            await Task.Delay(200);
        }
        
        void Update()
        {
            SetModelTowardsVelocity();
        }

        private void SetModelTowardsVelocity()
        {
            _model.transform.forward = _rigidbody.velocity.normalized;
        }
    }
}
