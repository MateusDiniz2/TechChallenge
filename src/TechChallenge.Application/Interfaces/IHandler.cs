namespace TechChallenge.Application.Interfaces
{
    public interface IHandler<TRequest, TResult> where TRequest : IRequest<TResult>
    {
        Task<TResult> HandleAsync(TRequest request);
    }
}
