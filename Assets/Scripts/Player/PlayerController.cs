using UnityEngine;

namespace ShootEmUp
{
    public sealed class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private Player character;

        private bool fireRequired;

        private void Update()
        {
            if (KeyboardHandler.Instance.Shoot()) 
                fireRequired = true;

            if (KeyboardHandler.Instance.MoveLeft())
                character.SetDirection(-1);
            else if (KeyboardHandler.Instance.MoveRight())
                character.SetDirection(1);
            else
                character.SetDirection(0);
        }

        private void FixedUpdate()
        {
            if (fireRequired)
            {
                character.Attack();
                fireRequired = false;
            }
        }
    }
}