using RabbitMQ.Client;
using System.Text;

namespace WebApi_csharp.Services
{
    public class RabbitService
    {
        private const string Queue = "orders_created";

        public void PublishOrderCreated(string orderId)
        {
            var factory = new ConnectionFactory() { HostName = "rabbitmq" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: Queue, durable: true, exclusive: false, autoDelete: false);

            var body = Encoding.UTF8.GetBytes($"Pedido creado: {orderId}");
            channel.BasicPublish(exchange: "", routingKey: Queue, body: body);
        }
    }
}
