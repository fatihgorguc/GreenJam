using UnityEngine;

namespace _SCRIPTS.Enemy
{
    public class Dart : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Ball"))
            {
                Time.timeScale = 0 ;
            }
        }
    }
}
