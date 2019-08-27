using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    [RequireComponent(typeof(TranslateMover))]
    public class Pipe : MonoBehaviour
    {
        [Header("Behaviour")]
        [SerializeField] [Range(3, 20)] private float pipeLifeSpan = 5;
        [Header("Position")]
        [SerializeField] private float maxY = 1f;
        [SerializeField] private float minY = -1f;
        
        private GameController gameController;
        private PointGainedEventChannel pointGainedEventChannel;
        private TranslateMover mover;
        private GameObject pipePair;
        private Sensor sensor;

        private void Awake()
        {
            gameController = Finder.GameController;
            pointGainedEventChannel = Finder.PointGainedEventChannel;
            mover = GetComponent<TranslateMover>();
            sensor = GetComponentInChildren<Sensor>();
        }

        private void Start()
        {
            transform.Translate(Vector3.up * Random.Range(minY, maxY));
        }

        private void OnEnable()
        {
            StartCoroutine(PipeDestroyRoutine());
            gameController.OnGameStateChanged += StopCoroutine;
            sensor.OnHit += PipePassed;
        }
        
        private void OnDisable()
        {
            gameController.OnGameStateChanged -= StopCoroutine;
            sensor.OnHit -= PipePassed;
        }

        private void Update()
        {
            if (gameController.GameState == GameState.Playing)
                mover.Move();
        }

        private void PipePassed(GameObject other)
        {
            pointGainedEventChannel.NotifyPointGained();
        }

        private void StopCoroutine(GameState state)
        {
            if(state == GameState.GameOver)
                StopAllCoroutines();
        }

        private IEnumerator PipeDestroyRoutine()
        {
                yield return new WaitForSeconds(pipeLifeSpan);
                Destroy(gameObject);
        }
    }
}