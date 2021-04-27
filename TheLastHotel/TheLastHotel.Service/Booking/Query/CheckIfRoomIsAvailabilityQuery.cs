using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TheLastHotel.Repository.Database.Interfaces;

namespace TheLastHotel.Service.Booking.Query
{
    public class CheckIfRoomIsAvailabilityQuery : ICheckIfRoomIsAvailabilityQuery
    {
        readonly IRepository<Domain.Booking> BookingRepository;
        public CheckIfRoomIsAvailabilityQuery(IRepository<Domain.Booking> bookingRepository)
        {
            BookingRepository = bookingRepository;
        }

        public async Task<(bool status, Notification notification)> Execute(Domain.Booking booking)
        {
            if (booking.StartReservationDate.Date == DateTime.MinValue)
                return (false, new Notification("Booking", "Invalid date of reservation."));

            if (booking.EndReservationDate.Date == DateTime.MinValue)
                return (false, new Notification("Booking", "Invalid reservation end date."));

            if (booking.StartReservationDate.Date < DateTime.UtcNow)
                return (false, new Notification("Booking", "Invalid start date of reservation."));

            if (booking.EndReservationDate.Date < DateTime.UtcNow)
                return (false, new Notification("Booking", "Invalid end reservation date."));


            if (booking.EndReservationDate.Date < booking.StartReservationDate.Date)
                return (false, new Notification("Booking", "Invalid range of dates."));

            if (booking.StartReservationDate.Date > (DateTime.UtcNow.AddDays(30)).Date)
                return (false, new Notification("Booking", "You can not make reservations more than 30 days before."));


            if ((booking.EndReservationDate.Date - booking.StartReservationDate.Date).TotalDays >= 3)
                return (false, new Notification("Booking", "The maximum number of days allowed for booking is 3."));


            var startDate = booking.StartReservationDate.Date;
            var endDate = booking.EndReservationDate.Date;

            var query = new List<Expression<Func<Domain.Booking, bool>>>();
            if (!string.IsNullOrWhiteSpace(booking.Id))
                query.Add(x => x.Id != booking.Id);

            query.Add(x => x.CancellationAt == null);
            query.Add(x => x.Room.Id == booking.Room.Id);
            query.Add(x => (x.StartReservationDate >= startDate && x.StartReservationDate <= endDate)
                        || (x.EndReservationDate >= startDate && x.EndReservationDate <= endDate));


            var result = await BookingRepository.GetByFilters(query);
            if (result.Count != 0)
                return (false, new Notification("Room", "Room not available for this date."));

            return (true, null);
        }
    }
}
