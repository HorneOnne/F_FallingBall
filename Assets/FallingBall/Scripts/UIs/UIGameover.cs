using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace FallingBall
{
    public class UIGameover : FallingBallCanvas
    {
        [Header("Buttons")]
        [SerializeField] private Button restartBtn;

        [Header("Text")]
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI bestText;


        private void Start()
        {
            UpdateText();

            restartBtn.onClick.AddListener(() =>
            {
                Loader.Load(Loader.Scene.GameplayScene);
                SoundManager.Instance.PlaySound(SoundType.Button, false);
            });
        }

        private void OnDestroy()
        {
            restartBtn.onClick.RemoveAllListeners();
        }


        private void UpdateText()
        {
            scoreText.text = $"SCORE\n{GameManager.Instance.score}";
            bestText.text = $"BEST\n{GameManager.Instance.GetBestScore()}";
        }

    }
}
