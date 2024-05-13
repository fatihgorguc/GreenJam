using System;
using _SCRIPTS.Controllers;
using _SCRIPTS.Signals;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.AI;

namespace _SCRIPTS.Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5;
        [SerializeField] private float damping = 5;
        [SerializeField] private MMFeedbacks dieFb;
        
        private Transform _player;
        private Vector3 _targetPosition;
        private NavMeshAgent _agent;
        
        private void Awake()
        {
            _player = FindObjectOfType<PlayerMovementController>().transform;
            _agent = GetComponent<NavMeshAgent>();
        }


        private void Update()
        {
            _agent.destination = _player.position;
        }

        
        public void Die()
        {
            dieFb.PlayFeedbacks();
        }
        
    }
}