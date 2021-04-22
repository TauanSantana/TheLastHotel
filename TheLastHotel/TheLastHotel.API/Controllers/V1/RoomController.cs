using AutoMapper;
using Flunt.Notifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheLastHotel.API.Models.RoomController;
using TheLastHotel.Domain;
using TheLastHotel.Service.Booking.Query;
using TheLastHotel.Service.Room.Command;
using TheLastHotel.Service.Room.Query;

namespace TheLastHotel.API.Controllers.V1
{
    [Route("v1/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        readonly IAddRoomCommand AddRoomCommand;
        readonly ICheckIfRoomIsAvailabilityQuery CheckIfRoomIsAvailabilityQuery;
        readonly IFindRoomByIdQuery FindRoomByIdQuery;
        readonly IMapper Mapper;
        public RoomController(IAddRoomCommand addRoomCommand, IMapper mapper, ICheckIfRoomIsAvailabilityQuery checkIfRoomIsAvailabilityQuery, IFindRoomByIdQuery findRoomByIdQuery)
        {
            AddRoomCommand = addRoomCommand;
            Mapper = mapper;
            CheckIfRoomIsAvailabilityQuery = checkIfRoomIsAvailabilityQuery;
            FindRoomByIdQuery = findRoomByIdQuery;
        }

        /// <summary>
        /// Includes a Room
        /// </summary>
        /// <response code="200">If the Room was included </response>
        /// <response code="400">If the Room JSON has any inconsistencies</response> 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Notification), StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<IActionResult> Post([FromBody] RoomPostModel model)
        {
            var client = Mapper.Map<Room>(model);
            await AddRoomCommand.Execute(client);

            if (AddRoomCommand.HasNotification)
                return BadRequest(AddRoomCommand.CommandNotifications.FirstOrDefault());
            else
                return Ok();
        }

        /// <summary>
        /// Checks availability by date
        /// </summary>
        /// <param name="id">Id of Room</param>  
        /// <param name="startDate">Start reservation date of Room</param>  
        /// <param name="endDate">End reservation date of Room</param>  
        /// <response code="200">If any Room was found</response>
        /// <response code="404">If the Room Id is null or not found</response> 
        [HttpGet("{id}/Availability")]
        [ProducesResponseType(typeof(RoomAvailabilityReturnModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Notification), StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<IActionResult> ChecksAvailability(string id, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var parametersValidation = await ValidateChecksAvailabilityParameters(id);
            if (parametersValidation.Status == true) {
                var availability = await CheckIfRoomIsAvailabilityQuery.Execute(new Booking { Room = new Room { Id = id },  StartReservationDate = startDate, EndReservationDate = endDate });
                if (availability.status)
                    return Ok(new { Availability = true });
                else
                    return BadRequest(availability.notification);
            }
            
            else
                return BadRequest(parametersValidation.Notification);

        }


        private async Task<(bool Status, string Notification)> ValidateChecksAvailabilityParameters(string roomId)
        {
            if (string.IsNullOrWhiteSpace(roomId))
                return (false, "Room not informed.");

            if (!ObjectId.TryParse(roomId, out _))
                return (false, "Room Id is invalid.");

            if(await FindRoomByIdQuery.Execute(roomId) == null)
                return (false, "Room not found.");

            return (true, null);
        }       
    }
}
