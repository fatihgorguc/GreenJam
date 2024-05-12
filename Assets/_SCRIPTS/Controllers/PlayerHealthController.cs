using UnityEngine;

namespace _SCRIPTS.Controllers
{
    public class PlayerHealthController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                Time.timeScale = 0;
            }
            if (other.gameObject.CompareTag("Dart"))
            {
                Time.timeScale = 0;
            }
        }
    }
}
