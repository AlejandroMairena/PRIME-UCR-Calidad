using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Infrastructure.DataProviders.Implementations.EntityConfiguration.Incidents
{
    public class CentroUbicacionMap : IEntityTypeConfiguration<CentroUbicacion>
    {
        public void Configure(EntityTypeBuilder<CentroUbicacion> builder)
        {
            builder.ToTable("Centro_Ubicacion");
            builder
                .Property(p => p.Id)
                .IsRequired();
            builder
                .HasKey(c => new { c.Id, c.CentroMedicoId });
            builder
                .HasOne(p => p.CentroMedico)
                .WithMany(p => p.UbicacionIncidentes)
                .HasForeignKey(p => p.CentroMedicoId);
            builder
                .HasOne(p => p.Ubicacion)
                .WithOne(p => p.CentroUbicacion)
                .HasForeignKey<CentroUbicacion>(p => p.UbicacionId);

        }

    }

}
