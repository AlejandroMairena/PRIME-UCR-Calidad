using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIME_UCR.Domain.Models.UserAdministration;
using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.Infrastructure.EntityConfiguration.UserAdministration
{
    public class FuncionarioMap : IEntityTypeConfiguration<Funcionario>
    {
        /**
        *  Function:       Used to configure the table of Funcionario of the database with its model.
        *  Requieres:      The EntityTypeBuilder used to configure the entity framework.
        *  Returns:        Nothing.
        */
        public void Configure(EntityTypeBuilder<Funcionario> builder)
        {
            builder.ToTable("Funcionario");
            builder
                .Property(p => p.Cédula)
                .HasColumnName("CédulaFuncionario")
                .IsRequired();
            builder
                .HasMany(p => p.Perfiles)
                .WithMany(p => p.Funcionarios)
                .UsingEntity(p => 
                {
                    p.ToTable("TienePerfil");
                });
        }
    }
}
