using _SCRIPTS.Enemy;
using _SCRIPTS.Signals;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace _SCRIPTS.Ball
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private int ballSpeed;

        private GameObject _model;
        private Rigidbody _rigidbody;
        [SerializeField] private int desiredSpeed;
        
        [SerializeField] private MMFeedbacks bounceFb;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _model = GetComponentInChildren<MeshRenderer>().gameObject;
        }

        private void Start()
        {
            _rigidbody.AddForce(transform.forward.normalized * ballSpeed, ForceMode.Impulse);
        }
        
        void Update()
        {
            SetModelTowardsVelocity();
        }

        private void FixedUpdate()
        {
            ConstantVelocity();
        }

        private void ConstantVelocity()
        {
            _rigidbody.velocity = _rigidbody.velocity.normalized * desiredSpeed;
        }

        private void SetModelTowardsVelocity()
        {
            _model.transform.forward = _rigidbody.velocity.normalized;
        }

        private void OnCollisionEnter(Collision collision)
        {
            bounceFb.PlayFeedbacks();
            if (collision.gameObject.CompareTag("Enemy"))
            {

                CoreGameSignals.Instance.OnIncreaseKillCount.Invoke();
                Debug.LogWarning(CoreGameSignals.Instance.OnGetKillCount());
                collision.gameObject.GetComponent<Enemy.Enemy>().Die();
                CoreGameSignals.Instance.OnScoreManagement?.Invoke();
                CoreGameSignals.Instance.OnIncreaseSoulMeter?.Invoke(0.1f);
            }

            if (collision.gameObject.CompareTag("Enemy1"))
            {

                CoreGameSignals.Instance.OnIncreaseKillCount.Invoke();
                Debug.LogWarning(CoreGameSignals.Instance.OnGetKillCount());
                collision.gameObject.GetComponent<EnemyRanged>().Die();
                CoreGameSignals.Instance.OnScoreManagement?.Invoke();
                CoreGameSignals.Instance.OnIncreaseSoulMeter?.Invoke(0.1f);
            }

            if (!collision.gameObject.CompareTag("Enemy1") || !collision.gameObject.CompareTag("Enemy") ||
                !collision.gameObject.CompareTag("Player"))
            {
                CoreGameSignals.Instance.OnIncreaseSoulMeter?.Invoke(0.1f);
            }
            
            CoreGameSignals.Instance.OnSetIsExitTrue.Invoke();
        }
    }
}
