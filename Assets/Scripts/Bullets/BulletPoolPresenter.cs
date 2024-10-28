using System;
using Intrefaces;
using UnityEngine;

namespace ShootEmUp
{
    public class BulletPoolPresenter : MonoBehaviour
    {
        public Pool<Bullet> BulletPool { get; private set; }

        [SerializeField] private Transform container;
        [SerializeField] private Bullet prefab;

        private void Awake()
        {
            BulletPool = new Pool<Bullet>(container, prefab);
            BulletPool.InitPool(10);
        }
    }
}