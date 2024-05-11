using System;
using _SCRIPTS.Signals;
using UnityEngine;
using UnityEngine.UI;

namespace _SCRIPTS.UI
{
    public class SoulMeter : MonoBehaviour
    {
        [SerializeField] private float reduceSoul;
        [SerializeField] private Gradient gradient;

        private Image _barMeter;
        
        #region Update

        private void Awake()
        {
            _barMeter = GetComponent<Image>();
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void Start()
        {
            ResetSoulMeter();
        }

        void Update()
        {
            ReduceBarMeter();
        }
    
        #endregion
            
        #region Private Functions

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.OnIncreaseSoulMeter += ResetSoulMeter;
        }
    
        private void ReduceBarMeter()
        {
            if (!CoreGameSignals.Instance.OnGetCanAttack.Invoke())
            {
                _barMeter.fillAmount -= reduceSoul * Time.deltaTime;
                _barMeter.color = gradient.Evaluate(_barMeter.fillAmount);
            }
        }

        private void ResetSoulMeter()
        {
            _barMeter.fillAmount = 1;
            _barMeter.color = gradient.Evaluate(1);
        }
    
        #endregion
    }
}
