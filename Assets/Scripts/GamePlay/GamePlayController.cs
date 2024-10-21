using UnityEngine;

public class GamePlayController : MonoBehaviour
{
    [SerializeField]
    private Character character;

    private void OnEnable()
    {
        character.OnDeath += _ => Time.timeScale = 0;
    }

    private void OnDisable()
    {
        character.OnDeath -= _ => Time.timeScale = 0;
    }
}
