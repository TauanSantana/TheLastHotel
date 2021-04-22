using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TheLastHotel.Repository.Database.Interfaces;

namespace TheLastHotel.Service.Booking.Query
{
    public class ListAllBookingsForClientQuery : IListAllBookingsForClientQuery
    {
        readonly IRepository<Domain.Booking> BookingRepository;
        public ListAllBookingsForClientQuery(IRepository<Domain.Booking> bookingRepository)
        {
            BookingRepository = bookingRepository;
        }

        public async Task<IEnumerable<Domain.Booking>> Execute(string ClientId)
        {
            var query = new List<Expression<Func<Domain.Booking, bool>>>
            {
                x => x.Client.Id == ClientId
            };

            return await BookingRepository.GetByFilters(query);
        }
    }
}
