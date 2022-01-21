namespace MediatMyR;

public interface IBaseIngestResponse
{
	public string Message { get; }
	public bool IsSuccessful { get; }
	public string Code { get; }
}
