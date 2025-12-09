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
    public class TourConfiguration : IEntityTypeConfiguration<Tour>
    {
        public void Configure(EntityTypeBuilder<Tour> builder)
        {
            builder.ToTable("Tours");

       

            builder.Property(t => t.Price)
                .HasColumnType("decimal(18,2)");

            builder.Property(t => t.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(t => t.IsActive)
                .HasDefaultValue(true);

            builder.HasMany(t => t.TourImgs)
                .WithOne(img => img.Tour)
                .HasForeignKey(img => img.FK_TourId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(t => t.Emails)
                .WithOne(e => e.Tour)
                .HasForeignKey(e => e.FK_TourId)
                .OnDelete(DeleteBehavior.SetNull);


            builder.HasMany(c => c.Includeds)
             .WithOne(t => t.Tour)
             .HasForeignKey(t => t.TourId)
             .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.NotIncludeds)
             .WithOne(t => t.Tour)
             .HasForeignKey(t => t.TourId)
             .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.Highlights)
             .WithOne(t => t.Tour)
             .HasForeignKey(t => t.TourId)
             .OnDelete(DeleteBehavior.Cascade);


            builder.Property(u => u.LinkVideo)
              .HasDefaultValue("");


        }
    }
}
