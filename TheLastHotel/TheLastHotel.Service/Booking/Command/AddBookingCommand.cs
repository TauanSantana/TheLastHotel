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
    public class AddBookingCommand : Notifiable<Notification>, IAddBookingCommand
    {
        readonly IRepository<Domain.Booking> BookingRepository;
        readonly ICheckIfRoomIsAvailabilityQuery CheckIfRoomIsAvailabilityQuery;
        readonly IFindClientByIdQuery FindClientByIdQuery;
        readonly IFindRoomByIdQuery FindRoomByIdQuery;

        public bool HasNotification { get { return !this.IsValid; } }
        public IReadOnlyCollection<Notification> CommandNotifications { get { return this.Notifications; } }
        public AddBookingCommand(
            IRepository<Domain.Booking> bookingRepository, 
            ICheckIfRoomIsAvailabilityQuery 
            checkIfRoomIsAvailabilityQuery, 
            IFindClientByIdQuery findClientByIdQuery,
            IFindRoomByIdQuery findRoomByIdQuery)
        {
            BookingRepository = bookingRepository;
            CheckIfRoomIsAvailabilityQuery = checkIfRoomIsAvailabilityQuery;
            FindClientByIdQuery = findClientByIdQuery;
            FindRoomByIdQuery = findRoomByIdQuery;
        }
      

        public async Task Execute(Domain.Booking booking)
        {
            await ValidateRules(booking);
            if (this.IsValid)
            {
                var client = await FindClientByIdQuery.Execute(booking.Client.Id);
                var room = await FindRoomByIdQuery.Execute(booking.Room.Id);

                if (client == null)
                {
                    AddNotification("Client", "Client not found.");
                    return;
                }

                if (room == null)
                {
                    AddNotification("Room", "Room not found.");
                    return;
                }

                booking.CreateAt = DateTime.UtcNow;
                booking.Client = client;
                booking.Room = room;
                booking.StartReservationDate = booking.StartReservationDate.Date;
                booking.EndReservationDate = booking.EndReservationDate.Date;
                await BookingRepository.Add(booking);
            }
        }

        

        private async Task ValidateRules(Domain.Booking booking)
        {
            #region "Validation of property values"
            if (booking.Client == null || string.IsNullOrWhiteSpace(booking.Client.Id))
            {
                AddNotification("Client", "Client is null.");
                return;
            }

            if (!ObjectId.TryParse(booking.Client.Id, out _))
            {
                AddNotification("Client", "ClientId is in the wrong format");
                return;
            }

            if (booking.Room == null || string.IsNullOrWhiteSpace(booking.Room.Id))
            {
                AddNotification("Room", "Room not informed.");
                return;
            }

            if (!ObjectId.TryParse(booking.Room.Id, out _))
            {
                AddNotification("Room", "RoomId is in the wrong format");
                return;
            }
           
            #endregion

            #region "Business validation"
            
            var availability = await CheckIfRoomIsAvailabilityQuery.Execute(booking);
            if (availability.status == false) { 
                AddNotification(availability.notification);
                return;
            }

            #endregion



        }
    }
}
