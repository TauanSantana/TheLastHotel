using System;

namespace TheLastHotel.Domain
{
    public class Booking
    {
        public DateTime ReservationDate { get; private set; }
        public Client Client { get; private set; }
        public Room Room { get; set; }
        public Booking(DateTime reservationDate, Client client, Room room)
        {
            ReservationDate = reservationDate;
            Client = client;
            Room = room;
        }
    }
}
