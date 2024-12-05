using System.Collections.Generic;
using Unity.Plastic.Antlr3.Runtime;

namespace Modules.Converter
{
    public class ConverterZone
    {
        private ItemConfig currentItem;
        private int currentAmount;

        private readonly int zoneLimit;
        
        public ConverterZone(int zoneLimit, ItemConfig item, int amount = 0)
        {
            this.zoneLimit = zoneLimit;
            currentItem = item;
            currentAmount = amount;
        }

        public bool CanAddItem(int count)
        {
            return CanAddItem(currentItem, count);
        }

        private bool CanAddItem(ItemConfig item, int count)
        {
            if (currentAmount >= zoneLimit || count < 0)
                return false;

            if (item == null)
                return false;

            return currentItem == null || currentItem.Id == item.Id;
        }

        public bool AddItem(int count)
        {
            return AddItem(currentItem, count);
        }

        public bool AddItem(ItemConfig item, int count)
        {
            if (!CanAddItem(item, count))
                return false;

            if (count + currentAmount <= zoneLimit && count <= zoneLimit)
                currentAmount += count;
            else
                currentAmount += zoneLimit - currentAmount;
            
            currentItem = item;
            
            return true;
        }

        public bool AddItem(ItemConfig item, int count, out int returnedAmount)
        {
            returnedAmount = count - (zoneLimit - currentAmount);

            if (!AddItem(item, count)) return false;
            
            if (returnedAmount <= 0)
                returnedAmount = 0;
                
            return true;

        }

        public int GetAmountOfCurrentItem()
        {
            return currentAmount;
        }

        public ItemConfig GetCurrentItem()
        {
            return currentItem;
        }

        public bool CanGetCountOfItems(int countToReturn)
        {
            return CanGetCountOfItems(currentItem, countToReturn);
        }
        
        private bool CanGetCountOfItems(ItemConfig item, int countToReturn)
        {
            if (item == null)
                return false;
            
            if (item.Id != currentItem.Id)
                return false;

            return countToReturn >= 0 && countToReturn <= zoneLimit && countToReturn <= currentAmount;
        }

        public int GetCountOfItems(int countToReturn)
        {
            return GetCountOfItems(currentItem, countToReturn);
        }

        public int GetCountOfItems(ItemConfig item, int countToReturn)
        {
            if (currentAmount <= 0)
            {
                currentAmount = 0;
                return currentAmount;
            }

            if (!CanGetCountOfItems(item, countToReturn))
                return 0;

            currentAmount -= countToReturn;
            return countToReturn;
        }

        
    }
}