using UnityEngine;

namespace InputHandlers
{
    public abstract class MoveInputHandler : MonoBehaviour
    {
        public static MoveInputHandler Instance;

        protected virtual void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        public abstract bool MoveLeft();

        public abstract bool MoveRight();
    }
}
