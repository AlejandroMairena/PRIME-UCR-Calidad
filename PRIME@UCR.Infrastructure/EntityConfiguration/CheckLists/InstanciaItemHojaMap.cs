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
                .HasOne(i => i.ItemPadre)
                .WithMany(i => i.ItemsHoja)
                .HasForeignKey(k => new { k.ItemIdPadre, k.PlantillaIdPadre, k.IncidentCodPadre });
        }
    }
}
