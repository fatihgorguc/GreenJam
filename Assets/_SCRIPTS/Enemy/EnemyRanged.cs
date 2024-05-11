using System;
using _SCRIPTS.Controllers;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace _SCRIPTS.Enemy
{
    public class EnemyRanged : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5;
        [SerializeField] private float damping = 5;
        [SerializeField] private float attackRange = 5;
        [SerializeField] private float dartSpeed = 5;
        [SerializeField] private MMFeedbacks attackFb;
        [SerializeField] private MMFeedbacks dieFb;
        [SerializeField] private GameObject dartPrefab;

        private bool _isAttacking;
        
        private PlayerMovementController _player;
        private Vector3 _targetPosition;
        
        private void Awake()
        {
            _player = FindObjectOfType<PlayerMovementController>();
        }
        
        private void Update()
        {
            ChooseAction();
            FollowTarget();
        }

        private void ChooseAction()
        {
            if (_isAttacking) return;
            if (CheckDistance() < attackRange) Attack();
            else FollowTarget();
        }

        private void Attack()
        {
            _isAttacking = true;
            attackFb.PlayFeedbacks();
            
        }

        private float CheckDistance()
        {
            return Vector3.Distance(_player.transform.position, transform.position);
        }

        private void FollowTarget()
        {
            _targetPosition = Vector3.Lerp(_targetPosition, _player.transform.position, damping * Time.deltaTime);
            transform.position += (_targetPosition - transform.position).normalized * (moveSpeed * Time.deltaTime);
        }

        private void InstantiateDart()
        {
            var clone = Instantiate(dartPrefab, transform.position, transform.rotation);
            clone.GetComponent<Rigidbody>().AddForce(transform.forward * dartSpeed, ForceMode.Impulse);
        }
        
        private void SetIsAttackingFalse()
        {
            _isAttacking = false;
        }

        private void Die()
        {
            dieFb.PlayFeedbacks();
            Destroy(gameObject,1f);
        }
        
    }
}