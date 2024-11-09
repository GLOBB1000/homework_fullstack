using UnityEngine;

namespace Common
{
    public class RandomPointGenerator : MonoBehaviour
    {
        public static RandomPointGenerator Instance;

        [SerializeField] private Transform[] _spawnPositions;

        [SerializeField] private Transform[] _attackPositions;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        public Transform RandomAttackPoint()
        {
            int index = Random.Range(0, _attackPositions.Length);
            return _attackPositions[index];
        }

        public Transform RandomSpawnPoint()
        {
            int index = Random.Range(0, _spawnPositions.Length);
            return _spawnPositions[index];
        }
    }
}
