﻿using ApiNoSqlDomain.Client;
using ApiNoSqlInfraestructure.Data;
using ApiNoSqlInfraestructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ApiNoSqlInfraestructure.Services
{
    public class MyAppServiceDB
    {
        private readonly IClients _clientsRepository;
        public MyAppServiceDB(IClients clientsRepository, IConfiguration config)
        {
            var repositoryConfig = new ClientsRepositoryConfiguration();
            config.GetSection("ClientsRepository").Bind(repositoryConfig);

            if (repositoryConfig.Type == ClientsRepositoryConfiguration.RepositoryType.SqlServer)
            {
                var optionsBuilder = new DbContextOptionsBuilder<ClientsContext>();
                optionsBuilder.UseSqlServer(repositoryConfig.ConnectionString);
                var dbContextOptions = optionsBuilder.Options;

                _clientsRepository = new ClientsSqlRepository(new ClientsContext(dbContextOptions));
            }
            else if (repositoryConfig.Type == ClientsRepositoryConfiguration.RepositoryType.MongoDB)
            {
                _clientsRepository = new ClientsMongoRepository(config);
            }
            else
            {
                throw new ArgumentException($"Tipo de repositorio no válido: {repositoryConfig.Type}", nameof(config));
            }
        }
        //public MyAppServiceDB(IClients clientsRepository, ClientsRepositoryConfiguration config)
        //{
        //    if (config.Type == ClientsRepositoryConfiguration.RepositoryType.SqlServer)
        //    {
        //        var optionsBuilder = new DbContextOptionsBuilder<ClientsContext>();
        //        optionsBuilder.UseSqlServer(config.ConnectionString);
        //        var dbContextOptions = optionsBuilder.Options;

        //        _clientsRepository = new ClientsSqlRepository(new ClientsContext(dbContextOptions));

        //    }
        //    else if (config.Type == ClientsRepositoryConfiguration.RepositoryType.MongoDB)
        //    {
        //        _clientsRepository = new ClientsMongoRepository();
        //    }

        //    else
        //    {
        //        throw new ArgumentException($"Tipo de repositorio no válido: {config.Type}", nameof(config));
        //    }
        //}

        public async Task<List<ClientModels>?> GetAll()
        {
            return await _clientsRepository.GetAll();
        }

        public async Task<bool> Add(ClientModels entity)
        {
            return await _clientsRepository.Add(entity);
        }

        public async Task<ClientModels?> GetOne(string Id)
        {
            return await _clientsRepository.GetOne(Id);
        }

        public async Task<bool> Upd(ClientModels entity)
        {
            return await _clientsRepository.Upd(entity);
        }

        public async Task<bool> Del(string Id)
        {
            return await _clientsRepository.Del(Id);
        }
    }

}
