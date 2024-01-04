using MPKDotNetCore.ATMWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace MPKDotNetCore.ATMWebApp.EFDbContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
    }
}
