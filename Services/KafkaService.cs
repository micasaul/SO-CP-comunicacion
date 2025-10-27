using Confluent.Kafka;

namespace WebApi_csharp.Services
{
    public class KafkaService
    {
        private readonly string _topic = "orders_topic";
        private readonly ProducerConfig _config = new() { BootstrapServers = "kafka:9092" };

        public async Task PublishStatusAsync(string orderId, string status)
        {
            using var producer = new ProducerBuilder<Null, string>(_config).Build();
            var message = $"Pedido {orderId} - Estado: {status}";
            await producer.ProduceAsync(_topic, new Message<Null, string> { Value = message });
            Console.WriteLine($"[KafkaService] Publicado en Kafka: {message}");
        }
    }
}
