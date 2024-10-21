using ShootEmUp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsPool
{

    public readonly HashSet<Bullet> m_activeBullets = new();
    public readonly Queue<Bullet> m_bulletPool = new();
    private readonly List<Bullet> m_cache = new();

    
}
