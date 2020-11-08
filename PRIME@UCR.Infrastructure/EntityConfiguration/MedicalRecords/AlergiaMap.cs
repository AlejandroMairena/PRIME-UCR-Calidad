﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIME_UCR.Domain.Models.MedicalRecords;
using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.Infrastructure.EntityConfiguration.MedicalRecords
{
    public class AlergiaMap : IEntityTypeConfiguration<Alergia>
    {
        public void Configure(EntityTypeBuilder<Alergia> builder)
        {
            builder.ToTable("Alergia");
            builder.HasKey("Id","IdExpediente","IdListaAlergia");

            builder
                .HasOne(e => e.Expediente)
                .WithOne()
                .HasForeignKey<Alergia>(e => e.IdExpediente);

            builder
                .HasOne(e => e.ListaAlergia)
                .WithMany(e => e.Alergia)
                .HasForeignKey(e => e.IdListaAlergia);

        }
    }
}
