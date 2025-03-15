using Microsoft.EntityFrameworkCore;
using SkySearchWorker.Infrastructure.Data.EfConfiguration;
using SkySearchWorker.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkySearchWorker.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Airline> Airlines { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<FlightPrice> FlightPrices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Flight>(FlightConfiguration.Configure);
            modelBuilder.Entity<FlightPrice>(FlightPriceConfiguration.Configure);
            modelBuilder.Entity<Airline>(AirlineConfiguration.Configure);
            modelBuilder.Entity<Airport>(AirportConfiguration.Configure);
        }
    }
}
