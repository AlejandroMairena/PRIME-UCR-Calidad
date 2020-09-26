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
                .HasForeignKey(p => p.NombrePais)
            builder
                .Property(p => p.Id)
                .IsRequired()
            builder
                .HasKey(c => new { c.Id, c.NombrePais });

        }

    }

}
