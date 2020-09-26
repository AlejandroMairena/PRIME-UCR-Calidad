using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Infrastructure.DataProviders.Implementations.EntityConfiguration.Incidents
{
    public class DomicilioUbicacionMap : IEntityTypeConfiguration<DomicilioUbicacion>
    {
        public void Configure(EntityTypeBuilder<DomicilioUbicacion> builder)
        {
            builder
                .HasOne(p => p.Domicilio)
                .WithMany(p => p.DomicilioUbicaciones)
                .HasForeignKey(p => p.DomicilioId);
            builder
                .Property(p => p.Id)
                .IsRequired()
            builder.HasKey("Id");

        }

    }

}
