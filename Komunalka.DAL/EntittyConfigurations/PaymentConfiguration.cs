using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Komunalka.DAL.Models
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payment").HasKey(e => e.Id);

            builder.Property(e => e.Year).IsRequired();

            builder.Property(e => e.Month).IsRequired();

            builder.Property(e => e.TotalSumma).HasColumnType("money");

            builder.Property(e => e.Timestamp).HasColumnType("datetime");

            builder.HasOne(d => d.Customer)
                .WithMany(p => p.Payment)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Payment__Custome__37A5467C");
        }
    }
}
