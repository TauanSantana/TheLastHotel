using Microsoft.Extensions.DependencyInjection;
using TheLastHotel.Domain;
using TheLastHotel.Repository;
using TheLastHotel.Repository.Database;
using TheLastHotel.Repository.Database.Interfaces;
using TheLastHotel.Service.Booking.Command;
using TheLastHotel.Service.Booking.Query;
using TheLastHotel.Service.Client.Command;
using TheLastHotel.Service.Client.Query;
using TheLastHotel.Service.Room.Command;
using TheLastHotel.Service.Room.Query;

namespace TheLastHotel.API.DependencyInjection
{
    public class DIContainer
    {
        public static void RegisterRepository(IServiceCollection services)
        {
            services.AddScoped<IMongoContext, MongoContext>();

            services.AddScoped<IRepository<Booking>, BookingRepository>();
            services.AddScoped<IRepository<Room>, RoomRepository>();
            services.AddScoped<IRepository<Client>, ClientRepository>();
        }

        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IAddBookingCommand, AddBookingCommand>();
            services.AddTransient<IUpdateBookingCommand, UpdateBookingCommand>();
            services.AddTransient<ICheckIfRoomIsAvailabilityQuery, CheckIfRoomIsAvailabilityQuery>();
            services.AddTransient<IListAllBookingsForClientQuery, ListAllBookingsForClientQuery>();
            services.AddTransient<IFindBookingByIdQuery, FindBookingByIdQuery>();
            services.AddTransient<ICancelBookingCommand, CancelBookingCommand>();

            services.AddTransient<IAddClientCommand, AddClientCommand>();
            services.AddTransient<IFindClientByIdQuery, FindClientByIdQuery>();
            
            services.AddTransient<IAddRoomCommand, AddRoomCommand>();
            services.AddTransient<IListAllRoomsQuery, ListAllRoomsQuery>();
            services.AddTransient<IFindRoomByIdQuery, FindRoomByIdQuery>();
            
        }
    }
}
