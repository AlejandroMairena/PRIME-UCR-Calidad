using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Domain.Models.Incidents;

namespace PRIME_UCR.Infrastructure.DataProviders.Implementations.EntityConfiguration.Incidents
{
    public class UnidadDeTransporteMap : IEntityTypeConfiguration<UnidadDeTransporte>
    {
        public void Configure(EntityTypeBuilder<UnidadDeTransporte> builder)
        {
            builder
                .Property(p => p.Matricula)
                .IsRequired();
            builder.HasKey("Matricula");
            builder
                .HasOne(p => p.ModalidadTrasporte)
                .WithMany(p => p.Unidades)
                .HasForeignKey(p => p.Modalidad);
        }
    }
}

