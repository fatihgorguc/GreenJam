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
        [SerializeField] private MMFeedbacks dieFb;

        private bool _isDead;

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.Die += Die;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                Die();
            }
            if (other.gameObject.CompareTag("Enemy1"))
            {
                Die();
            }
            if (other.gameObject.CompareTag("Dart"))
            {
                Die();
            }
        }

        private void Die()
        {
            if (_isDead) return;
            _isDead = true;
            Time.timeScale = 0.2f;
            dieFb.PlayFeedbacks();
            StartCoroutine(Restart());
        }

        IEnumerator Restart()
        {
            yield return new WaitForSecondsRealtime(3);
            SceneManager.LoadScene("Final");
        }
    }
}
