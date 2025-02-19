namespace Game.Scripts.SaveSystem.Core
{
    public interface ISavable<T> where T : struct
    {
        T GetFields();
    }
}