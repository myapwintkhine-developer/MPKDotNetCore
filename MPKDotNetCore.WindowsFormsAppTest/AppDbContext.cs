﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppTest
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }

        public DbSet<BlogDataModel> Blogs { get; set; }
    }
}
