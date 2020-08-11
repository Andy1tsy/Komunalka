using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Komunalka.DAL.Models
{
    public class PayingByCounterConfiguration : IEntityTypeConfiguration<PayingByCounter>
    {
        public void Configure(EntityTypeBuilder<PayingByCounter> builder)
        {
            builder.ToTable("PayingByCounter").HasKey(e => e.Id);

            builder.Property(e => e.Account)
                    .IsRequired()
                    .HasMaxLength(50);

            builder.Property(e => e.Summa).HasColumnType("money");

            builder.HasOne(d => d.Payment)
                    .WithMany(p => p.PayingByCounter)
                    .HasForeignKey(d => d.PaymentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PayingByC__Payme__5CD6CB2B");

            builder.HasOne(d => d.ServiceProvider)
                    .WithMany(p => p.PayingByCounter)
                    .HasForeignKey(d => d.ServiceProviderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PayingByC__Servi__5DCAEF64");
         
        }
    }
}
