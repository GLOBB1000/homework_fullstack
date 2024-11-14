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
        [TestCase(10, 0, false)]
        [TestCase(-1, 0, false)]
        [TestCase(0, 0, true)]
        
        public void AddItem(int count, int expectedCount, bool expectedSuccess)
        {
            ItemConfig itemConfig = new ItemConfig("Wood");
            
            ConverterZone converterZone = new ConverterZone(5, itemConfig);

            bool success = converterZone.AddItem(itemConfig, count);

            int actualCount = converterZone.GetAmount(itemConfig);

            Assert.AreEqual(success, expectedSuccess);
            Assert.AreEqual(actualCount, expectedCount);
        }

        //[TestCaseSource(nameof(AddItemByIndexCases))]
        [TestCaseSource(nameof(AddItemByIndexCases))]
        public void AddItemByIndex(ConverterZone zone, ItemConfig itemConfig, int count, int expectedCount, bool expectedSuccess)
        {
            //Arrange:
            
            
            //Act:
            bool success = zone.AddItem(itemConfig, count);
            
            //Assert:
            Assert.AreEqual(expectedCount, zone.GetAmount(itemConfig));
            Assert.AreEqual(success, expectedSuccess);
        }

        private static IEnumerable<TestCaseData> AddItemByIndexCases()
        {
            yield return new TestCaseData(
                    new ConverterZone(5, new ItemConfig("Wood")),
                    new ItemConfig("Wood"),
                    2, 
                    2, 
                    true)
                .SetName("AddItemByIndexCases Wood");
            
            yield return new TestCaseData(
                    new ConverterZone(5, new ItemConfig("Wood")),
                    new ItemConfig("Cement"), 
                    1, 
                    0, 
                    false)
                .SetName("AddItemByIndexCases Cement");
            
            yield return new TestCaseData(
                    new ConverterZone(5, new ItemConfig("Wood")),
                    null, 
                    1, 
                    0, 
                    false)
                .SetName("null");
            
            yield return new TestCaseData(
                    new ConverterZone(5, new ItemConfig("Wood")),
                    new ItemConfig(""), 
                    1, 
                    0, 
                    false)
                .SetName("without name");
        }

        [Test]
        public void GetCount()
        {
            //Arrange:
            ConverterZone converterZone = new ConverterZone(5, new ItemConfig("Wood"));
            ItemConfig itemConfig = new ItemConfig("Wood");
            int count = converterZone.GetAmount(itemConfig);
            
            //Act:

            var success = converterZone.AddItem(itemConfig, count);
            var actualCount = converterZone.GetAmount(itemConfig);

            //Assert:
            
            Assert.AreEqual(count, actualCount);
            Assert.IsTrue(success);
        }

        [TestCaseSource(nameof(GetCountByIdCases))]
        public void GetCountById(ItemConfig item, int expectedCount)
        {
            //Arrange
            
            ConverterZone zone = new ConverterZone(5, new ItemConfig("Wood"), 2);
            
            //Assert
            Assert.AreEqual(expectedCount, zone.GetAmount(item));
        }

        private static IEnumerable<TestCaseData> GetCountByIdCases()
        {
            yield return new TestCaseData(new ItemConfig("Wood"), 2).SetName("Get count by wood");
            
            yield return new TestCaseData(new ItemConfig("Bricks"), 0).SetName("Get count by bricks");
            
            yield return new TestCaseData(null, 0).SetName("Get count of null");
        }

        [TestCase(0, 0)]
        public void GetItem(int count, int expectedCount)
        {
            //Arrange:
            ConverterZone converterZone = new ConverterZone(5, new ItemConfig("Wood"), 5);
            ItemConfig itemConfig = new ItemConfig("Wood");
        }

        [TestCaseSource(nameof(GetItemByIdCases))]
        public void GetItemById(ItemConfig item, int countOfItems, int expectedCount, int expectedAmount)
        {
            //Arrange
            ConverterZone converterZone = new ConverterZone(5, new ItemConfig("Wood"), 5);
            
            //Act:
            int returnCount = converterZone.GetCountOfItems(item, countOfItems);
            int amount = converterZone.GetAmount(item);
            
            //Assert
            Assert.AreEqual(expectedCount, returnCount);
            Assert.AreEqual(expectedAmount, amount);
        }

        private static IEnumerable<TestCaseData> GetItemByIdCases()
        {
            yield return new TestCaseData(new ItemConfig("Wood"), 2, 2, 3).SetName("Get count by wood");
            yield return new TestCaseData(new ItemConfig("Bricks"), 2, 0, 5).SetName("Get count by wood");
            yield return new TestCaseData(null, 0, 0, 5).SetName("null");
        }
    }
}