using System;
using System.Collections.Generic;
using Modules;
using UnityEngine;
using Zenject;

namespace CoinsHandlers
{
    public class CoinsPool : MonoMemoryPool<Vector2Int, Coin>, ICoinsSpawner
    {
        public event Action<ICoin> OnCoinSpawned;
        public event Action OnCoinDeSpawned;
        
        private readonly List<ICoin> coins = new();

        public List<ICoin> SpawnedCoins()
        {
            return coins;
        }

        ICoin ICoinsSpawner.Spawn(Vector2Int position)
        {
            var coin = this.Spawn(position);
            coin.Position = position;
            Debug.Log("Spawned coin: " + coin);
            
            coins.Add(coin);
            
            return coin;
        }

        public void Despawn(ICoin coin)
        {
            OnDespawned(coin as Coin);
            coins.Remove(coin);
            Debug.Log("Despawned coin: " + coin);
            OnCoinDeSpawned?.Invoke();
        }

        protected override void OnSpawned(Coin item)
        {
            base.OnSpawned(item);
            item.Generate();
            
            OnCoinSpawned?.Invoke(item);
        }
    }
}