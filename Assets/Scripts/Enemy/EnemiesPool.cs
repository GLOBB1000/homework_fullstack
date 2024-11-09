using Common;

namespace Enemy
{
    public class EnemiesPool : Pool<EnemyAI>
    {
        private void Awake()
        {
            InitPool();
        }
    }
}