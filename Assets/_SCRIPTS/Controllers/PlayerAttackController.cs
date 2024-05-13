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
        
        //private Coroutine _exitRoutine;

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
        }

        private void Start()
        {
            Time.timeScale = 1;
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
                CoreGameSignals.Instance.OnSetIsExitFalse.Invoke();
                //if (_exitRoutine != null) StopCoroutine(_exitRoutine);
                Instantiate(Resources.Load<GameObject>("Ball"),
                    attackPoint.position,attackPoint.rotation);
                shootFb.PlayFeedbacks();
                //_exitRoutine = StartCoroutine(SetIsExitTrue());
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

        IEnumerator SetIsExitTrue()
        {
            yield return new WaitForSeconds(0.5f);
            CoreGameSignals.Instance.OnSetIsExitTrue.Invoke();
        }
        
    }
}
