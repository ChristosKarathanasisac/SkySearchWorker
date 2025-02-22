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
    public static class FlightConfiguration
    {
        public static void Configure(EntityTypeBuilder<Flight> builder)
        {
            builder.ToTable("Flight");

            builder.HasKey(f => f.Id);
            builder.Property(f => f.AirlineId).IsRequired();
            builder.Property(f => f.AirportId).IsRequired();
            builder.Property(f => f.ArrivalTime).IsRequired();
            builder.Property(f => f.DepartureTime).IsRequired();

            builder.HasOne(f => f.Airline)
                   .WithMany(a => a.Flights)
                   .HasForeignKey(f => f.AirlineId);

            builder.HasOne(f => f.Airport)
                   .WithMany(a => a.Flights)
                   .HasForeignKey(f => f.AirportId);
        }
    }
}
