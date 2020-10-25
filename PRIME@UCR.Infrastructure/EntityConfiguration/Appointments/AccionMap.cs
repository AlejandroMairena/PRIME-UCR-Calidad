using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIME_UCR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.Infrastructure.EntityConfiguration.Multimedia
{
    public class AccionMap : IEntityTypeConfiguration<Accion>
    {
        void IEntityTypeConfiguration<Accion>.Configure(EntityTypeBuilder<Accion> builder)
        {
            builder.HasKey("Id", "CitaId");

            builder
                .HasOne(a => a.Cita)
                .WithMany(c => c.Acciones)
                .HasForeignKey(a => a.CitaId);

            builder
                .HasOne(a => a.TipoAccion)
                .WithMany()
                .HasForeignKey(a => a.NombreAccion);
        }
    }
}
