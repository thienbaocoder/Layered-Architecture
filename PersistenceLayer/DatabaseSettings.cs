namespace LayeredArchitecture.PersistenceLayer
    {
    public class DatabaseSettings
        {
        public string ConnectionString { get; set; } = "mongodb://localhost:27017";
        public string DatabaseName { get; set; } = "Customer";
        public string CollectionName { get; set; } = "Customer";
        }
    }
