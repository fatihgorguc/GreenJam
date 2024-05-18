using System;
using System.Collections;
using _SCRIPTS.Signals;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _SCRIPTS.Controllers
{
    public class PlayerHealthController : MonoBehaviour
    {
        #region Serialize Field

        [SerializeField] private MMFeedbacks dieFb;

        #endregion

        #region Private Field

        private bool _isDead;

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
            CoreGameSignals.Instance.OnDie += OnDie;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Enemy1") || other.gameObject.CompareTag("Dart"))
            {
                OnDie();
            }
        }

        private void OnDie()
        {
            if (_isDead) return;
            _isDead = true;
            Time.timeScale = 0.2f;
            dieFb.PlayFeedbacks();
            CoreGameSignals.Instance.OnRestart?.Invoke();
        }

        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.OnDie -= OnDie;
        }
        
        #endregion


    }
}
