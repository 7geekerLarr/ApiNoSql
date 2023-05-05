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
        #region [GyfController]     
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
    }
}
