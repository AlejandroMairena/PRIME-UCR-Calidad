using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIME_UCR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.Infrastructure.EntityConfiguration.Appointments
{
    public class MetricasMap : IEntityTypeConfiguration<Metricas>
    {
        public void Configure(EntityTypeBuilder<Metricas> builder)
        {
            builder.ToTable("Metricas");

            builder.HasKey("Id", "CitaId");

            builder
                .HasOne(m => m.Cita)
                .WithMany(c => c.Metricas)
                .HasForeignKey(m => m.CitaId);

        }
    }
}
