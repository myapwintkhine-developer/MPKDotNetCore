using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MPKDotNetCore.RestApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPKDotNetCore.RestApi
{
    public class AppDbContext:DbContext
    {
        private readonly SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = "DESKTOP-F0KQS1A",
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
