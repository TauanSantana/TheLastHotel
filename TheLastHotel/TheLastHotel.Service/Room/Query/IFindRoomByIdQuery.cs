using System;
using System.Threading.Tasks;

namespace TheLastHotel.Service.Room.Query
{
    public interface IFindRoomByIdQuery
    {
        Task<Domain.Room> Execute(string id);
    }
}