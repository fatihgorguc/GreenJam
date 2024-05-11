using _SCRIPTS.Signals;
using UnityEngine;

namespace _SCRIPTS.Ball
{
    public class CatchBall : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Ball"))
            {
                Destroy(other.gameObject);
                CoreGameSignals.Instance.OnSetGetAttack.Invoke();
            }
        }
    }
}
