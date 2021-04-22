using System.Threading.Tasks;
using TheLastHotel.Service.Util;

namespace TheLastHotel.Service.Booking.Command
{
    public interface IAddBookingCommand : ICommandBase
    {
        Task Execute(Domain.Booking booking);
    }
}