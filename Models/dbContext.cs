using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZCounter.Models
{
    public class dbContext : DbContext
    {
        public dbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbfile = Path.Combine(
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "EZCounter"),
                "data.db");
            optionsBuilder.UseSqlite(@"DataSource="+dbfile+";");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Counter>()
                .HasKey(p => p.ID);
            builder.Entity<Counter>().Property(e => e.ID).ValueGeneratedOnAdd();
        }

        public DbSet<Counter> Counters { get; set; }
    }
}
