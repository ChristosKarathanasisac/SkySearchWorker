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
    public static class AirlineConfiguration
    {
        public static void Configure(EntityTypeBuilder<Airline> builder)
        {
            builder.ToTable("Airline");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Name).HasMaxLength(100).IsRequired();
            builder.Property(a => a.Code).HasMaxLength(10).IsRequired();
        }
    }
}
