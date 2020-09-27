using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Infrastructure.DataProviders.Implementations.EntityConfiguration.Incidents
{
    public class ModalidadMap : IEntityTypeConfiguration<Modalidad>
    {
        public void Configure(EntityTypeBuilder<Modalidad> builder)
        {
            builder
                .Property(p => p.Tipo)
                .IsRequired();
            builder.HasKey("Tipo");
        }
    }
}
