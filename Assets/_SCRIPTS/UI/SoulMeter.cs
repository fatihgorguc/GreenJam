using System;
using _SCRIPTS.Signals;
using MoreMountains.Feedbacks;
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
        [SerializeField] private MMFeedbacks dieFb;

        private Volume _volume;
        private Image _barMeter;
        private bool isDead = false;
        
        #region Awake, OnEnable, Start, Update, OnDisable

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

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        #endregion
            
        #region Private Functions

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.OnResetSoulMeter += ResetSoulMeter;
            CoreGameSignals.Instance.OnIncreaseSoulMeter += OnIncreaseSoulMeter;
        }
    
        private void ReduceBarMeter()
        {
            if (!CoreGameSignals.Instance.OnGetCanAttack.Invoke())
            {
                _barMeter.fillAmount -= reduceSoul * Time.deltaTime;
                if (_barMeter.fillAmount ==0 && !isDead)
                {
                    isDead = true;
                    CoreGameSignals.Instance.OnDie?.Invoke();
                    dieFb.PlayFeedbacks();
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

        private void OnIncreaseSoulMeter(float val)
        {
            _barMeter.fillAmount += val;
            if (_barMeter.fillAmount >=1)
            {
                _barMeter.fillAmount = 1;
            }
        }
        
        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.OnResetSoulMeter -= ResetSoulMeter;
            CoreGameSignals.Instance.OnIncreaseSoulMeter -= OnIncreaseSoulMeter;
        }
    
        #endregion
    }
}
