using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SoftverskaRealizacijaBackend.Models;

namespace SoftverskaRealizacijaBackend.Infrastructure.Configurations
{
    public class KvarConfiguration : IEntityTypeConfiguration<Kvar>
    {
        public void Configure(EntityTypeBuilder<Kvar> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
           

        }
    }
}
