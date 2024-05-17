using System;
using System.Collections;
using _SCRIPTS.Signals;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace _SCRIPTS.Controllers
{
    public class PlayerAttackController : MonoBehaviour
    {
        [SerializeField] private Transform attackPoint;

        [SerializeField] private MMFeedbacks shootFb;
        [SerializeField] private MMFeedbacks catchFb;
        private bool _canAttack = true;
        private int _killCount;

        private PlayerMovementController _pmc;

        private void Awake()
        { 
            _pmc = GetComponent<PlayerMovementController>();
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.OnSetGetAttack += OnSetCanAttack;
            CoreGameSignals.Instance.OnGetCanAttack += OnGetCanAttack;
            CoreGameSignals.Instance.OnGetKillCount += OnGetKillCount;
            CoreGameSignals.Instance.OnIncreaseKillCount += OnIncreaseKillCount;
            CoreGameSignals.Instance.OnAttack += OnAttack;
        }

        private void Start()
        {
            Time.timeScale = 1;
        }

        private void OnAttack()
        {
            if (_canAttack)
            {
                _canAttack = false;
                CoreGameSignals.Instance.OnSetIsExitFalse.Invoke();
                Quaternion rotation = Quaternion.LookRotation(_pmc.lookAtPos - transform.position, transform.up);
                Instantiate(Resources.Load<GameObject>("Ball"),
                    attackPoint.position, transform.rotation);
                shootFb.PlayFeedbacks();
            }
        }

        private void OnSetCanAttack()
        {
            _canAttack = true;
            catchFb.PlayFeedbacks();
        }

        private bool OnGetCanAttack()
        {
            return _canAttack;
        }

        private int OnGetKillCount()
        {
            return _killCount;
        }

        private void OnIncreaseKillCount()
        {
            _killCount++;
        }
    }
}
