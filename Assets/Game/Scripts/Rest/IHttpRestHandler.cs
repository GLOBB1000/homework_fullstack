using System.Threading.Tasks;

namespace Game.Scripts.Rest
{
    public interface IHttpRestHandler
    {
        Task<string> Load(string version);
        
        Task Save(string version, string json);
    }
}