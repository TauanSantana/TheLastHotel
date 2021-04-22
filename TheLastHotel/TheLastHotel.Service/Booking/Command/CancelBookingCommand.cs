using Flunt.Notifications;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheLastHotel.Repository.Database.Interfaces;
using TheLastHotel.Service.Booking.Query;

namespace TheLastHotel.Service.Booking.Command
{
    public class CancelBookingCommand : Notifiable<Notification>, ICancelBookingCommand
    {
        readonly IRepository<Domain.Booking> BookingRepository;
        readonly IFindBookingByIdQuery FindBookingByIdQuery;

        public bool HasNotification { get { return !this.IsValid; } }
        public IReadOnlyCollection<Notification> CommandNotifications { get { return this.Notifications; } }

        public CancelBookingCommand(IRepository<Domain.Booking> bookingRepository, IFindBookingByIdQuery findBookingByIdQuery)
        {
            BookingRepository = bookingRepository;
            FindBookingByIdQuery = findBookingByIdQuery;
        }

        public async Task Execute(string bookingId)
        {
            ValidateRules(bookingId);
            if (this.IsValid)
            {
                var booking = await FindBookingByIdQuery.Execute(bookingId);
                if (booking == null)
                {
                    AddNotification("Booking", "Booking not found");
                    return;
                }

                if (booking.ClientChecking != null)
                {
                    AddNotification("Booking", "Customer has used the booking");
                    return;
                }

                if (booking.CancellationAt != null)
                {
                    AddNotification("Booking", "Reservation already canceled");
                    return;
                }

                booking.CancellationAt = DateTime.UtcNow;
                await BookingRepository.Update(booking);
            }
        }

        private void ValidateRules(string bookingId)
        {
            if (string.IsNullOrWhiteSpace(bookingId))
            {
                AddNotification("Booking", "Booking Id is null.");
                return;
            }

            if (!ObjectId.TryParse(bookingId, out _))
            {
                AddNotification("Booking", "Booking Id is in the wrong format");
                return;
            }

            
        }
    }
}
