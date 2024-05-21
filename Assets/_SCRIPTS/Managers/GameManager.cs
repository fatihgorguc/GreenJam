using System.Collections;
using _SCRIPTS.Enums;
using _SCRIPTS.Signals;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _SCRIPTS.Managers
{
    public class GameManager : MonoBehaviour
    {
        #region OnEnable, Start, OnDisable

        private void OnEnable()
        {
            SubscribeEvents();
        }
        
        private void Start()
        {
            Time.timeScale = 1;
            
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        #endregion
        #region Functions

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.OnStopGame += OnStopGame;
            CoreGameSignals.Instance.OnRetryGame += OnRetryGame;
        }

        private void OnStopGame()
        {
            StartCoroutine(StopGame());
        }
        
        IEnumerator StopGame()
        {
            
            yield return new WaitForSecondsRealtime(3);
            CoreGameSignals.Instance.OnCheckHighScore?.Invoke();
            CoreGameSignals.Instance.OnUIManagement?.Invoke(UIStates.EndGamePanel);
            Time.timeScale = 0;
        }

        private void OnRetryGame()
        {
            SceneManager.LoadScene("Final");
        }
        
        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.OnStopGame -= OnStopGame;
            CoreGameSignals.Instance.OnRetryGame -= OnRetryGame;
        }

        #endregion
    }
}
