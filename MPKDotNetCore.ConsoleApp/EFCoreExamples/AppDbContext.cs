using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MPKDotNetCore.ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPKDotNetCore.ConsoleApp.EFCoreExamples
{
    public class AppDbContext:DbContext
    {
        private readonly SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "TestDb",
            UserID = "sa",
            Password = "sasa",
            Encrypt = true, // Enable SSL encryption
            TrustServerCertificate = true // Validate the server certificate
        };

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(sqlConnectionStringBuilder.ConnectionString);
            }
        }

        public DbSet<BlogDataModel> Blogs { get; set; }
    }
}
