using System;
using System.Collections.Generic;
using System.Text;

namespace TheLastHotel.Repository.Database
{
    public static class MongoDbPersistence<T>
         where T : RepositoryMapBase
    {
        public static void Configure()
        {
            var configurator = (RepositoryMapBase)Activator.CreateInstance(typeof(T));
            configurator.Configure();
        }
    }
}
