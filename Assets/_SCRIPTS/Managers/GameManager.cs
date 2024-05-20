using System;
using System.Collections;
using _SCRIPTS.Signals;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

namespace _SCRIPTS.Managers
{
    public class GameManager : MonoBehaviour
    {
        #region OnEnable, Start, OnDisable
        
        void Awake()
        {
            Application.targetFrameRate = 60;
        }

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
            CoreGameSignals.Instance.OnRestart += OnRestart;
        }

        private void OnRestart()
        {
            StartCoroutine(Restart());
        }
        
        IEnumerator Restart()
        {
            yield return new WaitForSecondsRealtime(3);
            SceneManager.LoadScene("Final");
        }
        
        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.OnRestart -= OnRestart;
        }

        #endregion
    }
}
