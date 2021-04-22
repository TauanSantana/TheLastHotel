using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TheLastHotel.Repository.Database.Interfaces;

namespace TheLastHotel.Service.Room.Command
{
    public class AddRoomCommand : Notifiable<Notification>, IAddRoomCommand
    {
        readonly IRepository<Domain.Room> RoomRepository;
        public bool HasNotification { get { return !this.IsValid; } }
        public IReadOnlyCollection<Notification> CommandNotifications { get { return this.Notifications; } }
        public AddRoomCommand(IRepository<Domain.Room> roomRepository)
        {
            RoomRepository = roomRepository;
        }

        public async Task Execute(Domain.Room room)
        {
            ValidateRules(room);
            if (this.IsValid)
            {
                room.CreateAt = DateTime.UtcNow;
                await RoomRepository.Add(room);
            }
        }
        private void ValidateRules(Domain.Room room)
        {
            #region "Validation of property values"
            if (string.IsNullOrEmpty(room.Description))
            {
                AddNotification("Room", "Description not informed.");
                return;
            }
            #endregion
        }
    }
}
