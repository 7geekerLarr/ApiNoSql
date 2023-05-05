using ApiNoSqlApplication.HandleError;
using ApiNoSqlDomain.Client;
using ApiNoSqlInfraestructure.Services;
using MediatR;
using System.Net;
using static ApiNoSqlApplication.Client.Commands.UpdateClientCommand;

namespace ApiNoSqlApplication.Client.CommandHandlers
{
    public class UpdateClientCommandHandler
    {
        #region HandlerClass
        public class HandlerClass : IRequestHandler<UpdateClient, ClientModels>
        {
            private readonly IClients _clientsRepository;
            public HandlerClass(IClients clientsRepository)
            {
                _clientsRepository = clientsRepository;
            }
            #region Handle           
            public async Task<ClientModels> Handle(UpdateClient request, CancellationToken cancellationToken)
            {
                ClientModels entity = new ClientModels();
                entity.ClientId = request.ClientId;
                entity.Level = request.Level;
                entity.Tipo = request.Tipo;
                entity.Name = request.Name;
                entity.Lastname = request.Lastname;
                entity.Dni = request.Dni;
                entity.Birthdate = request.Birthdate;
                entity.NroCliente = request.NroCliente;

                if (entity == null)
                {
                    throw new HandleException(HttpStatusCode.BadRequest, new { Cliente = "La estructura no es correcta!" });
                }
                if (request.ClientId == "0")
                {
                    throw new HandleException(HttpStatusCode.NotFound, new { Cliente = "Id del Cliente no es valido, no puede ser 0!" });
                }
                if (request.ClientId == null)
                {
                    throw new HandleException(HttpStatusCode.NotFound, new { Cliente = "Id del Cliente no es valido, no puede ser null!" });
                }

                var result = await _clientsRepository.Upd(entity);
                if (result)
                {
                    return entity;
                }
                else
                {
                    throw new HandleException(HttpStatusCode.NotFound, new { Cliente = "Error, Cliente no han sido encontrado, Cliente no ha sido actualizado!" });
                }
            }

            #endregion
        }
        #endregion
    }
}
