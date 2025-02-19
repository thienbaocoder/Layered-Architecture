using Microsoft.Extensions.Options;
using MongoDB.Driver;
using LayeredArchitecture.Models;

namespace LayeredArchitecture.PersistenceLayer
    {
    public class CustomerContext
        {
        private readonly IMongoDatabase _database;
        public IMongoCollection<Customer> CustomersCollection { get; }

        public CustomerContext(IOptions<DatabaseSettings> settings)
            {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
            CustomersCollection = _database.GetCollection<Customer>(settings.Value.CollectionName);
            }
        }
    }
