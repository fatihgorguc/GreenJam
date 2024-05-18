using System;
using _SCRIPTS.Controllers;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.AI;

namespace _SCRIPTS.Enemy
{
    public class EnemyRanged : MonoBehaviour
    {
        #region SerializedField
        [SerializeField] private float attackRange = 18;
        [SerializeField] private float dartSpeed = 5;
        [SerializeField] private MMFeedbacks attackFb;
        [SerializeField] private MMFeedbacks dieFb;
        [SerializeField] private GameObject dartPrefab;
        
        #endregion

        #region Private Field

        private NavMeshAgent _agent;

        private bool _isAttacking = false;
        private bool _isDead = false;
        
        private Transform _player;
        private Vector3 _targetPosition;
        
        #endregion

        #region Awake,Update
        private void Awake()
        {
            _player = FindObjectOfType<PlayerMovementController>().transform;
            _agent = GetComponent<NavMeshAgent>();
        }
        private void Update()
        {
            Attack();
            Movement();
        }
        #endregion

        #region Function
        
        private void Attack()
        {
            if (CheckDistance()<attackRange && !_isAttacking)
            {
                _isAttacking = true;
                attackFb.PlayFeedbacks();
            }
        }

        private void Movement()
        {
            _agent.destination = _player.position;
            if (_isAttacking)
            {
                transform.rotation = Quaternion.LookRotation(_player.position-transform.position,transform.up);
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
        
        #endregion
        
        
    }
}