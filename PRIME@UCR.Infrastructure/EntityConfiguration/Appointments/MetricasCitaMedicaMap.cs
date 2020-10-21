using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIME_UCR.Domain.Models.Appointments;
using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.Infrastructure.EntityConfiguration.Appointments
{
    public class MetricasCitaMedicaMap : IEntityTypeConfiguration<MetricasCitaMedica>
    {
        public void Configure(EntityTypeBuilder<MetricasCitaMedica> builder)
        {
            builder.ToTable("MetricasCitaMedica");
        }
    }
}
