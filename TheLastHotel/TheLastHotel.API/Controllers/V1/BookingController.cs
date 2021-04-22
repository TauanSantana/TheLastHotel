using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheLastHotel.API.Models.BookingController;
using TheLastHotel.Domain;
using TheLastHotel.Service.Booking.Command;
using TheLastHotel.Service.Booking.Query;
using TheLastHotel.Service.Client.Query;

namespace TheLastHotel.API.Controllers.V1
{
    [Route("v1/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        readonly IListAllBookingsForClientQuery ListAllBookingsForClientQuery;
        readonly IFindClientByIdQuery FindClientByIdQuery;
        readonly IAddBookingCommand AddBookingCommand;
        public BookingController(IListAllBookingsForClientQuery listAllBookingsForClientQuery, IFindClientByIdQuery findClientByIdQuery, IAddBookingCommand addBookingCommand)
        {
            ListAllBookingsForClientQuery = listAllBookingsForClientQuery;
            FindClientByIdQuery = findClientByIdQuery;
            AddBookingCommand = addBookingCommand;
        }

        [HttpGet("client/{id}")]
        public async Task<IActionResult> GetAllBookingsForUser(string id)
        {
            if (await ClientValidate(id))
                return Ok(await ListAllBookingsForClientQuery.Execute(id));
            else
                return NotFound("Client not found");
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromServices]IMapper mapper, [FromBody]BookingPostModel model)
        {
            var booking = mapper.Map<Booking>(model);
            await AddBookingCommand.Execute(booking);

            if (AddBookingCommand.HasNotification)
                return BadRequest(AddBookingCommand.CommandNotifications.FirstOrDefault());
            else
                return Ok();
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        /// <summary>
        /// Deletes a specific Booking.
        /// </summary>
        /// <param name="id"></param>  
        /// <response code="200">If the Booking was deleted</response>
        /// <response code="400">If the Booking is null or not found</response>    
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public void Delete(int id)
        {
        }

        private async Task<bool> ClientValidate(string id)
        {
            if (!Guid.TryParse(id, out _))
                return false;
            else if (await FindClientByIdQuery.Execute(id) == null)
                return false;
            else
                return true;

        }
    }
}
