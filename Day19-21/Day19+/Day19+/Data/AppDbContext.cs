using ExpenseTracker.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace ExpenseTracker.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Expense> Expenses { get; set; }

        // Начальные данные
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Expense>().HasData(
                new Expense { Id = 1, Title = "Продукты", Amount = 500, Category = "Еда", Date = DateTime.Now.AddDays(-5) },
                new Expense { Id = 2, Title = "Метро", Amount = 150, Category = "Транспорт", Date = DateTime.Now.AddDays(-3) },
                new Expense { Id = 3, Title = "Кино", Amount = 800, Category = "Развлечения", Date = DateTime.Now.AddDays(-1) }
            );
        }
    }
}