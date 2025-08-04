
namespace TechChallenge.Application.Interfaces
{
    public interface IKafkaClient
    {
        public interface IKafkaProducer
        {
            Task SendMessageAsync<T>(T message);
        }

        public interface IKafkaConsumer
        {
            void StartConsuming(CancellationToken cancellationToken);
        }
    }
}
