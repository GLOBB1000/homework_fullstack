using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Intrefaces
{
    public class Pool<T> where T : Object
    {
        private readonly HashSet<T> _activeInstances = new();
        private readonly Queue<T> _instancePool = new();
        
        private T _prefab;

        private Transform _container;

        public Pool(Transform parent, T prefab)
        {
            _container = parent;
            _prefab = prefab;
        }

        public void InitPool(int instanceCount)
        {
            for (int i = 0; i < instanceCount; i++)
            {
                CreatePoolInstance();
            }
        }

        public T CreatePoolInstance()
        {
            T instance = Object.Instantiate(_prefab, _container);
            _instancePool.Enqueue(instance);
            return instance;
        }

        public bool TryAddToHashSet(T item)
        {
            return _activeInstances.Add(item);
        }

        public void ReturnToPool(T item, Transform instancer)
        {
            instancer.SetParent(_container);
            _activeInstances.Remove(item);
            _instancePool.Enqueue(item);
        }

        public bool TryGetInstance(out T item)
        {
            return _instancePool.TryDequeue(out item);
        }

        public bool TryRemoveInstance(T item)
        {
            return _activeInstances.Remove(item);
        }

        public HashSet<T> GetActiveInstances()
        {
            return _activeInstances;
        }
    }
}