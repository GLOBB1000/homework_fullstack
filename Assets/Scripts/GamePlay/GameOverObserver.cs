using Ships;
using UnityEngine;
using UnityEngine.Serialization;

namespace GamePlay
{
    public class GameOverObserver : MonoBehaviour
    {
        [SerializeField] private Ship characterShip;

        private void OnEnable()
        {
            characterShip.OnShipDeath += GameOver;
        }

        private void OnDisable()
        {
            characterShip.OnShipDeath -= GameOver;
        }

        private void GameOver()
        {
            Time.timeScale = 0;
        }
    }
}
