namespace MediatMyR
{
    public interface IHaveRequestContext
    {
        DomainContext RequestContext { get; }
    }
}