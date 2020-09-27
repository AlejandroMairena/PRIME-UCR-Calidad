using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Infrastructure.DataProviders.Implementations.EntityConfiguration.Incidents
{
    public class DomicilioUbicacionMap : IEntityTypeConfiguration<DomicilioUbicacion>
    {
        public void Configure(EntityTypeBuilder<DomicilioUbicacion> builder)
        {
            builder.ToTable("Domicilio_Ubicacion");
            builder
                .HasOne(p => p.Domicilio)
                .WithMany(p => p.UbicacionIncidentes)
                .HasForeignKey(p => p.IdDomicilio);
            builder
                .HasOne(p => p.Ubicacion)
                .WithOne(p => p.DomicilioUbicacion)
                .HasForeignKey<DomicilioUbicacion>(p => p.UbicacionId);
            builder
                .Property(p => p.Id)
                .IsRequired();
            builder
                .HasKey(c => new { c.Id, c.IdDomicilio });

        }

    }

}
