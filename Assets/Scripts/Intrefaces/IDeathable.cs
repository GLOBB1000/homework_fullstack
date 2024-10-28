using System;

namespace Intrefaces
{
    public interface IDeathable
    {
        event Action<IDeathable> OnDeath;
    }
}