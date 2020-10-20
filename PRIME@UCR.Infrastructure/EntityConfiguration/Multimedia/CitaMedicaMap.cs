using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIME_UCR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;


namespace PRIME_UCR.Infrastructure.EntityConfiguration.Multimedia
{
    class CitaMedicaMap : IEntityTypeConfiguration<CitaMedica>
    {
        public void Configure(EntityTypeBuilder<CitaMedica> builder)
        {
            builder
                .HasOne(p => p.Cita)
                .WithMany(p => p.CitasMedicas)
                .HasForeignKey(p => p.IDCita);

            builder
                .HasOne(p => p.Medico)
                .WithMany(p => p.CitasMedicas)
                .HasForeignKey(p => p.IDCita);

            builder
                .HasOne(p => p.Expediente)
                .WithMany(p => p.CitasMedicas)
                .HasForeignKey(p => p.IDCita);

            builder
                .Property(p => p.Codigo)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(p => p.Estado)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(p => p.IDCita)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(p => p.CedMedicoAsignado)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(p => p.NumExpediente)
                .IsRequired();

            builder.HasKey("Codigo");

        }
    }
}
