using UnityEngine;

namespace Game
{
    public class PointGainedEventChannel : MonoBehaviour
    {
        public event PointGainedEventHandler OnPointGained;

        public void NotifyPointGained()
        {
            if (OnPointGained != null) OnPointGained();
        }
        
    }

    public delegate void PointGainedEventHandler();
}