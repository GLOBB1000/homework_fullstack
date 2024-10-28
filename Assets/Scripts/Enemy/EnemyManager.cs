using System.Collections;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyManager : MonoBehaviour
    {
        [SerializeField] private EnemySpawnerController _enemySpawnerController;

        private IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(1, 2));
                _enemySpawnerController.Spawn();
            }
        }
    }
}