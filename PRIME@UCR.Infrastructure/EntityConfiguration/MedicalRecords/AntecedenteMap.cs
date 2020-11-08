using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIME_UCR.Domain.Models.MedicalRecords;
using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.Infrastructure.EntityConfiguration.MedicalRecords
{
    public class AntecedenteMap : IEntityTypeConfiguration<Antecedente>
    {
        public void Configure(EntityTypeBuilder<Antecedente> builder)
        {
            builder.ToTable("Antecedente");
            builder.HasKey("Id", "IdExpediente", "IdListaAntecedentes");

            builder
                .HasOne(e => e.Expediente)
                .WithOne()
                .HasForeignKey<Antecedente>(e => e.Expediente);

            builder
                .HasOne(e => e.ListaAntecedentes)
                .WithMany(e => e.Antecedente)
                .HasForeignKey(e => e.ListaAntecedentes);

        }
    }
}
