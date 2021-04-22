using TheLastHotel.Domain;
using TheLastHotel.Repository.Database;
using TheLastHotel.Repository.Database.Interfaces;

namespace TheLastHotel.Repository
{
    public class BookingRepository : BaseRepository<Booking>, IRepository<Booking>
    {
        public BookingRepository(IMongoContext context) : base(context)
        {
        }
    }
}
