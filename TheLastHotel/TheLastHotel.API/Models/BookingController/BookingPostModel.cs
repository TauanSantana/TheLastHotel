using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheLastHotel.API.Models.BookingController
{
    public class BookingPostModel
    {
        [Required(ErrorMessage = "The StartReservationDate field is required.")]
        public DateTime StartReservationDate { get; set; }

        [Required(ErrorMessage = "The EndReservationDate field is required.")]
        public DateTime EndReservationDate { get; set; }

        [Required(ErrorMessage = "The ClientId field is required.")]
        public string ClientId { get; set; }

        [Required(ErrorMessage = "The RoomId field is required.")]
        public string RoomId { get; set; }
    }
}
