
using MediatR;

using Newtonsoft.Json.Linq;

namespace MediatMyR;

public interface IBaseIngestRequest
{
	JToken IngestedData { get; init; }

	IBaseIngestResponse GetSuccesResponse(string message, string code);
	IBaseIngestResponse GetFailedResponse(string message, string code);
}