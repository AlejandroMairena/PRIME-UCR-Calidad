using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Domain.Models.CheckLists;

namespace PRIME_UCR.Infrastructure.EntityConfiguration.CheckLists
{
    class InstanciaItemMap : IEntityTypeConfiguration<InstanciaItem>
    {
        public void Configure(EntityTypeBuilder<InstanciaItem> builder)
        {
            builder.ToTable("InstanciaItem");
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
                .Property(i => i.ItemPadreId)
                .HasColumnName("Id_Item_Padre");
            builder
                .Property(i => i.PlantillaPadreId)
                .HasColumnName("Id_Lista_Padre");
            builder
                .Property(i => i.IncidentCodPadre)
                .HasColumnName("Codigo_Incidente_Padre");
            builder
                .HasOne(i => i.MyItem)
                .WithMany(I => I.Instances)
                .HasForeignKey(i => i.ItemId);
            builder
                .HasOne(i => i.MyFather)
                .WithMany(i => i.SubItems)
                .HasForeignKey(k => new { k.ItemPadreId, k.PlantillaPadreId, k.IncidentCodPadre });
        }
    }
}
