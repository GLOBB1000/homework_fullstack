using System;
using System.Collections.Generic;
using GameCycleInstances;
using Modules;
using SnakeGame;
using UnityEngine;
using Zenject;

namespace Coins
{
    public class CoinsPool : MonoMemoryPool<Vector2Int, Coin>, ICoinsSpawner
    {
        public event Action<ICoin> OnCoinSpawned;
        public event Action OnCoinDeSpawned;
        
        private readonly List<ICoin> coins = new();

        public int CountOfCoins()
        {
            return coins.Count;
        }

        public bool CanGetCoin(Vector2Int point)
        {
            var coin = coins.Find(x => x.Position == point);

            if (coin == null)
                return false;
            
            Despawn(coin);
            return true;
        }

        public ICoin CreateCoin(Vector2Int position)
        {
            var coin = this.Spawn(position);
            coin.Position = position;
            Debug.Log("Spawned coin: " + coin);
            
            coins.Add(coin);
            
            return coin;
        }

        private void Despawn(ICoin coin)
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