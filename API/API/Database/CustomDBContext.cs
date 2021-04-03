using API.Controllers;
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("FileName=database.db", option =>
            {
                option.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Card>().ToTable("Cards");
            modelBuilder.Entity<Transaction>().ToTable("Transactions");

            modelBuilder.Entity<User>(entity=> {
                entity.HasKey(k => k.Id);
            });

            modelBuilder.Entity<Card>(entity => {
                entity.HasKey(k => k.Id);
            });
            
            modelBuilder.Entity<Transaction>(entity => {
                entity.HasKey(k => k.Id);
            });

            base.OnModelCreating(modelBuilder);
        }
        
    }
}
