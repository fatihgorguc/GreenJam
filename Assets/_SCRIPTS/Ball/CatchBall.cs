using System;
using _SCRIPTS.Signals;
using UnityEngine;

namespace _SCRIPTS.Ball
{
    public class CatchBall : MonoBehaviour
    {
        private bool _isExit = false;

        private void OnEnable()
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
            CoreGameSignals.Instance.OnIncreaseSoulMeter.Invoke();
        }

        private void SetIsExitTrue()
        {
            _isExit = true;
        }
        
        private void SetIsExitFalse()
        {
            _isExit = false;
        }
    }
}
