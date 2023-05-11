using ApiNoSqlDomain.Client;
using ApiNoSqlInfraestructure.Data;
using ApiNoSqlInfraestructure.Entitys;
using Microsoft.EntityFrameworkCore;
using IClients = ApiNoSqlInfraestructure.Services.IClients;

namespace ApiNoSqlInfraestructure.Repository
{
    public class ClientsSqlRepository:IClients
    {
        #region Var
        private readonly ClientsContext context;
        #endregion
        #region ClientsSqlRepository
        public ClientsSqlRepository(ClientsContext _context)
        {
            this.context = _context;
        }
        #endregion
        #region GetAll
        public async Task<List<ClientModels>?> GetAll()
        {
            return await context.Clients.ToListAsync();            
        }
        #endregion
        #region Add
        public async Task<bool> Add(ClientModels entity)
        {
            try
            {
                var client = new ClientModels
                {
                    ClientId = entity.ClientId,
                    Level = entity.Level,
                    Tipo = entity.Tipo,
                    Person = new PersonModels
                    {
                        Name = entity.Person?.Name,
                        Lastname = entity.Person?.Lastname,
                        Dni = entity.Person?.Dni,
                        ClientId = entity.ClientId,
                        Birthdate = entity.Person?.Birthdate ?? DateTime.MinValue
                    }
                };

                await context.AddAsync(client);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw new Exception("No se pudo insertar el Cliente.");
            }
        }

        #endregion
        #region GetOne
        public async Task<ClientModels?> GetOne(string Id)
        {
            try
            {
                var client = await context.Clients.FirstOrDefaultAsync(c => c.ClientId == Id);
                return client;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }
        #endregion
        #region Upd
        public async Task<bool> Upd(ClientModels entity)
        {
            try
            {
                var client = await context.Clients.FindAsync(entity.ClientId);
                if (client == null)
                {
                    return false;
                }

                client.Level = entity.Level;
                client.Tipo = entity.Tipo;
                if (client.Person != null)
                {
                    client.Person.Name = entity.Person?.Name;
                    client.Person.Lastname = entity.Person?.Lastname;
                    client.Person.Dni = entity.Person?.Dni;
                    client.Person.ClientId = entity.Person?.ClientId;
                    client.Person.Birthdate = entity.Person?.Birthdate ?? DateTime.MinValue;
                }

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        #endregion
        #region Del
        public async Task<bool> Del(string Id)
        {
            try
            {
                var client = await context.Clients.FirstAsync(c => c.ClientId == Id, CancellationToken.None);
                if (client == null)
                {
                    return false;
                }
                context.Remove(client);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        #endregion

    }
}
