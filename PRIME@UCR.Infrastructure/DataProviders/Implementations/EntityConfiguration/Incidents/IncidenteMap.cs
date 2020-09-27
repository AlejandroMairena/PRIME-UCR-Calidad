using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Infrastructure.DataProviders.Implementations.EntityConfiguration.Incidents
{
    public class IncidenteMap : IEntityTypeConfiguration<Incidente>
    {
        public void Configure(EntityTypeBuilder<Incidente> builder)
        {
            builder
                .HasOne(p => p.Origen)
                .WithOne(p => p.Incidente)
                .HasForeignKey<Incidente>(p => p.IdOrigen);
            builder
                .HasOne(p => p.Destino)
                .WithOne(p => p.Incidente)
                .HasForeignKey<Incidente>(p => p.IdDestino);
            builder
                .Property(p => p.Codigo)
                .IsRequired();
            builder
                .HasOne(p => p.Modalidad)
                .WithMany(p => p.Incidentes)
                .HasForeignKey(p => p.TipoModalidad);
            builder
                .HasOne(p => p.UnidadDeTransporte)
                .WithMany(p => p.Incidentes)
                .HasForeignKey(p => p.MatriculaTrans);
            builder.HasKey("Codigo");
        }
    }
}

