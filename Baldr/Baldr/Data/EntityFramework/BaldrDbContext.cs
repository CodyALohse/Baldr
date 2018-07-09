using Baldr.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.EntityFramework
{
    public class BaldrDbContext : DbContext
    {
        public BaldrDbContext() { }

        public BaldrDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Institution> Institutions { get; set; }
    }
}
