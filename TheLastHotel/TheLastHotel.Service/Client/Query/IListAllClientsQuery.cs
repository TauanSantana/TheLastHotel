using System.Collections.Generic;
using System.Threading.Tasks;

namespace TheLastHotel.Service.Client.Query
{
    public interface IListAllClientsQuery
    {
        Task<IEnumerable<Domain.Client>> Execute();
    }
}