using Baldr.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.EntityFramework
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Institution> Institutions { get; set; }
    }
}
