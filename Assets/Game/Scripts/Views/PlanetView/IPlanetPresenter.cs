using System;
using Game.Views;
using UnityEngine;

namespace Game.View
{
    public interface IPlanetPresenter
    {
        event Action<Transform> OnMoneyGathered;
    }
}