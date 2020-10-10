using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIME_UCR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;



namespace PRIME_UCR.Infrastructure.EntityConfiguration.Multimedia
{
    class ExpedienteMap : IEntityTypeConfiguration<Expediente>
    {
        public void Configure(EntityTypeBuilder<Expediente> builder)
        {
            builder
                .HasOne(p => p.Paciente)
                .WithOne(p => p.Expediente)
                .HasForeignKey<Expediente>(p => p.CedPaciente);

            builder
                .HasOne(p => p.Medico)
                .WithMany(p => p.Expedientes)
                .HasForeignKey(p => p.CedMedicoDuenno);


            builder
                .Property(p => p.NumExpediente)
                .IsRequired();
           
            builder
                .Property(p => p.Clinica)
                .IsRequired()
                .HasMaxLength(500);

            builder
            .Property(p => p.FechaHoraCreacion)
            .IsRequired();

            builder
                .Property(p => p.CedPaciente)
                .IsRequired()
                .HasMaxLength(50);

            builder
            .Property(p => p.CedMedicoDuenno)
            .IsRequired()
            .HasMaxLength(50);

            builder.HasKey("NumExpediente"); 


        }
    }
}
