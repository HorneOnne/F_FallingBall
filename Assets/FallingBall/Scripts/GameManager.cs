using UnityEngine;

namespace FallingBall
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        [HideInInspector] public int score;
        private int bestScore;

        private void Awake()
        {
            // Check if an instance already exists, and destroy the duplicate
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            // FPS
            Application.targetFrameRate = 60;
        }

        private void Start()
        {
            // Make the GameObject persist across scenes
            DontDestroyOnLoad(this.gameObject);
        }

        public void SetBestScore(int score)
        {
            this.score = score;
            if (bestScore < score)
            {
                bestScore = score;
            }
        }

        public int GetBestScore()
        {
            return bestScore;
        }
    }
}
