using ApiNoSqlApplication.HandleError;
using ApiNoSqlDomain.Client;
using ApiNoSqlInfraestructure.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static ApiNoSqlApplication.Client.Commands.CreateClientCommand;

namespace ApiNoSqlApplication.Client.CommandHandlers
{
    public class CreateClientCommandHandler
    {

        #region HandlerClass
        public class HandlerClass : IRequestHandler<CreateSystem, ClientModels>
        {
            private readonly IClients _clientsRepository;
            public HandlerClass(IClients clientsRepository)
            {
                _clientsRepository = clientsRepository;
            }
            public async Task<ClientModels> Handle(CreateSystem request, CancellationToken cancellationToken)
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
                    throw new HandleException(HttpStatusCode.BadRequest, new { GySistemas = "La estructura no es correcta!" });
                }
                if (entity.ClientId == "0")
                {
                    throw new HandleException(HttpStatusCode.BadRequest, new { GySistemas = "La estructura no es correcta!" + entity.ClientId });
                }
                if (entity.ClientId == null || entity.Name == null)
                {
                    throw new HandleException(HttpStatusCode.BadRequest, new { GySistemas = "La estructura no es correcta!" });
                }
                if (entity.ClientId == "" || entity.Name == "")
                {
                    throw new HandleException(HttpStatusCode.BadRequest, new { GySistemas = "Campos obligatotios:IdSystem y Name, La estructura no es correcta!" });
                }
                var result = await _clientsRepository.Add(entity);
                if (result)
                {
                    return entity;
                }
                else
                {
                    throw new HandleException(HttpStatusCode.NotImplemented, new { GySistemas = "Error, Sistema no ha sido creado!" });
                }

            }


        }
        #endregion
    }
}
