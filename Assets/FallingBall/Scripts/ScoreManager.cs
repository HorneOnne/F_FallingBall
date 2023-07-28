using System.Collections;
using UnityEngine;

namespace FallingBall
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance { get; private set; }   
        public static event System.Action<int> OnScoreChanged;

        private int score = 0;
        private float scoreIncreaseInterval = 0.1f; // The interval (in seconds) to increase the score
        private int scoreIncreaseAmount = 1; // The amount to increase the score


        // Cached
        private GameplayManager gameplayManager;
        private WaitForSeconds waitForIncreaseScore;

        #region Properties
        public int Score { get { return score; } }
        public int Best { get { return score; } }
        #endregion


        private void Awake()
        {
            Instance = this;
        }

     

        private void Start()
        {
            gameplayManager = GameplayManager.Instance;
            waitForIncreaseScore = new WaitForSeconds(scoreIncreaseInterval);
        }


        public void StartCalculateScore()
        {
            StartCoroutine(IncreaseScoreCoroutine());
        }

        private IEnumerator IncreaseScoreCoroutine()
        {
            while (true)
            {
                yield return waitForIncreaseScore;
                IncreaseScore();
            }
        }

        private void IncreaseScore()
        {
            if(gameplayManager.currentState == GameplayManager.GameState.PLAYING)
            {
                score += scoreIncreaseAmount;
                OnScoreChanged?.Invoke(score);
            }           
        }
    }
}
