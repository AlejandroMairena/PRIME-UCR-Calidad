using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Infrastructure.DataProviders.Implementations.EntityConfiguration.Incidents
{
    public class PaisMap : IEntityTypeConfiguration<Pais>
    {
        public void Configure(EntityTypeBuilder<Pais> builder)
        {
            builder
                .Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(50);
            builder.HasKey("Nombre");
        }
    }
}