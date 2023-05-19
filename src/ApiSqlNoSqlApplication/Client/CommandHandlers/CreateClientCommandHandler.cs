﻿using ApiNoSqlApplication.Client.Commands;
using ApiNoSqlApplication.HandleError;
using ApiNoSqlApplication.Helper;
using ApiNoSqlDomain.Client;
using ApiNoSqlInfraestructure.Services;
using MediatR;
using System.Net;
using IClients = ApiNoSqlInfraestructure.Services.IClients;

namespace ApiNoSqlApplication.Client.CommandHandlers
{
    public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, ClientModels>
    {

        #region CreateClientCommandHandler

        private readonly IClients _clientsRepository;
        public CreateClientCommandHandler(IClients clientsRepository)
        {
            _clientsRepository = clientsRepository;
        }
        public async Task<ClientModels> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            #region entity           
            int levelValue = (int)request.Level; // El valor numérico del nivel
            HelperLevel helperLevel = new HelperLevel(levelValue);
            LevelClient levelTipo = helperLevel.GetLevelTipo();
                        
            var entity = new ClientModels()
            {
                

                ClientId = request.ClientId,
                Level = levelTipo,
                Tipo = request.Tipo,
                Person = new PersonModels
                {
                    Name = request.Name,
                    Lastname = request.Lastname,
                    Dni = request.Dni,
                    ClientId = request.ClientId,
                    Birthdate = request.Birthdate
                }
            };
            #endregion
            #region Validaciones
            if (entity == null)
            {
                throw new HandleException(HttpStatusCode.BadRequest, new { Cliente = "La estructura no es correcta!" });
            }
            if (Convert.ToInt32(entity.ClientId) == 0)
            {
                throw new HandleException(HttpStatusCode.BadRequest, new { Cliente = "La estructura no es correcta!" + entity.ClientId });
            }
            if (entity.ClientId == null || entity.Person.Name == null)
            {
                throw new HandleException(HttpStatusCode.BadRequest, new { Cliente = "La estructura no es correcta!" });
            }
            if (entity.ClientId == "" || entity.Person.Name == "")
            {
                throw new HandleException(HttpStatusCode.BadRequest, new { Cliente = "Campos obligatotios:IdSystem y Name, La estructura no es correcta!" });
            }
            var result = await _clientsRepository.Add(entity);
            if (result)
            {
                
                return entity;
            }
            else
            {
                throw new HandleException(HttpStatusCode.NotImplemented, new { Cliente = "Error, Sistema no ha sido creado!" });
            }
            #endregion
        }
        #endregion
    }
}
