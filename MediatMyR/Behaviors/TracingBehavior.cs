

using MediatR;

using Microsoft.Extensions.Logging;

namespace MediatMyR;

public class TracingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<TracingBehavior<TRequest, TResponse>> _logger;

    public TracingBehavior(ILogger<TracingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        _logger.LogInformation($"Starting {typeof(TRequest).Name}");
        var response = await next();
        _logger.LogInformation($"Ending {typeof(TResponse).Name}");

        return response;
    }
}
