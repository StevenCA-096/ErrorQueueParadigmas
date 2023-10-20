namespace ErrorQueue.DatabaseSettings
{
    public class ShoppingCartDatabaseSettings:IShoppingCartDatabaseSettings
    {
        public string shooppingCartCollectionName { get; set; } = string.Empty;
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
    }
}
