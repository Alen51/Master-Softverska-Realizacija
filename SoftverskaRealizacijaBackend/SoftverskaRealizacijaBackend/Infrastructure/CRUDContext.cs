using Microsoft.EntityFrameworkCore;
using SoftverskaRealizacijaBackend.Models;

namespace SoftverskaRealizacijaBackend.Infrastructure
{
    public class CRUDContext:DbContext
    {

        public DbSet<Client> Clienti { get; set; }
        public DbSet<Node> Nodes { get; set; }
        public DbSet<Kvar> Kvraovi { get; set; }
        public DbSet<NodeConnection> NodeConnections { get; set; }

        public CRUDContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Kazemo mu da pronadje sve konfiguracije u Assembliju i da ih primeni nad bazom
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CRUDContext).Assembly);

            modelBuilder.Entity<Node>().HasData(
                new Node
                {
                    Id = 1,
                    Latitude = 45.2396,
                    Longitude = 19.8227,
                },
                new Node
                {
                    Id = 2,
                    Latitude = 45.2396,
                    Longitude = 19.8297,
                }
            );

            modelBuilder.Entity<NodeConnection>().HasData(
                new NodeConnection
                {
                    Id = 1,
                    StartPinId = 1,
                    EndPinId = 2,
                }
            );
        }
    }

}
