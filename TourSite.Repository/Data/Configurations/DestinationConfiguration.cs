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
    public class DestinationConfiguration : IEntityTypeConfiguration<Destination>
    {
        public void Configure(EntityTypeBuilder<Destination> builder)
        {
            builder.ToTable("Destinations");

            builder.Property(d => d.IsActive)
                .HasDefaultValue(true);

            builder.HasMany(d => d.Tours)
                .WithOne(t => t.Destination)
                .HasForeignKey(t => t.FK_DestinationID)
                .OnDelete(DeleteBehavior.SetNull);


    
        }
    }
}
