using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SoftverskaRealizacijaBackend.Models;

namespace SoftverskaRealizacijaBackend.Infrastructure.Configurations
{
    public class NodeConnectionConfiguration : IEntityTypeConfiguration<NodeConnection>
    {
        public void Configure(EntityTypeBuilder<NodeConnection> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();


        }
    }
}
