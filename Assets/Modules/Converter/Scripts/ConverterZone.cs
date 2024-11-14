using System.Collections.Generic;
using Unity.Plastic.Antlr3.Runtime;

namespace Modules.Converter
{
    public class ConverterZone
    {
        private ItemConfig currentItem;
        private int currentCount;
        
        public int ZoneLimit { get; private set; }
        
        public ConverterZone(int zoneLimit, ItemConfig item, int count = 0)
        {
            ZoneLimit = zoneLimit;
            currentItem = item;
            currentCount = count;
        }

        public bool AddItem(ItemConfig item, int count)
        {
            if (count > ZoneLimit || count < 0)
                return false;

            if (item == null)
                return false;

            if (currentItem != null && currentItem.Id != item.Id)
                return false;
            
            currentItem = item;
            currentCount += count;

            return true;
        }

        public int GetAmount(ItemConfig item)
        {
            if (item == null)
                return 0;
            
            return item.Id != currentItem.Id ? 0 : currentCount;
        }

        public int GetCountOfItems(ItemConfig item, int countToReturn)
        {
            if (item == null)
                return 0;
            
            if (item.Id != currentItem.Id)
                return 0;
            
            currentCount -= countToReturn;
            return countToReturn;
        }
    }
}