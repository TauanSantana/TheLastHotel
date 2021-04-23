using AutoMapper;
using Flunt.Notifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheLastHotel.API.Models.ClientController;
using TheLastHotel.Domain;
using TheLastHotel.Service.Client.Command;
using TheLastHotel.Service.Client.Query;

namespace TheLastHotel.API.Controllers.V1
{
    [Route("v1/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        readonly IAddClientCommand AddClientCommand;
        readonly IListAllClientsQuery ListAllClientsQuery;
        readonly IMapper Mapper;
        public ClientController(IAddClientCommand addClientCommand, IMapper mapper, IListAllClientsQuery listAllClientsQuery)
        {
            AddClientCommand = addClientCommand;
            Mapper = mapper;
            ListAllClientsQuery = listAllClientsQuery;
        }

        /// <summary>
        /// Includes a Client
        /// </summary>
        /// <response code="200">If the Client was included </response>
        /// <response code="400">If the Client JSON has any inconsistencies</response> 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Notification), StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<IActionResult> Post([FromBody] ClientPostModel model)
        {
            var client = Mapper.Map<Client>(model);
            await AddClientCommand.Execute(client);

            if (AddClientCommand.HasNotification)
                return BadRequest(AddClientCommand.CommandNotifications.FirstOrDefault());
            else
                return Ok();
        }

        /// <summary>
        /// Get all Clients
        /// </summary>
        /// <response code="200"></response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Client>), StatusCodes.Status200OK)]
        [Produces("application/json")]
        public async Task<IActionResult> Get()
        {
            return Ok(await ListAllClientsQuery.Execute());
        }
    }
}
