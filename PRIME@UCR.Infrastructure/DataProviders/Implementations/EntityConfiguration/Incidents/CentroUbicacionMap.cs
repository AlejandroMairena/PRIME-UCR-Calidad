using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Infrastructure.DataProviders.Implementations.EntityConfiguration.Incidents
{
    public class CentroUbicacionMap : IEntityTypeConfiguration<CentroUbicacion>
    {
        public void Configure(EntityTypeBuilder<CentroUbicacion> builder)
        {
            builder
                .Property(p => p.Id)
                .IsRequired();
            builder
                .HasKey(c => new { c.Id, c.CentroMedicoId });
            builder
                .HasOne(p => p.CentroMedico)
                .WithMany(p => p.CentroMedicoUbicaciones)
                .HasForeignKey(p => p.CentroMedicoId);

        }

    }

}
