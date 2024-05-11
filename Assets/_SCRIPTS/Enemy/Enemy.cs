using System;
using _SCRIPTS.Controllers;
using _SCRIPTS.Signals;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace _SCRIPTS.Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5;
        [SerializeField] private float damping = 5;
        [SerializeField] private MMFeedbacks dieFb;
        
        private PlayerMovementController _player;
        private Vector3 _targetPosition;
        
        private void Awake()
        {
            _player = FindObjectOfType<PlayerMovementController>();
        }
        
        private void Update()
        {
            FollowTarget();
        }

        private void FollowTarget()
        {
            _targetPosition = Vector3.Lerp(_targetPosition, _player.transform.position, damping * Time.deltaTime);
            transform.position += (_targetPosition - transform.position).normalized * (moveSpeed * Time.deltaTime);
        }

        private void Die()
        {
            dieFb.PlayFeedbacks();
            Destroy(gameObject,1f);
        }
        
    }
}