using ApiNoSqlApplication.Client.Commands;
using ApiNoSqlApplication.Client.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiNoSqlApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        #region IDependencies
        private readonly IMediator _mediator;
        private readonly ILogger<ClientsController> _logger;
        #endregion
        #region [ClientsController]     
        public ClientsController(IMediator mediator, ILogger<ClientsController> logger)
        {
            _logger = logger;
            _mediator = mediator;
        }
        #endregion
        #region [Get] 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {

            var resultado = await _mediator.Send(new GetAllClientsQuery.GetAllClients());
            return Ok(resultado);
        }
        #endregion
        #region [Add]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add(CreateClientCommand.CreateSystem data)
        {
            var registerToReturn = await _mediator.Send(data);
            //return Ok(registerToReturn);
            return CreatedAtRoute("GetOne", new { id = registerToReturn.ClientId }, registerToReturn);
        }
        #endregion
        #region [GetOne] 
        [HttpGet("{id:int}", Name = "GetOne")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> GetOne(int id)
        {
            var resultado = await _mediator.Send(new GetClientByIdQuery.GetSystemById { ClientId = id.ToString() });
            return Ok(resultado);
        }
        #endregion
        #region [Del]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Del(int id)
        {

            var resultado = await _mediator.Send(new DeleteClientCommand.DeleteClient { ClientId = id.ToString() });
            return NoContent();
        }
        #endregion
        #region [Upd]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Upd([FromBody] UpdateClientCommand.UpdateClient data)
        {
            await _mediator.Send(data);
            return NoContent();
        }
        #endregion        
    }
}
