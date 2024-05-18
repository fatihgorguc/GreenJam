using System;
using _SCRIPTS.Enums;
using _SCRIPTS.Signals;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _SCRIPTS.Handlers
{
    public class UIButtonSubscriber : MonoBehaviour
    {
        #region Serialized Field

        [SerializeField] private ButtonTypes buttonType;

        #endregion
        
        #region Private Fields

        private Button _button;

        #endregion

        #region Awake OnEnable
        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }
        #endregion

        #region Functions
        private void SubscribeEvents()
        {
            switch (buttonType)
            {
                case ButtonTypes.Attack:
                    _button.onClick.AddListener(()=>CoreGameSignals.Instance.OnAttack?.Invoke());
                    break;
                case ButtonTypes.Dash:
                    _button.onClick.AddListener(()=>CoreGameSignals.Instance.OnDash?.Invoke());
                    break;
            }
        }
        #endregion

    }
}
