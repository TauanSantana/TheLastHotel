using System.Threading.Tasks;

namespace TheLastHotel.Service.Booking.Query
{
    public interface IFindBookingByIdQuery
    {
        Task<Domain.Booking> Execute(string id);
    }
}