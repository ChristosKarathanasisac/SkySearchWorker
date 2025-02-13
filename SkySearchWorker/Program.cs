using Serilog;
using SkySearchWorker.Startup;
using SkySearchWorker.Worker;

try
{
    HostApplicationBuilder builder = Host.CreateApplicationBuilder(args)
        .ConfigureLogging();

    builder.Services.RegisterInfrastructureServices(builder.Configuration);
    builder.Services.AddHostedService<UpdateDbWorker>();

    var host = builder.Build();
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



