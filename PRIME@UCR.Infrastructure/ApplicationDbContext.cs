using Microsoft.EntityFrameworkCore;
using PRIME_UCR.Core.Models;
using System;

namespace PRIME_UCR.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<TestModel> TestModels { get; set; }
    }
}
