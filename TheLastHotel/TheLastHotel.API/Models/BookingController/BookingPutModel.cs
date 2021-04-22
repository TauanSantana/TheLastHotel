using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheLastHotel.API.Models.BookingController
{
    public class BookingPutModel
    {
        [Required(ErrorMessage = "The StartReservationDate field is required.")]
        public DateTime StartReservationDate { get; set; }

        [Required(ErrorMessage = "The EndReservationDate field is required.")]
        public DateTime EndReservationDate { get; set; }
    }
}
