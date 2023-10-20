namespace ErrorQueue.DatabaseSettings
{
    public class FailedStatusDatabaseSettings : IFailedStatusDatabaseSettings
    {
        public string failedStatusCollectionName { get; set; } = string.Empty;
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
    }
}
