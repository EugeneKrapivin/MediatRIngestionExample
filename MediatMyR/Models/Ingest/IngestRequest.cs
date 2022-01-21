using MediatR;

using Newtonsoft.Json.Linq;

namespace MediatMyR;

public class IngestRequest :
    IBaseIngestRequest,
    
    IHaveClientContext,
    IHaveRequestContext,
    IRequest<IngestResponse>
{
    public JToken IngestedData { get; init; }

    public ClientContext ClientRequestContext { get; init; }

    public DomainContext RequestContext { get; init; }

    public IBaseIngestResponse GetSuccesResponse(string message, string code) => new IngestResponse()
    {
        Message = message,
        Code = code,
        IsSuccessful = true,
    };

    public IBaseIngestResponse GetFailedResponse(string message, string code) => new IngestResponse()
    {
        Message = message,
        Code = code,
        IsSuccessful = false,
    };
}

public class IngestRoutedRequest :
    IngestRequest,
    IHaveRoute
{
    public string Route => "rrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrr";
}
