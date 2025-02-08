using Serilog;
using SkySearchWorker;
using SkySearchWorker.Startup;

try
{
    HostApplicationBuilder builder = Host.CreateApplicationBuilder(args)
        .ConfigureLogging();
        
    builder.Services.AddHostedService<Worker>();

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



