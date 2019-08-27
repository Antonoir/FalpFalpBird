using UnityEngine;

namespace Game
{
    public class Sensor : MonoBehaviour
    {
        public event SensorEventHandler OnHit;
        
        private void Awake()
        {
        }
        
        private void OnTriggerEnter2D(Collider2D other)
                {
                    var stimuli = other.gameObject.GetComponent<Stimuli>();
                    if (stimuli != null)
                    {
                        var parent = other.transform.parent;
                        //gerer situation avec et sans parent
                        //pipe hurtbox->pipe | floor->noParent
                        var gameObject = parent != null ? parent.gameObject : other.gameObject;
                        if(OnHit != null) OnHit(gameObject);
                    }
                }
    }

    public delegate void SensorEventHandler(GameObject other);
}