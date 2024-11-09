using UnityEngine;

namespace InputHandlers
{
    public abstract class InteractInputHandler : MonoBehaviour
    {
        public static InteractInputHandler Instance;

        protected virtual void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        public abstract bool Shoot();
    }
}