using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace Modules.Converter
{
    public class ResourсeConverterTests
    {
        [Test]
        public void Instantiate()
        {
            ConverterZone leftZone = new ConverterZone(2, new ItemConfig("Wood"));
            ConverterZone rightZone = new ConverterZone(2, new ItemConfig("Wood"));

            ResourсeConverter converter = new ResourсeConverter(leftZone, rightZone);

            Assert.IsNotNull(converter);
        }

        // [Test]
        // public void SetUpConverter()
        // {
        //     //Arrange:
        //     
        //     // ItemConfig leftItem = new ItemConfig("Wood", new ItemConfig("Board"));
        //     // ItemConfig rightItem = new ItemConfig("Board");
        //     var rightItem = new ItemConfig("Board");
        //     var leftItem = new ItemConfig("Wood", rightItem);
        //
        //     ConverterZone leftZone = new ConverterZone(10, leftItem, 5);
        //     ConverterZone rightZone = new ConverterZone(10, rightItem, 0);
        //     
        //     ResourseConverter converter = new ResourseConverter(leftZone, rightZone);
        //     
        //     
        // }

        [TestCase(2, 3, 2, true)]
        [TestCase(-1, 5, 0, false)]
        [TestCase(10, 5, 0, false)]
        [TestCase(5, 0, 5, true)]
        public void Convert(int countToConvert, int expectedAmountLeft, int expectedAmountRight, bool expectedSuccess)
        {
            //Arrange:
            
            ItemConfig rightItem = new ItemConfig("Board");
            ItemConfig leftItem = new ItemConfig("Wood", rightItem);
            
            
            ConverterZone leftZone = new ConverterZone(10, leftItem, 5);
            ConverterZone rightZone = new ConverterZone(10, leftItem.CraftedItemConfig, 0);
            
            ResourсeConverter converter = new ResourсeConverter(leftZone, rightZone);

            //Act:
 
            bool success = converter.Convert(countToConvert);
            
            int actualCraftedItemsCount = rightZone.GetAmountOfCurrentItem();
            int leftZoneRemainingCount = leftZone.GetAmountOfCurrentItem();

            //Assert:

            Assert.AreEqual(expectedSuccess, success);
            
            Assert.AreEqual(expectedAmountLeft, leftZoneRemainingCount);
            Assert.AreEqual(expectedAmountRight, actualCraftedItemsCount);
            
            Assert.AreEqual(leftItem.CraftedItemConfig.Id, rightZone.GetCurrentItem().Id);
        }

        [TestCase(2, 3, 2, true, true)]
        [TestCase(-1, 5, 0, false, false)]
        public void ConvertItemForTime(int countToConvert, int expectedAmountLeft, int expectedAmountRight, bool expectedSuccessTimeEnded, bool expectedTimeStarted)
        {
            //Arrange:
            
            ItemConfig rightItem = new ItemConfig("Board");
            ItemConfig leftItem = new ItemConfig("Wood", rightItem);
            
            ConverterZone leftZone = new ConverterZone(10, leftItem, 5);
            ConverterZone rightZone = new ConverterZone(10, leftItem.CraftedItemConfig, 0);
            
            ResourсeConverter converter = new ResourсeConverter(leftZone, rightZone);

            var isStartConvering = false;
            bool isConverted = false;
            
            converter.OnStartConvert += (iConfig) =>
            {
                isStartConvering = true;
            };
            
            converter.OnEndConvert += (iConfig, amount) =>
            {
                isConverted = true;
            };
            
            //Act
            
            //Simulate update
            
            for (var i = 10; i >= 0; i--)
            {
                converter.ConvertByTimer(countToConvert, 1);
            }

            //Assert
            Assert.AreEqual(expectedSuccessTimeEnded, isConverted);
            Assert.AreEqual(expectedTimeStarted, isStartConvering);
        }

        [TestCase(5, 5, 0, 0, 5, true)]
        [TestCase(5,5, 8, 3, 10, true)]
        [TestCase(10,9, 0, 0, 9, true)]
        [TestCase(-1,5, 0, 5, 0, false)]
        public void ConvertCycle(int countToConvert, int initialLeftZoneAmount, int initialRightZoneAmount, int expectedAmountLeft, int expectedAmountRight, bool expectedSuccessTimeEnded)
        {
            //Arrange:
            ItemConfig rightItem = new ItemConfig("Board");
            ItemConfig leftItem = new ItemConfig("Wood", rightItem);
            
            ConverterZone leftZone = new ConverterZone(10, leftItem, initialLeftZoneAmount);
            ConverterZone rightZone = new ConverterZone(10, leftItem.CraftedItemConfig, initialRightZoneAmount);
            
            ResourсeConverter converter = new ResourсeConverter(leftZone, rightZone);

            bool isConverted = false;

            converter.OnStartConvert += (iConfig) =>
            {
                isConverted = false;
            };
            
            converter.OnEndConvert += (iConfig, amount) =>
            {
                isConverted = true;
            };
            
            //Act:
            
            for (var i = countToConvert * 10; i >= 0; i--)
            {
                converter.MockUpdate();
            }
            
            //Assert:
            Assert.AreEqual(expectedSuccessTimeEnded, isConverted);
            Assert.AreEqual(expectedAmountLeft, leftZone.GetAmountOfCurrentItem());
            Assert.AreEqual(expectedAmountRight, rightZone.GetAmountOfCurrentItem());
        }
        
        [TestCase(2, 5, 50, 0, 0, 60, true)]
        [TestCase(2, 20, 100, 0, 20, 100, true)]
        [TestCase(5, 20, 20, 0, 20, 100, true)]

        public void ConvertCycleIterationsWhenAddedToLeftZoneCases(int iterations, int countToAddToLeftZoneByIteration, int initialLeftZoneAmount, int initialRightZoneAmount,
            int expectedAmountLeft, int expectedAmountRight, bool expectedSuccessTimeEnded)
        {
            //Arrange:
            ItemConfig rightItem = new ItemConfig("Board");
            ItemConfig leftItem = new ItemConfig("Wood", rightItem);
            
            ConverterZone leftZone = new ConverterZone(100, leftItem, initialLeftZoneAmount);
            ConverterZone rightZone = new ConverterZone(100, leftItem.CraftedItemConfig, initialRightZoneAmount);
            
            ResourсeConverter converter = new ResourсeConverter(leftZone, rightZone);

            bool isConverted = false;
            bool isAddedToLeftZone = false;

            converter.OnStartConvert += (iConfig) =>
            {
                isConverted = false;
            };
            
            converter.OnEndConvert += (iConfig, amount) =>
            {
                isConverted = true;
            };
            
            //Act:
            for (var iter = 0; iter < iterations; iter++)
            {
                isAddedToLeftZone = leftZone.AddItem(countToAddToLeftZoneByIteration);
                for (var i = leftZone.GetAmountOfCurrentItem() * 10; i >= 0; i--)
                {
                    converter.MockUpdate();
                }
            }
            
            //Assert:
            Assert.IsTrue(isAddedToLeftZone);
            Assert.AreEqual(expectedSuccessTimeEnded, isConverted);
            Assert.AreEqual(expectedAmountLeft, leftZone.GetAmountOfCurrentItem());
            Assert.AreEqual(expectedAmountRight, rightZone.GetAmountOfCurrentItem());
        }

        [TestCase(true)]
        [TestCase(false)]
        public void ConverterSwitcherTest(bool isEnabled)
        {
            //Arrange:
            ConverterZone leftZone = new ConverterZone(100, null, 0);
            ConverterZone rightZone = new ConverterZone(100, null, 0);
            
            ResourсeConverter converter = new ResourсeConverter(leftZone, rightZone);

            bool isConverterEnabled = false;
            
            converter.OnConverterSwitchValueChanged += (switchState) =>
            {
                isConverterEnabled = switchState;
            };
            
            //Act:
            
            converter.SetSwitcherState(isEnabled);
            bool converterState = converter.GetSwitcherState();

            //Assert:
            Assert.AreEqual(isConverterEnabled, converterState);
        }

        [TestCase(true, 0, 10)]
        [TestCase(false, 10, 0)]
        public void CheckIsConverterWorkAndNotWorkWhenChangeSwitcherValue(bool isEnabled, int expectedAmountLeft, int expectedAmountRight)
        {
            //Arrange:
            ItemConfig rightItem = new ItemConfig("Board");
            ItemConfig leftItem = new ItemConfig("Wood", rightItem);
            
            ConverterZone leftZone = new ConverterZone(10, leftItem, 10);
            ConverterZone rightZone = new ConverterZone(10, leftItem.CraftedItemConfig, 0);
            
            ResourсeConverter converter = new ResourсeConverter(leftZone, rightZone);
            
            //Act:
            converter.SetSwitcherState(isEnabled);
            bool isConverted = converter.Convert(10);
            
            //Asert
            
            Assert.AreEqual(isEnabled, isConverted);

            Assert.AreEqual(expectedAmountLeft, leftZone.GetAmountOfCurrentItem());
            Assert.AreEqual(expectedAmountRight, rightZone.GetAmountOfCurrentItem());

        }
        
        [TestCase(true, 0, 10)]
        [TestCase(false, 10, 0)]
        public void CheckIsConverterWorkAndNotWorkWhenChangeSwitcherValueWhenCycleIsRunning(bool isEnabled, int expectedAmountLeft, int expectedAmountRight)
        {
            //Arrange:
            ItemConfig rightItem = new ItemConfig("Board");
            ItemConfig leftItem = new ItemConfig("Wood", rightItem);
            
            ConverterZone leftZone = new ConverterZone(10, leftItem, 10);
            ConverterZone rightZone = new ConverterZone(10, leftItem.CraftedItemConfig, 0);
            
            ResourсeConverter converter = new ResourсeConverter(leftZone, rightZone);
            
            bool isStartConverting = false;
            bool isEndConverting = false;

            converter.OnStartConvert += (iConfig) =>
            {
                isStartConverting = true;
            };
            
            converter.OnEndConvert += (iConfig, amount) =>
            {
                isEndConverting = true;
            };

            //Act:
            
            converter.SetSwitcherState(isEnabled);
            
            for (int i = 0; i < 100; i++)
            {
                converter.MockUpdate();
            }
            
            //Asert
            
            Assert.AreEqual(isEnabled, isStartConverting);
            Assert.AreEqual(isEnabled, isEndConverting);
            
            Assert.AreEqual(expectedAmountLeft, leftZone.GetAmountOfCurrentItem());
            Assert.AreEqual(expectedAmountRight, rightZone.GetAmountOfCurrentItem());

        }

         [TestCase(9, 1, 85)]
         [TestCase(8, 2, 80)]
         [TestCase(8, 2, 75)]
         [TestCase(8, 2, 73)]
         [TestCase(7, 3, 70)]
         public void SetSwitcherOffWhenConverterIsWorking(int expectedAmountLeft, int expectedAmountRight, int stopTime)
         {
             //Arrange:
             ItemConfig rightItem = new ItemConfig("Board");
             ItemConfig leftItem = new ItemConfig("Wood", rightItem);
             
             ConverterZone leftZone = new ConverterZone(10, leftItem, 10);
             ConverterZone rightZone = new ConverterZone(10, leftItem.CraftedItemConfig, 0);
             
             ResourсeConverter converter = new ResourсeConverter(leftZone, rightZone);
             
             //Act:
             //isAddedToLeftZone = leftZone.AddItem(countToAddToLeftZoneByIteration);
             for (var i = leftZone.GetAmountOfCurrentItem() * 10; i >= 0; i--)
             {
                 if(i == stopTime)
                     converter.SetSwitcherState(false);
                 
                 converter.MockUpdate();
             }
             
             //Assert:
             Assert.AreEqual(expectedAmountLeft, leftZone.GetAmountOfCurrentItem());
             Assert.AreEqual(expectedAmountRight, rightZone.GetAmountOfCurrentItem());
         }
         
         [TestCase(10, 1, 85)]
         [TestCase(10, 2, 75)]
         public void SetSwitcherOffWhenConverterIsWorkingWhileAddedItemToTheLeft(int expectedAmountLeft, int expectedAmountRight, int stopTime)
         {
             //Arrange:
             ItemConfig rightItem = new ItemConfig("Board");
             ItemConfig leftItem = new ItemConfig("Wood", rightItem);
             
             ConverterZone leftZone = new ConverterZone(10, leftItem, 10);
             ConverterZone rightZone = new ConverterZone(10, leftItem.CraftedItemConfig, 0);
             
             ResourсeConverter converter = new ResourсeConverter(leftZone, rightZone);
             
             //Act:
                 
             for (var i = leftZone.GetAmountOfCurrentItem() * 10; i >= 0; i--)
             {
                 if(i == stopTime)
                     converter.SetSwitcherState(false);
                 
                 if(i == stopTime + 5)
                     leftZone.AddItem(5);

                 converter.MockUpdate();
             }
             
             //Assert:
             Assert.AreEqual(expectedAmountLeft, leftZone.GetAmountOfCurrentItem());
             Assert.AreEqual(expectedAmountRight, rightZone.GetAmountOfCurrentItem());
         }
    }
}
