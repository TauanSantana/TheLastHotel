using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TheLastHotel.Repository.Database.Interfaces;

namespace TheLastHotel.Service.Client.Query
{
    public class FindClientByIdQuery : IFindClientByIdQuery
    {
        readonly IRepository<Domain.Client> ClientRepository;
        public FindClientByIdQuery(IRepository<Domain.Client> clientRepository)
        {
            ClientRepository = clientRepository;
        }

        public async Task<Domain.Client> Execute(string id)
        {
            return await ClientRepository.GetById(id);
        }
    }
}
