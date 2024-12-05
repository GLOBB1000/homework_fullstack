namespace Modules.Converter
{
    public class ConverterTimer : ITimer
    {
        private int maxTime;
        
        private float elapsedTime;

        public ConverterTimer(int maxTime)
        {
            this.maxTime = maxTime;
            ResetTime();
        }

        public float GetTime(float deltaTime)
        {
            elapsedTime -= deltaTime;

            return elapsedTime;
        }

        public void ResetTime()
        {
            elapsedTime = maxTime;
        }
    }
}