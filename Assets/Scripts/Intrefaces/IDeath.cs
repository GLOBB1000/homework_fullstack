using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDeath
{
    public Action<IHealth> OnDeath { get; set; }
}
