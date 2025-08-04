using Confluent.Kafka;
using System.Text.Json;
using TechChallenge.Application.Interfaces;
using TechChallenge.Domain.Entities;

namespace TechChallenge.Worker.Services
{
    public class KafkaConsumerService : BackgroundService
    {
        private readonly ILogger<KafkaConsumerService> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IConsumer<Ignore, string> _consumer;

        public KafkaConsumerService(ILogger<KafkaConsumerService> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;

            var config = new ConsumerConfig
            {
                GroupId = "techchallenge-group",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            _consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            _consumer.Subscribe("products-topic");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var consumeResult = _consumer.Consume(stoppingToken);
                    var message = consumeResult.Message.Value;

                    _logger.LogInformation("Mensagem consumida do Kafka: {Message}", message);

                    using var scope = _scopeFactory.CreateScope();
                    var productService = scope.ServiceProvider.GetRequiredService<IProductService>();

                    var product = JsonSerializer.Deserialize<Product>(message, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    if (product != null)
                    {
                        await productService.CreateAsync(product);
                    }
                }
                catch (ConsumeException ex)
                {
                    _logger.LogError(ex, "Erro ao consumir mensagem do Kafka.");
                }
                catch (OperationCanceledException)
                {
                    _logger.LogInformation("Cancelando consumo Kafka...");
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro inesperado no consumidor Kafka.");
                }
            }

            _consumer.Close(); // Fecha a conexão ao encerrar o serviço
        }
    }

}
