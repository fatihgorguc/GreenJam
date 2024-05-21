using _SCRIPTS.Enums;
using _SCRIPTS.Signals;
using UnityEngine;

namespace _SCRIPTS.Managers
{
    public class UIManager : MonoBehaviour
    {
        #region Serailize Field

        [SerializeField] private Canvas uICanvas; 

        #endregion
        #region OnEnable, OnDisable

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        #endregion

        #region Functions

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.OnUIManagement += OnUIManagement;
        }

        private void OnUIManagement(UIStates state)
        {
            switch (state)
            {
                case UIStates.EndGamePanel:
                    Instantiate(Resources.Load<GameObject>("UI/EndGamePanel"), uICanvas.transform);
                    break;
            }
        }

        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.OnUIManagement -= OnUIManagement;
        }

        #endregion
    }
}