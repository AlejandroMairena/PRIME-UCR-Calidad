using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Infrastructure.DataProviders.Implementations.EntityConfiguration.Incidents
{
    public class CentroMedicoMap : IEntityTypeConfiguration<CentroMedico>
    {
        public void Configure(EntityTypeBuilder<CentroMedico> builder)
        {
            builder
                .Property(p => p.Id)
                .IsRequired();
            builder.HasKey("Id");
            builder
                .HasOne(p => p.Distrito)
                .WithMany(p => p.CentroMedicos)
                .HasForeignKey(p => p.DistritoId);
        }

    }

}
