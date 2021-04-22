using TheLastHotel.Domain;
using TheLastHotel.Repository.Database;
using TheLastHotel.Repository.Database.Interfaces;

namespace TheLastHotel.Repository
{
    public class RoomRepository : BaseRepository<Room>, IRepository<Room>
    {
        public RoomRepository(IMongoContext context) : base(context)
        {
        }
    }
}
