using Microsoft.EntityFrameworkCore;
using Serilog;
using SkySearchWorker.Infrastructure.Configuration;
using SkySearchWorker.Infrastructure.Data;
using SkySearchWorker.Startup;
using SkySearchWorker.Worker;

try
{
    HostApplicationBuilder builder = Host.CreateApplicationBuilder(args)
        .ConfigureLogging();

    builder.Services.RegisterInfrastructureServices(builder.Configuration);
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
    builder.Services.AddHostedService<UpdateDbWorker>();

    bool isDevelopment = builder.Configuration.GetSection("Amadeus").GetValue<bool>("IsDevelopment");

    var host = builder.Build();

    if (isDevelopment) 
    {
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            SeedData.Initialize(services);
        }
    }
    
    host.Run();
}
catch (Exception exc)
{
    Log.Logger.Error(exc, "Worker terminated unexpectedly!");
}
finally
{
    Log.CloseAndFlush();
}



