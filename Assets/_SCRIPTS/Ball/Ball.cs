using _SCRIPTS.Enemy;
using _SCRIPTS.Signals;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace _SCRIPTS.Ball
{
    public class Ball : MonoBehaviour
    {
        #region Serialize Field

        [SerializeField] private int ballSpeed;
        [SerializeField] private int desiredSpeed;
        [SerializeField] private MMFeedbacks bounceFb;

        #endregion

        #region Private Field

        private GameObject _model;
        private Rigidbody _rigidbody;

        #endregion

        #region Awake, Start, Update
        
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
        
        #endregion

        #region Functions
        
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
            if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Enemy1"))
            {

                CoreGameSignals.Instance.OnIncreaseKillCount.Invoke();
                Debug.LogWarning(CoreGameSignals.Instance.OnGetKillCount());
                var enemy =collision.gameObject.GetComponent<Enemy.Enemy>();
                if (enemy != null)
                {
                    enemy.Die();
                }
                else
                {
                    collision.gameObject.GetComponent<EnemyRanged>().Die();;
                }
                CoreGameSignals.Instance.OnIncreaseScore?.Invoke();
                CoreGameSignals.Instance.OnIncreaseSoulMeter?.Invoke(0.1f);
            }
            else
            {
                CoreGameSignals.Instance.OnIncreaseSoulMeter?.Invoke(0.1f);
            }
            
            CoreGameSignals.Instance.OnSetIsExitTrue.Invoke();
        }

        #endregion


    }
}
