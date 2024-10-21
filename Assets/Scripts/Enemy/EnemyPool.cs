using ShootEmUp;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool
{
    private Enemy _prefab;

    private Transform _container;

    private readonly HashSet<Enemy> _m_activeEnemies = new();
    private readonly Queue<Enemy> _enemyPool = new();

    public EnemyPool(Transform container, Enemy prefab) 
    {
        _container = container;
        _prefab = prefab;
    }

    public void InitPool()
    {
        for (var i = 0; i < 7; i++)
        {
            Enemy enemy = UnityEngine.Object.Instantiate(_prefab, _container);
            _enemyPool.Enqueue(enemy);
        }
    }

    public void DequeEnemy(Enemy enemy)
    {
        enemy.transform.SetParent(_container);

        _m_activeEnemies.Remove(enemy);
        _enemyPool.Enqueue(enemy);

    }

    public void GetEnemy(Transform _parent, Player _character, Action<Enemy> CallBack)
    {
        if (!_enemyPool.TryDequeue(out Enemy enemy))
        {
            enemy = UnityEngine.Object.Instantiate(_prefab, _container);
        }

        enemy.transform.SetParent(_parent);

        Transform spawnPosition = RandomPointGenerator.Instance.RandomSpawnPoint();
        enemy.transform.position = spawnPosition.position;

        Transform attackPosition = RandomPointGenerator.Instance.RandomAttackPoint();
        enemy.SetDestination(attackPosition.position);
        enemy._target = _character;

        if (_m_activeEnemies.Count < 5 && _m_activeEnemies.Add(enemy))
        {
            CallBack?.Invoke(enemy);
        }
    }
}
