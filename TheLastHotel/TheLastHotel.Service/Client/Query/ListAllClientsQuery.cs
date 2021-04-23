using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TheLastHotel.Repository.Database.Interfaces;

namespace TheLastHotel.Service.Client.Query
{
    public class ListAllClientsQuery : IListAllClientsQuery
    {
        readonly IRepository<Domain.Client> ClientRepository;
        public ListAllClientsQuery(IRepository<Domain.Client> clientRepository)
        {
            ClientRepository = clientRepository;
        }

        public async Task<IEnumerable<Domain.Client>> Execute()
        {
            return await ClientRepository.GetAll();
        }
    }
}
