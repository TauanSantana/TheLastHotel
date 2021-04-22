using System.Collections.Generic;
using System.Threading.Tasks;

namespace TheLastHotel.Service.Room.Query
{
    public interface IListAllRoomsQuery
    {
        Task<IEnumerable<Domain.Room>> Execute();
    }
}