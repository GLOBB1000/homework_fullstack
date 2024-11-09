using Common;

namespace Bullets
{
    public class BulletsPool : Pool<Bullet>
    {
        private void Awake()
        {
            InitPool();
        }
    }
}