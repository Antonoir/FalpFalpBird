using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class HUDController : MonoBehaviour
    {
        private GameController gameController;
        private Text scoreText;
        private Canvas canvas;

        private void Awake()
        {
            gameController = Finder.GameController;
            scoreText = GetComponentInChildren<Text>();
            canvas = GetComponent<Canvas>();
        }
        
        private void Start()
        {
            UpdateVisibility(gameController.GameState);
        }

        private void OnEnable()
        {
            gameController.OnGameStateChanged += UpdateVisibility;
            gameController.OnScoreChange += UpdateScore;
        }

        private void OnDisable()
        {
            gameController.OnGameStateChanged -= UpdateVisibility;
            gameController.OnScoreChange -= UpdateScore;
        }

        private void UpdateScore(uint score)
        {
            scoreText.text = score.ToString("00");
        }
        
        private void UpdateVisibility(GameState gameState)
        {
            canvas.enabled = gameState != GameState.MainMenu;
        }
    }
}