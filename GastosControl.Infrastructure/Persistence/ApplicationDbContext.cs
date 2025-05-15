using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GastosControl.Domain.Entities;


namespace GastosControl.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets (tablas)
        public DbSet<User> Users { get; set; }
        public DbSet<ExpenseType> ExpenseTypes { get; set; }
        public DbSet<MonetaryFund> MonetaryFunds { get; set; }
        public DbSet<UserBudget> UserBudgets { get; set; }
        public DbSet<ExpenseHeader> ExpenseHeaders { get; set; }
        public DbSet<ExpenseDetail> ExpenseDetails { get; set; }
        public DbSet<Deposit> Deposits { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar precisión para campos decimal
            modelBuilder.Entity<Deposit>()
                .Property(p => p.Amount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<ExpenseDetail>()
                .Property(p => p.Amount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<UserBudget>()
                .Property(p => p.Amount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<ExpenseDetail>()
                .HasOne(d => d.Header)
                .WithMany(h => h.Details)
                .HasForeignKey(d => d.HeaderId);

            modelBuilder.Entity<MonetaryFund>()
                .Property(m => m.Balance)
                .HasPrecision(18, 2);
        }
    }
}
