

using MediatR;

using Microsoft.Extensions.Logging;

namespace MediatMyR;

public class ClientValidatingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, IHaveClientContext
{
    private readonly ILogger<ClientValidatingBehavior<TRequest, TResponse>> _logger;

    public ClientValidatingBehavior(ILogger<ClientValidatingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        // TODO: Use fluent validation to validate client context
        _logger.LogInformation($"Validating client {typeof(TRequest).Name}");
        var response = await next();

        return response;
    }
}
