using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Project2.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project2.Data
{
    class Project2ContextFactory : IDesignTimeDbContextFactory<Project2Context>
    {
        public IConfiguration Configuration { get; }

        //public static readonly string connectionString = System.IO.File.ReadAllText("C:/Users/james/Desktop/Revature/Project0Connect.txt");
        public Project2Context CreateDbContext(string[] args = default)
        {
            var options = new DbContextOptionsBuilder<Project2Context>()
                .UseSqlServer(Configuration.GetConnectionString("SqlServer"))
                .Options;


            return new Project2Context(options);

        }
    }
}
