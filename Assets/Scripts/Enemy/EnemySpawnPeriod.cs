using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemy
{
    public sealed class EnemySpawnPeriod : MonoBehaviour
    {
        [SerializeField] private EnemySpawner enemySpawner;

        private IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(1, 2));
                enemySpawner.Spawn();
            }
        }
    }
}