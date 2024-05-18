using System;
using System.Collections;
using _SCRIPTS.Signals;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace _SCRIPTS.Controllers
{
    public class PlayerAttackController : MonoBehaviour
    {
        #region Serialize Field

        [SerializeField] private Transform attackPoint;
        [SerializeField] private MMFeedbacks shootFb;
        [SerializeField] private MMFeedbacks catchFb;
        
        #endregion

        #region Private Fields

        private bool _canAttack = true;
        private int _killCount;

        #endregion

        #region OnEnable, OnDisable

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        #endregion

        #region Functions
        
        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.OnSetGetAttack += OnSetCanAttack;
            CoreGameSignals.Instance.OnGetCanAttack += OnGetCanAttack;
            CoreGameSignals.Instance.OnGetKillCount += OnGetKillCount;
            CoreGameSignals.Instance.OnIncreaseKillCount += OnIncreaseKillCount;
            CoreGameSignals.Instance.OnAttack += OnAttack;
        }

        private void OnAttack()
        {
            if (_canAttack)
            {
                _canAttack = false;
                CoreGameSignals.Instance.OnSetIsExitFalse.Invoke();
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
        
        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.OnSetGetAttack -= OnSetCanAttack;
            CoreGameSignals.Instance.OnGetCanAttack -= OnGetCanAttack;
            CoreGameSignals.Instance.OnGetKillCount -= OnGetKillCount;
            CoreGameSignals.Instance.OnIncreaseKillCount -= OnIncreaseKillCount;
            CoreGameSignals.Instance.OnAttack -= OnAttack;
        }

        #endregion


    }
}
