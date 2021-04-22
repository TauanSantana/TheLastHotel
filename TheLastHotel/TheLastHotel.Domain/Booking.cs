using System;

namespace TheLastHotel.Domain
{
    public class Booking
    {
        public string Id { get; set; }
        public DateTime StartReservationDate { get; private set; }
        public DateTime EndReservationDate { get; private set; }

        public DateTime? ClientChecking { get; set; }
        public DateTime? ClientCheckout { get; set; }

        public DateTime CreateAt { get; set; }
        public DateTime? CancellationAt{ get; set; }

        public Client Client { get; set; }
        public Room Room { get; set; }
       
    }
}
