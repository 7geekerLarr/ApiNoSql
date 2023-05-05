using ApiNoSqlApplication.Client.Commands;
using ApiNoSqlApplication.HandleError;
using ApiNoSqlInfraestructure.Services;
using MediatR;
using System.Net;
using static ApiNoSqlApplication.Client.Commands.DeleteClientCommand;

namespace ApiNoSqlApplication.Client.CommandHandlers
{
    public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand>
    {
        #region DeleteClientCommandHandler

        private readonly IClients _clientsRepository;
            public DeleteClientCommandHandler(IClients clientsRepository)
            {
                _clientsRepository = clientsRepository;
            }
            public async Task<Unit> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
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
                throw new Exception("Error, Cliente no ha sido borrado  (Id: " + request.ClientId + ")" );               

            }


         
        #endregion
    }
}
