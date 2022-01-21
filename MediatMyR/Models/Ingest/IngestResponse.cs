namespace MediatMyR;

public sealed class IngestResponse : IBaseIngestResponse
{
    public string Message { get; init; }

    public bool IsSuccessful { get; init; }

    public string Code { get; init; }
}
