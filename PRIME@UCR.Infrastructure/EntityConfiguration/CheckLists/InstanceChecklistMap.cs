using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Domain.Models.CheckLists;

namespace PRIME_UCR.Infrastructure.EntityConfiguration.CheckLists
{
    public class InstanceChecklistMap : IEntityTypeConfiguration<InstanceChecklist>
    {
        // User Story PIG01IIC20-127 LG-Asignat Lista de chekeo a insidente
        public void Configure(EntityTypeBuilder<InstanceChecklist> builder)
        {
            builder.ToTable("InstanceChecklist");
            builder.HasKey("InstanciadoId");
            //builder
            //   .HasMany(p => p.Items)
            //  .WithOne(p => p.InstanceChecklist);
            builder
                .HasKey("IncidentCod");
            builder
                .HasKey("PlantillaId");
        }
    }
}