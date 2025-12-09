using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.Entities;

namespace TourSite.Repository.Data.Configurations
{
    public class EmailConfiguration : IEntityTypeConfiguration<Email>
    {
        public void Configure(EntityTypeBuilder<Email> builder)
        {
            builder.ToTable("Emails");

            builder.Property(e => e.FullName)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(e => e.EmailAddress)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.HasOne(e => e.Tour)
                .WithMany(t => t.Emails)
                .HasForeignKey(e => e.FK_TourId)
                .OnDelete(DeleteBehavior.SetNull);

              builder.Property(u => u.FullTourName)
                .HasDefaultValue("");
        }
    }
}
