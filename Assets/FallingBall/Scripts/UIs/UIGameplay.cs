using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace FallingBall
{
    public class UIGameplay : FallingBallCanvas
    {
        [Header("Buttons")]
        [SerializeField] private Button soundFXBtn;

        [Header("Text")]
        [SerializeField] private TextMeshProUGUI scoreText;

        [Header("Image Object")]
        [SerializeField] private GameObject unmuteObject;
        [SerializeField] private GameObject muteObject;

        private void OnEnable()
        {
            ScoreManager.OnScoreChanged += UpdateScoreText;
        }

        private void OnDisable()
        {
            ScoreManager.OnScoreChanged -= UpdateScoreText;
        }

        private void Start()
        {
            UpdateScoreText(ScoreManager.Instance.Score);
            UpdateSoundFXUI();

            soundFXBtn.onClick.AddListener(() =>
            {
                ToggleSFX();
                SoundManager.Instance.PlaySound(SoundType.Button, false);
            });
        }

        private void OnDestroy()
        {
            soundFXBtn.onClick.RemoveAllListeners();
        }

        private void ToggleSFX(bool updateUI = true)
        {
            SoundManager.Instance.MuteSoundFX(SoundManager.Instance.isSoundFXActive);
            SoundManager.Instance.isSoundFXActive = !SoundManager.Instance.isSoundFXActive;

            if (updateUI)
                UpdateSoundFXUI();
        }
        private void UpdateSoundFXUI()
        {
            if (SoundManager.Instance.isSoundFXActive)
            {
                unmuteObject.SetActive(true);
                muteObject.SetActive(false);
            }
            else
            {
                unmuteObject.SetActive(false);
                muteObject.SetActive(true);
            }
        }

        private void UpdateScoreText(int score)
        {
            scoreText.text = $"SCORE  {score}";
        }

    }
}
