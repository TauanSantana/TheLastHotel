using System.Threading.Tasks;
using TheLastHotel.Service.Util;

namespace TheLastHotel.Service.Booking.Command
{
    public interface ICancelBookingCommand : ICommandBase
    {
        Task Execute(string bookingId);
    }
}