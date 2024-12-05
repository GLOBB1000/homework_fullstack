using System;
using UnityEngine;

namespace Modules.Converter
{
    public class ResourсeConverter
    {
        private readonly ConverterZone leftZone;
        private readonly ConverterZone rightZone;

        public event Action<ItemConfig> OnStartConvert;
        public event Action<ItemConfig, int> OnEndConvert;

        public event Action<bool> OnConverterSwitchValueChanged;
        
        private bool isConvertingStart;
        
        //Временное решение со временем, его можно будет перенести в MonoBeh
        private readonly int maxTimerTime = 10;
        private readonly ITimer timer;
        
        private bool converterSwitcher = true;
        
        public ResourсeConverter(ConverterZone leftZone, ConverterZone rightZone)
        {
            this.leftZone = leftZone;
            this.rightZone = rightZone;

            timer = new ConverterTimer(maxTimerTime);
        }

        public void SetSwitcherState(bool isEnabled)
        {
            converterSwitcher = isEnabled;
            OnConverterSwitchValueChanged?.Invoke(converterSwitcher);
        }

        public bool GetSwitcherState()
        {
            return converterSwitcher;
        }

        private bool CanConvert(int countToConvert)
        {
            if (!converterSwitcher)
                return false;
            
            return countToConvert >= 0 && leftZone.CanGetCountOfItems(countToConvert);
        }

        public bool Convert(int countToConvert)
        {
            if (!CanConvert(countToConvert))
                return false;
            
            var leftZoneItemConfig = leftZone.GetCurrentItem();
            var rightZoneItemConfig = rightZone.GetCurrentItem();

            if (leftZoneItemConfig.CraftedItemConfig.Id != rightZoneItemConfig.Id)
                return false;

            var leftZoneItems = leftZone.GetCountOfItems(countToConvert);

            if (rightZone.AddItem(leftZoneItemConfig.CraftedItemConfig, leftZoneItems, out var itemCount))
            {
                Debug.Log($"Convert successful left zone count: {leftZone.GetAmountOfCurrentItem()}");
                leftZone.AddItem(itemCount);
                return true;
            }
            
            leftZone.AddItem(countToConvert);
            return false;
        }

        public void ConvertByTimer(int countToConvert, float deltaTime)
        {
            if(countToConvert < 0)
                return;
            
            if(!CanConvert(countToConvert))
                return;
            
            if (!leftZone.CanGetCountOfItems(countToConvert) || !rightZone.CanAddItem(countToConvert))
            {
                OnEndConvert?.Invoke(rightZone.GetCurrentItem(), rightZone.GetAmountOfCurrentItem());
                Debug.Log("ConvertByTimer");
                return;
            }

            var time = timer.GetTime(deltaTime);

            if (!isConvertingStart)
            {
                OnStartConvert?.Invoke(leftZone.GetCurrentItem());
                Debug.Log("StartConvertByTimer");
                isConvertingStart = true;
            }

            if (!converterSwitcher && isConvertingStart)
            {
                leftZone.AddItem(countToConvert);
                return;
            }

            if (!(time <= 0))
                return;

            timer.ResetTime();
            
            if (!Convert(countToConvert))
                return;

            isConvertingStart = false;
            OnEndConvert?.Invoke(rightZone.GetCurrentItem(), rightZone.GetAmountOfCurrentItem());
            Debug.Log("ConvertByTimer");
        }

        public void MockUpdate()
        {
            ConvertByTimer(1, 1);
        }
    }
}