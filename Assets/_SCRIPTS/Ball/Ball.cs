using System;
using System.Threading.Tasks;
using _SCRIPTS.Signals;
using UnityEngine;

namespace _SCRIPTS.Ball
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private int ballSpeed;
        private Rigidbody _rigidbody;
        [SerializeField] private Camera mainCamera;
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            mainCamera = Camera.main;
        }

        private void Start()
        {
            _rigidbody.AddForce(transform.forward.normalized*ballSpeed,ForceMode.Impulse);
        }

        
        private async Task CanDashAsync()
        {
            await Task.Delay(200);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
