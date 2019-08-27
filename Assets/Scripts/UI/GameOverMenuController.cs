using UnityEngine;

namespace Game
{
    public class GameOverMenuController : MonoBehaviour
    {
        private GameController gameController;
        private Canvas canvas;

        private void Awake()
        {
            gameController = Finder.GameController;
            canvas = GetComponent<Canvas>();
        }
        
        private void Start()
        {
            UpdateVisibility(gameController.GameState);
        }

        private void OnEnable()
        {
            gameController.OnGameStateChanged += UpdateVisibility;
        }

        private void OnDisable()
        {
            gameController.OnGameStateChanged -= UpdateVisibility;
        }

        private void UpdateVisibility(GameState gameState)
        {
            canvas.enabled = gameState == GameState.GameOver;
        }
    }
}