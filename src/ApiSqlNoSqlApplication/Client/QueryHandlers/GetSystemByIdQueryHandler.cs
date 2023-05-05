using ApiNoSqlApplication.Client.Queries;
using ApiNoSqlApplication.HandleError;
using ApiNoSqlDomain.Client;
using ApiNoSqlInfraestructure.Services;
using MediatR;
using System.Net;

namespace ApiNoSqlApplication.Client.QueryHandlers
{
    public class GetSystemByIdQueryHandler : IRequestHandler<GetClientByIdQuery, ClientModels>
    {
        #region GetSystemByIdQueryHandler

        private readonly IClients _clientsRepository;
        public GetSystemByIdQueryHandler(IClients clientsRepository)
        {
            _clientsRepository = clientsRepository;
        }
        public async Task<ClientModels> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.ClientId == "0")
            {
                throw new HandleException(HttpStatusCode.BadRequest, new { Cliente = "Id del cliente no es valido, no puede ser (0)" });
            }
            if (request.ClientId == null)
            {
                throw new HandleException(HttpStatusCode.BadRequest, new { Cliente = "Id del cliente no es valido, no puede ser (0)" });
            }
            var result = await _clientsRepository.GetOne(request.ClientId);
            if (result == null)
            {
                throw new HandleException(HttpStatusCode.NotFound, new { Cliente = "cliente no encontrado" });
            }
            return result;
        }
        #endregion
    }
}
