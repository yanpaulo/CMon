using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMon.IoTApp.Models
{
    public class AppDbContext : DbContext
    {
        public static object LockObject { get; private set; } = new object();

        public virtual DbSet<Reading> Readings { get; set; }

        protected virtual DbSet<AppConfiguration> AppConfiguration { get; set; }

        public AppConfiguration GetAppConfiguration() => 
            AppConfiguration.SingleOrDefault() ?? new AppConfiguration();
        
        public void UpdateAppConfiguration(AppConfiguration config)
        {
            if (AppConfiguration.Any())
            {
                this.Entry(config).State = EntityState.Modified;
            }
            else
            {
                this.AppConfiguration.Add(config);
            }

        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=local.db");
        }
    }
}
