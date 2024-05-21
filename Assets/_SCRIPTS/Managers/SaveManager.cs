using System;
using _SCRIPTS.Signals;
using UnityEngine;

namespace _SCRIPTS.Managers
{
    public class SaveManager : MonoBehaviour
    {
        #region Private Field

        private int _highestScore;

        #endregion
        #region OnEnable, OnDisable

        private void OnEnable()
        {
            SubscribeEvents();
        }

        /*private void Start()
        {
            if (ES3.KeyExists("HighestScore"))
            {
                ES3.DeleteKey("HighestScore");
                Debug.Log("kEYdELETED");
            }
        }*/

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        #endregion
        #region Functions

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.OnSaveHighestScore += OnSaveHighestScore;
            CoreGameSignals.Instance.OnLoadHighestScore += OnLoadHighestScore;
        }

        private void OnSaveHighestScore()
        {
            ES3.Save("HighestScore", CoreGameSignals.Instance.OnGetScore.Invoke());
            Debug.Log("Saved Score" + CoreGameSignals.Instance.OnGetScore?.Invoke());
        }

        private int OnLoadHighestScore()
        {
            if (ES3.KeyExists("HighestScore"))
            {
                _highestScore = ES3.Load<int>("HighestScore");
                Debug.Log(_highestScore);
                return _highestScore;
                
            }
            else
            {
                Debug.Log("HighScore NULL");
                return 0;
            }
        }
        
        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.OnSaveHighestScore -= OnSaveHighestScore;
            CoreGameSignals.Instance.OnLoadHighestScore -= OnLoadHighestScore;
        }

        #endregion
    }
}
