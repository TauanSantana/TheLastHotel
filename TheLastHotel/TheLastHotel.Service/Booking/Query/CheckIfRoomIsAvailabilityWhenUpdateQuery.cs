using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TheLastHotel.Repository.Database.Interfaces;

namespace TheLastHotel.Service.Booking.Query
{
    public class CheckIfRoomIsAvailabilityWhenUpdateQuery : ICheckIfRoomIsAvailabilityWhenUpdateQuery
    {
        readonly IRepository<Domain.Booking> BookingRepository;
        public CheckIfRoomIsAvailabilityWhenUpdateQuery(IRepository<Domain.Booking> bookingRepository)
        {
            BookingRepository = bookingRepository;
        }

        public async Task<bool> Execute(Domain.Booking booking)
        {
            var query = new List<Expression<Func<Domain.Booking, bool>>>();
            query.Add(x => x.Id != booking.Id);
            query.Add(x => x.CancellationAt == null);
            query.Add(x => x.Room.Id == booking.Room.Id);
            query.Add(x => (x.StartReservationDate <= booking.StartReservationDate && x.EndReservationDate >= booking.StartReservationDate)
            ||  (x.StartReservationDate >= booking.StartReservationDate && x.EndReservationDate <= booking.EndReservationDate));
            return  await BookingRepository.Count(query) == 0;
        }
    }
}
