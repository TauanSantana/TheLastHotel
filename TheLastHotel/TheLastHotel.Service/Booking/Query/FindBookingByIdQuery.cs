using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TheLastHotel.Repository.Database.Interfaces;

namespace TheLastHotel.Service.Booking.Query
{
    public class FindBookingByIdQuery : IFindBookingByIdQuery
    {
        readonly IRepository<Domain.Booking> BookingRepository;
        public FindBookingByIdQuery(IRepository<Domain.Booking> bookingRepository)
        {
            BookingRepository = bookingRepository;
        }

        public async Task<Domain.Booking> Execute(string id)
        {
            return await BookingRepository.GetById(id);
        }
    }
}
