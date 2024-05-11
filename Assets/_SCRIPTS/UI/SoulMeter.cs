using System;
using _SCRIPTS.Signals;
using UnityEngine;
using UnityEngine.UI;

namespace _SCRIPTS.UI
{
    public class SoulMeter : MonoBehaviour
    {
        [SerializeField] private float reduceSoul;
        #region Update

        private void OnEnable()
        {
            SubscribeEvents();
        }

        void Update()
        {
            LaughMeter();
            
        }
    
        #endregion
            
        #region Private Functions

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.OnIncreaseSoulMeter += OnIncreaseSoulMeter;
        }
    
        private void LaughMeter()
        {
            if (!CoreGameSignals.Instance.OnGetCanAttack.Invoke())
            {
                Debug.LogWarning("OBSSSS");
                GetComponent<Image>().fillAmount -= reduceSoul * Time.deltaTime;
            }
        }

        private void OnIncreaseSoulMeter()
        {
            GetComponent<Image>().fillAmount = 1;
        }
    
        #endregion
    }
}
