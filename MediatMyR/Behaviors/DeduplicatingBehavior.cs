using MediatR;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json.Linq;

namespace MediatMyR;

public class DeduplicatingBehavior<TRequest, TResponse> 
    : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>, IBaseIngestRequest
        where TResponse : IBaseIngestResponse
    
{
    private readonly IDeduplicationSource _source;
    private readonly ILogger<DeduplicatingBehavior<TRequest, TResponse>> _logger;

    public DeduplicatingBehavior(
        IDeduplicationSource source,
        ILogger<DeduplicatingBehavior<TRequest, TResponse>> logger)
    {
        _source = source;
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        
        if (request.IngestedData is not JObject jobj)
        {
            _logger.LogWarning("attempted dedupe on a none JSON Object");
            return await next();
        }
        _logger.LogInformation("Checking deduplication status");

        var status = await _source.GetStatus(jobj);

        if (status.IsDuplicated)
        {
            _logger.LogWarning("encounted duplicated message for key {key}", status.Key);

            // not sure why C# doesn't allow me to return IBaseIngestResponse
            // here even though TResponse if constrained to IBaseIngestResponse
            return (TResponse)request.GetFailedResponse($"encounted duplicated message for key {status.Key}", "400-dedupe");
        }
        
        var response = await next();

        if (response.IsSuccessful)
        {
            await _source.Tick(status.Key);
        }

        return response;
    }
}