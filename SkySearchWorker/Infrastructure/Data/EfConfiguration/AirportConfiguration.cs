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
    public static class AirportConfiguration
    {
        public static void Configure(EntityTypeBuilder<Airport> builder)
        {
            builder.ToTable("Airport");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Name).HasMaxLength(100).IsRequired();
            builder.Property(a => a.Code).HasMaxLength(10).IsRequired();
            builder.Property(a => a.CityCode).HasMaxLength(10).IsRequired();
            builder.Property(a => a.CountryCode).HasMaxLength(10).IsRequired();
        }
    }
}
