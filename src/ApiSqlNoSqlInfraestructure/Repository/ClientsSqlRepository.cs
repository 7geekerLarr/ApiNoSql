using ApiNoSqlDomain.Client;
using ApiNoSqlInfraestructure.Data;
using Microsoft.EntityFrameworkCore;
using IClients = ApiNoSqlInfraestructure.Services.IClients;
using System.Xml;
using Newtonsoft.Json;

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
            var clients = await context.Clients.Include(c => c.Person).ToListAsync();
            var json = JsonConvert.SerializeObject(clients, Newtonsoft.Json.Formatting.None, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore 
            });
            return JsonConvert.DeserializeObject<List<ClientModels>>(json);
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
                var client = await context.Clients.Include(c => c.Person).FirstOrDefaultAsync(c => c.ClientId == Id);
                var json = JsonConvert.SerializeObject(client, Newtonsoft.Json.Formatting.None, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore // Ignora las referencias en bucle
                });
                return JsonConvert.DeserializeObject<ClientModels>(json);
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
                var client = await context.Clients.Include(c => c.Person).FirstOrDefaultAsync(c => c.ClientId == entity.ClientId);
                if (client == null)
                {
                    return false;
                }

                client.Level = entity.Level;
                client.Tipo = entity.Tipo;

                if (client.Person == null)
                {
                    client.Person = new PersonModels();
                }

                if (entity.Person != null)
                {
                    // Actualizar propiedades de Person solo si entity.Person no es nulo
                    client.Person.Name = entity.Person.Name;
                    client.Person.Lastname = entity.Person.Lastname;
                    client.Person.Dni = entity.Person.Dni;
                    client.Person.Birthdate = entity.Person.Birthdate ?? DateTime.MinValue;
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
                var client = await context.Clients.Include(c => c.Person).FirstOrDefaultAsync(c => c.ClientId == Id, CancellationToken.None);
                if (client == null)
                {
                    return false;
                }

                if (client.Person != null)
                {
                    context.Remove(client.Person); // Eliminar la entidad Person asociada al cliente
                }

                context.Remove(client); // Eliminar el cliente

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
