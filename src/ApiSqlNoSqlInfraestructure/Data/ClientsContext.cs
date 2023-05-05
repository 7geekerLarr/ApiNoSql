using ApiNoSqlDomain.Client;
using Microsoft.EntityFrameworkCore;

namespace ApiNoSqlInfraestructure.Data
{
    public class ClientsContext : DbContext
    {
        public ClientsContext(DbContextOptions<ClientsContext> options) : base(options) { }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ClientModels>().HasKey(cm => new { cm.Name });            
        }
        public DbSet<ClientModels> Client { get; set; }
       // public DbSet<PersonModels> People { get; set; }
    }
}
