using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheLastHotel.API.Models.RoomController;
using TheLastHotel.Domain;
using TheLastHotel.Service.Room.Command;

namespace TheLastHotel.API.Controllers.V1
{
    [Route("v1/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        readonly IAddRoomCommand AddRoomCommand;
        public RoomController(IAddRoomCommand addRoomCommand)
        {
            AddRoomCommand = addRoomCommand;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromServices] IMapper mapper, [FromBody] RoomPostModel model)
        {
            var client = mapper.Map<Room>(model);
            await AddRoomCommand.Execute(client);

            if (AddRoomCommand.HasNotification)
                return BadRequest(AddRoomCommand.CommandNotifications.FirstOrDefault());
            else
                return Ok();
        }
    }
}
