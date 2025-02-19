using SampleGame.Common;
using SampleGame.SerializedData;

namespace SampleGame.Gameplay
{
    public interface ISerializedComponent
    {
        SerializedComponentData Serialize();
        
        void Deserialize(SerializedComponentData serializedComponentData);
    }
}