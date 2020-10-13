using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Infrastructure.EntityConfiguration.CheckList
{
    public class CheckListMap : IEntityTypeConfiguration<PRIME_UCR.Domain.Models.CheckList>
    {
        // User Story PIG01IIC20-267 LG - Agregar imagen descriptiva a lista de chequeo
        public void Configure(EntityTypeBuilder<PRIME_UCR.Domain.Models.CheckList> builder)
        {
            builder.ToTable("CheckList");
            builder.HasKey("Id");
            // Sets NombreImagen to "defaultCheckList.png" as default value
            builder
                .Property(p => p.NombreImagen)
                .HasDefaultValue("defaultCheckList.png");
        }
    }
}