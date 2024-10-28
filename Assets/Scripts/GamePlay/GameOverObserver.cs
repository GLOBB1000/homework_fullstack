using Intrefaces;
using Ship;
using ShootEmUp;
using UnityEngine;
using UnityEngine.Serialization;

public class GameOverObserver : MonoBehaviour
{
    [FormerlySerializedAs("character")] [SerializeField]
    private ShipHandler characterShip;

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
