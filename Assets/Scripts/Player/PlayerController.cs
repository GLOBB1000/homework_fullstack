using UnityEngine;

namespace ShootEmUp
{
    public sealed class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private Player character;

        private bool fireRequired;

        private void Awake()
        {
            this.character.OnDeath += _ => Time.timeScale = 0;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) 
                fireRequired = true;

            if (Input.GetKey(KeyCode.LeftArrow))
                character.SetDirection(-1);
            else if (Input.GetKey(KeyCode.RightArrow))
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