using MPKDotNetCore.MinimalApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MPKDotNetCore.MinimalApi
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BlogDataModel> Blogs { get; set; }
    }
}
