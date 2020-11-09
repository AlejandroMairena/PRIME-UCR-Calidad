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
                .HasOne<Item>()
                .WithMany()
                .HasForeignKey(i => i.ItemId);
            builder
                .HasOne<CheckList>()
                .WithMany()
                .HasForeignKey(i => i.PlantillaId);
            builder
                .HasOne<Incidente>()
                .WithMany()
                .HasForeignKey(i => i.IncidentCod);
        }
    }
}
