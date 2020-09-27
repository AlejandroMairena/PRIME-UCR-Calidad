using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIME_UCR.Domain.Models.Incidents;

namespace PRIME_UCR.Infrastructure.DataProviders.Implementations.EntityConfiguration.Incidents
{
    public class EstadoIncidenteMap : IEntityTypeConfiguration<EstadoIncidente>
    {
        public void Configure(EntityTypeBuilder<EstadoIncidente> builder)
        {
            builder.ToTable("EstadoIncidente");
            
            builder.HasKey(e => new {e.CodigoIncidente, e.NombreEstado});

            builder
                .HasOne(e => e.Estado)
                .WithMany(e => e.EstadoIncidentes)
                .HasForeignKey(e => e.NombreEstado);

            builder
                .HasOne(e => e.Incidente)
                .WithMany(i => i.EstadoIncidentes)
                .HasForeignKey(e => e.CodigoIncidente);
        }
    }
}