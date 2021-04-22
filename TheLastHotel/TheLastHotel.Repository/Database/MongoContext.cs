using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System;
using TheLastHotel.Repository.Database.Interfaces;

namespace TheLastHotel.Repository.Database
{
    public class MongoContext : IMongoContext
    {
        private IMongoDatabase Database { get; set; }
        public MongoContext(IOptions<MongoDbSettings> configuration)
        {
            if (configuration == null || configuration.Value == null)
                throw new ArgumentException("'MongoDbSettings' is null");

            if (string.IsNullOrWhiteSpace(configuration.Value.ConnectionString))
                throw new ArgumentException("'ConnectionString' is null");

            if (string.IsNullOrWhiteSpace(configuration.Value.DatabaseName))
                throw new ArgumentException("'DatabaseName' is null");

            

            var mongoConnectionUrl = new MongoUrl(configuration.Value.ConnectionString);
            var mongoClientSettings = MongoClientSettings.FromUrl(mongoConnectionUrl);

            var mongoClient = new MongoClient(mongoClientSettings);

            Database = mongoClient.GetDatabase(configuration.Value.DatabaseName);
            
            RegisterConventions();
        }

        private void RegisterConventions()
        {
            var pack = new ConventionPack
            {
                new IgnoreExtraElementsConvention(true),
                new IgnoreIfDefaultConvention(true),
            };
            ConventionRegistry.Register("Conventions", pack, t => true);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return Database.GetCollection<T>(name);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}