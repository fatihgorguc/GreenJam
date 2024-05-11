using System;
using _SCRIPTS.Signals;
using UnityEngine;

namespace _SCRIPTS.Controllers
{
    public class PlayerAttackController : MonoBehaviour
    {
        [SerializeField] private Transform attackPoint;
        private bool _canAttack = true;

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.OnSetGetAttack += OnSetCanAttack;
        }


        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Attack();
            }
        
        }

        private void Attack()
        {
            if (_canAttack)
            {
                _canAttack = false;
                Instantiate(Resources.Load<GameObject>("Ball"),
                    attackPoint.position,attackPoint.rotation);
            }
        }

        private void OnSetCanAttack()
        {
            _canAttack = true;
        }

        
    }
}
