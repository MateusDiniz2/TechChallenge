using static TechChallenge.Application.Interfaces.IKafkaClient;

namespace TechChallenge.Worker.Services
{
    public class KafkaConsumerService : BackgroundService
    {
        private readonly IKafkaConsumer _kafkaConsumer;

        public KafkaConsumerService(IKafkaConsumer kafkaConsumer)
        {
            _kafkaConsumer = kafkaConsumer;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _kafkaConsumer.StartConsuming(stoppingToken);
            return Task.CompletedTask;
        }
    }
}
