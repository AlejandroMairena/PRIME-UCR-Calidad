﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Infrastructure.DataProviders.Implementations.EntityConfiguration.Incidents
{
    public class IncidenteMap : IEntityTypeConfiguration<Incidente>
    {
        public void Configure(EntityTypeBuilder<Incidente> builder)
        {
            builder.ToTable("Incidente");
            builder.HasKey("Codigo");
            builder
                .HasOne(p => p.Origen)
                .WithOne()
                .HasForeignKey<Incidente>(p => p.IdOrigen);
            builder
                .HasOne(p => p.Destino)
                .WithOne()
                .HasForeignKey<Incidente>(p => p.IdDestino);
            builder
                .Property(p => p.Codigo)
                .IsRequired()
                .HasMaxLength(50);
            builder
                .Property(p => p.TipoModalidad)
                .HasColumnName("Modalidad");
            builder
                .HasOne(p => p.Modalidad)
                .WithMany(p => p.Incidentes)
                .HasForeignKey(p => p.TipoModalidad);
            builder
                .HasOne(p => p.UnidadDeTransporte)
                .WithMany(p => p.Incidentes)
                .HasForeignKey(p => p.MatriculaTrans);
        }
    }
}

