using AutoMapper;
using TheLastHotel.API.Models.BookingController;
using TheLastHotel.API.Models.ClientController;
using TheLastHotel.API.Models.RoomController;
using TheLastHotel.Domain;

namespace TheLastHotel.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BookingPostModel, Booking>()
                .ForPath(dest => dest.Client.Id, source => source.MapFrom(source => source.ClientId))
                .ForPath(dest => dest.Room.Id, source => source.MapFrom(source => source.RoomId));

            CreateMap<BookingPutModel, Booking>();

            CreateMap<ClientPostModel, Client>();

            CreateMap<RoomPostModel, Room>();
        }
    }
}
