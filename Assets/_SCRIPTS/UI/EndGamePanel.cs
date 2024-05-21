using System;
using _SCRIPTS.Signals;
using TMPro;
using UnityEngine;

namespace _SCRIPTS.UI
{
    public class EndGamePanel : MonoBehaviour
    {
        #region Serialize Field

        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI highScoreText;

        #endregion

        #region Start

        private void Start()
        {
            scoreText.text = "Your Score: " + CoreGameSignals.Instance.OnGetScore?.Invoke();
            highScoreText.text = "High Score: " + CoreGameSignals.Instance.OnLoadHighestScore?.Invoke();
        }

        #endregion

    }
}
