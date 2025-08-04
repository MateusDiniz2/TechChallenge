using Confluent.Kafka;

namespace TechChallenge.Worker.Services
{
    public class KafkaConsumerService : BackgroundService
    {
        private readonly ILogger<KafkaConsumerService> _logger;
        private readonly IConsumer<Ignore, string> _consumer;

        public KafkaConsumerService(ILogger<KafkaConsumerService> logger)
        {
            _logger = logger;

            var config = new ConsumerConfig
            {
                GroupId = "techchallenge-group",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            _consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            _consumer.Subscribe("products-topic");
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Run(() =>
            {
                try
                {
                    while (!stoppingToken.IsCancellationRequested)
                    {
                        var consumeResult = _consumer.Consume(stoppingToken);

                        if (consumeResult != null)
                        {
                            var message = consumeResult.Message.Value;
                            _logger.LogInformation("Mensagem consumida do Kafka: {Message}", message);
                            Console.WriteLine($"Mensagem consumida do Kafka: {message}");
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    
                }
                finally
                {
                    _consumer.Close();
                }
            }, stoppingToken);
        }
    }
}
