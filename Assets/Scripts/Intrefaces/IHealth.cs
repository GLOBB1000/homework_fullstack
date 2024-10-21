using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth
{
    public void ChangeHealth(int damage);

    public int GetHealth();
}
