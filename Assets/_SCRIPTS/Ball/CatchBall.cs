using System;
using _SCRIPTS.Signals;
using UnityEngine;

namespace _SCRIPTS.Ball
{
    public class CatchBall : MonoBehaviour
    {
        #region Private Field
        
        private bool _isExit;
        
        #endregion

        #region OnEnable

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
            CoreGameSignals.Instance.OnSetIsExitTrue += SetIsExitTrue;
            CoreGameSignals.Instance.OnSetIsExitFalse += SetIsExitFalse;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Ball"))
            {
                CatchTheBall(other);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Ball") )
            {
                CatchTheBall(other);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Ball") )
            {
                _isExit = true;
            }
        }

        private void CatchTheBall(Collider other)
        {
            if (!_isExit) return;
            
            Destroy(other.gameObject);
            _isExit = false;
            CoreGameSignals.Instance.OnSetGetAttack.Invoke();
            CoreGameSignals.Instance.OnResetSoulMeter.Invoke();
        }

        private void SetIsExitTrue()
        {
            _isExit = true;
        }
        
        private void SetIsExitFalse()
        {
            _isExit = false;
        }
        
        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.OnSetIsExitTrue -= SetIsExitTrue;
            CoreGameSignals.Instance.OnSetIsExitFalse -= SetIsExitFalse;
        }

        #endregion


    }
}
