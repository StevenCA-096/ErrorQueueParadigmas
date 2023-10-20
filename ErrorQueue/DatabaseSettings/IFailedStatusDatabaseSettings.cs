namespace ErrorQueue.DatabaseSettings
{
    public interface IFailedStatusDatabaseSettings
    {
        public string failedStatusCollectionName { get; set; }
        public string ConnectionString { set; get; }
        public string DatabaseName { get; set; }
    }
}
