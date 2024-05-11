using System;
using _SCRIPTS.Signals;
using UnityEngine;

namespace _SCRIPTS.Ball
{
    public class CatchBall : MonoBehaviour
    {
        private bool _isExit = false;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Ball") && _isExit)
            {
                Destroy(other.gameObject);
                _isExit = false;
                CoreGameSignals.Instance.OnSetGetAttack.Invoke();
                CoreGameSignals.Instance.OnIncreaseSoulMeter.Invoke();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Ball") )
            {
                _isExit = true;
                Debug.Log(_isExit);
            }
        }
    }
}
