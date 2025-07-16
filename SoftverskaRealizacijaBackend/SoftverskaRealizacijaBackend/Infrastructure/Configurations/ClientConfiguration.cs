using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoftverskaRealizacijaBackend.Models;

namespace SoftverskaRealizacijaBackend.Infrastructure.Configurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.fullName).HasMaxLength(50);
            
            builder.HasIndex(x => x.Email).IsUnique();

        }
    }
}
