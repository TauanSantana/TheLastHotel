using System.Threading.Tasks;
using TheLastHotel.Service.Util;

namespace TheLastHotel.Service.Client.Command
{
    public interface IAddClientCommand : ICommandBase
    {
        Task Execute(Domain.Client room);
    }
}