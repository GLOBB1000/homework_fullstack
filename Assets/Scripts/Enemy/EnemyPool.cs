using ShootEmUp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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

    public void GetEnemy(Transform _parent, Transform[] _spawnPositions, Transform[] _attackPositions, Player _character, Action<Enemy> CallBack)
    {
        if (!_enemyPool.TryDequeue(out Enemy enemy))
        {
            enemy = UnityEngine.Object.Instantiate(_prefab, _container);
        }

        enemy.transform.SetParent(_parent);

        Transform spawnPosition = RandomPointGenerator.RandomPoint(_spawnPositions);
        enemy.transform.position = spawnPosition.position;

        Transform attackPosition = RandomPointGenerator.RandomPoint(_attackPositions);
        enemy.SetDestination(attackPosition.position);
        enemy.target = _character;

        if (_m_activeEnemies.Count < 5 && _m_activeEnemies.Add(enemy))
        {
            CallBack?.Invoke(enemy);
        }
    }
}
