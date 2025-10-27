using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace WebApi_csharp.Services
{
    public class NotificationService
    {
        private const string Queue = "orders_created";

        public void Start()
        {
            while (true)
            {
                try
                {
                    var factory = new ConnectionFactory() { HostName = "rabbitmq" };
                    using var connection = factory.CreateConnection();
                    using var channel = connection.CreateModel();

                    channel.QueueDeclare(queue: Queue, durable: true, exclusive: false, autoDelete: false);

                    Console.WriteLine("[NotificationService] Conectado a RabbitMQ. Esperando mensajes...");

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (sender, e) =>
                    {
                        var message = Encoding.UTF8.GetString(e.Body.ToArray());
                        Console.WriteLine($"[NotificationService] Enviando correo: {message}");
                    };

                    channel.BasicConsume(queue: Queue, autoAck: true, consumer: consumer);

                    // Mantiene el hilo activo mientras RabbitMQ esté funcionando
                    while (connection.IsOpen)
                        Thread.Sleep(1000);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[NotificationService] Error de conexión: {ex.Message}. Reintentando en 5s...");
                    Thread.Sleep(5000);
                }
            }
        }
    }
}
