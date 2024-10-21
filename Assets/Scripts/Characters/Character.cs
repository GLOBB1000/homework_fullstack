using ShootEmUp;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public abstract class Character : MonoBehaviour, IAttacker
{
    [SerializeField]
    protected bool _isPlayer;

    [SerializeField]
    protected Transform _firePoint;

    [SerializeField]
    protected int _health;

    [SerializeField]
    protected Rigidbody2D _rigidbody;

    [SerializeField]
    protected float _speed = 5.0f;

    protected BulletManager _bulletManager;

    public virtual event Action<IHealth> OnDeath;

    protected virtual void Start()
    {
        _bulletManager = BulletManager.Instance;
    }

    protected abstract void Move(Vector2 direction);

    public abstract void Attack();

    protected abstract void FixedUpdate();
}
