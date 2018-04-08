using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace KS_StockMgmtSystem.Model.Entities
{
    public class SystemDBContext : IdentityDbContext<ApplicationUser>
    {
        public SystemDBContext(DbContextOptions<SystemDBContext> options) : base(options)
        {
        }

        DbSet<VersionData> VersionData { get; set; }
        DbSet<StockData> StockData { get; set; }
    }

    public class TemporaryDbContextFactory : IDesignTimeDbContextFactory<SystemDBContext>
    {
        public SystemDBContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<SystemDBContext>();
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                //   .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            //            builder.UseSqlite(configuration["ConnectionStrings:DefaultConnection"]);
            builder.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);
            var result = new SystemDBContext(builder.Options);
            return result;
        }
    }

}
