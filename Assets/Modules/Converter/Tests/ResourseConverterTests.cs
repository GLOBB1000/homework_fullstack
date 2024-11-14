using NUnit.Framework;

namespace Modules.Converter
{
    public class ResourseConverterTests
    {
        [Test]
        public void Instantiate()
        {
            ConverterZone leftZone = new ConverterZone(2, new ItemConfig("Wood"));
            ConverterZone rightZone = new ConverterZone(2, new ItemConfig("Wood"));

            ResourseConverter converter = new ResourseConverter(leftZone, rightZone);

            Assert.IsNotNull(converter);
        }

        // [Test]
        // public void Convert()
        // {
        //     ConverterZone leftZone = new ConverterZone();
        //     ConverterZone rightZone = new ConverterZone();
        //
        //     ResourseConverter converter = new ResourseConverter(leftZone, rightZone);
        //
        //
        //     
        //
        // }
    }
}
