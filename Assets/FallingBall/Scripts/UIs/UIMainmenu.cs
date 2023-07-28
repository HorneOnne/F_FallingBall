using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace FallingBall
{
    public class UIMainmenu : FallingBallCanvas
    {
        [Header("Buttons")]
        [SerializeField] private Button playBtn;

        [Header("Text")]
        [SerializeField] private TextMeshProUGUI bestText;

  
        private void OnEnable()
        {
            ScoreManager.OnScoreChanged += UpdateBestScoreText;
        }

        private void OnDisable()
        {
            ScoreManager.OnScoreChanged -= UpdateBestScoreText;
        }

        private void Start()
        {
            UpdateBestScoreText(GameManager.Instance.GetBestScore());

            playBtn.onClick.AddListener(() =>
            {
                Loader.Load(Loader.Scene.GameplayScene);
                SoundManager.Instance.PlaySound(SoundType.Button, false);
            });
        }

        private void OnDestroy()
        {
            playBtn.onClick.RemoveAllListeners();
        }


        private void UpdateBestScoreText(int score)
        {
            bestText.text = $"SCORE  {score}";
        }

    }
}
