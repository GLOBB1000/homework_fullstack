using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyManager : MonoBehaviour
    {        
        [SerializeField]
        private Player _character;

        [SerializeField]
        private Transform _worldTransform;

        [SerializeField]
        private Transform _container;

        [SerializeField]
        private Enemy _prefab;

        private EnemyPool _enemyPool;

        private void Awake()
        {
            _enemyPool = new EnemyPool(_container, _prefab);
            _enemyPool.InitPool();
        }

        private void Start()
        {
            for (var i = 0; i < 5; i++) 
            {
                CreateEnemyFromPool();
            }
        }

        private void CreateEnemyFromPool()
        {
            _enemyPool.GetEnemy(_worldTransform, _character, x =>
            {
                x.OnDeath += OnDeath;
            });
        }

        private void OnDeath(IHealth health)
        {
            var enemy = health as Enemy;

            _enemyPool.DequeEnemy(enemy);
            
            CreateEnemyFromPool();

            enemy.OnDeath -= this.OnDeath;
        }
    }
}