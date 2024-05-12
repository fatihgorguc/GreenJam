using System;
using _SCRIPTS.Controllers;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.AI;

namespace _SCRIPTS.Enemy
{
    public class EnemyRanged : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5;
        [SerializeField] private float damping = 5;
        [SerializeField] private float attackRange = 18;
        [SerializeField] private float dartSpeed = 5;
        [SerializeField] private MMFeedbacks attackFb;
        [SerializeField] private MMFeedbacks dieFb;
        [SerializeField] private GameObject dartPrefab;
        private NavMeshAgent _agent;

        private bool _isAttacking = false;
        private bool _isDead = false;
        
        private Transform _player;
        private Vector3 _targetPosition;
        
        private void Awake()
        {
            _player = FindObjectOfType<PlayerMovementController>().transform;
            _agent = GetComponent<NavMeshAgent>();
        }
        
        private void Update()
        {
            
            Attack();
            _agent.destination = _player.position;
        }

        

        private void Attack()
        {
            if (CheckDistance()<attackRange && !_isAttacking)
            {
                _isAttacking = true;
                attackFb.PlayFeedbacks();
            }
              
        }
        
        private float CheckDistance()
        {
            return Vector3.Distance(_player.transform.position, transform.position);
        }

        private void InstantiateDart()
        {
            if (_isDead) return;
            var clone = Instantiate(dartPrefab, new Vector3(transform.position.x,transform.position.y +1,transform.position.z), transform.rotation);
            clone.GetComponent<Rigidbody>().velocity = (_player.transform.position - transform.position).normalized * dartSpeed;
            clone.transform.forward = _player.transform.position - transform.position;
            Destroy(clone, 5f);
        }
        
        private void SetIsAttackingFalse()
        {
            _isAttacking = false;
        }

        public void Die()
        {
            _isDead = true;
            dieFb.PlayFeedbacks();
            Destroy(gameObject,1f);
        }
        
    }
}