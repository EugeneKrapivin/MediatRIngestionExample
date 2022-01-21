using MediatR;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json.Linq;

namespace MediatMyR;

public class HostedConsole : IHostedService
{
    private readonly IMediator _mediator;
    
    private readonly ILogger<HostedConsole> _logger;

    public HostedConsole(IMediator mediator, ILogger<HostedConsole> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("starting");

        await _mediator.Send(new IngestRequest()
        {
            IngestedData = new JObject()
            {
                ["key"] = "abc"
            }
        });

        await _mediator.Send(new IngestRequest()
        {
            IngestedData = new JObject()
            {
                ["key"] = "abc"
            }
        });

        await _mediator.Send(new IngestRequest()
        {
            IngestedData = new JObject()
            {
                ["key"] = "abc1"
            }
        });

        await Task.Delay(TimeSpan.FromSeconds(5));

        await _mediator.Send(new IngestRequest()
        {
            IngestedData = new JObject()
            {
                ["key"] = "abc"
            }
        });
        await _mediator.Send(new IngestRoutedRequest()
        {
            IngestedData = new JObject()
            {
                ["key"] = "abcRRR"
            }
        });
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("stopping");

        return Task.CompletedTask;
    }
}