using Flunt.Notifications;
using System.Threading.Tasks;

namespace TheLastHotel.Service.Booking.Query
{
    public interface ICheckIfRoomIsAvailabilityQuery
    {
        Task<(bool status, Notification notification)> Execute(Domain.Booking booking);
    }
}