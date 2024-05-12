using System;
using _SCRIPTS.Signals;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

namespace _SCRIPTS.UI
{
    public class SoulMeter : MonoBehaviour
    {
        [SerializeField] private float reduceSoul;
        [SerializeField] private Gradient gradient;

        private Volume _volume;
        private Image _barMeter;
        private bool isDead = false;
        
        #region Update

        private void Awake()
        {
            _barMeter = GetComponent<Image>();
            _volume = FindObjectOfType<Volume>();
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
                if (_barMeter.fillAmount ==0 && !isDead)
                {
                    isDead = true;
                    CoreGameSignals.Instance.Die?.Invoke();
                }
                _barMeter.color = gradient.Evaluate(_barMeter.fillAmount);
                if (_volume.profile.TryGet<ChromaticAberration>(out ChromaticAberration ca))
                {
                    ca.intensity.value = 1 - _barMeter.fillAmount;
                }
                if (_volume.profile.TryGet<Vignette>(out Vignette vig))
                {
                    vig.intensity.value = 0.3f - _barMeter.fillAmount * 0.1f;
                }
            }
        }

        private void ResetSoulMeter()
        {
            _barMeter.fillAmount = 1;
            _barMeter.color = gradient.Evaluate(1);
            
            if (_volume.profile.TryGet<ChromaticAberration>(out ChromaticAberration ca))
            {
                ca.intensity.value = 0;
            }
            if (_volume.profile.TryGet<Vignette>(out Vignette vig))
            {
                vig.intensity.value = 0.2f;
            }
        }
    
        #endregion
    }
}
