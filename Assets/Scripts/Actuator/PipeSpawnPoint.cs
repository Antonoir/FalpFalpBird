using System;
using System.Collections;
using UnityEngine;

namespace Game
{
    public class PipeSpawnPoint :MonoBehaviour
    {
        [SerializeField] private float pipeSpawningDelay = 1;

        private GameController gameController;
        private PrefabFactory prefabFactory;

        private void Awake()
        {
            gameController = Finder.GameController;
            prefabFactory = Finder.PrefabFactory;
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

                if (gameController.GameState == GameState.Playing)
                    prefabFactory.CreatePipePair(transform.position, Quaternion.identity, null);
            }
        }
    }
}