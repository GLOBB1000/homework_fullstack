namespace Modules.Converter
{
    public class ItemConfig
    {
        public string Id { get; private set; }
        
        public ItemConfig(string id)
        {
            Id = id;
        }
    }
}