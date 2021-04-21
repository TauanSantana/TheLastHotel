using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheLastHotel.Repository.Database.Interfaces;

namespace TheLastHotel.Repository.Database
{
    public class MongoContext : IMongoContext
    {
        private IMongoDatabase Database { get; set; }
        public MongoContext(string mongoConnection, string databaseName)
        {
            if (string.IsNullOrWhiteSpace(mongoConnection) && string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("MONGOCONNECTION")))
                throw new ArgumentException("O parâmetro 'mongoConnection' não foi informado");

            if (string.IsNullOrWhiteSpace(databaseName) && string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("DATABASENAME")))
                throw new ArgumentException("O parâmetro 'databaseName' não foi informado");


            RegisterConventions();

            var mongoConnectionUrl = new MongoUrl(mongoConnection);
            var mongoClientSettings = MongoClientSettings.FromUrl(mongoConnectionUrl);


            var mongoClient = new MongoClient(mongoClientSettings);

            Database = mongoClient.GetDatabase(databaseName);
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