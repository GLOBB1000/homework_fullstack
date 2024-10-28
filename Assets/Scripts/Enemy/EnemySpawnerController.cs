using Ship;
using UnityEngine;

namespace ShootEmUp
{
    public class EnemySpawnerController : MonoBehaviour
    {
        [SerializeField]
        private Player _character;

        [SerializeField]
        private Transform _worldTransform;
        
        [SerializeField]
        private EnemiesPoolPresenter _enemyPool;

        public void Spawn()
        {
            if (!_enemyPool.EnemiesPool.TryGetInstance(out EnemyAI enemy))
                enemy = _enemyPool.EnemiesPool.CreatePoolInstance();

            enemy.transform.SetParent(this._worldTransform);

            Transform spawnPosition = RandomPointGenerator.Instance.RandomSpawnPoint();
            enemy.transform.position = spawnPosition.position;

            Transform attackPosition = RandomPointGenerator.Instance.RandomAttackPoint();
            enemy.SetDestination(attackPosition.position);
            enemy.Target = _character;
            
            if (_enemyPool.EnemiesPool.TryAddToHashSet(enemy))
            {
                enemy.ship.OnShipDeathVariable += EnemyOnOnDeath;
            }
        }

        private void EnemyOnOnDeath(ShipHandler shipHandler)
        {
            Debug.Log("Return to Pool");
            var enemy = shipHandler.GetComponent<EnemyAI>();
            shipHandler.OnShipDeathVariable -= EnemyOnOnDeath;
            
            _enemyPool.EnemiesPool.ReturnToPool(enemy, enemy?.transform);
        }
    }
}