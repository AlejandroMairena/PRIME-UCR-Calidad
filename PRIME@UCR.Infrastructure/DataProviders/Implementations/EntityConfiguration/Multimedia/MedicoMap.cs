using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIME_UCR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.Infrastructure.DataProviders.Implementations.EntityConfiguration.Multimedia
{
    public class MedicoMap : IEntityTypeConfiguration<Medico>
    {
        public void Configure(EntityTypeBuilder<Medico> builder)
        {

        }
    }
}
