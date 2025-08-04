using TechChallenge.Application.Interfaces;

namespace TechChallenge.Application.Dispatcher
{
    public class Dispatcher : IDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public Dispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<TResult> SendAsync<TResult>(IRequest<TResult> request)
        {
            var handlerType = typeof(IHandler<,>).MakeGenericType(request.GetType(), typeof(TResult));
            dynamic handler = _serviceProvider.GetService(handlerType);

            if (handler == null)
                throw new Exception($"Handler for {request.GetType().Name} not found.");

            return await handler.HandleAsync((dynamic)request);
        }
    }
}
