using System;
using System.Threading.Tasks;

namespace TheLastHotel.Service.Client.Query
{
    public interface IFindClientByIdQuery
    {
        Task<Domain.Client> Execute(string id);
    }
}