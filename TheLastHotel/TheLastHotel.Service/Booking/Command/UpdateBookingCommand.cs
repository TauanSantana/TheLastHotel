using Flunt.Notifications;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheLastHotel.Repository.Database.Interfaces;
using TheLastHotel.Service.Booking.Query;
using TheLastHotel.Service.Client.Query;
using TheLastHotel.Service.Room.Query;
using TheLastHotel.Service.Util;

namespace TheLastHotel.Service.Booking.Command
{
    public class UpdateBookingCommand : Notifiable<Notification>, IUpdateBookingCommand
    {
        readonly IRepository<Domain.Booking> BookingRepository;
        readonly ICheckIfRoomIsAvailabilityQuery CheckIfRoomIsAvailabilityQuery;
        readonly IFindBookingByIdQuery FindBookingByIdQuery;

        public bool HasNotification { get { return !this.IsValid; } }
        public IReadOnlyCollection<Notification> CommandNotifications { get { return this.Notifications; } }
        public UpdateBookingCommand(
            IRepository<Domain.Booking> bookingRepository, 
            ICheckIfRoomIsAvailabilityQuery checkIfRoomIsAvailabilityQuery, 
            IFindBookingByIdQuery findBookingByIdQuery)
        {
            BookingRepository = bookingRepository;
            CheckIfRoomIsAvailabilityQuery = checkIfRoomIsAvailabilityQuery;
            FindBookingByIdQuery = findBookingByIdQuery;
        }
      

        public async Task Execute(Domain.Booking booking)
        {
            ValidateRules(booking);
            if (this.IsValid)
            {
                var bookingDB = await FindBookingByIdQuery.Execute(booking.Id);
                if (bookingDB == null)
                {
                    AddNotification("Booking", "Booking not found or Id is inválid");
                    return;
                }

                booking.Room = bookingDB.Room;
                var availability = await CheckIfRoomIsAvailabilityQuery.Execute(booking);
                if (availability.status == false)
                {
                    AddNotification(availability.notification);
                    return;
                }

                bookingDB.StartReservationDate = booking.StartReservationDate;
                bookingDB.EndReservationDate = booking.EndReservationDate;

                await BookingRepository.Update(bookingDB);
            }
        }

        private void ValidateRules(Domain.Booking booking)
        {
            #region "Validation of property values"
            if (string.IsNullOrWhiteSpace(booking.Id))
            {
                AddNotification("Booking", "Booking Id is null.");
                return;
            }

            if (!ObjectId.TryParse(booking.Id, out _))
            {
                AddNotification("Booking", "Booking Id is in the wrong format");
                return;
            }
          
            #endregion

        }
    }
}
