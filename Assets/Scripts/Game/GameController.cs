using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private KeyCode startGameKey = KeyCode.Space;

        private GameState gameState = GameState.MainMenu;
        private PlayerDeathEventChannel playerDeathEventChannel;
        private PointGainedEventChannel pointGainedEventChannel;
        private uint score;

        public event GameStateChangedEventHandler OnGameStateChanged;
        public event ScoreChangedEventHandler OnScoreChange;

        private void Awake()
        {
            playerDeathEventChannel = Finder.PlayerDeathEventChannel;
            pointGainedEventChannel = Finder.PointGainedEventChannel;
            score = 0;
        }

        private void Start()
        {
            //Charger la scene Game
            //Vérifier que la scene ne soi pas déjà charger avant (dans le build vs dans lediteur)

            if (!SceneManager.GetSceneByName(Scenes.GAME).isLoaded)
                StartCoroutine(LoadGame());
            else
                SceneManager.SetActiveScene(SceneManager.GetSceneByName(Scenes.GAME));
        }


        private void OnEnable()
        {
            playerDeathEventChannel.OnPlayerDeath += EndGame;
            pointGainedEventChannel.OnPointGained += ScoreUp;
        }

        private void OnDisable()
        {
            playerDeathEventChannel.OnPlayerDeath -= EndGame;
            pointGainedEventChannel.OnPointGained -= ScoreUp;
        }

        private void EndGame()
        {
            GameState = GameState.GameOver;
        }

        private void ScoreUp()
        {
            score++;
            NotifyScoreChanged();
        }

        public GameState GameState
        {
            get => gameState;
            private set
            {
                if (gameState != value)
                {
                    gameState = value;

                    NotifyGameStateChanged();
                }
            }
        }

        private void Update()
        {
            //Physics2D.gravity = Vector2.zero;

            if (GameState == GameState.MainMenu && Input.GetKeyDown(startGameKey))
                GameState = GameState.Playing;

            if (GameState == GameState.GameOver && Input.GetKeyDown(startGameKey))
                RestartGame();
        }

        private void RestartGame()
        {
            GameState = GameState.MainMenu;
            score = 0;
            StartCoroutine(ReloadGame());
        }

        private IEnumerator LoadGame()
        {
            //Todo : Show loading screen
            yield return SceneManager.LoadSceneAsync("Game", LoadSceneMode.Additive);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(Scenes.GAME));
            //Todo : Hide loading screen
        }

        private IEnumerator UnloadGame()
        {
            yield return SceneManager.UnloadSceneAsync("Game");
        }

        private IEnumerator ReloadGame()
        {
            yield return UnloadGame();
            yield return LoadGame();
        }

        private void NotifyScoreChanged()
        {
            if (OnScoreChange != null) OnScoreChange(score);
        }

        private void NotifyGameStateChanged()
        {
            if (OnGameStateChanged != null) OnGameStateChanged(gameState);
        }
    }

    public delegate void GameStateChangedEventHandler(GameState newGameState);

    public delegate void ScoreChangedEventHandler(uint score);

    public enum GameState
    {
        MainMenu,
        Playing,
        GameOver
    }
}