using UnityEngine;

namespace Game
{
    public class PlayerDeathEventChannel : MonoBehaviour
    {
        //The bird as fallen I repeat the bird as fallen!!

        public event PlayerDeathEventHandler OnPlayerDeath;
        
        public void NotifyPlayerDeath()
        {
            if (OnPlayerDeath != null) OnPlayerDeath();
        }
    }

    public delegate void PlayerDeathEventHandler();
}