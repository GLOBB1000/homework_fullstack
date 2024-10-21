using UnityEngine;

public class KeyboardHandler : InputHandler
{
    protected override void Awake()
    {
        base.Awake();
    }

    public override bool MoveLeft()
    {
        return Input.GetKey(KeyCode.LeftArrow);
    }

    public override bool MoveRight()
    {
        return Input.GetKey(KeyCode.RightArrow);
    }

    public override bool Shoot()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
}
