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
        #region Serialize Field

        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private MMFeedbacks scoreFb;
        
        #endregion

        #region OnEnable, Start, OnDisable
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void Start()
        {
            scoreText.text = CoreGameSignals.Instance.OnGetScore?.Invoke().ToString();
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        #endregion

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.OnScoreUIManagement += OnScoreUIManagement;
        }

        
        private void OnScoreUIManagement()
        {
            scoreText.text = CoreGameSignals.Instance.OnGetScore?.Invoke().ToString();
            scoreFb.PlayFeedbacks();
        }
        
        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.OnScoreUIManagement -= OnScoreUIManagement;
        }
    }
}
