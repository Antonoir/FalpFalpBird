using System;
using System.Collections;
using UnityEngine;

namespace Game
{
    public class PipeSpawnPoint :MonoBehaviour
    {
        [SerializeField] private GameObject pipePrefab;
        [SerializeField] private float pipeSpawningDelay = 1;

        private GameController gameController;

        private void Awake()
        {
            gameController = Finder.GameController;
        }

        private void OnEnable()
        {
            StartCoroutine(SpawnPipeRoutine());
        }



        private IEnumerator SpawnPipeRoutine()
        {
            while (isActiveAndEnabled)
            {
                yield return new WaitForSeconds(pipeSpawningDelay);
                
                if(gameController.GameState == GameState.Playing)
                    Instantiate(pipePrefab, transform.position, Quaternion.identity);
            }
        }
    }
}