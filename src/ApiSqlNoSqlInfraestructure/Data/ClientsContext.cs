using ApiNoSqlDomain.Client;
using Microsoft.EntityFrameworkCore;

namespace ApiNoSqlInfraestructure.Data
{
    public class ClientsContext : DbContext
    {
        public ClientsContext(DbContextOptions<ClientsContext> options) : base(options) { }

        public DbSet<ClientModels> Clients { get; set; }
        public DbSet<PersonModels> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ClientModels>().HasKey(c => c.ClientId);
            modelBuilder.Entity<PersonModels>().HasKey(p => p.ClientId);

            //modelBuilder.Entity<PersonModels>()
                //.HasOne(p => p.Client)
                //.WithOne(c => c.Person)
                //.HasForeignKey<PersonModels>(p => p.ClientId)
                //.IsRequired();

            modelBuilder.Entity<PersonModels>()
                .Property(p => p.Name)
                .HasMaxLength(100);

            modelBuilder.Entity<PersonModels>()
                .Property(p => p.Lastname)
                .HasMaxLength(100);

            modelBuilder.Entity<PersonModels>()
                .Property(p => p.Dni)
                .HasMaxLength(20);

            modelBuilder.Entity<PersonModels>()
                .Property(p => p.Birthdate)
                .HasColumnName("Birthdate");

        }

        public void EnsureClientTableCreated()
        {
            Database.EnsureCreated();
        }
    }
}
