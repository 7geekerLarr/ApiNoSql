using ApiNoSqlApplication.Client.Commands;
using ApiNoSqlApplication.HandleError;
using ApiNoSqlDomain.Client;
using MediatR;
using System.Net;
using IClients = ApiNoSqlInfraestructure.Services.IClients;

namespace ApiNoSqlApplication.Client.CommandHandlers
{
    public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, ClientModels>
    {
        #region UpdateClientCommandHandler

        private readonly IClients _clientsRepository;
        public UpdateClientCommandHandler(IClients clientsRepository)
        {
            _clientsRepository = clientsRepository;
        }
        #region Handle           
        public async Task<ClientModels> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            #region entity           
            var entity = new ClientModels()
            {
                ClientId = request.ClientId,
                Level = request.Level,
                Tipo = request.Tipo,
                Name = request.Name,
                Lastname = request.Lastname,
                Dni = request.Dni,
                Birthdate = request.Birthdate,
                NroCliente = request.NroCliente
            };
            #endregion
            #region Validations            
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
            #endregion
        }

        #endregion

        #endregion
    }
}
