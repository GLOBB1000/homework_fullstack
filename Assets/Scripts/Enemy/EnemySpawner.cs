using Common;
using Ships;
using UnityEngine;

namespace Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField]
        private Ship _character;

        [SerializeField]
        private Transform _worldTransform;
        
        [SerializeField]
        private Pool<EnemyAI> _enemyPool;

        public void Spawn()
        {
            if (!_enemyPool.TryGetInstance(out EnemyAI enemy))
                enemy = _enemyPool.CreatePoolInstance();

            enemy.transform.SetParent(this._worldTransform);

            Transform spawnPosition = RandomPointGenerator.Instance.RandomSpawnPoint();
            enemy.transform.position = spawnPosition.position;

            Transform attackPosition = RandomPointGenerator.Instance.RandomAttackPoint();
            enemy.SetDestination(attackPosition.position);
            enemy.PlayerShip = _character;
            
            if (_enemyPool.TryAddToHashSet(enemy))
            {
                enemy.AIShip.OnDeath += AIOnOnDeath;
            }
        }

        private void AIOnOnDeath(Ship ship)
        {
            var enemy = ship.GetComponent<EnemyAI>();
            ship.OnDeath -= AIOnOnDeath;
            
            _enemyPool.ReturnToPool(enemy, enemy?.transform);
        }
    }
}