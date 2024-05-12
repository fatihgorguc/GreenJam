using System;
using _SCRIPTS.Signals;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _SCRIPTS.UI
{
    public class ScoreUI : MonoBehaviour
    {
        private int _score;
        [SerializeField] private TextMeshProUGUI scoreText;
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.OnScoreManagement += OnScoreManagement;
            CoreGameSignals.Instance.OnGetScore += OnGetScore;
        }

        
        private void OnScoreManagement()
        {
            int killValue = Random.Range(50, 150);
            _score += killValue;
            scoreText.text = _score+"!";
        }

        private int OnGetScore()
        {
            return _score;
        }
    }
}
