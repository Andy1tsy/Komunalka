using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Komunalka.DAL.Models
{
    public class PayingFixedSummaConfiguration : IEntityTypeConfiguration<PayingFixedSumma>
    {
        public void Configure(EntityTypeBuilder<PayingFixedSumma> builder)
        {
            builder.ToTable("PayingFixedSumma").HasKey(e => e.Id);

            builder.Property(e => e.Account)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Summa).HasColumnType("money");

            builder.HasOne(d => d.Payment)
                .WithMany(p => p.PayingFixedSumma)
                .HasForeignKey(d => d.PaymentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PayingFix__Payme__3C69FB99");

            builder.HasOne(d => d.ServiceProvider)
                .WithMany(p => p.PayingFixedSumma)
                .HasForeignKey(d => d.ServiceProviderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PayingFix__Servi__3D5E1FD2");
        }
    }
}
