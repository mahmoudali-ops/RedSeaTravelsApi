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
    public class TourImgConfiguration : IEntityTypeConfiguration<TourImg>
    {
  
        public void Configure(EntityTypeBuilder<TourImg> builder)
        {

            builder.Property(i => i.ImageCarouselUrl)
                .IsRequired();

            builder.Property(i => i.IsActive)
                .HasDefaultValue(true);

            builder.HasOne(i => i.Tour)
           .WithMany(t => t.TourImgs)
           .HasForeignKey(i => i.FK_TourId)
           .OnDelete(DeleteBehavior.SetNull);

         
        }
    }
}
