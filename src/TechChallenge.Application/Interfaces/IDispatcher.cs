namespace TechChallenge.Application.Interfaces
{
    public interface IDispatcher
    {
        Task<TResult> SendAsync<TResult>(IRequest<TResult> request);
    }
}