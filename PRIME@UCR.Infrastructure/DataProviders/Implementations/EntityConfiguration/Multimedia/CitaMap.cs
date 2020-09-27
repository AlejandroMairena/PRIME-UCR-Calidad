using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIME_UCR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.Infrastructure.DataProviders.Implementations.EntityConfiguration.Multimedia
{
    public class CitaMap : IEntityTypeConfiguration<Cita>
    {
        void IEntityTypeConfiguration<Cita>.Configure(EntityTypeBuilder<Cita> builder)
        {
            builder
                .Property(p => p.IDCita)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(p => p.FechaHoraCreacion);
            //es tipo date_time, no se que restricciones se le podrian poner. 

            builder
                .Property(p => p.FechaHoraEstimada);

            builder.HasKey(p => p.IDCita); 


        }
    }
}
