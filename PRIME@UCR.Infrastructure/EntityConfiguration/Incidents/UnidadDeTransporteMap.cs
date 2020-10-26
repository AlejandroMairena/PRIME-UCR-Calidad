using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Domain.Models.Incidents;

namespace PRIME_UCR.Infrastructure.EntityConfiguration.Incidents
{
    public class UnidadDeTransporteMap : IEntityTypeConfiguration<UnidadDeTransporte>
    {
        public void Configure(EntityTypeBuilder<UnidadDeTransporte> builder)
        {
            builder.ToTable("Unidad_De_Transporte");
            builder
                .Property(p => p.Matricula)
                .IsRequired();
            builder.HasKey("Matricula");
            builder
                .HasOne<Modalidad>()
                .WithMany(p => p.Unidades)
                .HasForeignKey(p => p.Modalidad);
        }
    }
}

