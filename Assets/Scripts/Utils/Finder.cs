using UnityEngine;

namespace Game
{
    public static class Finder
    {
        private const string GAME_CONTROLLER_TAG = "GameController";

        private static GameController gameController;
        private static PlayerDeathEventChannel playerDeathEventChannel;
        private static PointGainedEventChannel pointGainedEventChannel;

        public static GameController GameController
        {
            get
            {
                if (gameController == null)
                    gameController = GameObject.FindWithTag(GAME_CONTROLLER_TAG).GetComponent<GameController>();
                return gameController;
            }
        }

        public static PlayerDeathEventChannel PlayerDeathEventChannel
        {
            get
            {
                if (playerDeathEventChannel == null)
                    playerDeathEventChannel = GameObject.FindWithTag(GAME_CONTROLLER_TAG)
                        .GetComponent<PlayerDeathEventChannel>();
                return playerDeathEventChannel;
            }
        }

        public static PointGainedEventChannel PointGainedEventChannel
        {
            get
            {
                if (pointGainedEventChannel == null)
                    pointGainedEventChannel = GameObject.FindWithTag(GAME_CONTROLLER_TAG)
                        .GetComponent<PointGainedEventChannel>();
                return pointGainedEventChannel;
            }
        }
    }
}