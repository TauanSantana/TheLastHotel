using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TheLastHotel.Repository.Database.Interfaces;

namespace TheLastHotel.Service.Client.Command
{
   
    public class AddClientCommand : Notifiable<Notification>, IAddClientCommand
    {
        readonly IRepository<Domain.Client> ClientRepository;
        public bool HasNotification { get { return !this.IsValid; } }
        public IReadOnlyCollection<Notification> CommandNotifications { get { return this.Notifications; } }

        public AddClientCommand(IRepository<Domain.Client> clientRepository)
        {
            ClientRepository = clientRepository;
        }

        public async Task Execute(Domain.Client room)
        {
            ValidateRules(room);
            if (this.IsValid)
            {
                room.CreateAt = DateTime.UtcNow;
                await ClientRepository.Add(room);
            }
        }
        private void ValidateRules(Domain.Client room)
        {
            #region "Validation of property values"
            if (string.IsNullOrWhiteSpace(room.Name))
            {
                AddNotification("Client", "Name not informed.");
                return;
            }
            #endregion
        }
    }
}
