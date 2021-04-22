using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TheLastHotel.Repository.Database.Interfaces;

namespace TheLastHotel.Service.Room.Query
{
    public class ListAllRoomsQuery : IListAllRoomsQuery
    {
        readonly IRepository<Domain.Room> RoomRepository;
        public ListAllRoomsQuery(IRepository<Domain.Room> roomRepository)
        {
            RoomRepository = roomRepository;
        }

        public async Task<IEnumerable<Domain.Room>> Execute()
        {
            return await RoomRepository.GetAll();
        }
    }
}
