using AutoMapper;
using Flunt.Notifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
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
        readonly IUpdateBookingCommand UpdateBookingCommand;
        readonly ICancelBookingCommand CancelBookingCommand;
        readonly IMapper Mapper;
        public BookingController(IListAllBookingsForClientQuery listAllBookingsForClientQuery, IFindClientByIdQuery findClientByIdQuery, IAddBookingCommand addBookingCommand,
            IUpdateBookingCommand updateBookingCommand, ICancelBookingCommand cancelBookingCommand, IMapper mapper)
        {
            ListAllBookingsForClientQuery = listAllBookingsForClientQuery;
            FindClientByIdQuery = findClientByIdQuery;
            AddBookingCommand = addBookingCommand;
            UpdateBookingCommand = updateBookingCommand;
            CancelBookingCommand = cancelBookingCommand;
            Mapper = mapper;
        }


        /// <summary>
        /// List all bookings of client
        /// </summary>
        /// <param name="id">Id of Client</param>  
        /// <response code="200">If any Client was found</response>
        /// <response code="404">If the Client Id is null or not found</response> 
        [HttpGet("client/{id}")]
        [ProducesResponseType(typeof(IEnumerable<Booking>),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public async Task<IActionResult> GetAllBookingsForUser(string id)
        {
            if (await ClientValidate(id))
                return Ok(await ListAllBookingsForClientQuery.Execute(id));
            else
                return NotFound("Client not found");
        }

        /// <summary>
        /// Includes a Booking
        /// </summary>
        /// <response code="200">If the Booking was included </response>
        /// <response code="400">If the Booking JSON has any inconsistencies</response> 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Notification), StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<IActionResult> Post([FromBody]BookingPostModel model)
        {
            var booking = Mapper.Map<Booking>(model);
            await AddBookingCommand.Execute(booking);

            if (AddBookingCommand.HasNotification)
                return BadRequest(AddBookingCommand.CommandNotifications.FirstOrDefault());
            else
                return Ok();
        }

        /// <summary>
        /// Updates a specific Booking
        /// </summary>
        /// <param name="id">Id of Booking</param>
        /// <param name="model"></param>  
        /// <response code="200">If the Booking was updated</response>
        /// <response code="400">If the Booking is null or not found</response> 
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Notification), StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<IActionResult> Update(string id, [FromBody] BookingPutModel model)
        {
            var booking = Mapper.Map<Booking>(model);
            booking.Id = id;
            await UpdateBookingCommand.Execute(booking);

            if (UpdateBookingCommand.HasNotification)
                return BadRequest(UpdateBookingCommand.CommandNotifications.FirstOrDefault());
            else
                return Ok();
        }

        /// <summary>
        /// Cancel a specific Booking.
        /// </summary>
        /// <param name="id">Id of Booking</param>  
        /// <response code="200">If the Booking was canceled</response>
        /// <response code="400">If the Booking is null or not found</response>    
        [HttpPut("{id}/Cancel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Notification), StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<IActionResult> Cancel(string id)
        {
            await CancelBookingCommand.Execute(id);
            if (CancelBookingCommand.HasNotification)
                return BadRequest(CancelBookingCommand.CommandNotifications.FirstOrDefault());
            else
                return Ok();
        }

        private async Task<bool> ClientValidate(string id)
        {
            if (!ObjectId.TryParse(id, out _))
                return false;
            else if (await FindClientByIdQuery.Execute(id) == null)
                return false;
            else
                return true;

        }
    }
}
