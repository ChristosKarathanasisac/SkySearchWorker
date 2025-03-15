using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkySearchWorker.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkySearchWorker.Infrastructure.Data.EfConfiguration
{
    public static class FlightPriceConfiguration
    {
        public static void Configure(EntityTypeBuilder<FlightPrice> builder)
        {
            builder.ToTable("FlightPrice");

            builder.HasKey(fp => fp.Id);
            builder.Property(fp => fp.Class).HasMaxLength(50).IsRequired();
            builder.Property(fp => fp.Date).IsRequired();
            builder.Property(fp => fp.FlightId).IsRequired();
            builder.Property(fp => fp.Price).HasColumnType("decimal(18,2)").IsRequired();

            builder.HasOne(fp => fp.Flight)
                  .WithMany(f => f.FlightPrices)
                  .HasForeignKey(fp => fp.FlightId);
        }
    }
}
