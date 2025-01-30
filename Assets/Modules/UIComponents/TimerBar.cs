using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Modules.UI
{
    public sealed class TimerBar : MonoBehaviour
    {
        [SerializeField] private Image timerBarImage;
        [SerializeField] TextMeshProUGUI timerText;

        public void ChangeTimerBar(float value, float income)
        {
            timerBarImage.fillAmount = income;
            timerText.text = value.ToString();
        }
    }
}