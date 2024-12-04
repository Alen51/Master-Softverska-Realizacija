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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Kazemo mu da pronadje sve konfiguracije u Assembliju i da ih primeni nad bazom
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CRUDContext).Assembly);
        }
    }

}
