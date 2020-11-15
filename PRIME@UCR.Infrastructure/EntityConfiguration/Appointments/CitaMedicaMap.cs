/*using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIME_UCR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.Infrastructure.EntityConfiguration.Appointments
{
    public class CitaMedicaMap : IEntityTypeConfiguration<CitaMedica>
    {
        void IEntityTypeConfiguration<CitaMedica>.Configure(EntityTypeBuilder<CitaMedica> builder)
        {
            builder
                .HasKey("Codigo");

            builder
                .HasOne(c => c.Cita)
                .WithOne();

            builder
                .HasOne(e => e.CitaMedica)
                .WithOne()
                .HasForeignKey<CitaMedica>(e => e.IDCita);

            builder.ToTable(nameof(CitaMedica));
        }

    }
}
*/