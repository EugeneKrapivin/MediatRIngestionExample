

using MediatR;

using Microsoft.Extensions.Logging;

namespace MediatMyR;

public class RouteValidatingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, IHaveRoute
{
    private readonly ILogger<RouteValidatingBehavior<TRequest, TResponse>> _logger;

    public RouteValidatingBehavior(ILogger<RouteValidatingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        _logger.LogInformation($"Route {request.Route} is valid");
        var response = await next();

        return response;
    }
}
