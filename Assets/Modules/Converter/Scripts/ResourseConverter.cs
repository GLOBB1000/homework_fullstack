using System;

namespace Modules.Converter
{
    public class ResourseConverter
    {
        private ConverterZone _leftZone;
        private ConverterZone _rightZone;
        
        public ResourseConverter(ConverterZone leftZone, ConverterZone rightZone)
        {
            _leftZone = leftZone;
            _rightZone = rightZone;
        }
    }
}