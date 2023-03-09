using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ScienceArchive.Infrastructure.Persistence.Models;

namespace ScienceArchive.Infrastructure.Persistence
{
    public class ScienceArchiveDbContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }

        public ScienceArchiveDbContext(DbContextOptions options) : base(options) { }
    }
}

