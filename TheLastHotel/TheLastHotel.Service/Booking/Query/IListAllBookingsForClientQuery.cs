using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TheLastHotel.Service.Booking.Query
{
    public interface IListAllBookingsForClientQuery
    {
        Task<IEnumerable<Domain.Booking>> Execute(string ClientId);
    }
}