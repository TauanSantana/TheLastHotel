using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TheLastHotel.Repository.Database.Interfaces;

namespace TheLastHotel.Service.Room.Query
{
    public class FindRoomByIdQuery : IFindRoomByIdQuery
    {
        readonly IRepository<Domain.Room> RoomRepository;
        public FindRoomByIdQuery(IRepository<Domain.Room> roomRepository)
        {
            RoomRepository = roomRepository;
        }

        public async Task<Domain.Room> Execute(string id)
        {
            return await RoomRepository.GetById(id);
        }
    }
}
