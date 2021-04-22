using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace TheLastHotel.Service.Util
{
    public interface ICommandBase
    {
        bool HasNotification { get; }
        IReadOnlyCollection<Notification> CommandNotifications { get; }
    }
}
