using MediatR;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using MediatMyR;
using Serilog;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();
try
{
    await Host.CreateDefaultBuilder(args)
        .ConfigureServices((hostContext, services) =>
        {
            services.AddSingleton<IDeduplicationSource, MockDeduplicationSource>();
            services.AddMemoryCache();
            services.AddMediatR(typeof(Program));
            services.AddHostedService<HostedConsole>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TracingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ClientValidatingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RouteValidatingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(DeduplicatingBehavior<,>));
        })
        .UseSerilog()
        .StartAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}