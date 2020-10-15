using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Domain.Models.CheckLists;

namespace PRIME_UCR.Infrastructure.EntityConfiguration.CheckLists
{
    public class ItemMap : IEntityTypeConfiguration<Item>
    {
        // User Story PIG01IIC20-122 LG - Agregar imagen descriptiva a lista de chequeo
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.ToTable("Item");
            builder.HasKey("Id");
            // Sets NombreImagen to "defaultCheckList.png" as default value
            builder
                .Property(p => p.NombreImagen)
                .HasDefaultValue("defaultCheckList.png");
        }
    }
}
