using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIME_UCR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.Infrastructure.EntityConfiguration.Multimedia
{
    public class MetricasMap : IEntityTypeConfiguration<Metricas>
    {
        public void Configure(EntityTypeBuilder<Metricas> builder)
        {
            builder
                .HasOne(p => p.Cita)
                .WithMany(p => p.Metricas)
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
                .Property(p => p.Presion)
                .IsRequired();

            builder
                .Property(p => p.Peso)
                .IsRequired();

            builder
                .Property(p => p.Altura)
                .IsRequired();

            builder
                .Property(p => p.Temperatura)
                .IsRequired();

            builder.HasKey("ID", "IDCita");

        }
    }
}
