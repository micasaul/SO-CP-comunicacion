using Confluent.Kafka;

namespace WebApi_csharp.Services
{
    public class AnalyticsService
    {
        public void Start()
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = "kafka:9092",
                GroupId = "analytics-group",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            consumer.Subscribe("orders_topic");

            Console.WriteLine("[AnalyticsService] Escuchando eventos de pedidos...");

            while (true)
            {
                try
                {
                    var cr = consumer.Consume();
                    Console.WriteLine($"[AnalyticsService] Registro analítico: {cr.Message.Value}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[AnalyticsService] Error: {ex.Message}");
                    Thread.Sleep(2000);
                }
            }
        }
    }
}
