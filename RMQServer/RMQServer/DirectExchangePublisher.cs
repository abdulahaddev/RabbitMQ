using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace RMQServer
{
    public static class DirectExchangePublisher
    {
        public static void Publish(IModel channel)
        {
            var args = new Dictionary<string, object>()
            {
                { "secret-key", 1258963 },
                { "secret-path", 4 },
                { "secret-header", 9}
            };

            channel.ExchangeDeclare("hello-direct-exchange", ExchangeType.Direct, arguments : args);          

            int counter = 0;
            while (counter <= 400)
            {
                var message = new { Name = "Ahad", Count = counter };

                var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

                channel.BasicPublish("hello-direct-exchange",
                                    routingKey: "key-direct-route",
                                    basicProperties: null,
                                    body: body);

                Console.WriteLine($" [x] Sent ");
                counter++;
            }
        }
    }
}
