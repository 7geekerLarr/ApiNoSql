using ApiNoSqlDomain.Client;
using Microsoft.EntityFrameworkCore;

namespace ApiNoSqlInfraestructure.Data
{
    public class ClientsContext : DbContext
    {
        public ClientsContext(DbContextOptions<ClientsContext> options) : base(options) { }

        public DbSet<ClientModels> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ClientModels>().HasKey(cm => cm.ClientId);
        }

        public void EnsureClientTableCreated()
        {
            Database.EnsureCreated();
        }
    }
}
