using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Infrastructure.DataProviders.Implementations.EntityConfiguration.Incidents
{
    public class ProvinciaMap : IEntityTypeConfiguration<Provincia>
    {
        public void Configure(EntityTypeBuilder<Provincia> builder)
        {
            builder.ToTable("Provincia");
            builder
                .HasOne(p => p.Pais)
                .WithMany(p => p.Provincias)
                .HasForeignKey(p => p.NombrePais);
            builder
                .Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(50);
            builder.HasKey("Nombre");
        }
    }
}