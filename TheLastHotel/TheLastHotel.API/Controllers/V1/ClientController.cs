using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheLastHotel.API.Models.ClientController;
using TheLastHotel.Domain;
using TheLastHotel.Service.Client.Command;

namespace TheLastHotel.API.Controllers.V1
{
    [Route("v1/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        readonly IAddClientCommand AddClientCommand;
        public ClientController(IAddClientCommand addClientCommand)
        {
            AddClientCommand = addClientCommand;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromServices] IMapper mapper, [FromBody] ClientPostModel model)
        {
            var client = mapper.Map<Client>(model);
            await AddClientCommand.Execute(client);

            if (AddClientCommand.HasNotification)
                return BadRequest(AddClientCommand.CommandNotifications.FirstOrDefault());
            else
                return Ok();
        }
    }
}
