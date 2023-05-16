using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Cosmos;
using ApiNoSqlDomain.Client;



namespace ApiNoSqlInfraestructure.Data
{
    public class ClientsCosmosDBContext : DbContext
    {
        public DbSet<ClientModels> TuEntidadSet { get; set; }

        public ClientsCosmosDBContext(DbContextOptions<ClientsCosmosDBContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseCosmos(
                "endpoint",     // URL de conexión a tu cuenta de Cosmos DB
                "authKey",      // Clave de autenticación
                "databaseName"  // Nombre de la base de datos
            );

            base.OnConfiguring(optionsBuilder);
        }
    }
}
