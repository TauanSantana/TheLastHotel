using System.Threading.Tasks;

namespace TheLastHotel.Service.Booking.Query
{
    public interface ICheckIfRoomIsAvailabilityQuery
    {
        Task<bool> Execute(Domain.Booking booking);
    }
}