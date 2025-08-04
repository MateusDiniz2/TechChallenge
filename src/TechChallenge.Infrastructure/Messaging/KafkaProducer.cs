using Confluent.Kafka;
using System.Text.Json;
using static TechChallenge.Application.Interfaces.IKafkaClient;

namespace TechChallenge.Infrastructure.Messaging
{
    public class KafkaProducer : IKafkaProducer
    {
        private readonly IProducer<Null, string> _producer;
        private readonly string _topic;

        public KafkaProducer(string broker, string topic)
        {
            var config = new ProducerConfig { BootstrapServers = broker };
            _producer = new ProducerBuilder<Null, string>(config).Build();
            _topic = topic;
        }

        public async Task SendMessageAsync<T>(T message)
        {
            var json = JsonSerializer.Serialize(message);
            await _producer.ProduceAsync(_topic, new Message<Null, string> { Value = json });
        }
    }
}
