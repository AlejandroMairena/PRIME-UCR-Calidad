using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Infrastructure.DataProviders.Implementations.EntityConfiguration.Multimedia
{
    public class MultimediaContentMap : IEntityTypeConfiguration<MultimediaContent>
    {

        public void Configure(EntityTypeBuilder<MultimediaContent> builder)
        {
            builder
                .HasOne(p => p.Acciones)
                .WithMany(p => p.MultimediaContents)
                .HasForeignKey(p => p.ID_accion);

            builder
                .Property(p => p.ID)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(200);

            builder
                .Property(p => p.Archivo)
                .IsRequired()
                .HasMaxLength(500);

            builder
                .Property(p => p.Descripcion)
                .HasMaxLength(2000);

            builder
                .Property(p => p.Fecha_Hora)
                .IsRequired();
                //.HasMaxLength(20);

            builder
                .Property(p => p.Tipo)
                .IsRequired()
                .HasMaxLength(20);

            builder
                .Property(p => p.ID_accion)
                .HasMaxLength(50);

            builder.HasKey("ID");
        }
    }
}
