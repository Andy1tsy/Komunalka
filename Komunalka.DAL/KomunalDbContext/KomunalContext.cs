using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Komunalka.DAL.Models;

namespace Komunalka.DAL.KomunalDbContext
{
    public partial class KomunalContext : DbContext
    {
        public KomunalContext()
        {
        }

        public KomunalContext(DbContextOptions<KomunalContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<PayingByCounter> PayingByCounter { get; set; }
        public virtual DbSet<PayingFixedSumma> PayingFixedSumma { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }
        public virtual DbSet<ServiceProvider> ServiceProvider { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Initial Catalog=Komunaldb;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());

            modelBuilder.ApplyConfiguration(new PayingByCounterConfiguration());

            modelBuilder.ApplyConfiguration(new PayingFixedSummaConfiguration());

            modelBuilder.ApplyConfiguration(new PaymentConfiguration());

            modelBuilder.ApplyConfiguration(new ServiceProviderConfiguration());

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
