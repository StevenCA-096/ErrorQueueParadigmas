namespace ErrorQueue.Models
{
    public interface IShoppingCartDatabaseSettings
    {
        public string shooppingCartCollectionName { get; set; }
        public string ConnectionString { set; get; }
        public string DatabaseName { get; set; }
    }
}
