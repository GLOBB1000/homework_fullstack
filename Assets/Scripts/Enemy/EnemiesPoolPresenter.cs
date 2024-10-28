using Intrefaces;
using UnityEngine;

namespace ShootEmUp
{
    public class EnemiesPoolPresenter : MonoBehaviour
    {
        public Pool<EnemyAI> EnemiesPool { get; private set; }
        
        [SerializeField] private Transform container;
        [SerializeField] private EnemyAI prefab;

        private void Awake()
        {
            EnemiesPool = new Pool<EnemyAI>(container, prefab);
            EnemiesPool.InitPool(6);
        }
    }
}