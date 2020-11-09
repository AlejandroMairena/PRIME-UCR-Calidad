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
            builder
                .Property(i => i.ItemId)
                .HasColumnName("Id_Item")
                .IsRequired();
            builder
                .Property(i => i.PlantillaId)
                .HasColumnName("Id_Lista")
                .IsRequired();
            builder
                .Property(i => i.IncidentCod)
                .HasColumnName("Codigo_Incidente")
                .IsRequired();
            builder.HasKey(k => new { k.ItemId, k.PlantillaId, k.IncidentCod });
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
