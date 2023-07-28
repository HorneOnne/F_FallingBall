using UnityEngine;

namespace FallingBall
{
    public class GameplayManager : MonoBehaviour
    {
        public static GameplayManager Instance { get; private set; }
        public static event System.Action OnStateChanged;
        public static event System.Action OnPlaying;
        public static event System.Action OnWin;
        public static event System.Action OnGameOver;

        public enum GameState
        {
            WAITING,
            PLAYING,
            WIN,
            GAMEOVER,
            PAUSE,
        }


        [Header("Properties")]
        public GameState currentState;
        [SerializeField] private float waitTimeBeforePlaying = 0.5f;



        private void Awake()
        {
            Instance = this;
        }

        private void OnEnable()
        {
            OnStateChanged += SwitchState;
        }

        private void OnDisable()
        {
            OnStateChanged -= SwitchState;
        }

        private void Start()
        {
            ChangeGameState(GameState.WAITING);       
        }

     

        public void ChangeGameState(GameState state)
        {
            currentState = state;
            OnStateChanged?.Invoke();
        }

        private void SwitchState()
        {
            switch(currentState)
            {
                default: break;
                case GameState.WAITING:
                    StartCoroutine(Utilities.WaitAfter(waitTimeBeforePlaying, () =>
                    {
                        ChangeGameState(GameState.PLAYING);
                    }));
                              
                    break;
                case GameState.PLAYING:
                    OnPlaying?.Invoke();
                    ScoreManager.Instance.StartCalculateScore();                      
                    break;
                case GameState.WIN:               
                    
                    OnWin?.Invoke();
                    break;
                case GameState.GAMEOVER:
                    GameManager.Instance.SetBestScore(ScoreManager.Instance.Score);
                    StartCoroutine(Utilities.WaitAfter(0.5f, () =>
                    {
                        Loader.Load(Loader.Scene.GameoverScene);
                        SoundManager.Instance.PlaySound(SoundType.GameOver, false);
                    }));
                    OnGameOver?.Invoke();
                    break;
                case GameState.PAUSE:
                    break;
            }
        }
    }       
}
