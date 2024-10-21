using UnityEngine;

public abstract class InputHandler : MonoBehaviour 
{
    public static InputHandler Instance;

    protected virtual void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public abstract bool Shoot();

    public abstract bool MoveLeft();

    public abstract bool MoveRight();
}
