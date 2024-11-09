using UnityEngine;

namespace InputHandlers
{
    public class KeyboardInteractHandler : InteractInputHandler
    {
        public override bool Shoot()
        {
            return Input.GetKeyDown(KeyCode.Space);
        }
    }
}