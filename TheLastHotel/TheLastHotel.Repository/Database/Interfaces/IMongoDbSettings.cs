using System;
using System.Collections.Generic;
using System.Text;

namespace TheLastHotel.Repository.Database.Interfaces
{
    public interface IMongoDbSettings
    {
        string DatabaseName { get; set; }
        string ConnectionString { get; set; }
    }
}
