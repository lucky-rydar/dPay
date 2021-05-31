using API.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace API.Database
{
    public class CustomDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Donation> Donations { get; set; }

        public CustomDBContext(DbContextOptions<CustomDBContext> options): base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //FileName
            /*optionsBuilder.UseSqlite("FileName=D:/home/site/wwwroot/database.db", option =>
            {
                option.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });*/
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Card>().ToTable("Cards");
            modelBuilder.Entity<Transaction>().ToTable("Transactions");
            modelBuilder.Entity<Donation>().ToTable("Donations");

            modelBuilder.Entity<User>(entity=> {
                entity.HasKey(k => k.Id);
            });

            modelBuilder.Entity<Card>(entity => {
                entity.HasKey(k => k.Id);
            });
            
            modelBuilder.Entity<Transaction>(entity => {
                entity.HasKey(k => k.Id);
            });

            modelBuilder.Entity<Donation>(entity => {
                entity.HasKey(k => k.Id);
            });

            base.OnModelCreating(modelBuilder);
        }

        
        
    }
}
