using System;
using System.Collections.Generic;
using System.Text;
using TheLastHotel.Repository.Database.Interfaces;

namespace TheLastHotel.Repository.Database
{
    public class MongoDbSettings : IMongoDbSettings
    {
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }

    }
}
