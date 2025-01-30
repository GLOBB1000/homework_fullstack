using DG.Tweening;
using Modules.UI;
using TMPro;
using UnityEngine;

namespace Game.Views.MoneyView
{
    public class MoneyView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private ParticleAnimator particleAnimator;
        [SerializeField] private Transform icon;
        
        public void OnMoneyEarned(int newValue, int range)
        {
            
        }

        public void PlayAnimation(Transform from)
        {
            particleAnimator.Emit(from.position, icon.position);
        }
        
        public void OnMoneyChanged(int newValue, int prevValue)
        {
            DOVirtual.Int(prevValue, newValue, 1, (x) =>
            {
                text.text = x.ToString();
            });
        }
    }
}