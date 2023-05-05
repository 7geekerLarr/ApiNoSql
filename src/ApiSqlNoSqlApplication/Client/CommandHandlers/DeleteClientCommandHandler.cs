using ApiNoSqlApplication.HandleError;
using ApiNoSqlInfraestructure.Services;
using MediatR;
using System.Net;
using static ApiNoSqlApplication.Client.Commands.DeleteClientCommand;

namespace ApiNoSqlApplication.Client.CommandHandlers
{
    public class DeleteClientCommandHandler
    {
        #region HandlerClass
        public class HandlerClass : IRequestHandler<DeleteClient>
        {
            private readonly IClients _clientsRepository;
            public HandlerClass(IClients clientsRepository)
            {
                _clientsRepository = clientsRepository;
            }
            public async Task<Unit> Handle(DeleteClient request, CancellationToken cancellationToken)
            {
                if (request.ClientId == "0")
                {
                    throw new HandleException(HttpStatusCode.NotFound, new { Cliente = "Id del cliente no es valido, no puede ser 0!" });
                }
                if (request.ClientId == null)
                {
                    throw new HandleException(HttpStatusCode.NotFound, new { Cliente = "Id del cliente no es valido, no puede ser null!" });
                }
                var resul = await _clientsRepository.Del(request.ClientId);

                if (resul)
                {
                    return Unit.Value;
                }
                else
                {
                    throw new HandleException(HttpStatusCode.NotImplemented, new { Cliente = "Error, Cliente no ha sido borrado  (Id: " + request.ClientId + ")" });
                }

            }


        }
        #endregion
    }
}
