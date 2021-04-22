using System.Threading.Tasks;

namespace TheLastHotel.Service.Booking.Query
{
    public interface ICheckIfRoomIsAvailabilityWhenUpdateQuery
    {
        Task<bool> Execute(Domain.Booking booking);
    }
}