using _SCRIPTS.Signals;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _SCRIPTS.Managers
{
    public class ScoreManager : MonoBehaviour
    {
        #region Private Field

        private int _score;

        #endregion

        #region OnEnable, Start, OnDisbale

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void Start()
        {
            _score = 0;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        #endregion

        #region Functions
        
        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.OnIncreaseScore += OnIncreaseScore;
            CoreGameSignals.Instance.OnGetScore += OnGetScore;
        }

        private void OnIncreaseScore()
        {
            var killValue = Random.Range(50, 150);
            _score += killValue;
            CoreGameSignals.Instance.OnScoreUIManagement?.Invoke();
        }

        private int OnGetScore()
        {
            return _score;
        }
        
        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.OnIncreaseScore -= OnIncreaseScore;
            CoreGameSignals.Instance.OnGetScore -= OnGetScore;
        }

        #endregion
    }
}
