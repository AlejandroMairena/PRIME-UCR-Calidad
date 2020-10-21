using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Infrastructure.EntityConfiguration.Incidents
{
    public class CentroUbicacionMap : IEntityTypeConfiguration<CentroUbicacion>
    {
        public void Configure(EntityTypeBuilder<CentroUbicacion> builder)
        {
            builder.ToTable("Centro_Ubicacion");
            // no key because it is a derived type
            builder
                .Property(p => p.Id)
                .IsRequired();
            builder
                .Property(p => p.CentroMedicoId)
                .HasColumnName("IdCentro");
            builder
                .HasOne(p => p.CentroMedico)
                .WithMany(p => p.UbicacionIncidentes)
                .HasForeignKey(p => p.CentroMedicoId);
            builder
                .Property(p => p.CedulaMedico)
                .HasColumnName("C�dulaM�dico");
            builder
                .HasOne(p => p.M�dico)
                .WithMany(p => p.CentrosMedicos)
                .HasForeignKey(p => p.CedulaMedico);
        }
    }

}
