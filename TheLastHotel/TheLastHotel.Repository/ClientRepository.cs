using TheLastHotel.Domain;
using TheLastHotel.Repository.Database;
using TheLastHotel.Repository.Database.Interfaces;

namespace TheLastHotel.Repository
{
    public class ClientRepository : BaseRepository<Client>, IRepository<Client>
    {
        public ClientRepository(IMongoContext context) : base(context)
        {
        }
    }
}
