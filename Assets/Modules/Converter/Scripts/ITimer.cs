namespace Modules.Converter
{
    public interface ITimer
    {
        public float GetTime(float deltaTime);
        
        public void ResetTime();
    }
}