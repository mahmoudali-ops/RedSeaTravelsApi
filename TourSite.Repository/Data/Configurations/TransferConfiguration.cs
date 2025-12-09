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
    public class TransferConfiguration : IEntityTypeConfiguration<Transfer>
    {
        public void Configure(EntityTypeBuilder<Transfer> builder)
        {
            builder.ToTable("Transfers");

            builder.Property(t => t.IsActive)
                .HasDefaultValue(true);

            builder.HasOne(t => t.Destination)
                .WithMany(d => d.Transfers)
                .HasForeignKey(t => t.FK_DestinationID)
                .OnDelete(DeleteBehavior.SetNull);

  

            builder.HasMany(c => c.PricesList)
            .WithOne(t => t.Transfer)
            .HasForeignKey(t => t.TransferId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.Includeds)
            .WithOne(t => t.Transfer)
            .HasForeignKey(t => t.TransferId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.NotIncludeds)
            .WithOne(t => t.Transfer)
            .HasForeignKey(t => t.TransferId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.Highlights)
            .WithOne(t => t.Transfer)
            .HasForeignKey(t => t.TransferId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
