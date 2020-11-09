using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Domain.Models.CheckLists;

namespace PRIME_UCR.Infrastructure.EntityConfiguration.CheckLists
{
    class InstanciaItemHojaMap : IEntityTypeConfiguration<InstanciaItemHoja>
    {
        public void Configure(EntityTypeBuilder<InstanciaItemHoja> builder)
        {
            builder.ToTable("InstanciaItemHoja");
            // no key because it is a derived type
            builder
                .HasOne<InstanciaItem>()
                .WithMany()
                .HasForeignKey(k => new { k.ItemId, k.PlantillaId, k.IncidentCod });
            builder
                .HasOne(i => i.ItemPadre)
                .WithMany(i => i.ItemsHoja)
                .HasForeignKey(k => new { k.ItemIdPadre, k.PlantillaIdPadre, k.IncidentCodPadre });
        }
    }
}
