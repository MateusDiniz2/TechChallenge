using Confluent.Kafka;
using static TechChallenge.Application.Interfaces.IKafkaClient;

namespace TechChallenge.Infrastructure.Messaging
{
    public class KafkaConsumer : IKafkaConsumer
    {
        private readonly IConsumer<Ignore, string> _consumer;

        public KafkaConsumer(string broker, string topic, string groupId)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = broker,
                GroupId = groupId,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            _consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            _consumer.Subscribe(topic);
        }

        public void StartConsuming(CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var consumeResult = _consumer.Consume(cancellationToken);
                    var message = consumeResult.Message.Value;

                    // TODO: Trate a mensagem aqui, por exemplo:
                    Console.WriteLine($"Mensagem recebida: {message}");

                    _consumer.Commit(consumeResult);
                }
            }
            catch (OperationCanceledException)
            {
                _consumer.Close();
            }
        }
    }
}
