using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIME_UCR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.Infrastructure.EntityConfiguration.Multimedia
{
    public class AccionesMap : IEntityTypeConfiguration<Acciones>
    {
        void IEntityTypeConfiguration<Acciones>.Configure(EntityTypeBuilder<Acciones> builder)
        {
            builder
                .HasOne(p => p.Cita)
                .WithMany(p => p.Acciones)
                .HasForeignKey(p => p.IDCita);

            builder
                .Property(p => p.ID)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(p => p.IDCita)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(p => p.Tipo)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(p => p.Descripcion)
                .IsRequired()
                .HasMaxLength(2000);

            builder.HasKey("ID", "IDCita"); 
        }
    }
}
