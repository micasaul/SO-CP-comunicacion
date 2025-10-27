using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Confluent.Kafka;
using System.Text;

namespace WebApi_csharp.Services
{
    public class NotificationService
    {
        private const string Queue = "orders_created";

        public void Start()
        {
            // Inicia dos tareas en paralelo: una escucha RabbitMQ y otra Kafka
            var rabbitTask = Task.Run(() => ListenRabbit());
            var kafkaTask = Task.Run(() => ListenKafka());

            Task.WaitAll(rabbitTask, kafkaTask);
        }

        private void ListenRabbit()
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
                        Console.WriteLine($"[NotificationService] (RabbitMQ) Enviando correo: {message}");
                    };

                    channel.BasicConsume(queue: Queue, autoAck: true, consumer: consumer);

                    while (connection.IsOpen)
                        Thread.Sleep(1000);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[NotificationService] Error RabbitMQ: {ex.Message}. Reintentando...");
                    Thread.Sleep(5000);
                }
            }
        }

        private void ListenKafka()
        {
            while (true)
            {
                try
                {
                    var config = new ConsumerConfig
                    {
                        BootstrapServers = "kafka:9092",
                        GroupId = "notification-group",
                        AutoOffsetReset = AutoOffsetReset.Earliest
                    };

                    using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
                    consumer.Subscribe("orders_topic");

                    Console.WriteLine("[NotificationService] Conectado a Kafka. Escuchando estados de pedidos...");

                    while (true)
                    {
                        try
                        {
                            var cr = consumer.Consume();
                            Console.WriteLine($"[NotificationService] (Kafka) Notificación enviada: {cr.Message.Value}");
                        }
                        catch (ConsumeException cex)
                        {
                            Console.WriteLine($"[NotificationService] Error de consumo Kafka: {cex.Error.Reason}");
                            Thread.Sleep(2000);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[NotificationService] Kafka no disponible ({ex.Message}). Reintentando en 5s...");
                    Thread.Sleep(5000);
                }
            }
        }
    }
}
