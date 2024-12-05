namespace Modules.Converter
{
    public class ItemConfig
    {
        private readonly string id;
        public string Id => this.id;
        
        public ItemConfig CraftedItemConfig { get; private set; }
        
        public ItemConfig(string id)
        {
            this.id = id;
        }

        public ItemConfig(string id, ItemConfig cratedItemConfig) : this(id)
        {
            CraftedItemConfig = cratedItemConfig;
        }
    }
}