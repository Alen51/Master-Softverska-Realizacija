using Microsoft.EntityFrameworkCore;
using SoftverskaRealizacijaBackend.Helpers;
using SoftverskaRealizacijaBackend.Models;

namespace SoftverskaRealizacijaBackend.Infrastructure
{
    public class CRUDContext:DbContext
    {

        public DbSet<Client> Clienti { get; set; }
        public DbSet<Node> Nodes { get; set; }
        public DbSet<Kvar> Kvarovi { get; set; }
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

            modelBuilder.Entity<Client>().HasData(
                new Client
                {
                    Id=1,
                    Email="admin1@gmail.com",
                    fullName="Admin1",
                    TipKorisnika = Enumerations.TipKorisnika.Administrator,
                    Password = ClientHelper.HashPassword("111"),

                }
                
            );

            modelBuilder.Entity<Client>().HasData(
                new Client
                {
                    Id = 2,
                    Email = "gost1@gmail.com",
                    fullName = "Gost1",
                    TipKorisnika = Enumerations.TipKorisnika.Gost,
                    Password = ClientHelper.HashPassword("111"),

                }

            );

            modelBuilder.Entity<Client>().HasData(
                new Client
                {
                    Id = 3,
                    Email = "kupac1@gmail.com",
                    fullName = "Kupac1",
                    TipKorisnika = Enumerations.TipKorisnika.Kupac,
                    Password = ClientHelper.HashPassword("111"),

                }

            );

            modelBuilder.Entity<Client>().HasData(
                new Client
                {
                    Id = 4,
                    Email = "kupac2@gmail.com",
                    fullName = "Kupac2",
                    TipKorisnika = Enumerations.TipKorisnika.Kupac,
                    Password = ClientHelper.HashPassword("111"),

                }

            );
        }
    }

}
