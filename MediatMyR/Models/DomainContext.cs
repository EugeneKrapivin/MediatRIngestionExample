namespace MediatMyR;

public sealed class DomainContext
{
	public string WorkspaceId { get; set; }
	public string BusinessUnitId { get; set; }
	public string ApplicationId { get; set; }
	public string DataEventId { get; set; }
	public string DataEventExternalId { get; set; }
}
