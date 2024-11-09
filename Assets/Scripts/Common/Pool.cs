using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

namespace Common
{
    public class Pool<T> : MonoBehaviour where T : Object
    {
        private readonly HashSet<T> _activeInstances = new();
        private readonly Queue<T> _instancePool = new();

        [SerializeField] private T prefab;

        [SerializeField] private Transform container;

        [SerializeField] private int poolSize;

        protected void InitPool()
        {
            for (int i = 0; i < poolSize; i++)
            {
                CreatePoolInstance();
            }
        }

        public T CreatePoolInstance()
        {
            T instance = Object.Instantiate(prefab, container);
            _instancePool.Enqueue(instance);
            return instance;
        }

        public bool TryAddToHashSet(T item)
        {
            return _activeInstances.Add(item);
        }

        public void ReturnToPool(T item, Transform instancer)
        {
            instancer.SetParent(container);
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