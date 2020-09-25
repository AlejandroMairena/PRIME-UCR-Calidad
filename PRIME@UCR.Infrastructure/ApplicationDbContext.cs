using Microsoft.EntityFrameworkCore;
using PRIME_UCR.Domain.Models;
using System;

namespace PRIME_UCR.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<TestModel> TestModels { get; set; }
        public DbSet<MultimediaContent> multimedia_content { get; set; }

    }
}
