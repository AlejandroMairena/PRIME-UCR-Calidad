using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Domain.Models.CheckLists;

namespace PRIME_UCR.Infrastructure.EntityConfiguration.CheckLists
{
    class InstanciaItemPadreMap : IEntityTypeConfiguration<InstanciaItemPadre>
    {
        public void Configure(EntityTypeBuilder<InstanciaItemPadre> builder)
        {
            builder.ToTable("InstanciaItemPadre");
            // no key because it is a derived type
            builder
                .HasOne<InstanciaItem>()
                .WithMany()
                .HasForeignKey(k => new { k.ItemId, k.PlantillaId, k.IncidentCod });
            builder
                .HasMany(i => i.ItemsHoja)
                .WithOne(i => i.ItemPadre);
        }
    }
}
