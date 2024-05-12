using System;
using _SCRIPTS.Signals;
using MoreMountains.Feedbacks;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _SCRIPTS.UI
{
    public class ScoreUI : MonoBehaviour
    {
        private int _score;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private MMFeedbacks scoreFb;
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void Start()
        {
            scoreText.text = _score.ToString();
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
            scoreText.text = _score.ToString();
            scoreFb.PlayFeedbacks();
        }

        private int OnGetScore()
        {
            return _score;
        }
    }
}
