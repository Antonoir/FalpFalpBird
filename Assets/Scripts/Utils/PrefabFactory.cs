using UnityEngine;

namespace Game
{
    public class PrefabFactory : MonoBehaviour
    {
        [SerializeField] private GameObject pipePairPrefab;

        public GameObject CreatePipePair()
        {
            return CreatePipePair(Vector3.zero, Quaternion.identity, null);
        }

        public GameObject CreatePipePair(Vector3 position, Quaternion rotation, GameObject parent)
        {
            var VERYSTUPIDNAME_pipePair = Instantiate(pipePairPrefab, position, rotation);
            if (parent)
                VERYSTUPIDNAME_pipePair.transform.parent = parent.transform;

            return VERYSTUPIDNAME_pipePair;
        }
    }
}