using System.Threading.Tasks;
using TheLastHotel.Service.Util;

namespace TheLastHotel.Service.Room.Command
{
    public interface IAddRoomCommand : ICommandBase
    {
        Task Execute(Domain.Room room);
    }
}