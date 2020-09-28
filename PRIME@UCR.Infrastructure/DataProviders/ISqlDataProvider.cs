using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PRIME_UCR.Domain.Models;
using System.Threading.Tasks;

namespace PRIME_UCR.Infrastructure.DataProviders
{
    public interface ISqlDataProvider
    {
        //DbSet<Acciones> Acciones { get; set;  }
        DbSet<MultimediaContent> Multimedia_Contents { get; set;  }
        //DbSet<Cita> Citas { get; set;  }
        DbSet<T> Set<T>() where T : class;
        Task<int> SaveChangesAsync();


    }
}