using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Infrastructure.DataProviders.Implementations.EntityConfiguration.Incidents
{
    public class DomicilioMap : IEntityTypeConfiguration<Domicilio>
    {
        public void Configure(EntityTypeBuilder<Domicilio> builder)
        {
            builder
                .HasOne(p => p.Distrito)
                .WithMany(p => p.Domicilios)
                .HasForeignKey(p => p.DistritoId);
            builder
                .Property(p => p.Id)
                .IsRequired();
            builder.HasKey("Id");

        }

    }

}
