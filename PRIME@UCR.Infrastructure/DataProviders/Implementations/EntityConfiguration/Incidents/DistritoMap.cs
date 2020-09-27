using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Infrastructure.DataProviders.Implementations.EntityConfiguration.Incidents
{
    public class DistritoMap : IEntityTypeConfiguration<Distrito>
    {
        public void Configure(EntityTypeBuilder<Distrito> builder)
        {
            builder
                .HasOne(p => p.Canton)
                .WithMany(p => p.Distritos)
                .HasForeignKey(p => p.CantonId);
            builder
                .Property(p => p.Id)
                .IsRequired();
            builder.HasKey("Id");

        }

    }

}
