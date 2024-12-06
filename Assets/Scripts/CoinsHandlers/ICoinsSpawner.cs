using System;
using System.Collections.Generic;
using Modules;
using UnityEngine;

namespace CoinsHandlers
{
    public interface ICoinsSpawner
    {
        public event Action<ICoin> OnCoinSpawned;
        public event Action OnCoinDeSpawned;

        public List<ICoin> SpawnedCoins();
        
        ICoin Spawn(Vector2Int position);
        
        void Despawn(ICoin coin);
    }
}