using ApiNoSqlDomain.Client;
using Microsoft.EntityFrameworkCore;

namespace ApiNoSqlInfraestructure.Data
{
    public class ClientsContext : DbContext
    {
        public ClientsContext(DbContextOptions<ClientsContext> options) : base(options) { }

        public DbSet<ClientModels> Clients { get; set; }
        public DbSet<PersonModels> People { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ClientModels>(entity =>
            {
                entity.ToTable("Client");
                entity.Property(e => e.level).HasColumnName("level");
                entity.Property(e => e.personid).HasColumnName("personid");
            });

            modelBuilder.Entity<PersonModels>(entity =>
            {
                entity.ToTable("Person");
                entity.HasKey(e => e.personid);
                entity.Property(e => e.name).HasColumnName("name");
                entity.Property(e => e.lastname).HasColumnName("lastname");
                entity.Property(e => e.dni).HasColumnName("dni");
                entity.Property(e => e.birthdate).HasColumnName("birthdate");
            });

            modelBuilder.Entity<ClientModels>()
                .HasOne(e => e.Person)
                .WithMany()
                .HasForeignKey(e => e.personid);
        }

    }
}
