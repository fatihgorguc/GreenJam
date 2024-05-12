using System;
using _SCRIPTS.Signals;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace _SCRIPTS.Controllers
{
    public class PlayerHealthController : MonoBehaviour
    {
        [SerializeField] private MMFeedbacks dieFb;

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
            dieFb.PlayFeedbacks();
        }
    }
}
