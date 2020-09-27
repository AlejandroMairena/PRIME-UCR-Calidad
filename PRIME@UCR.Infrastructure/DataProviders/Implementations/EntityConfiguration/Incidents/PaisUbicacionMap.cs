using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Infrastructure.DataProviders.Implementations.EntityConfiguration.Incidents
{
    public class PaisUbicacionMap : IEntityTypeConfiguration<PaisUbicacion>
    {
        public void Configure(EntityTypeBuilder<PaisUbicacion> builder)
        {
            builder
                .HasOne(p => p.Pais)
                .WithMany(p => p.PaisUbicaciones)
                .HasForeignKey(p => p.NombrePais);
            builder
                .HasOne(p => p.Ubicacion)
                .WithOne(p => p.PaisUbicacion)
                .HasForeignKey<PaisUbicacion>(p => p.UbicacionId);
            builder
                .Property(p => p.Id)
                .IsRequired();
            builder
                .HasKey(c => new { c.Id, c.NombrePais });
        }

    }

}
