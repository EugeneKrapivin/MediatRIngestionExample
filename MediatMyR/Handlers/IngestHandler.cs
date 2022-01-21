

using MediatR;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

namespace MediatMyR;

public class IngestHandler : IRequestHandler<IngestRequest, IngestResponse>,
    IRequestHandler<IngestRoutedRequest, IngestResponse>
{
    private readonly ILogger<IngestHandler> _logger;

    public IngestHandler(ILogger<IngestHandler> logger)
	{
        _logger = logger;
    }
	
    public async Task<IngestResponse> Handle(IngestRequest request, CancellationToken cancellationToken)
    {
        await Task.Delay(500, cancellationToken);
        _logger.LogInformation($"handled request {JsonConvert.SerializeObject(request)}");
        return new IngestResponse
        {
            Code = "202",
            IsSuccessful = true,
            Message = "message enqueued for processing"
        };
	}

    public async Task<IngestResponse> Handle(IngestRoutedRequest request, CancellationToken cancellationToken)
    {
        await Task.Delay(500, cancellationToken);
        _logger.LogInformation($"handled request {JsonConvert.SerializeObject(request)}");
        return new IngestResponse
        {
            Code = "202",
            IsSuccessful = true,
            Message = "message enqueued for processing"
        };
    }
}