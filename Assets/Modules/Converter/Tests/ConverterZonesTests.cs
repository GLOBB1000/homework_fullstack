using System.Collections.Generic;
using NUnit.Framework;

namespace Modules.Converter
{
    public class ConverterZonesTests
    {
        [Test]
        public void Instantiate()
        {
            ConverterZone converterZone = new ConverterZone(5, new ItemConfig("Wood"));
            
            Assert.IsNotNull(converterZone);
        }

        [TestCase(1, 1, true)]
        [TestCase(5, 5, true)]
        [TestCase(10, 5, true)]
        [TestCase(-1, 0, false)]
        [TestCase(0, 0, true)]
        
        public void AddItem(int count, int expectedCount, bool expectedSuccess)
        {
            ItemConfig itemConfig = new ItemConfig("Wood");
            
            ConverterZone converterZone = new ConverterZone(5, itemConfig);

            bool success = converterZone.AddItem(itemConfig, count);

            int actualCount = converterZone.GetAmountOfCurrentItem();

            Assert.AreEqual(expectedSuccess, success);
            Assert.AreEqual(actualCount, expectedCount);
        }

        [TestCase(10, 5, 5, true)]
        [TestCase(3, 3, 0, true)]
        public void AddItemAndReturnSomeItemsBack(int count, int expectedCount, int expectedReturnValue, bool expectedSuccess)
        {
            ItemConfig itemConfig = new ItemConfig("Wood");
            
            ConverterZone converterZone = new ConverterZone(5, itemConfig);

            bool success = converterZone.AddItem(itemConfig, count, out int returnableCount);

            int actualCount = converterZone.GetAmountOfCurrentItem();

            Assert.AreEqual(expectedSuccess, success);
            Assert.AreEqual(actualCount, expectedCount);
            Assert.AreEqual(expectedReturnValue, returnableCount);
        }

        [TestCase(10, 5, 5, false)]
        [TestCase(3, 3, 0, true)]
        public void AddItemAndReturnSomeItemsBackForCycle(int count, int expectedCount, int expectedReturnValue, bool expectedSuccess)
        {
            //Arrange:
            ItemConfig itemConfig = new ItemConfig("Wood");
            
            ConverterZone converterZone = new ConverterZone(5, itemConfig);

            bool success = false;
            int returnableCount = 0;
            
            //Act:
            for (int i = 0; i < count; i++)
            {
                success = converterZone.AddItem(itemConfig, 1, out var returnableValue);
                returnableCount += returnableValue;
            }

            int actualCount = converterZone.GetAmountOfCurrentItem();

            //Assert
            Assert.AreEqual(expectedSuccess, success);
            Assert.AreEqual(actualCount, expectedCount);
            Assert.AreEqual(expectedReturnValue, returnableCount);
        }

        [TestCaseSource(nameof(AddItemByIndexCases))]
        public void AddItemByIndex(ItemConfig itemConfig, int count, int expectedCount, bool expectedSuccess)
        {
            //Arrange:
            ConverterZone zone = new ConverterZone(5, new ItemConfig("Wood"));
            
            //Act:
            bool success = zone.AddItem(itemConfig, count);
            
            //Assert:
            Assert.AreEqual(expectedCount, zone.GetAmountOfCurrentItem());
            Assert.AreEqual(success, expectedSuccess);
        }

        private static IEnumerable<TestCaseData> AddItemByIndexCases()
        {
            yield return new TestCaseData(new ItemConfig("Wood"), 2, 2, true).SetName("AddItemByIndexCases Wood");
            
            yield return new TestCaseData(new ItemConfig("Cement"), 1, 0, false).SetName("AddItemByIndexCases Cement");
            
            yield return new TestCaseData(null, 1, 0, false).SetName("null");
            
            yield return new TestCaseData(new ItemConfig(""), 1, 0, false).SetName("without name");
        }

        [Test]
        public void GetCurrentItemAmount()
        {
            //Arrange:
            ConverterZone converterZone = new ConverterZone(5, new ItemConfig("Wood"));
            ItemConfig itemConfig = new ItemConfig("Wood");
            int count = converterZone.GetAmountOfCurrentItem();
            
            //Act:

            var success = converterZone.AddItem(itemConfig, count);
            var actualCount = converterZone.GetAmountOfCurrentItem();

            //Assert:
            
            Assert.AreEqual(count, actualCount);
            Assert.IsTrue(success);
        }
        
        [TestCase(0, 0, 5)]
        [TestCase(-1, 0, 5)]
        [TestCase(10, 0, 5)]
        [TestCase(3, 3, 2)]
        public void GetItem(int count, int expectedCount, int expectedAmount)
        {
            //Arrange:
            ConverterZone converterZone = new ConverterZone(5, new ItemConfig("Wood"), 5);
            ItemConfig itemConfig = new ItemConfig("Wood");
            
            //Act:
            int returnCount = converterZone.GetCountOfItems(itemConfig, count);
            int amount = converterZone.GetAmountOfCurrentItem();
            
            //Assert
            Assert.AreEqual(expectedCount, returnCount);
            Assert.AreEqual(expectedAmount, amount);
        }

        [TestCaseSource(nameof(GetCertainNumbersOfItemCases))]
        public void GetCertainNumbersOfItem(ItemConfig item, int countOfItems, int expectedCount, int expectedAmount)
        {
            //Arrange
            ConverterZone converterZone = new ConverterZone(5, new ItemConfig("Wood"), 5);
            
            //Act:
            int returnCount = converterZone.GetCountOfItems(item, countOfItems);
            int amount = converterZone.GetAmountOfCurrentItem();
            
            //Assert
            Assert.AreEqual(expectedCount, returnCount);
            Assert.AreEqual(expectedAmount, amount);
        }

        private static IEnumerable<TestCaseData> GetCertainNumbersOfItemCases()
        {
            yield return new TestCaseData(new ItemConfig("Wood"), 2, 2, 3).SetName("Get count by wood");
            yield return new TestCaseData(new ItemConfig("Bricks"), 2, 0, 5).SetName("Get count by bricks");
            yield return new TestCaseData(null, 0, 0, 5).SetName("null");
        }
    }
}