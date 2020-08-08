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
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Address).IsRequired();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<PayingByCounter>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Account)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Summa).HasColumnType("money");

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.PayingByCounter)
                    .HasForeignKey(d => d.PaymentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PayingByC__Payme__5CD6CB2B");

                entity.HasOne(d => d.ServiceProvider)
                    .WithMany(p => p.PayingByCounter)
                    .HasForeignKey(d => d.ServiceProviderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PayingByC__Servi__5DCAEF64");
            });

            modelBuilder.Entity<PayingFixedSumma>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Account)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Summa).HasColumnType("money");

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.PayingFixedSumma)
                    .HasForeignKey(d => d.PaymentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PayingFix__Payme__3C69FB99");

                entity.HasOne(d => d.ServiceProvider)
                    .WithMany(p => p.PayingFixedSumma)
                    .HasForeignKey(d => d.ServiceProviderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PayingFix__Servi__3D5E1FD2");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.Property(e => e.Timestamp).HasColumnType("datetime");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Payment)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Payment__Custome__37A5467C");
            });

            modelBuilder.Entity<ServiceProvider>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
