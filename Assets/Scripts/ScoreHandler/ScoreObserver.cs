using System;
using CoinsHandlers;
using Modules;
using UnityEngine;
using Zenject;

namespace ScoreHandler
{
    public class ScoreObserver : IDisposable
    {
        private const int SCORE_RAISE_POINTS = 10;
        
        private IScore _score;
        private ICoinsSpawner _coinsSpawner;
        
        public ScoreObserver(IScore score, ICoinsSpawner coinsSpawner)
        {
            _score = score;
            _coinsSpawner = coinsSpawner;
            
            _coinsSpawner.OnCoinDeSpawned += CoinsSpawnerOnCoinDeSpawned;
            
            Debug.Log("ScoreObserver constructed");
        }

        private void CoinsSpawnerOnCoinDeSpawned()
        {
            Debug.Log("Added points");
            _score.Add(SCORE_RAISE_POINTS);
        }

        public void Dispose()
        {
            _coinsSpawner.OnCoinDeSpawned -= CoinsSpawnerOnCoinDeSpawned;
        }
    }
}