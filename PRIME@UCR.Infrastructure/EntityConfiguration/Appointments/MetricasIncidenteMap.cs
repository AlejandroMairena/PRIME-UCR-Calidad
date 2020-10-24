using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIME_UCR.Domain.Models.Appointments;
using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.Infrastructure.EntityConfiguration.Appointments
{
    public class MetricasIncidenteMap : IEntityTypeConfiguration<MetricasIncidente>
    {
        public void Configure(EntityTypeBuilder<MetricasIncidente> builder)
        {
            builder.ToTable("MetricasIncidente");
        }
    }
}
