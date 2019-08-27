using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(FlapMover))]

    public class Player : MonoBehaviour
    {
        /*https://docs.unity3d.com/Manual/ExecutionOrder.html
         * 
         * Ordre
         * Awake
         * OnEnable si actif
         * Start
         *
         * FixedUpdate (lorsque appellé)
         * Update
         *
         * OnDisable
         * OnDestroy
         */
        
        [SerializeField] private float targetMainMenuHeight = -1;
        
        private GameController gameController;
        private PlayerDeathEventChannel playerDeathEventChannel;
        private FlapMover flapMover;
        private Sensor sensor;
        
        private void Awake() //equivalent de onCreate
        {
            //Appellé lorsque le composant est créé.
            gameController = Finder.GameController;
            playerDeathEventChannel = Finder.PlayerDeathEventChannel;
            flapMover = GetComponent<FlapMover>();
            sensor = GetComponentInChildren<Sensor>();
        }


        private void OnEnable()
        {
            //Appellé lorsque le composant est activé. Appellé avant "Start()".
            sensor.OnHit += OnHit;
        }

        private void OnDisable()
        {
            //Appellé lorsque le composant est désactivé. N'est pas appellé si
            //le composant est initialement désactivé.
            sensor.OnHit += OnHit;
        }

        private void Update()
        {
            //Appellé à chaque frame.
            //Les opérations devrait toujours être très légèrte.
            
            //Voici comment on fait une translation
            //deltaTime = temps écouler depuis la derniere frame donc, multiplier sidessus == 5 unité seconde
            //transform.Translate(Vector3.right * Time.deltaTime * 5);
            
            //GetKeyDown juste une fois 
            //GetKey, tant quelle est enfoncé

            var gameState = gameController.GameState;
            
            if (gameState == GameState.MainMenu)
            {
                if (transform.position.y < targetMainMenuHeight)
                    flapMover.Flap();
            }
            else if (gameState == GameState.Playing)
            {
                if (Input.GetKeyDown(KeyCode.Space)) 
                    flapMover.Flap();
            }
            else
            {
                
            }

        }

        private void OnHit(GameObject other)
        {
            Die();
        }

        [ContextMenu ("Die")] //creer un bouton dans lediteur
        private void Die()
        {
            playerDeathEventChannel.NotifyPlayerDeath();
            Destroy(gameObject);
        }
        
        
        private void Start()
        {
            //Appellé juste avant la première frame ou ce composant est utilisé. n'est appelé qu'une seul fois.
        }
        
        private void FixedUpdate()
        {
            //Appellé à intervalle régulier, lorsque l'engin de physique se met à jour.
            //utilisé ex: composant de déplacement de joueur.
        }

        private void OnDestroy()
        {
            //Appellé lorsque le composant est détruit. Cela ne veut pas dire que 
            //le GameObject est détruit.
        }
    }
}