using System;
using System.Collections.Generic;
using Modules;
using UnityEngine;

namespace Coins
{
    public interface ICoinsSpawner
    {
        public event Action OnCoinDeSpawned;

        public int CountOfCoins();

        public bool CanGetCoin(Vector2Int point);
        
        ICoin CreateCoin(Vector2Int position);
    }
}